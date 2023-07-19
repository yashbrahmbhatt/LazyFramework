# BasicDispatcherFailureTest
Class: BasicDispatcherFailureTest

A basic template for a test with the expected outcome being failure.

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
- UiPath.Core.Activities.Orchestrator
- System.Security
- UiPath.Mail
- UiPath.Mail.IMAP.Activities
- UiPath.Mail.Activities
- System.Activities.Runtime.Collections
- UiPath.Platform.ResourceHandling

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
- System.Collections
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
- System.Net.Mail
- System.ObjectModel
- System.Private.CoreLib
- System.Private.DataContractSerialization
- System.Private.ServiceModel
- System.Private.Uri
- System.Reflection.DispatchProxy
- System.Reflection.Metadata
- System.Reflection.TypeExtensions
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
- UiPath.Excel
- UiPath.Excel.Activities
- UiPath.Mail
- UiPath.Mail.Activities
- UiPath.Mail.Activities.Design
- UiPath.Platform
- UiPath.Studio.Constants
- UiPath.System.Activities
- UiPath.System.Activities.Design
- UiPath.System.Activities.ViewModels
- UiPath.Testing.Activities
- UiPath.Workflow
- WindowsBase

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

Sequence_1: BasicDispatcherFailureTest
state Sequence_1 {
direction TB
LogMessage_1 : LogMessage - LM -- Start
TimeoutScope_2: Timed Test
state TimeoutScope_2 {
direction TB
Sequence_13: Test
state Sequence_13 {
direction TB
Sequence_11: Initialize Test
state Sequence_11 {
direction TB
MultipleAssign_2 : MultipleAssign - Initialize Variables
InvokeWorkflowFile_1 : InvokeWorkflowFile - Load Test Config
MultipleAssign_2 --> InvokeWorkflowFile_1
If_1: Exception Screenshots Exists?
state If_1 {
direction TB
DeleteFolderX_1 : DeleteFolderX - Delete Exception Screenshots
}
CreateDirectory_1 : CreateDirectory - Create Exception Screenshots
If_1 --> CreateDirectory_1
CreateFile_1 : CreateFile - Create Placeholder
CreateDirectory_1 --> CreateFile_1
}
LogMessage_8 : LogMessage - LM -- Initialization Complete
Sequence_11 --> LogMessage_8
Sequence_12: Execute Test
state Sequence_12 {
direction TB
TryCatch_1: Execute
state TryCatch_1 {
direction TB
InvokeWorkflowFile_3 : InvokeWorkflowFile - Run BasicDispatcher
MultipleAssign_1 : MultipleAssign - Set TestException
InvokeWorkflowFile_3 --> MultipleAssign_1
}
}
LogMessage_9 : LogMessage - LM -- Test Executed
Sequence_12 --> LogMessage_9
Sequence_8: Validate Results
state Sequence_8 {
direction TB
GetRobotCredential_1 : GetRobotCredential - Get Email Credentials
GetIMAPMailMessages_2 : GetIMAPMailMessages - Get Emails (IMAP)
GetRobotCredential_1 --> GetIMAPMailMessages_2
MultipleAssign_3 : MultipleAssign - Get Exception Screenshot Files
GetIMAPMailMessages_2 --> MultipleAssign_3
ForEach`1_1: Delete Screenshot
state ForEach`1_1 {
direction TB
DeleteFileX_1 : DeleteFileX - Delete Screenshot File
}
VerifyExpression_8 : VerifyExpression - Verify Exception Screenshot
ForEach`1_1 --> VerifyExpression_8
VerifyExpression_7 : VerifyExpression - Verify TestException
VerifyExpression_8 --> VerifyExpression_7
VerifyExpression_5 : VerifyExpression - Verify EmailCount
VerifyExpression_7 --> VerifyExpression_5
}
}
}
LogMessage_5 : LogMessage - LM -- Complete
TimeoutScope_2 --> LogMessage_5
}
```