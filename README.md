# Methodology
This project template was created as a result of having to make the same changes to the REFramework template over and over again across multiple processes. It felt like there had to be a better way to set myself up to develop more robust processes and make development easier on myself at the same time. These are the key fundamental practices/ideas that guided the decision making for how to create this framework.

### Always Use Orchestrator Queues
The REFramework supports cases where Orchestrator queues are not utilized. While this is useful in an edge-case scenario where your requirements are such, the vast majority of clients and companies do not have this requirement and will be using Orchestrator. Our project template should not be tailored to edge cases as that increases complexity for very little pay-off.

### No More UiAutomation Package
I wanted to completely remove the UiAutomation package as a direct dependancy of the project template. This 'enforces' you to have to isolate all UI logic to libraries, which is the best practice. This will ensure that as you create automations, you will have an ever expanding set of workflows organized by libraries at your disposal to reuse as needed. No more copy pasting. The only portion of the REFramework that uses the UiAutomation package is TakeScreenshot.xaml, which uses it to take a screenshot of the screen during exceptions. This project template works around that by using the core System.Drawing and System.Windows.Forms imports from the System.Activities package.

### Entry Points
This project template assumes that you will have multiple entry points within it. Entry Points allow you to create multiple processes from a single package, simplifying deployment, and making maintenance easier by being able to share workflows between entry points. The idea is that all your code for a particular automation (end to end) should be within the same package. What's the point of deploying the Dispatcher, if you don't also deploy the Performer? The one downside to this is that it makes the package larger, however, this is mitigated significantly due to the recent improvements in UiPath's compiler. If you do notice that you are seeing performance downsides, you can just create multiple projects of this template.

### Templates, Templates, Templates
You'll notice that there are no XAMLs in the root directory. This is because this is an all-purpose template; I wanted to be able to support all sorts of combinations of modules into a single project template. To do this, we leverage the Templates functionality within Studio (.templates folder). Currently, within this folder, there are templates for different modules including:
1. Dispatcher
2. Performer
3. Reporter
4. Configs
5. Templates
This lets us be able to customize the project depending on the requirements. Do you need multiple dispatchers because you need to look at multiple sources of input at different schedules? Just copy the BasicDispatcher.xaml as needed. Do you have multiple units of work for this automation and require multiple performers? Just copy the Performer/Basic folder into your root directory as needed. Do you need a tasker in between different modules of the automation? No problem, just copy the folder as needed.

The idea is to have an modular template that can accommodate a large variety of designs, instead of having to create a completely different project.

