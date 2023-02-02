using UnityEngine;
using UnityEngine.UI;

/*
    MainHealth Class: Handles the character health bars

    Necessary Components: Foreground (Image) => the colored portion of the health bar to update
                                                based on health
*/
public class MainHealth : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField]
    private Image foreground;

    [Header("Deduction Speed Settings")]
    private float reduceSpeed = 1f;
    private float target = 1f;

    [Header("Low Health Bool")]
    bool lowHealth;


    /*
        Update Function: - Sets the color to red or green based on if the health is low and updates 
                         the fill amount of the health bar foreground every frame based on target
    */
    private void Update() 
    {

        if(lowHealth) 
        {
            foreground.color = Color.red;
        }

        else
        {
            foreground.color = Color.green;
        }
        foreground.fillAmount = Mathf.MoveTowards(foreground.fillAmount,target,reduceSpeed * Time.deltaTime);
    }


    /*
        Input: float maxHealth => Max Health of the character
               float currentHealth => Health of the player during run time

        Functionality: - Detemines the target fill amount based on the currentHealth and maxHealth
                         difference
                       - Sets low health bool based on halfway health

        Called In: Update()
    */
    public void UpdateHealth(float maxHealth, float currentHealth) 
    {
        target = currentHealth/maxHealth;
        if(currentHealth < 50f) 
        {
            lowHealth = true;
        }
        else 
        {
            lowHealth = false;
        }
    }
}
