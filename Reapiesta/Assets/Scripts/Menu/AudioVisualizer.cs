using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{

    public AudioSource audioSource;
    public float[] samples = new float[7];
    public List<RectTransform> toScale;

    void Start()
    {
		toScale.Clear();
		for (int i = 0; i < transform.childCount; i++)
		{
			toScale.Add(transform.GetChild(i).GetComponent<RectTransform>());
		}
    }

    void Update()
    {
        GetAudioInfo();
        SetScale();
    }

    void GetAudioInfo()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void SetScale()
    {
        for (int i = 0; i < toScale.Count; i++)
        {
            toScale[i].localScale = new Vector3(toScale[i].localScale.x, samples[i] * 20, toScale[i].localScale.z);
        }
    }
}
