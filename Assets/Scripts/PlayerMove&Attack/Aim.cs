using UnityEngine;

/*
    Aim Class: Attached to all controllable characters to allow thier heads to move along the crosshair

    Necessary Components: ref_aimTarget (Transform) => attached to the AimRig component in characters
*/
public class Aim : MonoBehaviour
{
    [Header("Aim Settings")]
    [SerializeField]
    private Transform aimTarget;
    [SerializeField]
    private float aimDistance = 10f;

    [Header("Controllers")]
    private PlayerController playerController;

    /*
        Awake Function: - Enables input controls
    */
    private void Awake() 
    {
        playerController = GetComponent<PlayerController>();
        playerController.PlayerControllerInstance();
    }

    /*
        Update Function: - Called HeadAim() every frame
    */
    private void Update() 
    {
        HeadAim();
    }

    /*
        Input: n/A

        Functionality: - Method to control head movement based on an aimPosition
                         - Functions through a RigBuilder component in the character

        Called In: Update()

        Notable Function Docs:
        - RigBuilder Class: https://docs.unity3d.com/Packages/com.unity.animation.rigging@1.2/api/UnityEngine.Animations.Rigging.RigBuilder.html
        - Bone Renderer: https://docs.unity3d.com/Packages/com.unity.animation.rigging@1.1/api/UnityEngine.Animations.Rigging.BoneRenderer.html
    */
    private void HeadAim() 
    {
        aimTarget.position = playerController.cameraTransform.position + playerController.cameraTransform.forward * (aimDistance + Time.deltaTime);
    }

}
