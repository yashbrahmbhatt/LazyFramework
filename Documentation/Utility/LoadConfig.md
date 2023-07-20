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

| Name | Direction | Type | Description |
|  --- | --- | --- | ---  |
| in_ConfigPath | InArgument | x:String | The path to the config file to read. |
| in_IgnoreSheets | InArgument | s:String[] | An array of sheet names to ignore loading into the config variable. |
| out_Config | OutArgument | scg:Dictionary(x:String, x:String) | The loaded config dictionary. |
| out_TextFiles | OutArgument | scg:Dictionary(x:String, x:String) | The loaded dictionary of text resources. |

    
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
}
}

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
Assign_6 : Assign - Set Asset Value
}

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
}

Sequence_14: Sequence - Storage Bucket Resource
state Sequence_14 {
direction TB

RetryScope_6: RetryScope - Retry Orch
state RetryScope_6 {
direction TB
ReadStorageText_2 : ReadStorageText - Get Storage Text
}
}
}
Assign_7 : Assign - Set TextFiles Value
}
}
}
}
}
}
}
}
LogMessage_3 : LogMessage - LM -- Complete
}
```