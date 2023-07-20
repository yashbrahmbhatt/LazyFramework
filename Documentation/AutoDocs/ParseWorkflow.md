# ParseWorkflow
Class: ParseWorkflow



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
- UiPath.Platform.ResourceHandling
- System.Xml.XPath
- System.IO
- Newtonsoft.Json.Linq
- System.Linq.Expressions
- System.Xml.Serialization
- System.ComponentModel
- System.Runtime.Serialization
- System.Runtime.CompilerServices
- Newtonsoft.Json
- System.Dynamic
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
- System.Data.SqlClient
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
- System.Runtime.CompilerServices.Unsafe
- System.Runtime.CompilerServices.VisualC
- System.Runtime.InteropServices
- System.Runtime.Serialization
- System.Runtime.Serialization.Formatters
- System.Runtime.Serialization.Primitives
- System.Security.Permissions
- System.ServiceModel
- System.ServiceModel.Activities
- System.Xaml
- System.Xml
- System.Xml.Linq
- System.Xml.XPath.XDocument
- UiPath.Platform
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
    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>in_FilePath</td><td>InArgument</td><td>x:String</td><td></td></tr><tr><td>out_Document</td><td>OutArgument</td><td>sxl:XDocument</td><td></td></tr><tr><td>out_Namespaces</td><td>OutArgument</td><td>scg:List(x:String)</td><td></td></tr><tr><td>out_References</td><td>OutArgument</td><td>scg:List(x:String)</td><td></td></tr><tr><td>out_DocumentClass</td><td>OutArgument</td><td>x:String</td><td></td></tr><tr><td>out_WorkflowName</td><td>OutArgument</td><td>x:String</td><td></td></tr><tr><td>out_WorkflowDescription</td><td>OutArgument</td><td>x:String</td><td></td></tr><tr><td>out_OutlineMarkdown</td><td>OutArgument</td><td>x:String</td><td></td></tr><tr><td>out_dt_Arguments</td><td>OutArgument</td><td>sd:DataTable</td><td></td></tr><tr><td>out_WorkflowsUsed</td><td>OutArgument</td><td>scg:IEnumerable(x:String)</td><td></td></tr></table>
    
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

Sequence_1: Sequence - ParseWorkflow
state Sequence_1 {
direction TB
BuildDataTable_1 : BuildDataTable - Initialize Arguments Table
MultipleAssign_1 : MultipleAssign - Parse
BuildDataTable_1 --> MultipleAssign_1
WriteLine_1 : WriteLine - Write Line
MultipleAssign_1 --> WriteLine_1
ForEach1_1: ForEach - Add to Arguments Table
state ForEach1_1 {
direction TB
Sequence_2: Sequence - Parse Argument
state Sequence_2 {
direction TB
MultipleAssign_5 : MultipleAssign - Parse Current Argument
AddDataRow_2 : AddDataRow - Add to ArgumentsTable
MultipleAssign_5 --> AddDataRow_2
}
AddDataRow_2 --> Sequence_2
}
Sequence_2 --> ForEach1_1
InvokeWorkflowFile_1 : InvokeWorkflowFile - AutoDocs\\Helper\\TraverseWorkflow.xaml - Invoke Workflow File
ForEach1_1 --> InvokeWorkflowFile_1
MultipleAssign_6 : MultipleAssign - Multiple Assign
InvokeWorkflowFile_1 --> MultipleAssign_6
}
MultipleAssign_6 --> Sequence_1
```