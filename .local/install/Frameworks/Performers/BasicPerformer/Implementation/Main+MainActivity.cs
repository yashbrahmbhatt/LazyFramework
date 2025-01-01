using System;
using System.Activities;
using UiPath.CodedWorkflows;
using UiPath.CodedWorkflows.Utils;
using System.Runtime;

namespace LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation
{
    [System.ComponentModel.Browsable(false)]
    public class MainActivity : System.Activities.Activity
    {
        public InArgument<System.String> configPath { get; set; }

        public InArgument<System.Collections.Generic.List<System.String>> ignored { get; set; }

        public MainActivity()
        {
            this.Implementation = () =>
            {
                return new MainActivityChild()
                {configPath = (this.configPath == null ? (InArgument<System.String>)Argument.CreateReference((Argument)new InArgument<System.String>(), "configPath") : (InArgument<System.String>)Argument.CreateReference((Argument)this.configPath, "configPath")), ignored = (this.ignored == null ? (InArgument<System.Collections.Generic.List<System.String>>)Argument.CreateReference((Argument)new InArgument<System.Collections.Generic.List<System.String>>(), "ignored") : (InArgument<System.Collections.Generic.List<System.String>>)Argument.CreateReference((Argument)this.ignored, "ignored")), };
            };
        }
    }

    internal class MainActivityChild : UiPath.CodedWorkflows.AsyncTaskCodedWorkflowActivity
    {
        public InArgument<System.String> configPath { get; set; }

        public InArgument<System.Collections.Generic.List<System.String>> ignored { get; set; }

        public System.Collections.Generic.IDictionary<string, object> newResult { get; set; }

        public MainActivityChild()
        {
            DisplayName = "Main";
        }

        protected override async System.Threading.Tasks.Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, System.Threading.CancellationToken cancellationToken)
        {
            var var_configPath = configPath.Get(context);
            var var_ignored = ignored.Get(context);
            var codedWorkflow = new global::LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.Main();
            CodedWorkflowHelper.Initialize(codedWorkflow, new UiPath.CodedWorkflows.Utils.CodedWorkflowsFeatureChecker(new System.Collections.Generic.List<string>()
            {UiPath.CodedWorkflows.Utils.CodedWorkflowsFeatures.AsyncEntrypoints}), context);
            await System.Threading.Tasks.Task.Run(() => CodedWorkflowHelper.RunWithExceptionHandlingAsync(() =>
            {
                if (codedWorkflow is IBeforeAfterRun codedWorkflowWithBeforeAfter)
                {
                    codedWorkflowWithBeforeAfter.Before(new BeforeRunContext()
                    {RelativeFilePath = "Frameworks\\Performers\\BasicPerformer\\Implementation\\Main.cs"});
                }

                return System.Threading.Tasks.Task.CompletedTask;
            }, () =>
            {
                CodedExecutionHelper.Run(() =>
                {
                    {
                        codedWorkflow.Execute(var_configPath, var_ignored);
                        newResult = new System.Collections.Generic.Dictionary<string, object>{};
                    }
                }, cancellationToken);
                return System.Threading.Tasks.Task.FromResult(newResult);
            }, (exception, outArgs) =>
            {
                if (codedWorkflow is IBeforeAfterRun codedWorkflowWithBeforeAfter)
                {
                    codedWorkflowWithBeforeAfter.After(new AfterRunContext()
                    {RelativeFilePath = "Frameworks\\Performers\\BasicPerformer\\Implementation\\Main.cs", Exception = exception});
                }

                return System.Threading.Tasks.Task.CompletedTask;
            }), cancellationToken);
            return endContext =>
            {
            };
        }
    }
}