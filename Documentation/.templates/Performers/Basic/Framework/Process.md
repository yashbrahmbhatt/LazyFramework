# {WorkflowName}
Class: Process

Carry out the required steps for this transaction/unit of work.

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

</details>
<details>
    <summary>
    <b>References</b>
    </summary>
    - Microsoft.CSharp
- Microsoft.VisualBasic
- System
- System.Activities
- System.Collections
- System.ComponentModel.TypeConverter
- System.Core
- System.Data
- System.Data.Common
- System.Linq
- System.ObjectModel
- System.Private.CoreLib
- System.Private.DataContractSerialization
- System.Private.ServiceModel
- System.Reflection.DispatchProxy
- System.Reflection.Metadata
- System.Reflection.TypeExtensions
- System.Runtime.Serialization
- System.Runtime.Serialization.Formatters
- System.Runtime.Serialization.Primitives
- System.ServiceModel
- System.ServiceModel.Activities
- System.Xaml
- System.Xml
- System.Xml.Linq
- UiPath.Studio.Constants
- UiPath.System.Activities
- UiPath.Workflow

</details>
<details>
    <summary>
    <b>Arguments</b>
    </summary>
    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>io_Data</td><td>InOutArgument</td><td>scg:Dictionary<x:String, x:Object></td><td>The transaction data to be used to perform the process.</td></tr><tr><td>in_Reference</td><td>InArgument</td><td>x:String</td><td>The reference of the queue item being processed.</td></tr></table>
</details>

<hr />

## Outline (Beta)

```mermaid
stateDiagram-v2

Sequence_1: Process
state Sequence_1 {
direction TB
LogMessage_1 : LogMessage - LM -- Start
}
```