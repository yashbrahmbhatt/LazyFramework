using System;
using System.Activities;
using UiPath.CodedWorkflows;
using UiPath.CodedWorkflows.Utils;
using System.Runtime;

namespace LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation
{
    [System.ComponentModel.Browsable(false)]
    public class Main_Tests_StateActivity : System.Activities.Activity
    {
        public Main_Tests_StateActivity()
        {
            this.Implementation = () =>
            {
                return new Main_Tests_StateActivityChild()
                {};
            };
        }
    }

    internal class Main_Tests_StateActivityChild : UiPath.CodedWorkflows.AsyncTaskCodedWorkflowActivity
    {
        public System.Collections.Generic.IDictionary<string, object> newResult { get; set; }

        public Main_Tests_StateActivityChild()
        {
            DisplayName = "Main_Tests_State";
        }

        protected override async System.Threading.Tasks.Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, System.Threading.CancellationToken cancellationToken)
        {
            var codedWorkflow = new global::LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation.Main_Tests_State();
            CodedWorkflowHelper.Initialize(codedWorkflow, new UiPath.CodedWorkflows.Utils.CodedWorkflowsFeatureChecker(new System.Collections.Generic.List<string>()
            {UiPath.CodedWorkflows.Utils.CodedWorkflowsFeatures.AsyncEntrypoints}), context);
            await System.Threading.Tasks.Task.Run(() => CodedWorkflowHelper.RunWithExceptionHandlingAsync(() =>
            {
                if (codedWorkflow is IBeforeAfterRun codedWorkflowWithBeforeAfter)
                {
                    codedWorkflowWithBeforeAfter.Before(new BeforeRunContext()
                    {RelativeFilePath = "Frameworks\\Performers\\BasicPerformer\\Implementation\\Main.Tests.State.cs"});
                }

                return System.Threading.Tasks.Task.CompletedTask;
            }, () =>
            {
                CodedExecutionHelper.Run(() =>
                {
                    {
                        codedWorkflow.RunTests();
                        newResult = new System.Collections.Generic.Dictionary<string, object>{};
                    }
                }, cancellationToken);
                return System.Threading.Tasks.Task.FromResult(newResult);
            }, (exception, outArgs) =>
            {
                if (codedWorkflow is IBeforeAfterRun codedWorkflowWithBeforeAfter)
                {
                    codedWorkflowWithBeforeAfter.After(new AfterRunContext()
                    {RelativeFilePath = "Frameworks\\Performers\\BasicPerformer\\Implementation\\Main.Tests.State.cs", Exception = exception});
                }

                return System.Threading.Tasks.Task.CompletedTask;
            }), cancellationToken);
            return endContext =>
            {
            };
        }
    }
}