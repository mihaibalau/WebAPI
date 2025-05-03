using System;
using Microsoft.UI.Xaml.Controls;
using WinUI.View;

public static class NavigationService
{
    public static Frame MainFrame { get; set; }
    
    public static void NavigateToLogin()
    {
        if (MainFrame != null)
        {
            MainFrame.Navigate(typeof(LogInView), null);
        }
    }

    public static void Navigate(Type pageType, object parameter = null)
    {
        if (MainFrame != null)
        {
            MainFrame.Navigate(pageType, parameter);
        }
    }
}
