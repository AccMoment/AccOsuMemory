using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace AccOsuMemory.Mobile;

public partial class MainPage : ContentPage
{
    private string[] files = new[] { "2024061.jpg", "2024321.jpg", "2024427.jpg" };
    private int i = 0;

    public MainPage()
    {
        InitializeComponent();
        Image.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync(files[i++]).Result);
    }


    private void Button_OnClicked(object sender, EventArgs e)
    {
        Image.Source =
            ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync(files[(i++) % files.Length]).Result);
    }
}