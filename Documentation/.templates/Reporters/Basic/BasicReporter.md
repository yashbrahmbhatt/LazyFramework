# BasicReporter
Class: BasicReporter

Reads all queue items for a single queue within a defined reporting period, writes them to an excel, and then sends an email with the report as an attachment.

<hr />

## Workflow Details
<details>
    <summary>
    <b>Namespaces</b>
    </summary>
    
    - System
- System.Activities
- System.Activities.Statements
- System.Collections
- System.Collections.Generic
- System.Collections.ObjectModel
- System.IO
- System.Linq
- UiPath.Core.Activities
- System.Reflection
- System.Runtime.Serialization
- System.Activities.Runtime.Collections
- Cronos
- System.Linq.Expressions
- UiPath.Core
- GlobalVariablesNamespace
- GlobalConstantsNamespace
- System.Diagnostics
- System.ComponentModel
- UiPath.Platform.ResourceHandling
- System.Data
- Newtonsoft.Json.Linq
- Newtonsoft.Json
- System.Dynamic
- Newtonsoft.Json.Linq
- Newtonsoft.Json
- System.Dynamic
- System.Xml.Serialization
- System.Xml.Serialization
- UiPath.Excel
- UiPath.Excel.Activities.Business


</details>
<details>
    <summary>
    <b>References</b>
    </summary>

    - Cronos
- Microsoft.CSharp
- Microsoft.PowerShell.Commands.Diagnostics
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
- System.Diagnostics.DiagnosticSource
- System.Diagnostics.EventLog
- System.Diagnostics.FileVersionInfo
- System.Diagnostics.PerformanceCounter
- System.Diagnostics.Process
- System.Diagnostics.TextWriterTraceListener
- System.Diagnostics.TraceSource
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
- UiPath.Excel
- UiPath.Excel.Activities
- UiPath.Excel.Activities.Design
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
    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>in_OutputFolder</td><td>InArgument</td><td>x:String</td><td>If defined, the process will also copy the report generated to this folder.</td></tr><tr><td>in_CRON</td><td>InArgument</td><td>x:String</td><td>A CRON expression to determine what the reporting period is. This should match the CRON expression used in the trigger for this Entry Point for most cases.</td></tr><tr><td>in_From</td><td>InArgument</td><td>s:DateTime</td><td>If defined, overrides the start of the reporting period to this value. in_To must also be defined if using this overload.</td></tr><tr><td>in_To</td><td>InArgument</td><td>s:DateTime</td><td>If defined, overrides the end of the reporting period to this value. in_From must also be defined if using this overload.</td></tr><tr><td>in_ConfigPath</td><td>InArgument</td><td>x:String</td><td>The path to the config file to load for this process.</td></tr><tr><td>in_IgnoreSheets</td><td>InArgument</td><td>s:String[]</td><td>A list of the sheets to ignore loading into the Config.</td></tr></table>
    
</details>
<details>
    <summary>
    <b>Workflows Used</b>
    </summary>

    - C:\Users\eyash\Documents\UiPath\LazyFramework\Utility\LoadConfig.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\.templates\Reporters\Basic\Logic\GetQueueData.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\.templates\Reporters\Basic\Logic\AddCalculatedColumns.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\.templates\Reporters\Basic\Logic\WriteTableToExcel.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\Utility\SendEmail.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\Utility\TakeScreenshot.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\Utility\GenerateDiagnosticDictionary.xaml

    
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

