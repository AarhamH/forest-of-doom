using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public CinemachineVirtualCamera thirdPersonCam;
    public CinemachineVirtualCamera aimCamera;

    float shakeTimer;   

    private void Update() {
        shakeTimer -=Time.deltaTime;
        if(shakeTimer <= 0f) {
            thirdPersonCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
            aimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        }
    }

    public void Shake(float intensity, float time) {
            thirdPersonCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
            aimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity; 
    }
}
