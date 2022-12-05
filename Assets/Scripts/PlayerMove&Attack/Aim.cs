using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Aim : MonoBehaviour
{
    [SerializeField]
    private Transform aimTarget;
    [SerializeField]
    private float aimDistance = 10f;

    PlayerController playerController;

    private void Start() {
        playerController = GetComponent<PlayerController>();
        playerController.PlayerControllerInstance();
    }

    private void Update() {
        HeadAim();
    }

    // controls the head movement relative to a line that goes through the cursor
    private void HeadAim() {
        aimTarget.position = playerController.cameraTransform.position + playerController.cameraTransform.forward * aimDistance;
    }

}
