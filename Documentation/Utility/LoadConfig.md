# LoadConfig
Class: LoadConfig

Reads the config file, ignoring the sheets defined, and outputs the config and textfiles arguments.

<hr />

## Workflow Details
<details>
    <summary>
    <b>Namespaces</b>
    </summary>
    
    - GlobalConstantsNamespace
- GlobalVariablesNamespace
- Microsoft.VisualBasic
- Microsoft.VisualBasic.Activities
- System
- System.Activities
- System.Activities.Expressions
- System.Activities.Statements
- System.Activities.Validation
- System.Activities.XamlIntegration
- System.Collections
- System.Collections.Generic
- System.Collections.ObjectModel
- System.Data
- System.Diagnostics
- System.Linq
- System.Net.Mail
- System.Reflection
- System.Runtime.Serialization
- System.Text
- System.Windows.Markup
- System.Xml
- System.Xml.Linq
- UiPath.Core
- UiPath.Core.Activities
- UiPath.Core.Activities.Orchestrator
- UiPath.Core.Activities.Storage
- UiPath.Excel
- UiPath.Excel.Activities.Business
- UiPath.Excel.Model
- UiPath.Platform.ResourceHandling
- UiPath.Shared.Activities.Business


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
- System.Collections
- System.Collections.Immutable
- System.ComponentModel
- System.ComponentModel.TypeConverter
- System.Configuration.ConfigurationManager
- System.Console
- System.Core
- System.Data
- System.Data.Common
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
- UiPath.Mail.Activities
- UiPath.Platform
- UiPath.Studio.Constants
- UiPath.System.Activities
- UiPath.System.Activities.Design
- UiPath.System.Activities.ViewModels
- UiPath.Testing.Activities
- UiPath.Workflow


</details>
<details>
    <summary>
    <b>Arguments</b>
    </summary>
    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>in_ConfigPath</td><td>InArgument</td><td>x:String</td><td>The path to the config file to read.</td></tr><tr><td>in_IgnoreSheets</td><td>InArgument</td><td>s:String[]</td><td>An array of sheet names to ignore loading into the config variable.</td></tr><tr><td>out_Config</td><td>OutArgument</td><td>scg:Dictionary(x:String, x:String)</td><td>The loaded config dictionary.</td></tr><tr><td>out_TextFiles</td><td>OutArgument</td><td>scg:Dictionary(x:String, x:String)</td><td>The loaded dictionary of text resources.</td></tr></table>
    
</details>
<details>
    <summary>
    <b>Workflows Used</b>
    </summary>

    

    
</details>
<details>
    <summary>
    <b>Tests</b>
    </summary>

    - C:\Users\eyash\Documents\UiPath\LazyFramework\Tests\Utility\LoadConfig\LoadConfigSuccess.xaml

    
</details>

<hr />

## Outline (Beta)

```mermaid
stateDiagram-v2

Sequence_1: Sequence - LoadConfig
state Sequence_1 {
direction TB
LogMessage_4 : LogMessage - LM -- Start
MultipleAssign_1 : MultipleAssign - Initialize Outputs
LogMessage_4 --> MultipleAssign_1
ExcelProcessScopeX_1: ExcelProcessScopeX - Using Excel App
state ExcelProcessScopeX_1 {
direction TB
ExcelApplicationCard_1: ExcelApplicationCard - Using Config File
state ExcelApplicationCard_1 {
direction TB
ForEachSheetX_1: ForEachSheetX - For Each Sheet
state ForEachSheetX_1 {
direction TB
Sequence_2: Sequence - Process Sheet
state Sequence_2 {
direction TB
LogMessage_1 : LogMessage - LM -- Processing sheet
If_1: If - Ignorable Sheet?
state If_1 {
direction TB
Sequence_3: Sequence - Skip
state Sequence_3 {
direction TB
LogMessage_2 : LogMessage - LM -- Skip
Continue_1 : Continue - Skip
LogMessage_2 --> Continue_1
}
Continue_1 --> Sequence_3
}
Sequence_3 --> If_1
ExcelForEachRowX_1: ExcelForEachRowX - For Each Row
state ExcelForEachRowX_1 {
direction TB
If_3: If - Not Empty Row?
state If_3 {
direction TB
Switch1_3: Switch - Sheet Name?
state Switch1_3 {
direction TB
Assign_5 : Assign - Set Default Value
Sequence_11: Sequence - Process Assets Row
state Sequence_11 {
direction TB
RetryScope_4: RetryScope - Asset Retry
state RetryScope_4 {
direction TB
GetRobotAsset_2 : GetRobotAsset - Get Current Asset
}
GetRobotAsset_2 --> RetryScope_4
Assign_6 : Assign - Set Asset Value
RetryScope_4 --> Assign_6
}
Assign_6 --> Sequence_11
Sequence_12: Sequence - Process TextFiles Row
state Sequence_12 {
direction TB
If_4: If - NOT Storage Bucket Resource?
state If_4 {
direction TB
Sequence_13: Sequence - Local/Network Resource
state Sequence_13 {
direction TB
RetryScope_5: RetryScope - Retry Network/Local
state RetryScope_5 {
direction TB
ReadTextFile_2 : ReadTextFile - Read Local File
}
ReadTextFile_2 --> RetryScope_5
}
RetryScope_5 --> Sequence_13
Sequence_14: Sequence - Storage Bucket Resource
state Sequence_14 {
direction TB
RetryScope_6: RetryScope - Retry Orch
state RetryScope_6 {
direction TB
ReadStorageText_2 : ReadStorageText - Get Storage Text
}
ReadStorageText_2 --> RetryScope_6
}
RetryScope_6 --> Sequence_14
}
Sequence_14 --> If_4
Assign_7 : Assign - Set TextFiles Value
If_4 --> Assign_7
}
Assign_7 --> Sequence_12
}
Sequence_12 --> Switch1_3
}
Switch1_3 --> If_3
}
If_3 --> ExcelForEachRowX_1
}
ExcelForEachRowX_1 --> Sequence_2
}
Sequence_2 --> ForEachSheetX_1
}
ForEachSheetX_1 --> ExcelApplicationCard_1
}
ExcelApplicationCard_1 --> ExcelProcessScopeX_1
LogMessage_3 : LogMessage - LM -- Complete
ExcelProcessScopeX_1 --> LogMessage_3
}
LogMessage_3 --> Sequence_1
```