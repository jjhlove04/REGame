using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MEC;

public class CameraShake : MonoBehaviour
{
    CinemachineBasicMultiChannelPerlin BasicMultiChannelPerlin;

    private void Start()
    {
        BasicMultiChannelPerlin = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        BasicMultiChannelPerlin.m_AmplitudeGain = 0;
        BasicMultiChannelPerlin.m_FrequencyGain = 0;
    }

    public void Shake(float duration, float magnitude)
    {
        //StopCoroutine(ShakeCor(duration, magnitude));
        //StartCoroutine(ShakeCor(duration, magnitude));
        StopCoroutine(ShakeCor(duration, magnitude));
        Timing.RunCoroutine(ShakeCor(duration, magnitude));
    }

    private IEnumerator<float> ShakeCor(float duration, float magnitude)
     {

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            BasicMultiChannelPerlin.m_AmplitudeGain = magnitude;
            BasicMultiChannelPerlin.m_FrequencyGain = duration;

            elapsed += Time.deltaTime;

            yield return Timing.WaitForOneFrame;
        }
        BasicMultiChannelPerlin.m_AmplitudeGain = 0;
        BasicMultiChannelPerlin.m_FrequencyGain = 0;
    }
}
