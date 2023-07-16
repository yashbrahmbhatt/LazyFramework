# Methodology
This project template was created as a result of having to make the same changes to the REFramework template over and over again across multiple processes. It felt like there had to be a better way to set myself up to develop more robust processes and make development easier on myself at the same time. These are the key fundamental practices/ideas that guided the decision making for how to create this framework.

<details>
  <summary>
    <b>What's Wrong With REFramework</b>
  </summary>
  I think there are a few fundamental flaws with the REFramework, outlined below:

  1. No separation between system exceptions for transactions vs framework components. This creates unnecessary confusing at the framework level, and requires the user to   do the heavy lifting of understanding when the SystemException variable is coming from a framework exception (ie. initialization/get transaction data/set transaction status failure) or a transaction (process.xaml exception). The answer is to just create separate variables for these different scenarios and modify the transitions to make it clearer.
  2. Lack of sending emails for exceptions. When the bot encounters a Business, System, or Framework exception, there is usually some action that a human must take. If its a business exception, the business must take action. If its a system or framework exception, the RPA or IT Ops team must take action. Therefore, it's just something that should be included.
  3. GetTransactionData.xaml is useless. As far as I can tell, it is used retrieve and parse the QueueItem.SpecificContent dictionary and prepare the data for the Process.xaml workflow. Considering the above point, in case the input data is incorrect and the bot fails, the REFramework just ends the process, without notifiying the business that they need to take action on this item. Parsing the input data should be a task within Process.xaml. You can then just have the GetQueueItem activity directly in Main.xaml.
  4. There is no need to support non-orchestrator queues. It is such an edge case, and considering how much bloat/complexity it adds to the framework, it doesn't seem worth including it. Just try refactoring the base REFramework template to only support Orchestrator queues, and you'll see SetTransactionStatus.xaml be simplified extensiely, even completely removing RetryTransaction.xaml (or whatever its called).
  5. I think log messages are fine to hard code within the code, and should not be included in the Config file. I find that the only time I change a log message is when I want to add additional variable information at runtime, not to change the semantics of the message, in which case, I would have to make a code change and publish a new version anyways. The benefit of hard-coding messages is that it declutters the Config file to only the important stuff.
  6. The base InitAllSettings.xaml isn't great. While it doesn't require Excel to be installed, it cannot deal with the config file being 'locked' by another user (ie. ReadOnly Open). It also could have more functionality like reading mapping files or text resources from storage buckets or local paths.

</details>
  
<details>
  <summary>
    <b>Why Can't I Find The Click Activity?</b>
  </summary>

  I wanted to completely remove the UiAutomation package as a direct dependancy of the project template. This signals you to try to isolate all UI logic to libraries, which is the best practice. This will ensure that as you create automations, you will have an ever expanding set of workflows organized by libraries at your disposal to reuse as needed. No more copy pasting. The only portion of the REFramework that uses the UiAutomation package is TakeScreenshot.xaml, which uses it to take a screenshot of the screen during exceptions. This project template works around that by using the core System.Drawing and System.Windows.Forms imports from the System.Activities package.
</details>

<details>
  <summary>
    <b>So Many Entry Points</b>
  </summary>
  Entry points map 'Modules' within your automation design. This project template assumes that you will have multiple entry points within it. Entry Points allow you to create multiple processes from a single package, simplifying deployment, version control/git, and making maintenance easier by being able to share workflows between entry points (and in the future C# CLASSES). The idea is that all your code for a particular automation (end to end) should be within the same package. The one downside to this is that it makes the package larger and memory constraints may have to be taken into account, however, this is mitigated significantly due to the improvements in UiPath's compiler.
</details>

<details>
  <summary>
    <b>Templates, Templates, Templates</b>
  </summary>
    You'll notice that there are no entry points defined within the project when you first open it. This is because this is an all-purpose template and leverages the .template folder of a project to do so. The project template should be able to be able to support all sorts of combinations of modules into a single project template, because you will occassionally have a more complex design than 1 Dispatcher, 1 Performer, 1 Reporter. Currently, within this project, there are templates for the below modules:

  1. Dispatcher
  2. Performer
  3. Reporter
  4. Configs
  5. Templates

  This lets us be able to customize the project depending on the design. Do you need multiple dispatchers because you need to look at different sources of input at different schedules? Just copy the BasicDispatcher.xaml as needed. Do you have multiple units of work for this automation and require multiple queues and performers? Just copy the Performer/Basic folder into your root directory as needed. Do you need a tasker in between different modules of the automation? No problem, just copy the folder as needed. Maybe some DU Extraction stuff? or Classification?

  The idea is to have an modular template that can accommodate a large variety of designs, instead of having to create a completely different project.

  Another amazing benefit is that it uncouples the adoption of a module template from adoption of the project template. Don't like a template that someone created? Cool, just don't use it. This also reduces the barrier for people to contribute to the template as well as adopt other's contributions because it is low-risk.
</details>

<details>
  <summary>
    <b>VB vs C#</b>
  </summary>
  > "Going forward, we do not plan to evolve Visual Basic as a language," the .NET team said. "This supports language stability and maintains compatibility between the .NET Core and .NET Framework versions of Visual Basic. Future features of .NET Core that require language changes may not be supported in Visual Basic. Due to differences in the platform, there will be some differences between Visual Basic on .NET Framework and .NET Core."

\- Microsoft, 2020 ([source](https://visualstudiomagazine.com/articles/2020/03/12/vb-in-net-5.aspx]))

Continuing to code in VB would be just poor planning for the future, and after 1 or 2 processes using C#, you'll realize how much easier and cleaner C# is.
</details>

