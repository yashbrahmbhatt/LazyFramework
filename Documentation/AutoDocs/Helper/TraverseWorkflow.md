# {WorkflowName}
Class: TraverseWorkflow



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
- System.Xml.Serialization
- System.IO
- Newtonsoft.Json.Linq
- Newtonsoft.Json
- System.Dynamic
- System.ComponentModel
- System.Collections.Specialized
- System.Linq.Expressions
- System.Runtime.Serialization

</details>
<details>
    <summary>
    <b>References</b>
    </summary>
    - Microsoft.CSharp
- System
- System.Linq
- System.Core
- System.Activities
- System.Data
- System.Data.Common
- System.Runtime.Serialization
- System.ServiceModel
- System.ServiceModel.Activities
- System.Xaml
- System.Activities
- Microsoft.VisualBasic
- System.Private.CoreLib
- System.Data
- System
- System.Core
- System.Xml
- System.Xml.Linq
- System.Xaml
- UiPath.System.Activities
- UiPath.UiAutomation.Activities
- UiPath.Studio.Constants
- System.Reflection.DispatchProxy
- System.Reflection.TypeExtensions
- System.ObjectModel
- System.Reflection.Metadata
- System.Private.Xml.Linq
- System.Private.Xml
- UiPath.Workflow
- System.Private.DataContractSerialization
- System.Linq.Expressions
- System.Linq.Parallel
- System.Collections.Immutable
- System.Linq.Queryable
- NPOI
- System.Memory.Data
- System.ComponentModel.TypeConverter
- System.Console
- System.Configuration.ConfigurationManager
- System.Security.Permissions
- System.ComponentModel
- System.Memory
- System.Private.Uri
- System.IO.FileSystem.Watcher
- System.IO.Packaging
- System.IO.FileSystem.AccessControl
- System.IO.FileSystem.DriveInfo
- System.Private.ServiceModel
- System.Collections
- netstandard
- Newtonsoft.Json
- System.ComponentModel.EventBasedAsync
- PresentationFramework
- WindowsBase
- Microsoft.Win32.Primitives
- System.ComponentModel.Primitives
- System.Collections.Specialized
- System.Collections.NonGeneric
- System.Runtime.Serialization.Formatters
- System.Runtime.Serialization.Primitives
- UiPath.System.Activities.Design
- UiPath.System.Activities.ViewModels

</details>
<details>
    <summary>
    <b>Arguments</b>
    </summary>
    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>in_XElement</td><td>InArgument</td><td>sxl:XElement</td><td></td></tr><tr><td>io_Markdown</td><td>InOutArgument</td><td>x:String</td><td></td></tr><tr><td>io_PreviousActivity</td><td>InOutArgument</td><td>x:String</td><td></td></tr></table>
</details>

<hr />

## Outline (Beta)

```mermaid
stateDiagram-v2

Sequence_1: TraverseWorkflow
state Sequence_1 {
direction TB
MultipleAssign_1 : MultipleAssign - Parse Element
If_1: ActivityName Not Empty?
state If_1 {
direction TB
WriteLine_2 : WriteLine - Write Line
If_2: Activity Has No Children?
state If_2 {
direction TB
MultipleAssign_2 : MultipleAssign - Update Markdown for Single Element
Switch`1_1: Switch
state Switch`1_1 {
direction TB
Sequence_6: Default
state Sequence_6 {
direction TB
MultipleAssign_4 : MultipleAssign - Multiple Assign
MultipleAssign_6 : MultipleAssign - Multiple Assign
MultipleAssign_4 --> MultipleAssign_6
ForEach`1_3: Recurse
state ForEach`1_3 {
direction TB
Sequence_7: Body
state Sequence_7 {
direction TB
InvokeWorkflowFile_2 : InvokeWorkflowFile - AutoDocs\\Helper\\TraverseWorkflow.xaml - Invoke Workflow File
}
}
MultipleAssign_5 : MultipleAssign - Multiple Assign
ForEach`1_3 --> MultipleAssign_5
}
}
}
ForEach`1_1: Recurse
state ForEach`1_1 {
direction TB
Sequence_2: Body
state Sequence_2 {
direction TB
InvokeWorkflowFile_1 : InvokeWorkflowFile - AutoDocs\\Helper\\TraverseWorkflow.xaml - Invoke Workflow File
}
}
}
}
```