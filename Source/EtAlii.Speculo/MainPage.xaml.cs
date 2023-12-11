using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;

namespace EtAlii.Speculo;

public partial class MainPage
{
    private static readonly uint MoveAnimationDuration = (uint)TimeSpan.FromSeconds(0.25f).TotalMilliseconds;

    private ContentView? _currentContent;
    
    public MainPage()
    {
        InitializeComponent();

        SwitchContent(new HomeOverview());
        
        MainMenu.SwitchContent += SwitchContent;
    }

    private void SwitchContent(ContentView newContent)
    {
        if (newContent.GetType() == _currentContent?.GetType())
        {
            return;
        }
        
        Task.Run(() =>
        {
            var oldContent = _currentContent;
           
            var direction = ContentViewer.Height;

            if (oldContent != null)
            {
                Task.WhenAll(
                    oldContent.FadeTo(0f, MoveAnimationDuration, Easing.CubicIn),
                    oldContent.TranslateTo(0, direction, MoveAnimationDuration, Easing.CubicIn));
            }
            Dispatcher.DispatchAsync(async () =>
            {
                _currentContent = newContent;

                ContentViewer.Add(newContent);
                newContent.CancelAnimations();
                
                newContent.Opacity = 0f;
                newContent.TranslationY = -direction;

                await Task.WhenAll(
                    newContent.FadeTo(1f, MoveAnimationDuration, Easing.CubicIn),
                    newContent.TranslateTo(0, 0, MoveAnimationDuration, Easing.CubicIn)
                );

                if (oldContent != null)
                {
                    ContentViewer.Remove(oldContent);
                }
            });
        });
    }
}