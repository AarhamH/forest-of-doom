using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public enum InteractionType {
        Click,
        Hold
    }

    float holdTime = 0f;

    public InteractionType interactionType;
    public abstract string GetDescription();
    public abstract void Interact();

    public void IncreaseHoldTime() => holdTime += Time.deltaTime/3;
    public void ResetHoldTime() => holdTime = 0f;

    public float GetHoldTime() =>  holdTime;
}
