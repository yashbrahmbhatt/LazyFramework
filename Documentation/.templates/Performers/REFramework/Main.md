# Main
Class: Main

[Process title]
[Process description]
[Additional information (e.g., author, contact information and applications involved and required external setup)]

<hr />

## Workflow Details
<details>
    <summary>
    <b>Namespaces</b>
    </summary>

    - GlobalConstantsNamespace
- GlobalVariablesNamespace
- System
- System.Activities
- System.Activities.DynamicUpdate
- System.Activities.Statements
- System.Collections
- System.Collections.Generic
- System.Collections.ObjectModel
- System.Data
- System.Linq
- System.Linq.Expressions
- System.Reflection
- System.Runtime.InteropServices
- System.Runtime.Serialization
- System.Text
- System.Windows
- UiPath.Core
- UiPath.Core.Activities


</details>
<details>
    <summary>
    <b>References</b>
    </summary>

    - Microsoft.Bcl.AsyncInterfaces
- Microsoft.CSharp
- NPOI
- PresentationCore
- PresentationFramework
- System
- System.Activities
- System.Collections
- System.ComponentModel
- System.ComponentModel.Composition
- System.ComponentModel.Primitives
- System.ComponentModel.TypeConverter
- System.Configuration.ConfigurationManager
- System.Console
- System.Core
- System.Data
- System.Data.Common
- System.Data.SqlClient
- System.Linq
- System.Memory
- System.Memory.Data
- System.ObjectModel
- System.Private.CoreLib
- System.Private.ServiceModel
- System.Private.Uri
- System.Private.Xml
- System.Runtime.Serialization
- System.Security.Permissions
- System.ServiceModel
- System.ServiceModel.Activities
- System.ValueTuple
- System.Xaml
- System.Xml
- System.Xml.Linq
- UiPath.Excel
- UiPath.Studio.Constants
- UiPath.System.Activities
- UiPath.System.Activities.Design
- WindowsBase
- WindowsFormsIntegration


</details>
<details>
    <summary>
    <b>Arguments</b>
    </summary>

    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>in_OrchestratorQueueName</td><td>InArgument</td><td>x:String</td><td>Allows the Orchestrator queue name to be passed as an argument, instead of only being defined in the configuration file.</td></tr><tr><td>in_OrchestratorQueueFolder</td><td>InArgument</td><td>x:String</td><td>Allows the Orchestrator folder name where the queue is created to be passed as an argument, instead of only being defined in the configuration file.</td></tr></table>
    
</details>

<hr />

## Outline (Beta)

