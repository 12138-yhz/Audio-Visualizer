using CSCore.DSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.DevScripts.MusicVicual
{
    internal class LineSpectrum : SpectrumBase
    {
        public int BarCount
        {
            get { return SpectrumResolution; }
            set { SpectrumResolution = value; }
        }

        public LineSpectrum(FftSize fftSize)
        {
            FftSize = fftSize;
        }

        public float[] GetSpectrumData(double maxValue)
        {
            // Get spectrum data internal
            var fftBuffer = new float[(int)FftSize];

            UpdateFrequencyMapping();

            if (SpectrumProvider.GetFftData(fftBuffer, this))
            {
                SpectrumPointData[] spectrumPoints = CalculateSpectrumPoints(maxValue, fftBuffer);

                // Convert to float[]
                List<float> spectrumData = new List<float>();
                spectrumPoints.ToList().ForEach(point => spectrumData.Add((float)point.Value*0.2f));
                return spectrumData.ToArray();
            }

            return null;
        }
    }
}
