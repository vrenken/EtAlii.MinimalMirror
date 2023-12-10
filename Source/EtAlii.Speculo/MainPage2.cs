using MauiReactor;

namespace EtAlii.Speculo;

class MainPage2 : Component
{
    public override VisualNode Render()
    {
        return new ContentPage
        {
            new VerticalStackLayout()
            {
                new Label("Hi!")
                    .HCenter()
                    .VCenter(),
                new Label("Hi!")
                    .HCenter()
                    .VCenter()
            }
        };
    }
}