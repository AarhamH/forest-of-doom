using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private int priorityBoostAmount = 10;
    [SerializeField]
    private CinemachineVirtualCamera thirdPersonCamera;
    [SerializeField]
    private CinemachineVirtualCamera aimCamera;
    [SerializeField]
    private Canvas zoomOutCanvas;
    [SerializeField]
    private Canvas zoomInCanvas;

    private CinemachineVirtualCamera virtualCamera;
    private InputAction aimAction;

    static public bool aimCalled;

    private void Awake() {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
        zoomInCanvas.enabled = false;
    }

    private void OnEnable(){
        // aim camera active, third person inactive
        aimAction.performed += _=> StartAim();
        aimAction.canceled +=_=> CancelAim();
    }

    private void OnDisabled(){
        // aim camera inactive, third person active
        aimAction.performed -= _=> StartAim();
        aimAction.canceled -=_=> CancelAim();
    }

    private void StartAim(){
        virtualCamera.Priority += priorityBoostAmount;
        zoomInCanvas.enabled = true;
        zoomOutCanvas.enabled = false;
        aimCalled = true;
    }

    private void CancelAim(){
        virtualCamera.Priority -= priorityBoostAmount;
        zoomInCanvas.enabled = false;
        zoomOutCanvas.enabled = true;
        aimCalled = false;
    }

}
