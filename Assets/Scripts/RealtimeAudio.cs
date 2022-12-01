using CSCore;
using CSCore.DSP;
using CSCore.SoundIn;
using CSCore.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.DevScripts.MusicVicual
{
    public  class RealtimeAudio
    {
        //快速傅里叶变换
        private const FftSize CFftSize = FftSize.Fft16384;
        //循环监听
        private WasapiLoopbackCapture _loopbackCapture;
        //音频源
        private SoundInSource _soundInSource;
        private SingleBlockNotificationStream _singleBlockNotificationStream;
        private IWaveSource _realtimeSource;
        private BasicSpectrumProvider _basicSpectrumProvider;
        public const int MaxAudioValue = 30;
        private int _spectrumSize;
        private Action<float[]> _receiveAudio;

        private LineSpectrum _lineSpectrum;
        public RealtimeAudio(int spectrumSize, Action<float[]> receiveAudio)
        {
            _spectrumSize = spectrumSize;
            _receiveAudio = receiveAudio;
        }

        public void StartListen()
        {
            _loopbackCapture = new WasapiLoopbackCapture();
            _loopbackCapture.Initialize();

            //获取音频源
            _soundInSource = new SoundInSource(_loopbackCapture);

            _basicSpectrumProvider = new BasicSpectrumProvider(_soundInSource.WaveFormat.Channels, _soundInSource.WaveFormat.SampleRate, CFftSize);

            _lineSpectrum = new LineSpectrum(CFftSize)
            {
                SpectrumProvider = _basicSpectrumProvider,
                BarCount = _spectrumSize,
                UseAverage = true,
                IsXLogScale = true,
                //ScalingStrategy = _scalingStrategy
            };

            _loopbackCapture.Start();

            _singleBlockNotificationStream = new SingleBlockNotificationStream(_soundInSource.ToSampleSource());
            _realtimeSource = _singleBlockNotificationStream.ToWaveSource();

            byte[] buffer = new byte[_realtimeSource.WaveFormat.BytesPerSecond / 2];

            _soundInSource.DataAvailable += (s, ea) =>
            {
                while (_realtimeSource.Read(buffer, 0, buffer.Length) > 0)
                {
                    float[] spectrumData = _lineSpectrum.GetSpectrumData(MaxAudioValue);
                    if (spectrumData != null && _receiveAudio != null)
                    {
                        _receiveAudio(spectrumData);
                    }
                }
            };

            _singleBlockNotificationStream.SingleBlockRead += singleBlockNotificationStream_SingleBlockRead;
        }

        public void StopListen()
        {
            _singleBlockNotificationStream.SingleBlockRead -= singleBlockNotificationStream_SingleBlockRead;

            _soundInSource.Dispose();
            _realtimeSource.Dispose();
            _receiveAudio = null;
            _loopbackCapture.Stop();
            _loopbackCapture.Dispose();
        }

        private void singleBlockNotificationStream_SingleBlockRead(object sender, SingleBlockReadEventArgs e)
        {
            _basicSpectrumProvider.Add(e.Left, e.Right);
        }
    }
}
