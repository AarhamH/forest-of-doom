using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : PlayerController
{
    [SerializeField]
    private Transform aimTarget;
    [SerializeField]
    private float aimDistance = 10f;

    private void Start() {
        PlayerControllerInstance();
    }

    // Update is called once per frame
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
        aimTarget.position = transform.position + Vector3.back * aimDistance;

    }

    

}
