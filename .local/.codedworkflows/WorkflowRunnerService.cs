using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UiPath.CodedWorkflows;
using UiPath.CodedWorkflows.Interfaces;
using UiPath.Activities.Contracts;
using LazyFramework.DX.Shared;

[assembly: WorkflowRunnerServiceAttribute(typeof(LazyFramework.DX.Shared.WorkflowRunnerService))]
namespace LazyFramework.DX.Shared
{
    public class WorkflowRunnerService
    {
        private readonly ICodedWorkflowServices _services;
        public WorkflowRunnerService(ICodedWorkflowServices services)
        {
            _services = services;
        }

        /// <summary>
        /// Invokes the Frameworks/Performers/BasicPerformer/Implementation/Main.Tests.State.cs
        /// </summary>
        public void Main_Tests_State()
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"Frameworks\Performers\BasicPerformer\Implementation\Main.Tests.State.cs", new Dictionary<string, object>{}, default, default, default, GetAssemblyName());
        }

        /// <summary>
        /// Invokes the Frameworks/Performers/BasicPerformer/Implementation/Main.Tests.State.cs
        /// </summary>
		/// <param name="isolated">Indicates whether to isolate executions (run them within a different process)</param>
        public void Main_Tests_State(System.Boolean isolated)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"Frameworks\Performers\BasicPerformer\Implementation\Main.Tests.State.cs", new Dictionary<string, object>{}, default, isolated, default, GetAssemblyName());
        }

        /// <summary>
        /// Invokes the LoadConfig.cs
        /// </summary>
        public System.Collections.Generic.Dictionary<System.String, System.Object> LoadConfig(System.String file, System.Collections.Generic.List<System.String> ignored)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"LoadConfig.cs", new Dictionary<string, object>{{"file", file}, {"ignored", ignored}}, default, default, default, GetAssemblyName());
            return (System.Collections.Generic.Dictionary<System.String, System.Object>)result["Output"];
        }

        /// <summary>
        /// Invokes the LoadConfig.cs
        /// </summary>
		/// <param name="isolated">Indicates whether to isolate executions (run them within a different process)</param>
        public System.Collections.Generic.Dictionary<System.String, System.Object> LoadConfig(System.String file, System.Collections.Generic.List<System.String> ignored, System.Boolean isolated)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"LoadConfig.cs", new Dictionary<string, object>{{"file", file}, {"ignored", ignored}}, default, isolated, default, GetAssemblyName());
            return (System.Collections.Generic.Dictionary<System.String, System.Object>)result["Output"];
        }

        /// <summary>
        /// Invokes the Frameworks/Performers/BasicPerformer/Implementation/Main.cs
        /// </summary>
        public void Main(System.String configPath, System.Collections.Generic.List<System.String> ignored)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"Frameworks\Performers\BasicPerformer\Implementation\Main.cs", new Dictionary<string, object>{{"configPath", configPath}, {"ignored", ignored}}, default, default, default, GetAssemblyName());
        }

        /// <summary>
        /// Invokes the Frameworks/Performers/BasicPerformer/Implementation/Main.cs
        /// </summary>
		/// <param name="isolated">Indicates whether to isolate executions (run them within a different process)</param>
        public void Main(System.String configPath, System.Collections.Generic.List<System.String> ignored, System.Boolean isolated)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"Frameworks\Performers\BasicPerformer\Implementation\Main.cs", new Dictionary<string, object>{{"configPath", configPath}, {"ignored", ignored}}, default, isolated, default, GetAssemblyName());
        }

        /// <summary>
        /// Invokes the Frameworks/Performers/BasicPerformer/Implementation/Framework/GetTransactionData.cs
        /// </summary>
        public LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData GetTransactionData(LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData state)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"Frameworks\Performers\BasicPerformer\Implementation\Framework\GetTransactionData.cs", new Dictionary<string, object>{{"state", state}}, default, default, default, GetAssemblyName());
            return (LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData)result["Output"];
        }

        /// <summary>
        /// Invokes the Frameworks/Performers/BasicPerformer/Implementation/Framework/GetTransactionData.cs
        /// </summary>
		/// <param name="isolated">Indicates whether to isolate executions (run them within a different process)</param>
        public LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData GetTransactionData(LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData state, System.Boolean isolated)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"Frameworks\Performers\BasicPerformer\Implementation\Framework\GetTransactionData.cs", new Dictionary<string, object>{{"state", state}}, default, isolated, default, GetAssemblyName());
            return (LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData)result["Output"];
        }

        /// <summary>
        /// Invokes the Frameworks/Performers/BasicPerformer/Implementation/MainWithoutWorkflows.cs
        /// </summary>
        public void MainWithoutWorkflows(System.String configPath, System.Collections.Generic.List<System.String> ignored)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"Frameworks\Performers\BasicPerformer\Implementation\MainWithoutWorkflows.cs", new Dictionary<string, object>{{"configPath", configPath}, {"ignored", ignored}}, default, default, default, GetAssemblyName());
        }

        /// <summary>
        /// Invokes the Frameworks/Performers/BasicPerformer/Implementation/MainWithoutWorkflows.cs
        /// </summary>
		/// <param name="isolated">Indicates whether to isolate executions (run them within a different process)</param>
        public void MainWithoutWorkflows(System.String configPath, System.Collections.Generic.List<System.String> ignored, System.Boolean isolated)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"Frameworks\Performers\BasicPerformer\Implementation\MainWithoutWorkflows.cs", new Dictionary<string, object>{{"configPath", configPath}, {"ignored", ignored}}, default, isolated, default, GetAssemblyName());
        }

        /// <summary>
        /// Invokes the Frameworks/Performers/BasicPerformer/Implementation/Framework/InitializeApplications.xaml
        /// </summary>
        public LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData InitializeApplications(LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData State)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"Frameworks\Performers\BasicPerformer\Implementation\Framework\InitializeApplications.xaml", new Dictionary<string, object>{{"State", State}}, default, default, default, GetAssemblyName());
            return (LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData)result["State"];
        }

        /// <summary>
        /// Invokes the Frameworks/Performers/BasicPerformer/Implementation/Framework/InitializeApplications.xaml
        /// </summary>
		/// <param name="isolated">Indicates whether to isolate executions (run them within a different process)</param>
        public LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData InitializeApplications(LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData State, System.Boolean isolated)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"Frameworks\Performers\BasicPerformer\Implementation\Framework\InitializeApplications.xaml", new Dictionary<string, object>{{"State", State}}, default, isolated, default, GetAssemblyName());
            return (LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData)result["State"];
        }

        /// <summary>
        /// Invokes the Frameworks/Performers/BasicPerformer/Implementation/Framework/Process.cs
        /// </summary>
        public LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData Process(LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData state)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"Frameworks\Performers\BasicPerformer\Implementation\Framework\Process.cs", new Dictionary<string, object>{{"state", state}}, default, default, default, GetAssemblyName());
            return (LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData)result["Output"];
        }

        /// <summary>
        /// Invokes the Frameworks/Performers/BasicPerformer/Implementation/Framework/Process.cs
        /// </summary>
		/// <param name="isolated">Indicates whether to isolate executions (run them within a different process)</param>
        public LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData Process(LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData state, System.Boolean isolated)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"Frameworks\Performers\BasicPerformer\Implementation\Framework\Process.cs", new Dictionary<string, object>{{"state", state}}, default, isolated, default, GetAssemblyName());
            return (LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData)result["Output"];
        }

        /// <summary>
        /// Invokes the Frameworks/Performers/BasicPerformer/Implementation/Framework/CloseApplications.cs
        /// </summary>
        public LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData CloseApplications(LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData state)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"Frameworks\Performers\BasicPerformer\Implementation\Framework\CloseApplications.cs", new Dictionary<string, object>{{"state", state}}, default, default, default, GetAssemblyName());
            return (LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData)result["Output"];
        }

        /// <summary>
        /// Invokes the Frameworks/Performers/BasicPerformer/Implementation/Framework/CloseApplications.cs
        /// </summary>
		/// <param name="isolated">Indicates whether to isolate executions (run them within a different process)</param>
        public LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData CloseApplications(LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData state, System.Boolean isolated)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"Frameworks\Performers\BasicPerformer\Implementation\Framework\CloseApplications.cs", new Dictionary<string, object>{{"state", state}}, default, isolated, default, GetAssemblyName());
            return (LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData)result["Output"];
        }

        /// <summary>
        /// Invokes the ReadExcelFile.cs
        /// </summary>
        public System.Data.DataSet ReadExcelFile(System.String file)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"ReadExcelFile.cs", new Dictionary<string, object>{{"file", file}}, default, default, default, GetAssemblyName());
            return (System.Data.DataSet)result["Output"];
        }

        /// <summary>
        /// Invokes the ReadExcelFile.cs
        /// </summary>
		/// <param name="isolated">Indicates whether to isolate executions (run them within a different process)</param>
        public System.Data.DataSet ReadExcelFile(System.String file, System.Boolean isolated)
        {
            var result = _services.WorkflowInvocationService.RunWorkflow(@"ReadExcelFile.cs", new Dictionary<string, object>{{"file", file}}, default, isolated, default, GetAssemblyName());
            return (System.Data.DataSet)result["Output"];
        }

        private string GetAssemblyName()
        {
            var assemblyProvider = _services.Container.Resolve<ILibraryAssemblyProvider>();
            return assemblyProvider.GetLibraryAssemblyName(GetType().Assembly);
        }
    }
}