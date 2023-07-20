# BasicDispatcher
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

| Name | Direction | Type | Description |
|  --- | --- | --- | ---  |
| in_ConfigPath | InArgument | x:String | The path to the config file to use to load variables and resources. |
| in_IgnoreSheets | InArgument | s:String[] | A list of the sheets to ignore loading from the config. |
| in_TestID | InArgument | x:String | Used to modify the workflow in order to test different scenarios. Only used to test exception handling in this workflow. Leave as null for production use. |

    
</details>
<details>
    <summary>
    <b>Workflows Used</b>
    </summary>

- C:\Users\eyash\Documents\UiPath\LazyFramework\Utility\LoadConfig.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\Utility\TakeScreenshot.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\Utility\GenerateDiagnosticDictionary.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\Utility\SendEmail.xaml

    
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


Sequence_1: Sequence - BasicDispatcher
state Sequence_1 {
direction TB

TryCatch_1: TryCatch - Try Dispatching
state TryCatch_1 {
direction TB

Sequence_2: Sequence - Dispatching
state Sequence_2 {
direction TB
InvokeWorkflowFile_1 : InvokeWorkflowFile - LoadConfig.xaml - Invoke Workflow File

Switch1_1: Switch - TestID?
state Switch1_1 {
direction TB
Throw_1 : Throw - Throw Test Exception
}
LogMessage_1 : LogMessage - LM -- Start
MultipleAssign_2 : MultipleAssign - Setup Queue Data

RetryScope_1: RetryScope - Retry - Orchestrator
state RetryScope_1 {
direction TB
AddQueueItem_1 : AddQueueItem - Add Item to Queue
}
MultipleAssign_3 : MultipleAssign - ItemsAdded++
LogMessage_2 : LogMessage - LM -- Complete
}

Sequence_3: Sequence - Send Exception Email
state Sequence_3 {
direction TB
InvokeWorkflowFile_4 : InvokeWorkflowFile - Utility\\TakeScreenshot.xaml - Invoke Workflow File
InvokeWorkflowFile_2 : InvokeWorkflowFile - GenerateDiagnosticDictionary.xaml - Invoke Workflow File
MultipleAssign_1 : MultipleAssign - Add Keys to Template Data
InvokeWorkflowFile_3 : InvokeWorkflowFile - SendEmail.xaml - Invoke Workflow File
Rethrow_1 : Rethrow - Rethrow Exception
}
}
}
```