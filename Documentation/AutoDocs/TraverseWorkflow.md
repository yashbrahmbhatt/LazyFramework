# TraverseWorkflow
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
- System.IO.FileSystem.AccessControl
- System.IO.FileSystem.DriveInfo
- System.IO.FileSystem.Watcher
- System.IO.Packaging
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
- System.Private.Xml
- System.Private.Xml.Linq
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
    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>in_XElement</td><td>InArgument</td><td>sxl:XElement</td><td></td></tr><tr><td>io_Markdown</td><td>InOutArgument</td><td>x:String</td><td></td></tr><tr><td>io_PreviousActivity</td><td>InOutArgument</td><td>x:String</td><td></td></tr></table>
    
</details>
<details>
    <summary>
    <b>Workflows Used</b>
    </summary>

    - C:\Users\eyash\Documents\UiPath\LazyFramework\AutoDocs\TraverseWorkflow.xaml

    
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

Sequence_1: Sequence - TraverseWorkflow
state Sequence_1 {
direction TB
MultipleAssign_1 : MultipleAssign - Parse Element
If_1: If - ActivityName Not Empty?
state If_1 {
direction TB
WriteLine_2 : WriteLine - Write Line
If_2: If - Activity Has No Children?
state If_2 {
direction TB
MultipleAssign_2 : MultipleAssign - Update Markdown for Single Element
Switch1_1: Switch - Switch
state Switch1_1 {
direction TB
Sequence_6: Sequence - Default
state Sequence_6 {
direction TB
MultipleAssign_4 : MultipleAssign - Multiple Assign
ForEach1_3: ForEach - Recurse
state ForEach1_3 {
direction TB
Sequence_7: Sequence - Body
state Sequence_7 {
direction TB
InvokeWorkflowFile_2 : InvokeWorkflowFile - AutoDocs\\Helper\\TraverseWorkflow.xaml - Invoke Workflow File
}
InvokeWorkflowFile_2 --> Sequence_7
}
Sequence_7 --> ForEach1_3
MultipleAssign_5 : MultipleAssign - Multiple Assign
ForEach1_3 --> MultipleAssign_5
}
MultipleAssign_5 --> Sequence_6
}
Sequence_6 --> Switch1_1
}
Switch1_1 --> If_2
ForEach1_1: ForEach - Recurse
state ForEach1_1 {
direction TB
Sequence_2: Sequence - Body
state Sequence_2 {
direction TB
InvokeWorkflowFile_1 : InvokeWorkflowFile - AutoDocs\\Helper\\TraverseWorkflow.xaml - Invoke Workflow File
}
InvokeWorkflowFile_1 --> Sequence_2
}
Sequence_2 --> ForEach1_1
}
ForEach1_1 --> If_1
}
If_1 --> Sequence_1
```