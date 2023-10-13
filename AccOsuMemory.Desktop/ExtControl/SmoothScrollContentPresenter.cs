using System;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Utilities;

namespace AccOsuMemory.Desktop.ExtControl;

public class SmoothScrollContentPresenter : ScrollContentPresenter
{
    protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
    {
        if (Extent.Height > Viewport.Height || Extent.Width > Viewport.Width)
        {
            var scrollable = Child as ILogicalScrollable;
            var isLogical = scrollable?.IsLogicalScrollEnabled == true;

            var x = Offset.X;
            var y = Offset.Y;
            var delta = e.Delta;

            // KeyModifiers.Shift should scroll in horizontal direction. This does not work on every platform. 
            // If Shift-Key is pressed and X is close to 0 we swap the Vector.
            if (e.KeyModifiers == KeyModifiers.Shift && MathUtilities.IsZero(delta.X))
            {
                delta = new Vector(delta.Y, delta.X);
            }
                
            if (Extent.Height > Viewport.Height)
            {
                double height = isLogical ? scrollable!.ScrollSize.Height : 50;
                y += -delta.Y * height;
                y = Math.Max(y, 0);
                y = Math.Min(y, Extent.Height - Viewport.Height);
            }

            if (Extent.Width > Viewport.Width)
            {
                double width = isLogical ? scrollable!.ScrollSize.Width : 50;
                x += -delta.X * width;
                x = Math.Max(x, 0);
                x = Math.Min(x, Extent.Width - Viewport.Width);
            }

            // Vector newOffset = SnapOffset(new Vector(x, y));
            //
            // bool offsetChanged = newOffset != Offset;
            // SetCurrentValue(OffsetProperty, newOffset);
            // Animatable a = new Animatable();

            // e.Handled = !IsScrollChainingEnabled || offsetChanged;
        }
        
    }
}