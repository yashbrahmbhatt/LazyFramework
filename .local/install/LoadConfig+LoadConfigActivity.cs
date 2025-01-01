using System;
using System.Activities;
using UiPath.CodedWorkflows;
using UiPath.CodedWorkflows.Utils;
using System.Runtime;

namespace LazyFramework.DX.Shared
{
    [System.ComponentModel.Browsable(false)]
    public class LoadConfigActivity : System.Activities.Activity
    {
        public InArgument<System.String> file { get; set; }

        public InArgument<System.Collections.Generic.List<System.String>> ignored { get; set; }

        public OutArgument<System.Collections.Generic.Dictionary<System.String, System.Object>> Output { get; set; }

        public LoadConfigActivity()
        {
            this.Implementation = () =>
            {
                return new LoadConfigActivityChild()
                {file = (this.file == null ? (InArgument<System.String>)Argument.CreateReference((Argument)new InArgument<System.String>(), "file") : (InArgument<System.String>)Argument.CreateReference((Argument)this.file, "file")), ignored = (this.ignored == null ? (InArgument<System.Collections.Generic.List<System.String>>)Argument.CreateReference((Argument)new InArgument<System.Collections.Generic.List<System.String>>(), "ignored") : (InArgument<System.Collections.Generic.List<System.String>>)Argument.CreateReference((Argument)this.ignored, "ignored")), Output = (this.Output == null ? (OutArgument<System.Collections.Generic.Dictionary<System.String, System.Object>>)Argument.CreateReference((Argument)new OutArgument<System.Collections.Generic.Dictionary<System.String, System.Object>>(), "Output") : (OutArgument<System.Collections.Generic.Dictionary<System.String, System.Object>>)Argument.CreateReference((Argument)this.Output, "Output")), };
            };
        }
    }

    internal class LoadConfigActivityChild : UiPath.CodedWorkflows.AsyncTaskCodedWorkflowActivity
    {
        public InArgument<System.String> file { get; set; }

        public InArgument<System.Collections.Generic.List<System.String>> ignored { get; set; }

        public OutArgument<System.Collections.Generic.Dictionary<System.String, System.Object>> Output { get; set; }

        public System.Collections.Generic.IDictionary<string, object> newResult { get; set; }

        public LoadConfigActivityChild()
        {
            DisplayName = "LoadConfig";
        }

        protected override async System.Threading.Tasks.Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, System.Threading.CancellationToken cancellationToken)
        {
            var var_file = file.Get(context);
            var var_ignored = ignored.Get(context);
            var codedWorkflow = new global::LazyFramework.DX.Shared.LoadConfig();
            CodedWorkflowHelper.Initialize(codedWorkflow, new UiPath.CodedWorkflows.Utils.CodedWorkflowsFeatureChecker(new System.Collections.Generic.List<string>()
            {UiPath.CodedWorkflows.Utils.CodedWorkflowsFeatures.AsyncEntrypoints}), context);
            var result = await System.Threading.Tasks.Task.Run(() => CodedWorkflowHelper.RunWithExceptionHandlingAsync(() =>
            {
                if (codedWorkflow is IBeforeAfterRun codedWorkflowWithBeforeAfter)
                {
                    codedWorkflowWithBeforeAfter.Before(new BeforeRunContext()
                    {RelativeFilePath = "LoadConfig.cs"});
                }

                return System.Threading.Tasks.Task.CompletedTask;
            }, () =>
            {
                CodedExecutionHelper.Run(() =>
                {
                    {
                        var result = codedWorkflow.Execute(var_file, var_ignored);
                        newResult = new System.Collections.Generic.Dictionary<string, object>{{"Output", result}};
                    }
                }, cancellationToken);
                return System.Threading.Tasks.Task.FromResult(newResult);
            }, (exception, outArgs) =>
            {
                if (codedWorkflow is IBeforeAfterRun codedWorkflowWithBeforeAfter)
                {
                    codedWorkflowWithBeforeAfter.After(new AfterRunContext()
                    {RelativeFilePath = "LoadConfig.cs", Exception = exception});
                }

                return System.Threading.Tasks.Task.CompletedTask;
            }), cancellationToken);
            return endContext =>
            {
                Output.Set(endContext, (System.Collections.Generic.Dictionary<System.String, System.Object>)result["Output"]);
            };
        }
    }
}