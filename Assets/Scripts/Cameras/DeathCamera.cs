using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// this camera zooms out when the character dies
// camera also does not allow mouse input
// all are cosmetic features lmao
public class DeathCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera thirdPersonCamera;
    [SerializeField]
    private CinemachineVirtualCamera aimCamera;
    [SerializeField]
    private Canvas zoomOutCanvas;
    [SerializeField]
    private Canvas zoomInCanvas;

    void Update()
    {
       if(PlayerStats.playerIsDead){
        DeathCameraInitiate();
       } 
    }
    private void DeathCameraInitiate(){
        thirdPersonCamera.enabled = false;
        if(aimCamera != null){
            aimCamera.enabled = false;
        }
        if(zoomInCanvas != null){
            zoomInCanvas.enabled = false;
        }
        zoomOutCanvas.enabled = false;
    }
}
