using System.Diagnostics;
using System.Timers;
using AccOsuMemory.Core.NetCoreAudio.Interfaces;
using NAudio.Wave;
using Timer = System.Timers.Timer;

namespace AccOsuMemory.Core.NetCoreAudio.Players
{
    internal class WindowsPlayer : IPlayer
    {
        private Timer? _playbackTimer;
        private Stopwatch? _playStopwatch;
        private Mp3FileReaderBase? _fileReader;

        private readonly WaveOutEvent _waveOutEvent = new();
        // private string _fileName;

        public event EventHandler? PlaybackFinished;

        public bool Playing { get; private set; }
        public bool Paused { get; private set; }

        public Task Play(string fileName)
        {
            _fileReader = new Mp3FileReaderBase(fileName, format => new AcmMp3FrameDecompressor(format));
            _playbackTimer = new Timer
            {
                AutoReset = false,
                Interval = _fileReader.TotalTime.TotalMilliseconds
            };
            _playStopwatch = new Stopwatch();
            _waveOutEvent.Init(_fileReader);
            _waveOutEvent.Play();
            Paused = false;
            Playing = true;
            _playbackTimer.Elapsed += HandlePlaybackFinished;
            _playbackTimer.Start();
            _playStopwatch.Start();
            return Task.CompletedTask;
        }

        public Task Pause()
        {
            if (Playing && !Paused)
            {
                _waveOutEvent.Pause();
                Paused = true;
                _playbackTimer?.Stop();
                _playStopwatch?.Stop();
                if (_playbackTimer != null && _playStopwatch != null)
                    _playbackTimer.Interval -= _playStopwatch.ElapsedMilliseconds;
            }

            return Task.CompletedTask;
        }

        public Task Resume()
        {
            if (Playing && Paused)
            {
                _waveOutEvent.Play();
                Paused = false;
                _playbackTimer?.Start();
                _playStopwatch?.Reset();
                _playStopwatch?.Start();
            }

            return Task.CompletedTask;
        }

        public Task Stop()
        {
            if (Playing)
            {
                _waveOutEvent.Stop();
                _fileReader?.Dispose();
                _waveOutEvent.Dispose();
                Playing = false;
                Paused = false;
                _playbackTimer?.Stop();
                _playStopwatch?.Stop();
                
                // FileUtil.ClearTempFiles();
            }

            return Task.CompletedTask;
        }

        public Task SetVolume(byte percent)
        {
            _waveOutEvent.Volume = (float)percent / ushort.MaxValue;
            return Task.CompletedTask;
        }

        private void HandlePlaybackFinished(object? sender, ElapsedEventArgs e)
        {
            _waveOutEvent.Dispose();
            _fileReader?.Dispose();
            Playing = false;
            PlaybackFinished?.Invoke(this, e);
            _playbackTimer?.Dispose();
            _playbackTimer = null;
        }

        ~WindowsPlayer()
        {
            _fileReader?.Dispose();
            _waveOutEvent.Dispose();
        }
    }
}