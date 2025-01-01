using System;
using System.Activities;
using UiPath.CodedWorkflows;
using UiPath.CodedWorkflows.Utils;
using System.Runtime;

namespace LazyFramework.DX.Shared
{
    [System.ComponentModel.Browsable(false)]
    public class ReadExcelFileActivity : System.Activities.Activity
    {
        public InArgument<System.String> file { get; set; }

        public OutArgument<System.Data.DataSet> Output { get; set; }

        public ReadExcelFileActivity()
        {
            this.Implementation = () =>
            {
                return new ReadExcelFileActivityChild()
                {file = (this.file == null ? (InArgument<System.String>)Argument.CreateReference((Argument)new InArgument<System.String>(), "file") : (InArgument<System.String>)Argument.CreateReference((Argument)this.file, "file")), Output = (this.Output == null ? (OutArgument<System.Data.DataSet>)Argument.CreateReference((Argument)new OutArgument<System.Data.DataSet>(), "Output") : (OutArgument<System.Data.DataSet>)Argument.CreateReference((Argument)this.Output, "Output")), };
            };
        }
    }

    internal class ReadExcelFileActivityChild : UiPath.CodedWorkflows.AsyncTaskCodedWorkflowActivity
    {
        public InArgument<System.String> file { get; set; }

        public OutArgument<System.Data.DataSet> Output { get; set; }

        public System.Collections.Generic.IDictionary<string, object> newResult { get; set; }

        public ReadExcelFileActivityChild()
        {
            DisplayName = "ReadExcelFile";
        }

        protected override async System.Threading.Tasks.Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, System.Threading.CancellationToken cancellationToken)
        {
            var var_file = file.Get(context);
            var codedWorkflow = new global::LazyFramework.DX.Shared.ReadExcelFile();
            CodedWorkflowHelper.Initialize(codedWorkflow, new UiPath.CodedWorkflows.Utils.CodedWorkflowsFeatureChecker(new System.Collections.Generic.List<string>()
            {UiPath.CodedWorkflows.Utils.CodedWorkflowsFeatures.AsyncEntrypoints}), context);
            var result = await System.Threading.Tasks.Task.Run(() => CodedWorkflowHelper.RunWithExceptionHandlingAsync(() =>
            {
                if (codedWorkflow is IBeforeAfterRun codedWorkflowWithBeforeAfter)
                {
                    codedWorkflowWithBeforeAfter.Before(new BeforeRunContext()
                    {RelativeFilePath = "ReadExcelFile.cs"});
                }

                return System.Threading.Tasks.Task.CompletedTask;
            }, () =>
            {
                CodedExecutionHelper.Run(() =>
                {
                    {
                        var result = codedWorkflow.Execute(var_file);
                        newResult = new System.Collections.Generic.Dictionary<string, object>{{"Output", result}};
                    }
                }, cancellationToken);
                return System.Threading.Tasks.Task.FromResult(newResult);
            }, (exception, outArgs) =>
            {
                if (codedWorkflow is IBeforeAfterRun codedWorkflowWithBeforeAfter)
                {
                    codedWorkflowWithBeforeAfter.After(new AfterRunContext()
                    {RelativeFilePath = "ReadExcelFile.cs", Exception = exception});
                }

                return System.Threading.Tasks.Task.CompletedTask;
            }), cancellationToken);
            return endContext =>
            {
                Output.Set(endContext, (System.Data.DataSet)result["Output"]);
            };
        }
    }
}