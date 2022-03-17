using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private static CameraShake instance;

    public static CameraShake Instance
    {
        get { return instance; }
    }

    CinemachineBasicMultiChannelPerlin BasicMultiChannelPerlin;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        BasicMultiChannelPerlin = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        print(BasicMultiChannelPerlin);
    }

    public void Shake(float duration, float magnitude)
    {
        StopCoroutine(ShakeCor(duration, magnitude));
        StartCoroutine(ShakeCor(duration, magnitude));
    }

    private IEnumerator ShakeCor(float duration, float magnitude)
     {

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            BasicMultiChannelPerlin.m_AmplitudeGain = magnitude;
            BasicMultiChannelPerlin.m_FrequencyGain = duration;

            elapsed += Time.deltaTime;

            yield return null;
        }
        BasicMultiChannelPerlin.m_AmplitudeGain = 0;
        BasicMultiChannelPerlin.m_FrequencyGain = 0;
    }
}
