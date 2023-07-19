# AddCalculatedColumns
Class: AddCalculatedColumns

Adds calculated columns for all queue items.

Current Fields:
- Time Saved
- Execution Time

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
- System.ComponentModel
- System.Runtime.Serialization
- System.Xml.Serialization
- UiPath.DataTableUtilities
- System.IO


</details>
<details>
    <summary>
    <b>References</b>
    </summary>

    - Microsoft.CSharp
- Microsoft.VisualBasic
- Microsoft.VisualBasic.Core
- Microsoft.VisualBasic.Forms
- Microsoft.Win32.Primitives
- NPOI
- PresentationFramework
- System
- System.Activities
- System.CodeDom
- System.Collections
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
- UiPath.Studio.Constants
- UiPath.System.Activities
- UiPath.Workflow
- WindowsBase


</details>
<details>
    <summary>
    <b>Arguments</b>
    </summary>

    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>in_SuccessTimeSaved</td><td>InArgument</td><td>x:Double</td><td>Time saved in minutes for successful queue items.</td></tr><tr><td>in_BusExTimeSaved</td><td>InArgument</td><td>x:Double</td><td>Time saved in minutes for business exception queue items.</td></tr><tr><td>in_SysExTimeSaved</td><td>InArgument</td><td>x:Double</td><td>Time saved in minutes for application exception queue items.</td></tr><tr><td>io_dt_Table</td><td>InOutArgument</td><td>sd:DataTable</td><td>The table to add the calculated columns to.</td></tr></table>
    
</details>

<hr />

## Outline (Beta)

```mermaid
stateDiagram-v2

 --> Sequence_1
Sequence_1: AddCalculatedColumns
state Sequence_1 {
direction TB
LogMessage_1 : LogMessage - LM -- Start
AddDataColumn`1_2 : AddDataColumn - Add Time Saved
LogMessage_1 --> AddDataColumn`1_2
AddDataColumn`1_3 : AddDataColumn - Add Execution Time
AddDataColumn`1_2 --> AddDataColumn`1_3
AddDataColumn`1_3 --> ForEachRow_1
ForEachRow_1: For Each Row
state ForEachRow_1 {
direction TB
 --> Sequence_6
Sequence_6: Update Rows
state Sequence_6 {
direction TB
 --> If_3
If_3: Item not completed?
state If_3 {
direction TB
Continue_1 : Continue - Skip Row
}
MultipleAssign_4 : MultipleAssign - Update Execution Time
If_3 --> MultipleAssign_4
MultipleAssign_4 --> If_1
If_1: Failed?
state If_1 {
direction TB
 --> If_2
If_2: System Or Business?
state If_2 {
direction TB
MultipleAssign_1 : MultipleAssign - Set System Exception Time Saved
MultipleAssign_2 : MultipleAssign - Set Business Exception Time Saved
MultipleAssign_1 --> MultipleAssign_2
}
MultipleAssign_3 : MultipleAssign - Set Success Time Saved
If_2 --> MultipleAssign_3
}
}
}
LogMessage_2 : LogMessage - LM -- Complete
ForEachRow_1 --> LogMessage_2
}
```