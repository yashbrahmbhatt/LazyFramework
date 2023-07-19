# FrameworkProcessError
Class: FrameworkProcessError

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
- UiPath.Platform.ResourceHandling
- System.Security
- UiPath.Mail
- UiPath.Mail.IMAP.Activities
- UiPath.Mail.Activities
- UiPath.Core.Activities.Orchestrator


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
- System.Collections.Immutable
- System.ComponentModel
- System.ComponentModel.TypeConverter
- System.Configuration.ConfigurationManager
- System.Console
- System.Core
- System.Data
- System.Drawing
- System.Linq
- System.Linq.Expressions
- System.Linq.Parallel
- System.Linq.Queryable
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


Sequence_1: Sequence - FrameworkProcessError
state Sequence_1 {
direction TB
LogMessage_4 : LogMessage - LM -- Start
LogMessage_4 --> TimeoutScope_1
TimeoutScope_1: TimeoutScope - Timed Test
state TimeoutScope_1 {
direction TB

Sequence_5: Sequence - Test
state Sequence_5 {
direction TB

Sequence_2: Sequence - Initialize Test
state Sequence_2 {
direction TB
MultipleAssign_2 : MultipleAssign - Initialize Vars
InvokeWorkflowFile_1 : InvokeWorkflowFile - Load Config
MultipleAssign_2 --> InvokeWorkflowFile_1
InvokeWorkflowFile_1 --> If_1
If_1: If - Exception Screenshots Exists?
state If_1 {
direction TB
DeleteFolderX_1 : DeleteFolderX - Delete Exception Screenshots
}
CreateDirectory_1 : CreateDirectory - Create Exception Screenshots
If_1 --> CreateDirectory_1
CreateFile_1 : CreateFile - Create Placeholder
CreateDirectory_1 --> CreateFile_1
AddQueueItem_1 : AddQueueItem - Add Item for Testing
CreateFile_1 --> AddQueueItem_1
}
LogMessage_3 : LogMessage - LM -- Initialization Complete
Sequence_2 --> LogMessage_3
LogMessage_3 --> TryCatch_1
TryCatch_1: TryCatch - Execute Test
state TryCatch_1 {
direction TB

Sequence_3: Sequence - ... When
state Sequence_3 {
direction TB
InvokeWorkflowFile_2 : InvokeWorkflowFile - .templates\\Performers\\Basic\\BasicPerformer.xaml - Invoke Workflow File
}
MultipleAssign_1 : MultipleAssign - Set TestException
Sequence_3 --> MultipleAssign_1
}
LogMessage_2 : LogMessage - LM -- Test Executed
TryCatch_1 --> LogMessage_2
LogMessage_2 --> Sequence_4
Sequence_4: Sequence - Validate Results
state Sequence_4 {
direction TB
GetRobotCredential_1 : GetRobotCredential - Get Email Credentials
GetIMAPMailMessages_1 : GetIMAPMailMessages - Get Emails (IMAP)
GetRobotCredential_1 --> GetIMAPMailMessages_1
MultipleAssign_3 : MultipleAssign - Get Exception Screenshot Files
GetIMAPMailMessages_1 --> MultipleAssign_3
MultipleAssign_3 --> ForEach1_1
ForEach1_1: ForEach - Delete Screenshot
state ForEach1_1 {
direction TB
DeleteFileX_1 : DeleteFileX - Delete Screenshot File
}
VerifyExpression_6 : VerifyExpression - Verify TestException
ForEach1_1 --> VerifyExpression_6
VerifyExpression_7 : VerifyExpression - Verify Exception Screenshot
VerifyExpression_6 --> VerifyExpression_7
VerifyExpression_8 : VerifyExpression - Verify EmailCount
VerifyExpression_7 --> VerifyExpression_8
}
}
}
LogMessage_1 : LogMessage - LM -- Complete
TimeoutScope_1 --> LogMessage_1
}
```