```mermaid
stateDiagram-v2


StateMachine_1: General Business Process
state StateMachine_1 {
direction TB

State_4: Initialization
state State_4 {
direction TB

TryCatch_1: Try initializing settings and applications
state TryCatch_1 {
direction TB

Sequence_1: Load configurations and open applications
state Sequence_1 {
direction TB
Assign_1 : Assign - Assign SystemException (initialization)
Assign_1 --> If_1
If_1: If first run, read local configuration file
state If_1 {
direction TB

Sequence_2: First run
state Sequence_2 {
direction TB
LogMessage_14 : LogMessage - Log Message screen resolution
InvokeWorkflowFile_1 : InvokeWorkflowFile - InitAllSettings.xaml - Invoke Workflow File
LogMessage_14 --> InvokeWorkflowFile_1
InvokeWorkflowFile_1 --> If_2
If_2: If in_OrchestratorQueueName is specified
state If_2 {
direction TB
Assign_2 : Assign - Assign OrchestratorQueueName
}
If_2 --> If_6
If_6: If in_OrchestratorQueueFolder is specified
state If_6 {
direction TB
Assign_9 : Assign - Assign OrchestratorQueueFolder
}
InvokeWorkflowFile_2 : InvokeWorkflowFile - KillAllProcesses.xaml - Invoke Workflow File
If_6 --> InvokeWorkflowFile_2
AddLogFields_1 : AddLogFields - Add Log Fields (BusinessProcessName)
InvokeWorkflowFile_2 --> AddLogFields_1
}
}
If_1 --> If_4
If_4: If maxConsecutiveSystemExceptions exceeded
state If_4 {
direction TB
Throw_1 : Throw - Throw Consecutive Exceptions exceeded
}
InvokeWorkflowFile_3 : InvokeWorkflowFile - InitAllApplications.xaml - Invoke Workflow File
If_4 --> InvokeWorkflowFile_3
}
Assign_3 : Assign - Assign SystemException
Sequence_1 --> Assign_3
}
TryCatch_1 --> Transition_6
Transition_6: Successful
state Transition_6 {
direction TB

State_3: Get Transaction Data
state State_3 {
direction TB

Sequence_5: Retrieve Data
state Sequence_5 {
direction TB
ShouldStop_1 : ShouldStop - Check Stop Signal
ShouldStop_1 --> If_3
If_3: Should Stop or Get Next
state If_3 {
direction TB

Sequence_3: Orchestrator stop requested
state Sequence_3 {
direction TB
LogMessage_1 : LogMessage - Log message (Stop process requested)
Assign_4 : Assign - End Process (Stop process requested)
LogMessage_1 --> Assign_4
}
Sequence_3 --> TryCatch_2
TryCatch_2: Try Catch Get transaction item
state TryCatch_2 {
direction TB
InvokeWorkflowFile_4 : InvokeWorkflowFile - GetTransactionData.xaml - Invoke Workflow File
InvokeWorkflowFile_4 --> Sequence_4
Sequence_4: Log exception message and end process
state Sequence_4 {
direction TB
LogMessage_2 : LogMessage - Log message  (Get transaction data error)
Assign_5 : Assign - End Process (Could not get new transaction)
LogMessage_2 --> Assign_5
}
}
}
}
Sequence_5 --> Transition_11
Transition_11: No Data
state Transition_11 {
direction TB

State_2: End Process
state State_2 {
direction TB

TryCatch_5: Try to close all aplications
state TryCatch_5 {
direction TB
InvokeWorkflowFile_7 : InvokeWorkflowFile - CloseAllApplications.xaml - Invoke Workflow File
InvokeWorkflowFile_7 --> Sequence_7
Sequence_7: Failed to close applications, so kill processes
state Sequence_7 {
direction TB
LogMessage_6 : LogMessage - Log message (Failed to close applications)
InvokeWorkflowFile_8 : InvokeWorkflowFile - Invoke KillAllProcesses workflow (End process)
LogMessage_6 --> InvokeWorkflowFile_8
}
Sequence_7 --> If_5
If_5: If SystemException IsNot Nothing
state If_5 {
direction TB
TerminateWorkflow_1 : TerminateWorkflow - Terminate Main Workflow
}
}
}
LogMessage_5 : LogMessage - Log message (No more transations available)
State_2 --> LogMessage_5
}
Transition_11 --> Transition_4
Transition_4: New Transaction
state Transition_4 {
direction TB

State_1: Process Transaction
state State_1 {
direction TB

TryCatch_4: Try to process transaction
state TryCatch_4 {
direction TB

Sequence_6: Process the current TransactionItem
state Sequence_6 {
direction TB
Assign_6 : Assign - Assign BusinessException
InvokeWorkflowFile_5 : InvokeWorkflowFile - Process.xaml - Invoke Workflow File
Assign_6 --> InvokeWorkflowFile_5
InvokeWorkflowFile_5 --> TryCatch_6
TryCatch_6: Try Catch Set Transaction Status (Success)
state TryCatch_6 {
direction TB
InvokeWorkflowFile_9 : InvokeWorkflowFile - SetTransactionStatus.xaml (Success)
LogMessage_8 : LogMessage - Log message (Failed to set transaction status Success)
InvokeWorkflowFile_9 --> LogMessage_8
}
}
Sequence_6 --> Sequence_8
Sequence_8: Handle Business Exception
state Sequence_8 {
direction TB
Assign_7 : Assign - Set transaction status to BusinessRuleException
Assign_7 --> TryCatch_7
TryCatch_7: Try Catch Set Transaction Status (BRE)
state TryCatch_7 {
direction TB
InvokeWorkflowFile_10 : InvokeWorkflowFile - SetTransactionStatus.xaml (BRE)
LogMessage_9 : LogMessage - Log message (Failed to set transaction status BRE)
InvokeWorkflowFile_10 --> LogMessage_9
}
}
Sequence_8 --> Sequence_9
Sequence_9: Handle System Exception
state Sequence_9 {
direction TB
Assign_8 : Assign - Set transaction status to SystemException
Assign_8 --> TryCatch_8
TryCatch_8: Try Catch SetTransactionStatus (SE)
state TryCatch_8 {
direction TB
InvokeWorkflowFile_11 : InvokeWorkflowFile - SetTransactionStatus.xaml - (SE)
LogMessage_10 : LogMessage - Log message (Failed to set transaction status SE)
InvokeWorkflowFile_11 --> LogMessage_10
}
}
}
Transition_8 : Transition - System Exception
TryCatch_4 --> Transition_8
Transition_10 : Transition - Success
Transition_8 --> Transition_10
Transition_9 : Transition - Business Exception
Transition_10 --> Transition_9
}
LogMessage_3 : LogMessage - Log message (New transaction retrieved)
State_1 --> LogMessage_3
}
}
Comment_1 : Comment - Comment (default transition)
State_3 --> Comment_1
}
Transition_6 --> Transition_12
Transition_12: System Exception (failed initialization)
state Transition_12 {
direction TB
LogMessage_7 : LogMessage - Log Message (initialization failure)
}
}
}
```