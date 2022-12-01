using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaleReactiveEffect : VisualizationEffectBase
{
    #region Private Member Variables

    private Vector3 _initialScale;

    #endregion

    #region Public Properties
 
    public float UpLerp ;

    #endregion

    #region Startup / Shutdown



    public override void Start()
    {
        base.Start();

        _initialScale = transform.localScale;

       // StartCoroutine(GetAudio());
    }
    public float yScaleAmount;
    public Vector3 vector3;
    #endregion

    //  IEnumerator d()
    // {

    // }

    #region Render

    public void Update()
    {

        float audioData = GetAudioData();
        float zScaleAmount = audioData  * 40;

        vector3 = _initialScale + new Vector3(0, 0, zScaleAmount);

       // gameObject.transform.localScale = _initialScale + new Vector3(xScaleAmount, yScaleAmount, zScaleAmount);
        gameObject.transform.localScale = Vector3.Lerp(transform.localScale, vector3, Time.deltaTime * UpLerp);
    }


    #endregion
}
