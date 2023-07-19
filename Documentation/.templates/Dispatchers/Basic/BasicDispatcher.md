# {WorkflowName}
Class: BasicDispatcher

Reads data from the srouce of work and adds it to a queue.

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
- System.Linq
- System.Reflection
- System.Runtime.Serialization
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
- NPOI
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
- System.Data.SqlClient
- System.Linq
- System.Memory
- System.Memory.Data
- System.ObjectModel
- System.Private.CoreLib
- System.Private.DataContractSerialization
- System.Private.ServiceModel
- System.Private.Uri
- System.Private.Xml
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
    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>in_ConfigPath</td><td>InArgument</td><td>x:String</td><td>The path to the config file to use to load variables and resources.</td></tr><tr><td>in_IgnoreSheets</td><td>InArgument</td><td>s:String[]</td><td>A list of the sheets to ignore loading from the config.</td></tr><tr><td>in_TestID</td><td>InArgument</td><td>x:String</td><td>Used to modify the workflow in order to test different scenarios. Only used to test exception handling in this workflow. Leave as null for production use.</td></tr></table>
</details>

<hr />

## Outline (Beta)

```mermaid
stateDiagram-v2

Sequence_1: BasicDispatcher
state Sequence_1 {
direction TB
TryCatch_1: Try Dispatching
state TryCatch_1 {
direction TB
Sequence_2: Dispatching
state Sequence_2 {
direction TB
InvokeWorkflowFile_1 : InvokeWorkflowFile - LoadConfig.xaml - Invoke Workflow File
Switch`1_1: TestID?
state Switch`1_1 {
direction TB
Throw_1 : Throw - Throw Test Exception
}
LogMessage_1 : LogMessage - LM -- Start
Switch`1_1 --> LogMessage_1
MultipleAssign_2 : MultipleAssign - Setup Queue Data
LogMessage_1 --> MultipleAssign_2
RetryScope_1: Retry - Orchestrator
state RetryScope_1 {
direction TB
AddQueueItem_1 : AddQueueItem - Add Item to Queue
}
MultipleAssign_3 : MultipleAssign - ItemsAdded++
RetryScope_1 --> MultipleAssign_3
LogMessage_2 : LogMessage - LM -- Complete
MultipleAssign_3 --> LogMessage_2
}
Sequence_3: Send Exception Email
state Sequence_3 {
direction TB
InvokeWorkflowFile_4 : InvokeWorkflowFile - Utility\\TakeScreenshot.xaml - Invoke Workflow File
InvokeWorkflowFile_2 : InvokeWorkflowFile - GenerateDiagnosticDictionary.xaml - Invoke Workflow File
InvokeWorkflowFile_4 --> InvokeWorkflowFile_2
MultipleAssign_1 : MultipleAssign - Add Keys to Template Data
InvokeWorkflowFile_2 --> MultipleAssign_1
InvokeWorkflowFile_3 : InvokeWorkflowFile - SendEmail.xaml - Invoke Workflow File
MultipleAssign_1 --> InvokeWorkflowFile_3
Rethrow_1 : Rethrow - Rethrow Exception
InvokeWorkflowFile_3 --> Rethrow_1
}
}
}
```