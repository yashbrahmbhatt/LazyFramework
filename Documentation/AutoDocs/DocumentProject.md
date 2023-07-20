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
- UiPath.DataTableUtilities


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
- System.Collections.Immutable
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
- System.Runtime.Serialization
- System.Runtime.Serialization.Formatters
- System.Runtime.Serialization.Primitives
- System.Security.Permissions
- System.ServiceModel
- System.ServiceModel.Activities
- System.Xaml
- System.Xml
- System.Xml.Linq
- System.Xml.ReaderWriter
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

| Name | Direction | Type | Description |
|  --- | --- | --- | ---  |
| OutputRootFolder | InArgument | x:String |  |

    
</details>
<details>
    <summary>
    <b>Workflows Used</b>
    </summary>

- C:\Users\eyash\Documents\UiPath\LazyFramework\AutoDocs\ParseProjectJSON.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\AutoDocs\ParseWorkflow.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\AutoDocs\DataTableToMarkdown.xaml

    
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


Sequence_1: Sequence - DocumentProject
state Sequence_1 {
direction TB
MultipleAssign_1 : MultipleAssign - Initialize Vars
InvokeWorkflowFile_3 : InvokeWorkflowFile - AutoDocs\\ParseProjectJSON.xaml - Invoke Workflow File
MultipleAssign_1 --> InvokeWorkflowFile_3
MultipleAssign_3 : MultipleAssign - Get Test Workflows
InvokeWorkflowFile_3 --> MultipleAssign_3
MultipleAssign_3 --> ForEach1_3
ForEach1_3: ForEach - For Each Test
state ForEach1_3 {
direction TB

Sequence_5: Sequence - Parse Test
state Sequence_5 {
direction TB
InvokeWorkflowFile_4 : InvokeWorkflowFile - AutoDocs\\ParseWorkflow.xaml - Invoke Workflow File
MultipleAssign_4 : MultipleAssign - Add Values to Dictioanry
InvokeWorkflowFile_4 --> MultipleAssign_4
}
}
InvokeWorkflowFile_6 : InvokeWorkflowFile - AutoDocs\\DataTableToMarkdown.xaml - Invoke Workflow File
ForEach1_3 --> InvokeWorkflowFile_6
MultipleAssign_5 : MultipleAssign - Get Project Content
InvokeWorkflowFile_6 --> MultipleAssign_5
DeleteFolderX_2 : DeleteFolderX - Delete Folder
MultipleAssign_5 --> DeleteFolderX_2
CreateDirectory_2 : CreateDirectory - Create Folder
DeleteFolderX_2 --> CreateDirectory_2
WriteTextFile_2 : WriteTextFile - Write Project.md
CreateDirectory_2 --> WriteTextFile_2
WriteTextFile_2 --> ForEach1_1
ForEach1_1: ForEach - For Each Workflow
state ForEach1_1 {
direction TB

Sequence_2: Sequence - Body
state Sequence_2 {
direction TB
InvokeWorkflowFile_1 : InvokeWorkflowFile - ParseWorkflow.xaml - Invoke Workflow File
InvokeWorkflowFile_7 : InvokeWorkflowFile - AutoDocs\\DataTableToMarkdown.xaml - Invoke Workflow File
InvokeWorkflowFile_1 --> InvokeWorkflowFile_7
MultipleAssign_2 : MultipleAssign - Multiple Assign
InvokeWorkflowFile_7 --> MultipleAssign_2
CreateDirectory_1 : CreateDirectory - Create Folder
MultipleAssign_2 --> CreateDirectory_1
WriteTextFile_1 : WriteTextFile - Write Text File
CreateDirectory_1 --> WriteTextFile_1
}
}
}
```