//using MauiReactor;

using MauiIcons.Fluent;
using MauiIcons.Material;
using MauiIcons.SegoeFluent;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;

namespace EtAlii.Speculo;

public static class MauiProgram
{
//     public static MauiApp CreateMauiApp()
//     {
//         var builder = MauiApp.CreateBuilder();
//         builder
//             .UseMauiReactorApp<MainPage2>()
// //#if DEBUG
//             .EnableMauiReactorHotReload()
// //#endif
//             .ConfigureFonts(fonts =>
//             {
//                 fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
//                 fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
//             });
//
//         return builder.Build();
//     }
    
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMaterialMauiIcons()
            .UseFluentMauiIcons()
            .UseSegoeFluentMauiIcons()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}