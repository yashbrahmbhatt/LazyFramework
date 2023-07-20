# GetQueueItemsSuccess
Class: GetQueueItemsSuccess

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
- Newtonsoft.Json.Linq
- Newtonsoft.Json
- System.Dynamic
- Newtonsoft.Json.Linq
- Newtonsoft.Json
- System.Dynamic


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
- UiPath.System.Activities.Design
- UiPath.System.Activities.ViewModels
- System.Collections
- Newtonsoft.Json
- netstandard


</details>
<details>
    <summary>
    <b>Arguments</b>
    </summary>

| Name | Direction | Type | Description |
|  --- | --- | --- | ---  |

    
</details>
<details>
    <summary>
    <b>Workflows Used</b>
    </summary>

- C:\Users\eyash\Documents\UiPath\LazyFramework\Utility\LoadConfig.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\.templates\Reporters\Basic\Orchestrator\GetQueueDefinitionId.xaml
- C:\Users\eyash\Documents\UiPath\LazyFramework\.templates\Reporters\Basic\Orchestrator\GetQueueItems.xaml

    
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


Sequence_1: Sequence - GetQueueItemsSuccess
state Sequence_1 {
direction TB
LogMessage_1 : LogMessage - LM -- Start

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
InvokeWorkflowFile_1 : InvokeWorkflowFile - Utility\\LoadConfig.xaml - Invoke Workflow File
InvokeWorkflowFile_3 : InvokeWorkflowFile - .templates\\Reporters\\Basic\\Orchestrator\\GetQueueDefinitionId.xaml - Invoke Workflow File
}
LogMessage_2 : LogMessage - LM -- Initialization Complete

TryCatch_1: TryCatch - Execute Test
state TryCatch_1 {
direction TB

Sequence_3: Sequence - ... When
state Sequence_3 {
direction TB
InvokeWorkflowFile_2 : InvokeWorkflowFile - .templates\\Reporters\\Basic\\Orchestrator\\GetQueueItems.xaml - Invoke Workflow File
}
MultipleAssign_1 : MultipleAssign - Set TestException
}
LogMessage_3 : LogMessage - LM -- Test Executed

Sequence_4: Sequence - Validate Results
state Sequence_4 {
direction TB
VerifyExpression_5 : VerifyExpression - Verify TextException
VerifyExpression_6 : VerifyExpression - Verify Items Read
}
}
}
LogMessage_4 : LogMessage - LM -- Complete
}
```