using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationEffectBase : MonoBehaviour
{

    #region Private Member Variables

    private LoopbackAudio _loopbackAudio;

    #endregion

    #region Protected Properties

    protected LoopbackAudio LoopbackAudio { get { return _loopbackAudio; } }

    #endregion

    #region Public Properties
    public int AudioSampleIndex;
    public float PrimaryScaleFactor;

    #endregion

    #region Startup / Shutdown

    public virtual void Start()
    {
        // References and setup
        _loopbackAudio = FindObjectOfType<LoopbackAudio>();
    }

    #endregion

    #region Protected Methods

    protected float GetAudioData()
    {
        // Get audio data
        return _loopbackAudio.GetSpectrumData( AudioSampleIndex);
    }

    #endregion
}
