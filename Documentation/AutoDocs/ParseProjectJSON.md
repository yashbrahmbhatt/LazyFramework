# ParseProjectJSON
Class: ParseProjectJSON



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
- Newtonsoft.Json
- Newtonsoft.Json.Linq
- System.Dynamic
- System.ComponentModel
- System.Collections.Specialized
- System.Runtime.Serialization
- System.Linq.Expressions
- System.Xml.Serialization


</details>
<details>
    <summary>
    <b>References</b>
    </summary>

    - Microsoft.CSharp
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
- System.Collections.NonGeneric
- System.Collections.Specialized
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
- UiPath.Studio.Constants
- UiPath.System.Activities
- UiPath.Workflow
- WindowsBase


</details>
<details>
    <summary>
    <b>Arguments</b>
    </summary>

    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>in_ProjectJSONPath</td><td>InArgument</td><td>x:String</td><td></td></tr><tr><td>out_Name</td><td>OutArgument</td><td>x:String</td><td></td></tr><tr><td>out_Description</td><td>OutArgument</td><td>x:String</td><td></td></tr><tr><td>out_Dependencies</td><td>OutArgument</td><td>sd:DataTable</td><td></td></tr><tr><td>out_FileInfoCollection</td><td>OutArgument</td><td>sd:DataTable</td><td></td></tr><tr><td>out_EntryPoints</td><td>OutArgument</td><td>scg:IEnumerable(x:String)</td><td></td></tr><tr><td>out_Language</td><td>OutArgument</td><td>x:String</td><td></td></tr><tr><td>out_ProjectVersion</td><td>OutArgument</td><td>x:String</td><td></td></tr><tr><td>out_StudioVersion</td><td>OutArgument</td><td>x:String</td><td></td></tr><tr><td>out_Type</td><td>OutArgument</td><td>x:String</td><td></td></tr></table>
    
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

    

    
</details>

<hr />

## Outline (Beta)

```mermaid
stateDiagram-v2

Sequence_1: Sequence - ParseProjectJSON
state Sequence_1 {
direction TB
BuildDataTable_2 : BuildDataTable - Build Dependencies Table
MultipleAssign_1 : MultipleAssign - Parse File
BuildDataTable_2 --> MultipleAssign_1
ForEach1_1: ForEach - For Each DependencyArray
state ForEach1_1 {
direction TB
AddDataRow_2 : AddDataRow - Add Row
}
AddDataRow_2 --> ForEach1_1
WriteLine_1 : WriteLine - Write Line
ForEach1_1 --> WriteLine_1
}
WriteLine_1 --> Sequence_1
```