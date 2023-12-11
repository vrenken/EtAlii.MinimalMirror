using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace EtAlii.Speculo;

public partial class MainMenu
{
    private static readonly uint MoveAnimationDuration = (uint)TimeSpan.FromSeconds(0.25f).TotalMilliseconds;
    private static readonly uint PressAnimationDuration = (uint)TimeSpan.FromSeconds(0.05f).TotalMilliseconds;
    private static readonly uint PressAnimationOffset = (uint)TimeSpan.FromSeconds(0.02f).TotalMilliseconds;
    private const float ButtonPressedScale = 1.05f;
    private readonly Button[] _buttons;

    public event Action<ContentView>? SwitchContent;
    
    public MainMenu()
    {
        InitializeComponent();

        _buttons = new[]
        {
            HomeButton,
            FamilyButton,
            ClimateButton,
            EnergyButton,
            InsightsButton,
            KnowledgeButton
        };
        MoveOnScreen();
    }
    
    private void MoveOnScreen()
    {
        Dispatcher.Dispatch(() => OpenMenuButton.IsVisible = false);

        Panel.TranslationX = Panel.Width;
        
        foreach (var button in _buttons)
        {
            button.TranslationX = button.Width;
            //button.X += 
            button.Scale = 0f;
        }
        Task.Run(async () =>
        {
            await Panel.TranslateTo(0, 0, MoveAnimationDuration, Easing.CubicIn);

            var tasks = new List<Task>();
            for(var i = 0; i< _buttons.Length; i++)
            {
                var button = _buttons[i];
                var task = button.ScaleTo(1f, MoveAnimationDuration, Easing.CubicIn);
                tasks.Add(task);
                var task2 = Task.Delay(TimeSpan.FromMilliseconds(PressAnimationOffset * i))
                    .ContinueWith(async _ =>
                    {
                        await button.TranslateTo(0, 0, MoveAnimationDuration, Easing.CubicIn);
                    });
                tasks.Add(task2);
            }

            await Task.WhenAll(tasks);
        });
    }

    private void MoveOffScreen()
    {
        Dispatcher.Dispatch(() => OpenMenuButton.IsVisible = true);

        Task.Run(async () =>
        {
            var tasks = new List<Task>();
            for(var i = 0; i< _buttons.Length; i++)
            {
                var button = _buttons[i];
                var task = button.ScaleTo(0f, MoveAnimationDuration, Easing.CubicIn);
                tasks.Add(task);
                var task2 = Task.Delay(TimeSpan.FromMilliseconds(PressAnimationOffset * i))
                    .ContinueWith(async _ =>
                    {
                        await button.TranslateTo(button.Width, 0, MoveAnimationDuration, Easing.CubicIn);
                    });
                tasks.Add(task2);

            }

            await Task.WhenAll(tasks);
            
            await Panel.TranslateTo(Panel.Width, 0, MoveAnimationDuration, Easing.CubicIn);

        });
    }

    private void OnButtonDown(object? sender, EventArgs e)
    {
        var button = (Button)sender!;

        Task.Run(async () =>
        {
            await button.ScaleTo(ButtonPressedScale, PressAnimationDuration, Easing.CubicOut);
        });
    }

    private void OnButtonUp(object? sender, EventArgs e)
    {
        var button = (Button)sender!;
        
        Task.Run(async () =>
        {
            await button.ScaleTo(1f, PressAnimationDuration, Easing.CubicIn);
            
            switch (button)
            {
                case var _ when sender == HomeButton: 
                    SwitchContent?.Invoke(new HomeOverview());
                    break;
                case var _ when sender == FamilyButton: 
                    SwitchContent?.Invoke(new FamilyOverview());
                    break;
                case var _ when sender == ClimateButton: 
                    break;
                case var _ when sender == EnergyButton: 
                    break;
                case var _ when sender == InsightsButton: 
                    break;
                case var _ when sender == KnowledgeButton:
                    MoveOffScreen();
                    break;
            }
        });

    }

    private void OnOpenMenu(object? sender, EventArgs e)
    {
        MoveOnScreen();
    }
}