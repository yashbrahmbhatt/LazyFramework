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

| Name | Direction | Type | Description |
|  --- | --- | --- | ---  |
| in_ConfigPath | InArgument | x:String | The path to the config file to use to load variables and resources. |
| in_IgnoreSheets | InArgument | s:String[] | A list of sheet names to ignore when loading the config file. |
| in_TestID | InArgument | x:String | Used to modify the workflow in order to test different scenarios. Only used to test exception handling in this workflow. Leave as null for production use. |

    
</details>
<details>
    <summary>
    <b>Workflows Used</b>
    </summary>

- C:\Users\eyash\Documents\UiPath\LazyFramework\Utility\LoadConfig.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\.templates\Performers\Basic\Framework\CloseApplications.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\.templates\Performers\Basic\Framework\KillProcesses.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\.templates\Performers\Basic\Framework\InitializeApplications.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\.templates\Performers\Basic\Framework\IsMaintenanceTime.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\Utility\TakeScreenshot.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\Utility\GenerateDiagnosticDictionary.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\Utility\SendEmail.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\.templates\Performers\Basic\Framework\Process.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\.templates\Performers\Basic\Framework\HandleTransactionOutcome.xaml

    
</details>
<details>
    <summary>
    <b>Tests</b>
    </summary>



    
</details>

<hr />

## Outline (Beta)

```mermaid
stateDiagram-v2


StateMachine_3: StateMachine - Performer State Machine
state StateMachine_3 {
direction TB

State_9: State - Initialization
state State_9 {
direction TB

TryCatch_8: TryCatch - Try Initializing
state TryCatch_8 {
direction TB

RetryScope_4: RetryScope - Retry - Initializing
state RetryScope_4 {
direction TB

Sequence_21: Sequence - Initialize
state Sequence_21 {
direction TB

If_5: If - First Run?
state If_5 {
direction TB

Sequence_19: Sequence - Initialize Settings
state Sequence_19 {
direction TB
LogMessage_11 : LogMessage - LM -- Screen
InvokeWorkflowFile_19 : InvokeWorkflowFile - Utility\\LoadConfig.xaml - Invoke Workflow File
}
}

Switch1_1: Switch - @Test Variations - Initialization
state Switch1_1 {
direction TB
MultipleAssign_21 : MultipleAssign - Update Maintenance Times
Throw_1 : Throw - Throw InitializationError
}
LogMessage_12 : LogMessage - LM -- Initializing
MultipleAssign_12 : MultipleAssign - Reset System Exception

TryCatch_7: TryCatch - Try Close, Catch Kill (Initialization)
state TryCatch_7 {
direction TB
InvokeWorkflowFile_21 : InvokeWorkflowFile - Close Applications (Initialization)
InvokeWorkflowFile_22 : InvokeWorkflowFile - Kill Processes (Initialization)
}
InvokeWorkflowFile_23 : InvokeWorkflowFile - Init All Applications
}
}
MultipleAssign_13 : MultipleAssign - Set Framework Exception (Initialization)
}

Transition_18: Transition - Success
state Transition_18 {
direction TB

State_8: State - Get Transaction Data
state State_8 {
direction TB

TryCatch_9: TryCatch - Try Getting Transaction
state TryCatch_9 {
direction TB

Sequence_17: Sequence - Get Transaction
state Sequence_17 {
direction TB
LogMessage_13 : LogMessage - LM -- Get Next Transaction

Switch1_2: Switch - @Test Variations - Get Transaction Data
state Switch1_2 {
direction TB
Throw_4 : Throw - Throw GetTransactionData Error
}
ShouldStop_2 : ShouldStop - Stop Requested?
InvokeWorkflowFile_24 : InvokeWorkflowFile - Is Maintenance Time?

IfElseIf_2: IfElseIf - Stop Requested/Maintenance Window/Success?
state IfElseIf_2 {
direction TB
LogMessage_14 : LogMessage - LM -- Stop
LogMessage_15 : LogMessage - LM -- Maintenance
LogMessage_14 --> LogMessage_15
LogMessage_15 --> RetryScope_5
RetryScope_5: RetryScope - Retry - Get Transaction
state RetryScope_5 {
direction TB
GetQueueItem_3 : GetQueueItem - Get Next Transaction
}
}
}
MultipleAssign_14 : MultipleAssign - Set Framework Exception (Get Transaction Data)
}

Transition_11: Transition - Error - Get Transaction Data
state Transition_11 {
direction TB

State_7: State - End
state State_7 {
direction TB

Sequence_23: Sequence - End Process
state Sequence_23 {
direction TB
LogMessage_16 : LogMessage - LM -- Start End

If_6: If - Framework Exception?
state If_6 {
direction TB

Sequence_18: Sequence - Handle Frameowrk Error
state Sequence_18 {
direction TB
LogMessage_17 : LogMessage - LM -- Framework Exception
InvokeWorkflowFile_25 : InvokeWorkflowFile - Take Screenshot
InvokeWorkflowFile_26 : InvokeWorkflowFile - Generate Diagnostic
MultipleAssign_15 : MultipleAssign - Set Variables
InvokeWorkflowFile_27 : InvokeWorkflowFile - Send Email
}
}

TryCatch_10: TryCatch - Try Close, Catch Kill (End)
state TryCatch_10 {
direction TB
InvokeWorkflowFile_28 : InvokeWorkflowFile - Close Applications (End)
InvokeWorkflowFile_29 : InvokeWorkflowFile - Kill Processes (End)
}

If_7: If - Rethrow?
state If_7 {
direction TB
Throw_2 : Throw - Rethrow FrameworkException
}
}
}
}
Transition_11 --> Transition_15
Transition_15: Transition - Data
state Transition_15 {
direction TB

State_6: State - Process
state State_6 {
direction TB

TryCatch_12: TryCatch - Try Processing Transaction
state TryCatch_12 {
direction TB

Sequence_24: Sequence - Process Transaction
state Sequence_24 {
direction TB
MultipleAssign_16 : MultipleAssign - Initialize State Variables

Switch1_3: Switch - @Test Variations - Process
state Switch1_3 {
direction TB
Throw_5 : Throw - Throw GetTransactionData Error
}
InvokeWorkflowFile_30 : InvokeWorkflowFile - Perform Transaction
}
MultipleAssign_17 : MultipleAssign - Set BusinessException
MultipleAssign_18 : MultipleAssign - Set SystemException
MultipleAssign_17 --> MultipleAssign_18

TryCatch_11: TryCatch - Try Setting Transaction Status
state TryCatch_11 {
direction TB
InvokeWorkflowFile_31 : InvokeWorkflowFile - HandleTransactionOutcome.xaml - Invoke Workflow File
InvokeWorkflowFile_31 --> Switch1_4
Switch1_4: Switch - @Test Variations - Process
state Switch1_4 {
direction TB
Throw_7 : Throw - Throw GetTransactionData Error
}
MultipleAssign_19 : MultipleAssign - Set FrameworkException (Process)
}
}
Transition_12 : Transition - Error - Framework
Transition_12 --> Transition_13
Transition_13: Transition - Success/BRE
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
Transition_20: Transition - MaxConsecutive
state Transition_20 {
direction TB

Sequence_25: Sequence - Update + Log
state Sequence_25 {
direction TB
MultipleAssign_22 : MultipleAssign - Set FrameworkException to Max Consecutive
LogMessage_19 : LogMessage - LM -- Max Consecutive
}
}
}
}
```