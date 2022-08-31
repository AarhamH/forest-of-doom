using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class makes the head rig move with respect to a point
public class Aim : PlayerController
{
    [SerializeField]
    private Transform aimTarget;
    [SerializeField]
    private float aimDistance = 10f;

    private void Start() {
        PlayerControllerInstance();
    }

    private void Update()
    {
        HeadAim();

        if(PlayerStats.playerIsDead){
            DeadAim();
        }
    }

    private void HeadAim(){
        aimTarget.position = cameraTransform.position + cameraTransform.forward * aimDistance;
    }

    private void DeadAim(){
        // makes the head orient in random directions after death
        // death looks more natural (cosmetic feature)
        aimTarget.position = transform.position + Vector3.back * aimDistance;

    }

    

}
