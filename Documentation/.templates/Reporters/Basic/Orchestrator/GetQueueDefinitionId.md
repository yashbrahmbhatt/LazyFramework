# GetQueueDefinitionId
Class: GetQueueDefinitionId

Gets the queue definition based on the queue folder and name.

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
- UiPath.Core.Activities.Orchestrator
- Newtonsoft.Json
- Newtonsoft.Json.Linq
- System.Dynamic
- System.ComponentModel
- System.Collections.Specialized
- System.Linq.Expressions

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
    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>in_QueueName</td><td>InArgument</td><td>x:String</td><td>The name of the queue to get the id for.</td></tr><tr><td>in_QueueFolder</td><td>InArgument</td><td>x:String</td><td>The folder that houses the queue to get the id for.</td></tr><tr><td>out_Id</td><td>OutArgument</td><td>x:Int32</td><td>The id retrieved.</td></tr></table>
</details>

<hr />

## Outline (Beta)

```mermaid
stateDiagram-v2

Sequence_1: GetQueueDefinitionId
state Sequence_1 {
direction TB
OrchestratorHttpRequest_1 : OrchestratorHttpRequest - Orchestrator API Call
If_1: Status Not 2xx?
state If_1 {
direction TB
Throw_1 : Throw - Throw Orchestrator Invalid Status
}
MultipleAssign_1 : MultipleAssign - Parse Response
If_1 --> MultipleAssign_1
If_2: Validate ID Count
state If_2 {
direction TB
MultipleAssign_2 : MultipleAssign - Set Output
Throw_2 : Throw - Throw CouldNotFind
MultipleAssign_2 --> Throw_2
}
LogMessage_1 : LogMessage - LM -- Complete
If_2 --> LogMessage_1
}
```