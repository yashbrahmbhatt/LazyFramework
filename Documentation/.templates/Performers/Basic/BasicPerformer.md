# BasicPerformer
Class: Performer

[Process Name]
[Author]

[Description of work done for each transaction]

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
- System.Activities.Runtime.Collections
- System.Activities.Statements
- System.Collections
- System.Collections.Generic
- System.Collections.ObjectModel
- System.ComponentModel
- System.Linq
- System.Reflection
- System.Runtime.Serialization
- System.Windows
- UiPath.Core
- UiPath.Core.Activities


</details>
<details>
    <summary>
    <b>References</b>
    </summary>

    - Microsoft.CSharp
- Microsoft.VisualBasic
- Microsoft.Win32.Primitives
- Newtonsoft.Json
- NPOI
- PresentationCore
- PresentationFramework
- System
- System.Activities
- System.Collections
- System.ComponentModel
- System.ComponentModel.EventBasedAsync
- System.ComponentModel.Primitives
- System.ComponentModel.TypeConverter
- System.Configuration.ConfigurationManager
- System.Console
- System.Core
- System.Data
- System.Data.Common
- System.Linq
- System.Linq.Expressions
- System.Memory
- System.Memory.Data
- System.ObjectModel
- System.Private.CoreLib
- System.Private.DataContractSerialization
- System.Private.ServiceModel
- System.Private.Uri
- System.Reflection.DispatchProxy
- System.Reflection.Metadata
- System.Reflection.TypeExtensions
- System.Runtime.Serialization
- System.Runtime.Serialization.Formatters
- System.Runtime.Serialization.Primitives
- System.Security.Permissions
- System.ServiceModel
- System.ServiceModel.Activities
- System.Xaml
- System.Xml
- System.Xml.Linq
- UiPath.Studio.Constants
- UiPath.System.Activities
- UiPath.System.Activities.Design
- UiPath.System.Activities.ViewModels
- UiPath.Workflow
- WindowsBase


</details>
<details>
    <summary>
    <b>Arguments</b>
    </summary>

    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>in_ConfigPath</td><td>InArgument</td><td>x:String</td><td>The path to the config file to use to load variables and resources.</td></tr><tr><td>in_IgnoreSheets</td><td>InArgument</td><td>s:String[]</td><td>A list of sheet names to ignore when loading the config file.</td></tr><tr><td>in_TestID</td><td>InArgument</td><td>x:String</td><td>Used to modify the workflow in order to test different scenarios. Only used to test exception handling in this workflow. Leave as null for production use.</td></tr></table>
    
</details>

<hr />

## Outline (Beta)

