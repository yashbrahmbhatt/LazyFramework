# WriteTableToExcel
Class: WriteTableToExcel

A basic template for a test with the expected outcome being success.

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
- System.Data
- System.Diagnostics
- System.Drawing
- System.IO
- System.Linq
- System.Net.Mail
- System.Xml
- System.Text
- System.Xml.Linq
- UiPath.Core
- UiPath.Core.Activities
- System.Windows.Markup
- System.Collections.ObjectModel
- System.Runtime.Serialization
- System.Reflection
- System.Linq.Expressions
- UiPath.Testing.Activities
- UiPath.Shared.Activities
- GlobalVariablesNamespace
- GlobalConstantsNamespace
- System.Activities.Runtime.Collections
- System.ComponentModel
- System.Xml.Serialization
- UiPath.Excel
- UiPath.Excel.Activities.Business
- UiPath.Excel.Model


</details>
<details>
    <summary>
    <b>References</b>
    </summary>

    - Microsoft.CSharp
- Microsoft.VisualBasic
- mscorlib
- NPOI
- PresentationCore
- PresentationFramework
- System
- System.Activities
- System.ComponentModel
- System.ComponentModel.TypeConverter
- System.Configuration.ConfigurationManager
- System.Console
- System.Core
- System.Data
- System.Drawing
- System.Linq
- System.Linq.Expressions
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
- UiPath.Excel
- UiPath.Excel.Activities
- UiPath.Mail.Activities
- UiPath.Studio.Constants
- UiPath.System.Activities
- UiPath.Testing.Activities
- UiPath.Workflow
- WindowsBase
- System.ComponentModel.EventBasedAsync
- Microsoft.Win32.Primitives
- System.ComponentModel.Primitives
- System.Private.Xml
- UiPath.System.Activities.Design
- UiPath.System.Activities.ViewModels
- System.Data.SqlClient
- System.Data.Common
- System.IO.FileSystem.Watcher
- System.IO.Packaging
- System.IO.FileSystem.AccessControl
- System.IO.FileSystem.DriveInfo
- UiPath.Excel.Activities.Design


</details>
<details>
    <summary>
    <b>Arguments</b>
    </summary>

    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr></table>
    
</details>

<hr />

## Outline (Beta)

```mermaid
stateDiagram-v2


Sequence_1: WriteTableToExcel
state Sequence_1 {
direction TB
LogMessage_1 : LogMessage - LM -- Start
LogMessage_1 --> TimeoutScope_1
TimeoutScope_1: Timed Test
state TimeoutScope_1 {
direction TB

Sequence_5: Test
state Sequence_5 {
direction TB

Sequence_2: Initialize Test
state Sequence_2 {
direction TB
BuildDataTable_1 : BuildDataTable - Build Table To Write
MultipleAssign_2 : MultipleAssign - Initialize Vars
BuildDataTable_1 --> MultipleAssign_2
}
LogMessage_2 : LogMessage - LM -- Initialization Complete
Sequence_2 --> LogMessage_2
LogMessage_2 --> TryCatch_1
TryCatch_1: Execute Test
state TryCatch_1 {
direction TB

Sequence_3: ... When
state Sequence_3 {
direction TB
InvokeWorkflowFile_1 : InvokeWorkflowFile - .templates\\Reporters\\Basic\\Logic\\WriteTableToExcel.xaml - Invoke Workflow File
}
MultipleAssign_1 : MultipleAssign - Set TestException
Sequence_3 --> MultipleAssign_1
}
LogMessage_3 : LogMessage - LM -- Test Executed
TryCatch_1 --> LogMessage_3
LogMessage_3 --> Sequence_4
Sequence_4: Validate Results
state Sequence_4 {
direction TB
VerifyExpression_5 : VerifyExpression - Verify TextException
VerifyExpression_5 --> ExcelProcessScopeX_1
ExcelProcessScopeX_1: Excel
state ExcelProcessScopeX_1 {
direction TB

ExcelApplicationCard_1: File
state ExcelApplicationCard_1 {
direction TB
ReadRangeX_1 : ReadRangeX - Read Sheet
}
}
DeleteFileX_1 : DeleteFileX - Delete Temp File
ExcelProcessScopeX_1 --> DeleteFileX_1
VerifyExpression_6 : VerifyExpression - Verify Tables
DeleteFileX_1 --> VerifyExpression_6
}
}
}
LogMessage_4 : LogMessage - LM -- Complete
TimeoutScope_1 --> LogMessage_4
}
```