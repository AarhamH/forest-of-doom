using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactionDistance;

    public TMPro.TextMeshProUGUI interactionText;
    Transform cam;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").transform;
        playerController = GetComponent<PlayerController>();
        playerController.PlayerControllerInstance();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        bool successfulHit = false;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactionDistance)) {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if(interactable != null) {
                HandleInteraction(interactable);
                interactionText.text = interactable.GetDescription();
                successfulHit = true;
            }
        }

        if(!successfulHit) { interactionText.text = "";}
    }

    void HandleInteraction(Interactable interactable) {
        switch (interactable.interactionType) {
            case Interactable.InteractionType.Click:
                if(playerController.shootAction.triggered) {
                    interactable.Interact();
                }
            break;

            case Interactable.InteractionType.Hold:
                if(playerController.test.IsPressed()) {
                    interactable.Interact();
                }
            break;

            default:
                throw new System.Exception("Interaction not available"); 
        }
    }

}