```mermaid
stateDiagram-v2

 --> StateMachine_3
StateMachine_3: Performer State Machine
state StateMachine_3 {
direction TB
 --> State_9
State_9: Initialization
state State_9 {
direction TB
 --> TryCatch_8
TryCatch_8: Try Initializing
state TryCatch_8 {
direction TB
 --> RetryScope_4
RetryScope_4: Retry - Initializing
state RetryScope_4 {
direction TB
 --> Sequence_21
Sequence_21: Initialize
state Sequence_21 {
direction TB
 --> If_5
If_5: First Run?
state If_5 {
direction TB
 --> Sequence_19
Sequence_19: Initialize Settings
state Sequence_19 {
direction TB
LogMessage_11 : LogMessage - LM -- Screen
InvokeWorkflowFile_19 : InvokeWorkflowFile - Utility\\LoadConfig.xaml - Invoke Workflow File
LogMessage_11 --> InvokeWorkflowFile_19
}
}
If_5 --> Switch`1_1
Switch`1_1: @Test Variations - Initialization
state Switch`1_1 {
direction TB
MultipleAssign_21 : MultipleAssign - Update Maintenance Times
Throw_1 : Throw - Throw InitializationError
MultipleAssign_21 --> Throw_1
}
LogMessage_12 : LogMessage - LM -- Initializing
Switch`1_1 --> LogMessage_12
MultipleAssign_12 : MultipleAssign - Reset System Exception
LogMessage_12 --> MultipleAssign_12
MultipleAssign_12 --> TryCatch_7
TryCatch_7: Try Close, Catch Kill (Initialization)
state TryCatch_7 {
direction TB
InvokeWorkflowFile_21 : InvokeWorkflowFile - Close Applications (Initialization)
InvokeWorkflowFile_22 : InvokeWorkflowFile - Kill Processes (Initialization)
InvokeWorkflowFile_21 --> InvokeWorkflowFile_22
}
InvokeWorkflowFile_23 : InvokeWorkflowFile - Init All Applications
TryCatch_7 --> InvokeWorkflowFile_23
}
}
MultipleAssign_13 : MultipleAssign - Set Framework Exception (Initialization)
RetryScope_4 --> MultipleAssign_13
}
TryCatch_8 --> Transition_18
Transition_18: Success
state Transition_18 {
direction TB
 --> State_8
State_8: Get Transaction Data
state State_8 {
direction TB
 --> TryCatch_9
TryCatch_9: Try Getting Transaction
state TryCatch_9 {
direction TB
 --> Sequence_17
Sequence_17: Get Transaction
state Sequence_17 {
direction TB
LogMessage_13 : LogMessage - LM -- Get Next Transaction
LogMessage_13 --> Switch`1_2
Switch`1_2: @Test Variations - Get Transaction Data
state Switch`1_2 {
direction TB
Throw_4 : Throw - Throw GetTransactionData Error
}
ShouldStop_2 : ShouldStop - Stop Requested?
Switch`1_2 --> ShouldStop_2
InvokeWorkflowFile_24 : InvokeWorkflowFile - Is Maintenance Time?
ShouldStop_2 --> InvokeWorkflowFile_24
InvokeWorkflowFile_24 --> IfElseIf_2
IfElseIf_2: Stop Requested/Maintenance Window/Success?
state IfElseIf_2 {
direction TB
LogMessage_14 : LogMessage - LM -- Stop
LogMessage_15 : LogMessage - LM -- Maintenance
LogMessage_14 --> LogMessage_15
LogMessage_15 --> RetryScope_5
RetryScope_5: Retry - Get Transaction
state RetryScope_5 {
direction TB
GetQueueItem_3 : GetQueueItem - Get Next Transaction
}
}
}
MultipleAssign_14 : MultipleAssign - Set Framework Exception (Get Transaction Data)
Sequence_17 --> MultipleAssign_14
}
TryCatch_9 --> Transition_11
Transition_11: Error - Get Transaction Data
state Transition_11 {
direction TB
 --> State_7
State_7: End
state State_7 {
direction TB
 --> Sequence_23
Sequence_23: End Process
state Sequence_23 {
direction TB
LogMessage_16 : LogMessage - LM -- Start End
LogMessage_16 --> If_6
If_6: Framework Exception?
state If_6 {
direction TB
 --> Sequence_18
Sequence_18: Handle Frameowrk Error
state Sequence_18 {
direction TB
LogMessage_17 : LogMessage - LM -- Framework Exception
InvokeWorkflowFile_25 : InvokeWorkflowFile - Take Screenshot
LogMessage_17 --> InvokeWorkflowFile_25
InvokeWorkflowFile_26 : InvokeWorkflowFile - Generate Diagnostic
InvokeWorkflowFile_25 --> InvokeWorkflowFile_26
MultipleAssign_15 : MultipleAssign - Set Variables
InvokeWorkflowFile_26 --> MultipleAssign_15
InvokeWorkflowFile_27 : InvokeWorkflowFile - Send Email
MultipleAssign_15 --> InvokeWorkflowFile_27
}
}
If_6 --> TryCatch_10
TryCatch_10: Try Close, Catch Kill (End)
state TryCatch_10 {
direction TB
InvokeWorkflowFile_28 : InvokeWorkflowFile - Close Applications (End)
InvokeWorkflowFile_29 : InvokeWorkflowFile - Kill Processes (End)
InvokeWorkflowFile_28 --> InvokeWorkflowFile_29
}
TryCatch_10 --> If_7
If_7: Rethrow?
state If_7 {
direction TB
Throw_2 : Throw - Rethrow FrameworkException
}
}
}
}
Transition_11 --> Transition_15
Transition_15: Data
state Transition_15 {
direction TB
 --> State_6
State_6: Process
state State_6 {
direction TB
 --> TryCatch_12
TryCatch_12: Try Processing Transaction
state TryCatch_12 {
direction TB
 --> Sequence_24
Sequence_24: Process Transaction
state Sequence_24 {
direction TB
MultipleAssign_16 : MultipleAssign - Initialize State Variables
MultipleAssign_16 --> Switch`1_3
Switch`1_3: @Test Variations - Process
state Switch`1_3 {
direction TB
Throw_5 : Throw - Throw GetTransactionData Error
}
InvokeWorkflowFile_30 : InvokeWorkflowFile - Perform Transaction
Switch`1_3 --> InvokeWorkflowFile_30
}
MultipleAssign_17 : MultipleAssign - Set BusinessException
Sequence_24 --> MultipleAssign_17
MultipleAssign_18 : MultipleAssign - Set SystemException
MultipleAssign_17 --> MultipleAssign_18
MultipleAssign_18 --> TryCatch_11
TryCatch_11: Try Setting Transaction Status
state TryCatch_11 {
direction TB
InvokeWorkflowFile_31 : InvokeWorkflowFile - HandleTransactionOutcome.xaml - Invoke Workflow File
InvokeWorkflowFile_31 --> Switch`1_4
Switch`1_4: @Test Variations - Process
state Switch`1_4 {
direction TB
Throw_7 : Throw - Throw GetTransactionData Error
}
MultipleAssign_19 : MultipleAssign - Set FrameworkException (Process)
Switch`1_4 --> MultipleAssign_19
}
}
Transition_12 : Transition - Error - Framework
TryCatch_12 --> Transition_12
Transition_12 --> Transition_13
Transition_13: Success/BRE
state Transition_13 {
direction TB
MultipleAssign_20 : MultipleAssign - Reset Consecutive Exceptions
}
Transition_14 : Transition - Error - Transaction
Transition_13 --> Transition_14
}
}
Transition_16 : Transition - No Data
Transition_15 --> Transition_16
Transition_17 : Transition - Stop
Transition_16 --> Transition_17
}
}
Transition_19 : Transition - Error - Initialization
Transition_18 --> Transition_19
Transition_19 --> Transition_20
Transition_20: MaxConsecutive
state Transition_20 {
direction TB
 --> Sequence_25
Sequence_25: Update + Log
state Sequence_25 {
direction TB
MultipleAssign_22 : MultipleAssign - Set FrameworkException to Max Consecutive
LogMessage_19 : LogMessage - LM -- Max Consecutive
MultipleAssign_22 --> LogMessage_19
}
}
}
}
```