# TakeScreenshot
Class: TakeScreenshot

Capture a screenshot, log its name and location and save it with the PNG extension.
If no specific filepath is passed as argument, it saves the image in the folder specified by in_Folder.

<hr />

## Workflow Details
<details>
    <summary>
    <b>Namespaces</b>
    </summary>
    
- System
- System.Collections.Generic
- System.Data
- System.IO
- System.Linq
- System.Linq.Expressions
- System.Runtime.Serialization
- System.Text
- UiPath.Core
- UiPath.Core.Activities
- System.Collections.ObjectModel


</details>
<details>
    <summary>
    <b>References</b>
    </summary>

- Microsoft.CSharp
- System.Private.CoreLib
- System
- System.Activities
- System.Core
- System.Data
- System.Linq
- System.Data.Common
- System.Private.CoreLib
- System.Runtime.Serialization
- System.ServiceModel
- System.ServiceModel.Activities
- System.ValueTuple
- System.Xaml
- System.Xml
- System.Xml.Linq
- UiPath.CV
- UiPath.Excel
- UiPath.Platform
- UiPath.System.Activities
- UiPath.System.Activities.Design
- UiPath.UiAutomation.Activities
- WindowsBase


</details>
<details>
    <summary>
    <b>Arguments</b>
    </summary>

| Name | Direction | Type | Description |
|  --- | --- | --- | ---  |
| in_Folder | InArgument | x:String | Path to the folder where the screenshot should be saved. |
| io_FilePath | InOutArgument | x:String | Optional argument that specifies the path and the name of the screenshot to be taken. |

    
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


Sequence_2: Sequence - Take and Save Screenshot
state Sequence_2 {
direction TB
TakeScreenshot_1 : TakeScreenshot - Take screenshot
TakeScreenshot_1 --> If_1
If_1: If - If no screenshot filepath
state If_1 {
direction TB
Assign_1 : Assign - Initialize screenshot filepath
}
Assign_2 : Assign - Initialize ScreenshotFileInfo
If_1 --> Assign_2
Assign_2 --> If_2
If_2: If - If screenshot folder does not exist, create it
state If_2 {
direction TB
CreateDirectory_1 : CreateDirectory - Create directory
}
SaveImage_1 : SaveImage - Save screenshot
If_2 --> SaveImage_1
LogMessage_1 : LogMessage - Log message (Take screenshot)
SaveImage_1 --> LogMessage_1
}
```