using System;
using System.Activities;
using UiPath.CodedWorkflows;
using UiPath.CodedWorkflows.Utils;
using System.Runtime;

namespace LazyFramework.DX.Shared.BasicPerformer.Implementation.Framework
{
    [System.ComponentModel.Browsable(false)]
    public class CloseApplicationsActivity : System.Activities.Activity
    {
        public InArgument<LazyFramework.DX.Shared.BasicPerformer.Implementation.StateData> state { get; set; }

        public OutArgument<LazyFramework.DX.Shared.BasicPerformer.Implementation.StateData> Output { get; set; }

        public CloseApplicationsActivity()
        {
            this.Implementation = () =>
            {
                return new CloseApplicationsActivityChild()
                {state = (this.state == null ? (InArgument<LazyFramework.DX.Shared.BasicPerformer.Implementation.StateData>)Argument.CreateReference((Argument)new InArgument<LazyFramework.DX.Shared.BasicPerformer.Implementation.StateData>(), "state") : (InArgument<LazyFramework.DX.Shared.BasicPerformer.Implementation.StateData>)Argument.CreateReference((Argument)this.state, "state")), Output = (this.Output == null ? (OutArgument<LazyFramework.DX.Shared.BasicPerformer.Implementation.StateData>)Argument.CreateReference((Argument)new OutArgument<LazyFramework.DX.Shared.BasicPerformer.Implementation.StateData>(), "Output") : (OutArgument<LazyFramework.DX.Shared.BasicPerformer.Implementation.StateData>)Argument.CreateReference((Argument)this.Output, "Output")), };
            };
        }
    }

    internal class CloseApplicationsActivityChild : UiPath.CodedWorkflows.AsyncTaskCodedWorkflowActivity
    {
        public InArgument<LazyFramework.DX.Shared.BasicPerformer.Implementation.StateData> state { get; set; }

        public OutArgument<LazyFramework.DX.Shared.BasicPerformer.Implementation.StateData> Output { get; set; }

        public System.Collections.Generic.IDictionary<string, object> newResult { get; set; }

        public CloseApplicationsActivityChild()
        {
            DisplayName = "CloseApplications";
        }

        protected override async System.Threading.Tasks.Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, System.Threading.CancellationToken cancellationToken)
        {
            var var_state = state.Get(context);
            var codedWorkflow = new global::LazyFramework.DX.Shared.BasicPerformer.Implementation.Framework.CloseApplications();
            CodedWorkflowHelper.Initialize(codedWorkflow, new UiPath.CodedWorkflows.Utils.CodedWorkflowsFeatureChecker(new System.Collections.Generic.List<string>()
            {UiPath.CodedWorkflows.Utils.CodedWorkflowsFeatures.AsyncEntrypoints}), context);
            var result = await System.Threading.Tasks.Task.Run(() => CodedWorkflowHelper.RunWithExceptionHandlingAsync(() =>
            {
                if (codedWorkflow is IBeforeAfterRun codedWorkflowWithBeforeAfter)
                {
                    codedWorkflowWithBeforeAfter.Before(new BeforeRunContext()
                    {RelativeFilePath = "BasicPerformer\\Implementation\\Framework\\CloseApplications.cs"});
                }

                return System.Threading.Tasks.Task.CompletedTask;
            }, () =>
            {
                CodedExecutionHelper.Run(() =>
                {
                    {
                        var result = codedWorkflow.Execute(var_state);
                        newResult = new System.Collections.Generic.Dictionary<string, object>{{"Output", result}};
                    }
                }, cancellationToken);
                return System.Threading.Tasks.Task.FromResult(newResult);
            }, (exception, outArgs) =>
            {
                if (codedWorkflow is IBeforeAfterRun codedWorkflowWithBeforeAfter)
                {
                    codedWorkflowWithBeforeAfter.After(new AfterRunContext()
                    {RelativeFilePath = "BasicPerformer\\Implementation\\Framework\\CloseApplications.cs", Exception = exception});
                }

                return System.Threading.Tasks.Task.CompletedTask;
            }), cancellationToken);
            return endContext =>
            {
                Output.Set(endContext, (LazyFramework.DX.Shared.BasicPerformer.Implementation.StateData)result["Output"]);
            };
        }
    }
}