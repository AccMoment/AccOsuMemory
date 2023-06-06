using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace AccOsuMemory.Desktop.Views.Component;

public class MenuButton : TemplatedControl
{

   private string _buttonContent;

   public static readonly DirectProperty<MenuButton, string> ButtonContentProperty = AvaloniaProperty.RegisterDirect<MenuButton, string>(
      "ButtonContent", o => o.ButtonContent, (o, v) => o.ButtonContent = v,"Button");


   private bool _isSelect;

   public static readonly DirectProperty<MenuButton, bool> IsSelectProperty = AvaloniaProperty.RegisterDirect<MenuButton, bool>(
      "IsSelect", o => o.IsSelect, (o, v) => o.IsSelect = v);

   private string _iconValue;

   public static readonly DirectProperty<MenuButton, string> IconValueProperty = AvaloniaProperty.RegisterDirect<MenuButton, string>(
      "IconValue", o => o.IconValue, (o, v) => o.IconValue = v);

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
   
   public string ButtonContent
   {
      get => _buttonContent;
      set => SetAndRaise(ButtonContentProperty, ref _buttonContent, value);
   }

 
}