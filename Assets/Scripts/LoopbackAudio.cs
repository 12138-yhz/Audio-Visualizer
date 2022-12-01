using Assets.DevScripts.MusicVicual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopbackAudio : MonoBehaviour
{

    private RealtimeAudio _realtimeAudio;
    //声卡中的数据
    public float[] SpectrumData;

    //采集数据的个数
    public int SpectrumSize;

    private void Awake()
    {
        SpectrumData = new float[SpectrumSize];

        _realtimeAudio = new RealtimeAudio(SpectrumSize, (spectrumData) =>
        {
            // Raw
            SpectrumData = spectrumData;
        });
    }

    void Start()
    {
        Debug.Log("开始！");
        //开始监听
        _realtimeAudio.StartListen();
    }

    public void OnApplicationQuit()
    {
        //停止监听
        _realtimeAudio.StopListen();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetSpectrumData(int index = 0)
    {
        return SpectrumData[index];
    }
}
