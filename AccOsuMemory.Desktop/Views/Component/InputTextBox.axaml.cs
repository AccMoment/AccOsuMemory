using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AccOsuMemory.Desktop.Views.Component;

public partial class InputTextBox : UserControl
{
    public static readonly StyledProperty<bool> IsErrorProperty = AvaloniaProperty.Register<InputTextBox, bool>(
        "IsError",defaultBindingMode: BindingMode.TwoWay);

    public bool IsError
    {
        get => GetValue(IsErrorProperty);
        set => SetValue(IsErrorProperty, value);
    }


    private string? _searchText;

    public static readonly DirectProperty<InputTextBox, string?> SearchTextProperty = AvaloniaProperty.RegisterDirect<InputTextBox, string?>(
        "SearchText", o => o.SearchText, (o, v) => o.SearchText = v,defaultBindingMode: BindingMode.TwoWay);

    public string? SearchText
    {
        get => _searchText;
        set => SetAndRaise(SearchTextProperty, ref _searchText, value);
    }

    private string? _errorText;

    public static readonly DirectProperty<InputTextBox, string?> ErrorTextProperty = AvaloniaProperty.RegisterDirect<InputTextBox, string?>(
        "ErrorText", o => o.ErrorText, (o, v) => o.ErrorText = v);

    public string? ErrorText
    {
        get => _errorText;
        set => SetAndRaise(ErrorTextProperty, ref _errorText, value);
    }
    
    public static readonly RoutedEvent<RoutedEventArgs> SearchEvent =
        RoutedEvent.Register<Button, RoutedEventArgs>(nameof(Search), RoutingStrategies.Direct);

    public event EventHandler<RoutedEventArgs>? Search
    {
        add => AddHandler(SearchEvent, value);
        remove => RemoveHandler(SearchEvent, value);
    }
    
    public InputTextBox()
    {
        InitializeComponent();
    }
    

    private void SearchTextBox_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        RaiseEvent(new RoutedEventArgs(SearchEvent));
        e.Handled = true;
    }
}