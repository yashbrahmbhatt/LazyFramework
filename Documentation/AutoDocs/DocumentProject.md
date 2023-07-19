# DocumentProject
Class: DocumentProject



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
- System.IO
- System.Linq.Expressions
- System.Runtime.Serialization
- UiPath.Platform.ResourceHandling
- System.ComponentModel
- System.Xml.Serialization
- System.ComponentModel
- System.Xml.Serialization


</details>
<details>
    <summary>
    <b>References</b>
    </summary>

    - Microsoft.CSharp
- Microsoft.VisualBasic
- NPOI
- System
- System.Activities
- System.ComponentModel
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
- System.Memory
- System.Memory.Data
- System.ObjectModel
- System.Private.CoreLib
- System.Private.Uri
- System.Reflection.DispatchProxy
- System.Reflection.Metadata
- System.Reflection.TypeExtensions
- System.Runtime.Serialization
- System.Security.Permissions
- System.ServiceModel
- System.ServiceModel.Activities
- System.Xaml
- System.Xml
- System.Xml.Linq
- UiPath.Studio.Constants
- UiPath.System.Activities
- UiPath.Workflow
- System.Private.ServiceModel
- System.Runtime.Serialization.Formatters
- System.Private.DataContractSerialization
- System.Runtime.Serialization.Primitives
- System.Private.Xml.Linq
- System.Collections
- UiPath.System.Activities.Design
- UiPath.System.Activities.ViewModels
- UiPath.Platform
- System.Data.SqlClient
- System.ComponentModel.Primitives
- System.Private.Xml
- System.ComponentModel.EventBasedAsync
- PresentationFramework
- WindowsBase
- Microsoft.Win32.Primitives
- System.Linq.Parallel
- System.Collections.Immutable
- System.Linq.Queryable


</details>
<details>
    <summary>
    <b>Arguments</b>
    </summary>

    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>OutputRootFolder</td><td>InArgument</td><td>x:String</td><td></td></tr></table>
    
</details>

<hr />

## Outline (Beta)

```mermaid
stateDiagram-v2


Sequence_1: DocumentProject
state Sequence_1 {
direction TB
MultipleAssign_1 : MultipleAssign - Initialize Vars
DeleteFolderX_1 : DeleteFolderX - Delete Folder
MultipleAssign_1 --> DeleteFolderX_1
DeleteFolderX_1 --> ForEach`1_1
ForEach`1_1: For Each Workflow
state ForEach`1_1 {
direction TB

Sequence_2: Body
state Sequence_2 {
direction TB
InvokeWorkflowFile_1 : InvokeWorkflowFile - AutoDocs\\Helper\\ParseWorkflow.xaml - Invoke Workflow File
InvokeWorkflowFile_2 : InvokeWorkflowFile - Utility\\DataTableToHTML.xaml - Invoke Workflow File
InvokeWorkflowFile_1 --> InvokeWorkflowFile_2
MultipleAssign_2 : MultipleAssign - Multiple Assign
InvokeWorkflowFile_2 --> MultipleAssign_2
CreateDirectory_1 : CreateDirectory - Create Folder
MultipleAssign_2 --> CreateDirectory_1
WriteTextFile_1 : WriteTextFile - Write Text File
CreateDirectory_1 --> WriteTextFile_1
}
}
}
```