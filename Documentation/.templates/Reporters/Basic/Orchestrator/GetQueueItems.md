# GetQueueItems
Class: GetQueueItems

Retrieves all queue items for a particular queue id into a list.

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
- System.Runtime.Serialization
- Newtonsoft.Json.Linq
- Newtonsoft.Json
- System.Dynamic
- System.Linq.Expressions
- UiPath.Core.Activities.Orchestrator
- System.ComponentModel
- System.Collections.Specialized


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
- System.Collections.Immutable
- System.Collections.NonGeneric
- System.Collections.Specialized
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
- System.Linq.Parallel
- System.Linq.Queryable
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
- UiPath.Workflow
- WindowsBase


</details>
<details>
    <summary>
    <b>Arguments</b>
    </summary>
    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>in_QueueId</td><td>InArgument</td><td>x:Int32</td><td>The ID of the queue to get queue items for.</td></tr><tr><td>in_From</td><td>InArgument</td><td>s:DateTime</td><td>The start of the reporting range.</td></tr><tr><td>in_To</td><td>InArgument</td><td>s:DateTime</td><td>The end of the reporting period.</td></tr><tr><td>in_Statuses</td><td>InArgument</td><td>s:String[]</td><td>A list of the statuses to include in the output queue items list.</td></tr><tr><td>out_QueueItems</td><td>OutArgument</td><td>scg:List(njl:JToken)</td><td>The list of queue items retrieved.</td></tr></table>
    
</details>
<details>
    <summary>
    <b>Workflows Used</b>
    </summary>

    

    
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

Sequence_1: Sequence - GetQueueItems
state Sequence_1 {
direction TB
LogMessage_1 : LogMessage - LM -- Start
MultipleAssign_1 : MultipleAssign - Initialize Vars
LogMessage_1 --> MultipleAssign_1
ForEach1_1: ForEach - For Each Status
state ForEach1_1 {
direction TB
If_1: If - Last Status
state If_1 {
direction TB
MultipleAssign_2 : MultipleAssign - Close off StatusFilter
MultipleAssign_3 : MultipleAssign - Append to StatusFilter
MultipleAssign_2 --> MultipleAssign_3
}
MultipleAssign_3 --> If_1
}
If_1 --> ForEach1_1
If_2: If - Status Included?
state If_2 {
direction TB
MultipleAssign_4 : MultipleAssign - Update EndPoint with Statuses
}
MultipleAssign_4 --> If_2
InterruptibleDoWhile_1: InterruptibleDoWhile - Loop While 1000 Items Returned
state InterruptibleDoWhile_1 {
direction TB
Sequence_6: Sequence - Body
state Sequence_6 {
direction TB
OrchestratorHttpRequest_1 : OrchestratorHttpRequest - Queue Items API Call
MultipleAssign_5 : MultipleAssign - Parse Response
OrchestratorHttpRequest_1 --> MultipleAssign_5
}
MultipleAssign_5 --> Sequence_6
}
Sequence_6 --> InterruptibleDoWhile_1
LogMessage_2 : LogMessage - LM -- Complete
InterruptibleDoWhile_1 --> LogMessage_2
}
LogMessage_2 --> Sequence_1
```