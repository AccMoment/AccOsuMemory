using System;
using System.Collections.ObjectModel;
using System.Timers;
using AccOsuMemory.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace AccOsuMemory.Desktop.Model;

public partial class HitCalculator : BaseModel
{
    [ObservableProperty] private double _bpm;
    [ObservableProperty] private TimeSpan _time = TimeSpan.FromSeconds(5);
    [ObservableProperty] private int _tapCount;
    [ObservableProperty] private bool _isSingleKey;
    [ObservableProperty] private bool _isStarted;
    [ObservableProperty] private bool _isCompeted;
    [ObservableProperty] private CalculateType _hitType;
    [ObservableProperty] private TimeSpan _remainingTime;
    private readonly ObservableCollection<double> _bpmLines = new();

    // private readonly ObservableCollection<double> _averageLines = new();
    private readonly Timer _timer = new();

    [ObservableProperty] private ObservableCollection<ISeries> _series;

    private long _startTime;
    // private int _tempTapCount;
    // private long _tempTime;

    public HitCalculator()
    {
        _series = new ObservableCollection<ISeries>
        {
            new LineSeries<double>
            {
                Values = _bpmLines,
                Stroke = new SolidColorPaint(SKColors.Violet) { StrokeThickness = 1.5f },
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                DataPadding = new LvcPoint(0, 0),
                YToolTipLabelFormatter = (p) => $"{p.Coordinate.PrimaryValue:F} BPM"
            },
            // new LineSeries<double>
            // {
            //     Values = _averageLines,
            //     Stroke = new SolidColorPaint(SKColors.HotPink) { StrokeThickness = 1 },
            //     Fill = null,
            //     GeometryFill = null,
            //     GeometryStroke = null,
            //     DataPadding = new LvcPoint(0,0),
            //     YToolTipLabelFormatter = p=>$"{p.Coordinate.PrimaryValue:F0}Hit/s"
            // }
        };
        _timer.Interval = 50;
        _timer.Elapsed += (_, _) =>
        {
            var gapTime = DateTime.Now.Ticks + 1 - _startTime;
            if (gapTime >= Time.Ticks)
            {
                IsCompeted = true;
                IsStarted = false;
                RemainingTime = TimeSpan.FromSeconds(0);
                _timer.Stop();
                return;
            }

            RemainingTime = TimeSpan.FromSeconds(Time.TotalSeconds).Subtract(TimeSpan.FromTicks(gapTime));
            Bpm = TapCount / (gapTime / 10000000.0) * 60.0 / (IsSingleKey ? 2.0 : 4.0);
            _bpmLines.Add(Bpm);
            // if (_tempTime != 0)
            // {
            //     var hits = TapCount - _tempTapCount;
            //     var time = endTime - _tempTime;
            //     var averageLine = hits / (time / 10000000.0);
            //     _averageLines.Add(averageLine);
            // }
            // _tempTapCount = TapCount;
            // _tempTime = endTime;
        };
        // _startTime = DateTime.Now.Ticks;
        // Time = TimeSpan.FromSeconds(15);
    }

    public void Start()
    {
        if (IsCompeted) return;
        _startTime = DateTime.Now.Ticks;
        RemainingTime = Time;
        // TapCount = 0;
        IsStarted = true;
        // _bpmLines.Clear();
        // _averageLines.Clear();
        _timer.Enabled = true;
        _timer.Start();
    }
    
    public void Reset()
    {
        IsCompeted = false;
        IsStarted = false;
        _timer.Enabled = false;
        _bpmLines.Clear();
        TapCount = 0;
        Bpm = 0;
    }

    ~HitCalculator()
    {
        _timer.Dispose();
    }

    public void Tap()
    {
        if (IsCompeted) return;
        TapCount++;
    }
}