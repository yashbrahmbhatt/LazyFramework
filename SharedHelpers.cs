using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Process = System.Diagnostics.Process;
#nullable enable
namespace LazyFramework.DX.Shared
{
    public static class SharedHelpers
    {
        public static bool CurrentlyBetweenTimes(TimeSpan? start, TimeSpan? end)
        {
            if(start == null) throw new ArgumentNullException(nameof(start));
            if(end == null) throw new ArgumentNullException(nameof(end));
            var current = DateTime.Now.TimeOfDay;
            return start <= end ? current >= start && current <= end : current >= start || current <= end;
        }

        public static string TakeScreenshot(string? inputPath)
        {

            string folderPath, filePath;
            if(inputPath == null) throw new ArgumentNullException(nameof(inputPath));
            if (Directory.Exists(inputPath))
            {
                // Input is a folder path
                folderPath = inputPath;
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = $"{Environment.MachineName}_{Environment.UserDomainName}_{Environment.UserName}_{timestamp}.png";
                if(folderPath == null) throw new Exception("WTF");
                filePath = Path.Combine(folderPath, fileName);
            }
            else
            {
                // Assume input is a file path
                filePath = inputPath;
                folderPath = Path.GetDirectoryName(filePath) ?? throw new Exception("WTF");
            }

            // Ensure the folder exists
            if (folderPath != null && !Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);


            // Capture the screenshot
            if(Screen.PrimaryScreen == null) throw new Exception("Cannot take screenshots without a primary screen");
            var bounds = Screen.PrimaryScreen.Bounds;
            
            using (var screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb))
            using (var graphics = Graphics.FromImage(screenshot))
            {
                graphics.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
                screenshot.Save(filePath, ImageFormat.Png);
            }

            return filePath;
        }

        public static T? Retry<T>(Delegate function, int count = 3, TimeSpan? durationBetweenRetries = null, params object[] args)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));

            // Set default values if not provided
            durationBetweenRetries ??= TimeSpan.FromSeconds(15); // Default 15 seconds

            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count), "Retry count must be at least 1.");
            if (durationBetweenRetries < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(durationBetweenRetries), "Duration must be non-negative.");

            Exception? lastException = null;

            for (int attempt = 0; attempt < count; attempt++)
            {
                try
                {
                    // Handle Action (void method)
                    if (function is Action action)
                    {
                        action.DynamicInvoke(args);
                        return default; // Return default for void methods
                    }

                    // Handle Func<T> (non-void method)
                    if (function is Delegate func)
                    {
                        object result = func.DynamicInvoke(args) ?? new object();

                        // Return the result cast to T
                        if (result is T typedResult)
                        {
                            return typedResult;
                        }

                        // If it's void or some other unexpected type, return default
                        return default;
                    }
                }
                catch (Exception ex)
                {
                    lastException = ex;

                    // If this is the last attempt, rethrow the exception
                    if (attempt == count - 1)
                        throw;

                    // Wait before retrying
                    Thread.Sleep(durationBetweenRetries.Value);
                }
            }

            // This point should not be reached because the exception is rethrown on the last attempt
            throw lastException ?? new Exception("WTF");
        }

        public static void KillProcesses(List<string> processNames)
        {
            IEnumerable<Process[]> processes = processNames.Select(p => Process.GetProcessesByName(p));
            foreach (var processType in processes)
            {
                if(processType == null) continue;
                foreach (var processInstance in processType)
                {
                    processInstance.Kill();
                }
            }
        }
    }
}