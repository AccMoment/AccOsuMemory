using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AccOsuMemory.Desktop.Views.Component;

public partial class MenuButton : UserControl
{

    private string _btnContent;

    public static readonly DirectProperty<MenuButton, string> BtnContentProperty = AvaloniaProperty.RegisterDirect<MenuButton, string>(
        "btnContent", o => o.BtnContent, (o, v) => o.BtnContent = v);

   
    
    
    private bool _isSelect;

    public static readonly DirectProperty<MenuButton, bool> IsSelectProperty = AvaloniaProperty.RegisterDirect<MenuButton, bool>(
        "IsSelect", o => o.IsSelect, (o, v) => o.IsSelect = v);

    private string _iconValue;

    public static readonly DirectProperty<MenuButton, string> IconValueProperty = AvaloniaProperty.RegisterDirect<MenuButton, string>(
        "IconValue", o => o.IconValue, (o, v) => o.IconValue = v);
   
    public static readonly RoutedEvent<RoutedEventArgs> ClickEvent =
        RoutedEvent.Register<MenuButton, RoutedEventArgs>(nameof(Click), RoutingStrategies.Bubble);

    // Provide CLR accessors for the event
    public event EventHandler<RoutedEventArgs> Click
    { 
        add => AddHandler(ClickEvent, value);
        remove => RemoveHandler(ClickEvent, value);
    }
   
    public string IconValue
    {
        get => _iconValue;
        set => SetAndRaise(IconValueProperty, ref _iconValue, value);
    }
   
   
    public bool IsSelect
    {
        get => _isSelect;
        set => SetAndRaise(IsSelectProperty, ref _isSelect, value);
    }
    
    public string BtnContent
    {
        get => _btnContent;
        set => SetAndRaise(BtnContentProperty, ref _btnContent, value);
    }

    public MenuButton()
    {
        InitializeComponent();
    }
    
    private void MenuBtn_OnClick(object sender, RoutedEventArgs e)
    {
        var arg = new RoutedEventArgs(ClickEvent);
        RaiseEvent(arg);
    }
}