
using System.Collections.Generic;
using UnityEngine;


#region Public Enums

public enum PrefabLayoutType
{
    XLinear,
    XZSpread,
    XZCircular
}

#endregion

public class PrefabLayoutAudioObject : VisualizationEffectBase
{
    #region Public Properties

    public GameObject Prefab;


    /// <summary> 圆半径 </summary>
    public  float fRadius = 20;

    #endregion

    public override void Start()
    {
        base.Start();
        for (int i = 0; i < LoopbackAudio.SpectrumSize; i++)
        {
            float angle = 360f / LoopbackAudio.SpectrumSize;


            GameObject go = Instantiate(Prefab);
           // _gameObjects.Add(go);
            go.transform.SetParent(transform, false);

            float x = fRadius * Mathf.Sin((angle * i) * (Mathf.PI / 180f));

            float y = fRadius * Mathf.Cos((angle * i) * (Mathf.PI / 180f));

            go.transform.localPosition = new Vector3(x, y, 0);
            go.transform.localEulerAngles = new Vector3(0, 0, Mathf.Abs(angle * i - 360));

            go.name = i.ToString();

            // Try to set various other used scripts
            VisualizationEffectBase[] visualizationEffects = go.GetComponents<VisualizationEffectBase>();

            if (visualizationEffects != null && visualizationEffects.Length > 0)
            {
                foreach (VisualizationEffectBase visualizationEffect in visualizationEffects)
                {
                    visualizationEffect.AudioSampleIndex = i;
                }
            }
        }

       // performLayout();
    }


    #region Render

    public void Update()
    {
        transform.Rotate(0, 5 * Time.deltaTime, 0, Space.World);
    }

    #endregion


}