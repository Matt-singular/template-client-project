#pragma warning disable
namespace Application.GUI;

using Foundation;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
  protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
#pragma warning restore