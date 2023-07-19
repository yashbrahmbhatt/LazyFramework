# GetQueueData
Class: GetQueueData

Gets the queue data list and parses it into a table.

<hr />

## Workflow Details
<details>
    <summary>
    <b>Namespaces</b>
    </summary>

    - System.Activities
- System.Activities.Statements
- System.Activities.Expressions
- System.Activities.Validation
- System.Activities.XamlIntegration
- Microsoft.VisualBasic
- Microsoft.VisualBasic.Activities
- System
- System.Collections
- System.Collections.Generic
- System.Collections.ObjectModel
- System.Data
- System.Diagnostics
- System.Linq
- System.Net.Mail
- System.Xml
- System.Text
- System.Xml.Linq
- UiPath.Core
- UiPath.Core.Activities
- System.Windows.Markup
- GlobalVariablesNamespace
- GlobalConstantsNamespace
- System.Reflection
- Newtonsoft.Json.Linq
- Newtonsoft.Json
- System.Dynamic
- System.ComponentModel
- System.Runtime.Serialization
- System.Xml.Serialization


</details>
<details>
    <summary>
    <b>References</b>
    </summary>

    - Microsoft.CSharp
- Microsoft.VisualBasic
- Microsoft.Win32.Primitives
- netstandard
- Newtonsoft.Json
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
- System.Linq.Expressions
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

    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>out_QueueList</td><td>OutArgument</td><td>scg:List(njl:JToken)</td><td>The unformatted list of queue items retrieved, as a List of JToken.</td></tr><tr><td>in_QueueName</td><td>InArgument</td><td>x:String</td><td>The name of the queue to report.</td></tr><tr><td>in_QueueFolder</td><td>InArgument</td><td>x:String</td><td>The path fo the folder that houses the queue to report.</td></tr><tr><td>in_From</td><td>InArgument</td><td>s:DateTime</td><td>The start of the reporting range.</td></tr><tr><td>in_To</td><td>InArgument</td><td>s:DateTime</td><td>The end of the reporting range.</td></tr><tr><td>in_Statuses</td><td>InArgument</td><td>s:String[]</td><td>The statuses to include when retrieving queue items.</td></tr><tr><td>out_dt_QueueTable</td><td>OutArgument</td><td>sd:DataTable</td><td>The queue items retrieved, formatted as a flattened table.</td></tr></table>
    
</details>

<hr />

## Outline (Beta)

```mermaid
stateDiagram-v2


Sequence_1: Sequence - GetQueueData
state Sequence_1 {
direction TB
LogMessage_1 : LogMessage - LM -- Start
MultipleAssign_1 : MultipleAssign - Initialize Outputs
LogMessage_1 --> MultipleAssign_1
InvokeWorkflowFile_1 : InvokeWorkflowFile - Get Queue ID
MultipleAssign_1 --> InvokeWorkflowFile_1
InvokeWorkflowFile_2 : InvokeWorkflowFile - Get Queue Items
InvokeWorkflowFile_1 --> InvokeWorkflowFile_2
InvokeWorkflowFile_3 : InvokeWorkflowFile - Flatten Queue Items Into Table
InvokeWorkflowFile_2 --> InvokeWorkflowFile_3
LogMessage_2 : LogMessage - LM -- Complete
InvokeWorkflowFile_3 --> LogMessage_2
}
```