# AddModules
Class: AddModules

Accelerates creating new modules by prompting the user for set of modules they want to create, and copies the configs, templates, and all workflow files for that module.

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
- System.Runtime.Serialization
- UiPath.Studio.Forms.Runtime
- System.Reflection
- UiPath.Forms.Activities
- UiPath.Platform.GlobalVariables
- UiPath.Forms.Activities.Base
- UiPath.Platform.Triggers
- UiPath.Platform.Triggers.Scope
- System.IO
- System.ComponentModel
- System.Xml.Serialization
- System.ComponentModel
- System.Xml.Serialization
- UiPath.Core.Activities.Orchestrator
- UiPath.Activities.System
- UiPath.Shared.Activities
- UiPath.DataTableUtilities
- System.Linq.Expressions
- UiPath.Platform.ResourceHandling
- UiPath.Excel
- UiPath.Excel.Activities.Business
- UiPath.Excel.Model
- UiPath.Shared.Activities.Business
- UiPath.Core.Activities.Storage


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
- UiPath.Forms.Activities
- UiPath.Platform
- UiPath.Studio.Constants
- UiPath.Studio.Forms.Runtime
- UiPath.System.Activities
- UiPath.System.Activities.Design
- UiPath.System.Activities.ViewModels
- UiPath.Workflow
- WindowsBase
- UiPath.Excel.Activities
- UiPath.Mail.Activities
- UiPath.Persistence.Activities
- UiPath.Testing.Activities
- UiPath.Excel.Activities.Design
- UiPath.Excel


</details>
<details>
    <summary>
    <b>Arguments</b>
    </summary>

| Name | Direction | Type | Description |
|  --- | --- | --- | ---  |

    
</details>
<details>
    <summary>
    <b>Workflows Used</b>
    </summary>

- C:\Users\eyash\Documents\UiPath\LazyFramework\Utility\LoadConfig.xaml

    
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


Sequence_1: Sequence - AddModules
state Sequence_1 {
direction TB
LogMessage_1 : LogMessage - LM -- Start
InvokeWorkflowFile_2 : InvokeWorkflowFile - Load AddModules Config
LogMessage_1 --> InvokeWorkflowFile_2
MultipleAssign_1 : MultipleAssign - Parse Config
InvokeWorkflowFile_2 --> MultipleAssign_1
LogMessage_2 : LogMessage - LM -- Form Start
MultipleAssign_1 --> LogMessage_2
ShowFormActivity_2 : ShowFormActivity - Show Modules Form
LogMessage_2 --> ShowFormActivity_2
LogMessage_3 : LogMessage - LM -- Form Complete
ShowFormActivity_2 --> LogMessage_3
LogMessage_3 --> ForEachRow_1
ForEachRow_1: ForEachRow - For Each Module
state ForEachRow_1 {
direction TB

Sequence_3: Sequence - Body
state Sequence_3 {
direction TB
LogMessage_4 : LogMessage - LM -- Module Start
MultipleAssign_2 : MultipleAssign - Parse Values
LogMessage_4 --> MultipleAssign_2
MessageBox_1 : MessageBox - Message for Folder
MultipleAssign_2 --> MessageBox_1
SelectFolder_1 : SelectFolder - Prompt Folder Select
MessageBox_1 --> SelectFolder_1
LogMessage_5 : LogMessage - LM -- Folder Selected
SelectFolder_1 --> LogMessage_5
LogMessage_5 --> ForEach1_2
ForEach1_2: ForEach - Copy All Files From Modules Root
state ForEach1_2 {
direction TB

Sequence_4: Sequence - Copy File
state Sequence_4 {
direction TB
MultipleAssign_3 : MultipleAssign - Set Target Path
MultipleAssign_3 --> If_1
If_1: If - Workflow File?
state If_1 {
direction TB

Sequence_5: Sequence - Is Workflow
state Sequence_5 {
direction TB
ReadTextFile_1 : ReadTextFile - Read File
ReadTextFile_1 --> If_2
If_2: If - Folder Exists for File?
state If_2 {
direction TB
CreateDirectory_1 : CreateDirectory - Create Folder For File
}
MultipleAssign_4 : MultipleAssign - Replace Invoke Paths
If_2 --> MultipleAssign_4
WriteTextFile_1 : WriteTextFile - Write Target File
MultipleAssign_4 --> WriteTextFile_1
}
CopyFile_4 : CopyFile - Copy Other File
}
}
}
CopyFile_2 : CopyFile - Copy Config
ForEach1_2 --> CopyFile_2
CopyFile_2 --> ExcelProcessScopeX_1
ExcelProcessScopeX_1: ExcelProcessScopeX - Excel Process Scope
state ExcelProcessScopeX_1 {
direction TB

ExcelApplicationCard_1: ExcelApplicationCard - Use Excel File
state ExcelApplicationCard_1 {
direction TB

ForEachSheetX_1: ForEachSheetX - For Each Excel Sheet
state ForEachSheetX_1 {
direction TB

ExcelForEachRowX_1: ExcelForEachRowX - For Each Excel Row
state ExcelForEachRowX_1 {
direction TB

If_5: If - Not Empty Row?
state If_5 {
direction TB

Switch1_1: Switch - Sheet Name?
state Switch1_1 {
direction TB
MultipleAssign_10 : MultipleAssign - Update Path (TextFiles)
MultipleAssign_11 : MultipleAssign - Update Path (ExcelFiles)
MultipleAssign_10 --> MultipleAssign_11
}
}
}
}
}
}
ExcelProcessScopeX_1 --> ForEach1_3
ForEach1_3: ForEach - Copy Templates
state ForEach1_3 {
direction TB

Sequence_7: Sequence - Copy Template File Steps
state Sequence_7 {
direction TB

If_3: If - Template File's Folder Exists?
state If_3 {
direction TB
CreateDirectory_3 : CreateDirectory - Create Templates Folder
}
CopyFile_3 : CopyFile - Copy Template File
If_3 --> CopyFile_3
}
}
LogMessage_6 : LogMessage - LM -- Module Complete
ForEach1_3 --> LogMessage_6
}
}
}
```