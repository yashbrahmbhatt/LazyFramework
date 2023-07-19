# GenerateDiagnosticDictionary
Class: GenerateDiagnosticDictionary

Generates a dictinoary that contains diagnostic information in cases of system exceptions.

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
- System.Runtime.Serialization


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
- System.ComponentModel
- System.ComponentModel.TypeConverter
- System.Configuration.ConfigurationManager
- System.Console
- System.Core
- System.Data
- System.Data.Common
- System.Linq
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
- UiPath.Studio.Constants
- UiPath.System.Activities
- UiPath.System.Activities.Design
- UiPath.System.Activities.ViewModels
- UiPath.Workflow


</details>
<details>
    <summary>
    <b>Arguments</b>
    </summary>

    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>in_Exception</td><td>InArgument</td><td>s:Exception</td><td>The exception to use to generate diagnostics.</td></tr><tr><td>out_TemplateData</td><td>OutArgument</td><td>scg:Dictionary<x:String, x:Object></td><td>A dictionary with the diagnostic data filled in.</td></tr></table>
    
</details>

<hr />

## Outline (Beta)

```mermaid
stateDiagram-v2

 --> Sequence_1
Sequence_1: GenerateDiagnosticDictionary
state Sequence_1 {
direction TB
LogMessage_1 : LogMessage - LM -- Start
MultipleAssign_1 : MultipleAssign - Setup Dictionary
LogMessage_1 --> MultipleAssign_1
}
```