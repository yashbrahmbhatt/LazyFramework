# {WorkflowName}
Class: RetryCurrentTransaction

Manage the retrying mechanism for the framework and it is invoked in SetTransactionStatus.xaml when a system exception occurs. 
The retrying method is based on the configurations defined in Config.xlsx.

<hr />

## Workflow Details
<details>
    <summary>
    <b>Namespaces</b>
    </summary>
    - System
- System.Collections.Generic
- System.Data
- System.Linq
- System.Text
- UiPath.Core
- UiPath.Core.Activities
- System.Linq.Expressions
- System.Collections.ObjectModel

</details>
<details>
    <summary>
    <b>References</b>
    </summary>
    - Microsoft.CSharp
- System
- System.Activities
- System.ComponentModel.TypeConverter
- System.Core
- System.Data
- System.Data.Common
- System.Linq
- System.ObjectModel
- System.Private.CoreLib
- System.Runtime.Serialization
- System.ServiceModel
- System.ServiceModel.Activities
- System.ValueTuple
- System.Xaml
- System.Xml
- System.Xml.Linq
- UiPath.Excel
- UiPath.System.Activities
- UiPath.System.Activities.Design

</details>
<details>
    <summary>
    <b>Arguments</b>
    </summary>
    <table><tr><th>Name</th><th>Direction</th><th>Type</th><th>Description</th></tr><tr><td>in_Config</td><td>InArgument</td><td>scg:Dictionary<x:String, x:Object></td><td>Dictionary structure to store configuration data of the process (settings, constants and assets).</td></tr><tr><td>io_RetryNumber</td><td>InOutArgument</td><td>x:Int32</td><td>Used to control the number of attempts of retrying the transaction processing in case of system exceptions.</td></tr><tr><td>io_TransactionNumber</td><td>InOutArgument</td><td>x:Int32</td><td>Sequential counter of transaction items.</td></tr><tr><td>in_SystemException</td><td>InArgument</td><td>s:Exception</td><td>Used during transitions between states to represent exceptions other than business exceptions.</td></tr><tr><td>in_QueueRetry</td><td>InArgument</td><td>x:Boolean</td><td>Used to indicate whether the retry procedure is managed by an Orchestrator queue.</td></tr></table>
</details>

<hr />

## Outline (Beta)

```mermaid
stateDiagram-v2

Flowchart_2: Retry Current Transaction
state Flowchart_2 {
direction TB
FlowDecision_3: Retry transaction?
state FlowDecision_3 {
direction TB
FlowDecision_2: Max retries reached?
state FlowDecision_2 {
direction TB
LogMessage_1 : LogMessage - Log message (Max retries reached)
Assign_1 : Assign - Reset retry counter
LogMessage_1 --> Assign_1
Assign_2 : Assign - Increment TransactionNumber (Local retry)
Assign_1 --> Assign_2
LogMessage_2 : LogMessage - Log message (Retry)
Assign_2 --> LogMessage_2
FlowDecision_1: Use Orchestrator's retry?
state FlowDecision_1 {
direction TB
Assign_3 : Assign - Increment TransactionNumber (Orchestrator retry)
Assign_4 : Assign - Increment retry counter
Assign_3 --> Assign_4
}
}
LogMessage_3 : LogMessage - Log message (No retry)
FlowDecision_2 --> LogMessage_3
Assign_5 : Assign - Increment TransactionNumber (No retry)
LogMessage_3 --> Assign_5
}
}
```