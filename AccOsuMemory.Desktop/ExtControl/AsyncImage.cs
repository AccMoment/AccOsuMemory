using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AccOsuMemory.Core.Net;
using AccOsuMemory.Core.Utils;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace AccOsuMemory.Desktop.ExtControl;

public class AsyncImage : Image
{
    public static readonly StyledProperty<string?> AsyncSourceProperty =
        AvaloniaProperty.Register<AsyncImage, string?>(nameof(AsyncSource));

    public static readonly StyledProperty<string?> FallbackSourceProperty =
        AvaloniaProperty.Register<AsyncImage, string?>(nameof(FallbackSource));

    public static readonly StyledProperty<bool> IsImageLoadedProperty =
        AvaloniaProperty.Register<AsyncImage, bool>(nameof(IsImageLoaded));

    public static readonly DirectProperty<AsyncImage, BitmapInterpolationMode> InterpolationModeProperty =
        AvaloniaProperty.RegisterDirect<AsyncImage, BitmapInterpolationMode>(nameof(InterpolationMode),
            i => i.interpolationMode, (i, v) => i.interpolationMode = v);

    private BitmapInterpolationMode interpolationMode = BitmapInterpolationMode.None;
    private string? _currentSourcePath;

    // private IImage? _currentSource;

    public string? AsyncSource
    {
        get => GetValue(AsyncSourceProperty);
        set => SetValue(AsyncSourceProperty, value);
    }

    public string? FallbackSource
    {
        get => GetValue(FallbackSourceProperty);
        set => SetValue(FallbackSourceProperty, value);
    }

    public bool IsImageLoaded
    {
        get => GetValue(IsImageLoadedProperty);
        set => SetValue(IsImageLoadedProperty, value);
    }

    public BitmapInterpolationMode InterpolationMode
    {
        get => interpolationMode;
        set => SetAndRaise(InterpolationModeProperty, ref interpolationMode, value);
    }


    protected override async void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == SourceProperty)
        {
            IsImageLoaded = change.NewValue is not null;
        }
        else if (change.Property == AsyncSourceProperty
                 || change.Property == FallbackSourceProperty
                 || change.Property == IsEnabledProperty
                 || change.Property == InterpolationModeProperty)
        {
            Source = null;

            if (!IsEnabled)
            {
                return;
            }

            var height = Height;

            var sourcePath = AsyncSource ?? FallbackSource;
            if (_currentSourcePath == sourcePath)
            {
                return;
            }

            if (sourcePath == null)
            {
                Source = null;
                return;
            }

            if (await GetImageFromSource(sourcePath, height) is { } normalSource)
            {
                Source = normalSource;
                _currentSourcePath = sourcePath;
                // _currentSource = normalSource;
            }
            else if (FallbackSource != null && FallbackSource != sourcePath
                                            && await GetImageFromSource(FallbackSource, height) is { } fallbackSource)
            {
                Source = fallbackSource;
                _currentSourcePath = FallbackSource;
                // _currentSource = fallbackSource;
            }
        }
    }

    private async Task<IImage?> GetImageFromSource(string sourcePath, double rescaleToHeight = double.NaN)
    {
        return await Task.Run(async () =>
        {
            Stream? imageStream = null;

            try
            {
                if (sourcePath.StartsWith("http"))
                {
                    var response =
                        await DownloadManager.HttpClient.GetAsync(sourcePath, HttpCompletionOption.ResponseContentRead);
                    imageStream = await response.Content.ReadAsStreamAsync();
                }
                else if (sourcePath.StartsWith("avares"))
                {
                    imageStream = AssetLoader.Open(new Uri(sourcePath));
                }
                else
                {
                    imageStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
                }

                return double.IsNaN(rescaleToHeight)
                    ? new Bitmap(imageStream)
                    : Bitmap.DecodeToHeight(imageStream, (int)rescaleToHeight, InterpolationMode);
            }
            catch (Exception)
            {
                // Log.Logger.Warning(ex, "Image reading or resizing failed.");
                return null;
            }
            finally
            {
                imageStream?.Dispose();
            }
        });
    }

    // protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    // {
    //     Source = _currentSource;
    //     
    //     base.OnAttachedToVisualTree(e);
    //     
    // }
    //
    // protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    // {
    //
    //     Source = null;
    //     base.OnDetachedFromVisualTree(e);
    // }
}