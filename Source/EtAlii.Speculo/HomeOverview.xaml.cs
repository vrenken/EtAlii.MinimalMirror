﻿using Microsoft.Maui.Accessibility;

namespace EtAlii.Speculo;

public partial class HomeOverview 
{
    int count = 0;

    public HomeOverview()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

}