Sequence_1: Sequence - BasicReporter
state Sequence_1 {
direction TB
LogMessage_1 : LogMessage - LM -- Start
InvokeWorkflowFile_1 : InvokeWorkflowFile - Utility\\LoadConfig.xaml - Invoke Workflow File
LogMessage_1 --> InvokeWorkflowFile_1
TryCatch_2: TryCatch - Try Reporting
state TryCatch_2 {
direction TB
Sequence_12: Sequence - Reporting
state Sequence_12 {
direction TB
Sequence_2: Sequence - Initialize
state Sequence_2 {
direction TB
If_1: If - Validate Time Frame
state If_1 {
direction TB
TryCatch_1: TryCatch - Try Parsing CRON
state TryCatch_1 {
direction TB
Sequence_5: Sequence - Parsing CRON
state Sequence_5 {
direction TB
MultipleAssign_2 : MultipleAssign - Parse CRON
LogMessage_2 : LogMessage - LM -- CRON
MultipleAssign_2 --> LogMessage_2
}
LogMessage_2 --> Sequence_5
Throw_1 : Throw - Throw InvalidCRON
Sequence_5 --> Throw_1
}
Throw_1 --> TryCatch_1
If_2: If - From/To Defined?
state If_2 {
direction TB
LogMessage_4 : LogMessage - LM -- Argument Override
Throw_2 : Throw - Throw NoOverload Available
LogMessage_4 --> Throw_2
}
Throw_2 --> If_2
}
If_2 --> If_1
MultipleAssign_1 : MultipleAssign - Set Paths
If_1 --> MultipleAssign_1
KillProcess_1 : KillProcess - Kill Excel
MultipleAssign_1 --> KillProcess_1
If_3: If - Cleanup Temp Folder
state If_3 {
direction TB
Delete_1 : Delete - Delete Temp Folder
}
Delete_1 --> If_3
CreateDirectory_1 : CreateDirectory - Create Temp Folder
If_3 --> CreateDirectory_1
LogMessage_5 : LogMessage - LM -- Initialization Complete
CreateDirectory_1 --> LogMessage_5
}
LogMessage_5 --> Sequence_2
Sequence_9: Sequence - Create Report
state Sequence_9 {
direction TB
LogMessage_6 : LogMessage - LM -- Create Report
CopyFile_1 : CopyFile - Copy Template To Temp
LogMessage_6 --> CopyFile_1
InvokeWorkflowFile_2 : InvokeWorkflowFile - Get Queue Data
CopyFile_1 --> InvokeWorkflowFile_2
InvokeWorkflowFile_4 : InvokeWorkflowFile - Add Calculated Columns
InvokeWorkflowFile_2 --> InvokeWorkflowFile_4
InvokeWorkflowFile_3 : InvokeWorkflowFile - Write Table to Excel
InvokeWorkflowFile_4 --> InvokeWorkflowFile_3
If_4: If - Output Path Not Empty?
state If_4 {
direction TB
CopyFile_2 : CopyFile - Copy to Output
LogMessage_9 : LogMessage - LM -- No Output
CopyFile_2 --> LogMessage_9
}
LogMessage_9 --> If_4
LogMessage_7 : LogMessage - LM -- Report Generated
If_4 --> LogMessage_7
}
LogMessage_7 --> Sequence_9
Sequence_10: Sequence - Send Email
state Sequence_10 {
direction TB
LogMessage_8 : LogMessage - LM -- Emailing
FilterDataTable_1 : FilterDataTable - Filter Columns to Summary
LogMessage_8 --> FilterDataTable_1
MultipleAssign_3 : MultipleAssign - Set Email Data
FilterDataTable_1 --> MultipleAssign_3
InvokeWorkflowFile_5 : InvokeWorkflowFile - Send Report Email
MultipleAssign_3 --> InvokeWorkflowFile_5
LogMessage_10 : LogMessage - LM -- Report Sent
InvokeWorkflowFile_5 --> LogMessage_10
}
LogMessage_10 --> Sequence_10
}
Sequence_10 --> Sequence_12
Sequence_13: Sequence - Send Exception Email
state Sequence_13 {
direction TB
InvokeWorkflowFile_7 : InvokeWorkflowFile - Utility\\TakeScreenshot.xaml - Invoke Workflow File
InvokeWorkflowFile_8 : InvokeWorkflowFile - GenerateDiagnosticDictionary.xaml - Invoke Workflow File
InvokeWorkflowFile_7 --> InvokeWorkflowFile_8
MultipleAssign_4 : MultipleAssign - Add Keys to Template Data
InvokeWorkflowFile_8 --> MultipleAssign_4
InvokeWorkflowFile_9 : InvokeWorkflowFile - SendEmail.xaml - Invoke Workflow File
MultipleAssign_4 --> InvokeWorkflowFile_9
}
InvokeWorkflowFile_9 --> Sequence_13
}
Sequence_13 --> TryCatch_2
}
TryCatch_2 --> Sequence_1
```