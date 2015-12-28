using Caliburn.Micro;

namespace LogoFX.Samples.Client.Presentation.Shell.Compliance.ViewModels
{
    public sealed class ConsoleToolbarViewModel : Conductor<ToolbarItemViewModel>.Collection.AllActive
    {
         
    }

    public abstract class ToolbarItemViewModel : Screen
    {
        
    }

    public sealed class ButtonToolbarItemViewModel : ToolbarItemViewModel
    {
        
    }

    public sealed class MenuButtonToolbarItemViewModel : ToolbarItemViewModel
    {

    }
}