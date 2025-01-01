using System;
using System.Activities;
using UiPath.CodedWorkflows;
using UiPath.CodedWorkflows.Utils;
using System.Runtime;

namespace LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.Framework
{
    [System.ComponentModel.Browsable(false)]
    public class GetTransactionDataActivity : System.Activities.Activity
    {
        public InArgument<LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData> state { get; set; }

        public OutArgument<LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData> Output { get; set; }

        public GetTransactionDataActivity()
        {
            this.Implementation = () =>
            {
                return new GetTransactionDataActivityChild()
                {state = (this.state == null ? (InArgument<LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData>)Argument.CreateReference((Argument)new InArgument<LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData>(), "state") : (InArgument<LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData>)Argument.CreateReference((Argument)this.state, "state")), Output = (this.Output == null ? (OutArgument<LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData>)Argument.CreateReference((Argument)new OutArgument<LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData>(), "Output") : (OutArgument<LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData>)Argument.CreateReference((Argument)this.Output, "Output")), };
            };
        }
    }

    internal class GetTransactionDataActivityChild : UiPath.CodedWorkflows.AsyncTaskCodedWorkflowActivity
    {
        public InArgument<LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData> state { get; set; }

        public OutArgument<LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData> Output { get; set; }

        public System.Collections.Generic.IDictionary<string, object> newResult { get; set; }

        public GetTransactionDataActivityChild()
        {
            DisplayName = "GetTransactionData";
        }

        protected override async System.Threading.Tasks.Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, System.Threading.CancellationToken cancellationToken)
        {
            var var_state = state.Get(context);
            var codedWorkflow = new global::LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.Framework.GetTransactionData();
            CodedWorkflowHelper.Initialize(codedWorkflow, new UiPath.CodedWorkflows.Utils.CodedWorkflowsFeatureChecker(new System.Collections.Generic.List<string>()
            {UiPath.CodedWorkflows.Utils.CodedWorkflowsFeatures.AsyncEntrypoints}), context);
            var result = await System.Threading.Tasks.Task.Run(() => CodedWorkflowHelper.RunWithExceptionHandlingAsync(() =>
            {
                if (codedWorkflow is IBeforeAfterRun codedWorkflowWithBeforeAfter)
                {
                    codedWorkflowWithBeforeAfter.Before(new BeforeRunContext()
                    {RelativeFilePath = "Frameworks\\Performers\\BasicPerformer\\Implementation\\Framework\\GetTransactionData.cs"});
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
                    {RelativeFilePath = "Frameworks\\Performers\\BasicPerformer\\Implementation\\Framework\\GetTransactionData.cs", Exception = exception});
                }

                return System.Threading.Tasks.Task.CompletedTask;
            }), cancellationToken);
            return endContext =>
            {
                Output.Set(endContext, (LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.StateData)result["Output"]);
            };
        }
    }
}