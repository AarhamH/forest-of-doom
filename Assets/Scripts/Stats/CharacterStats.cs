using UnityEngine;

/*
    CharacterStats Class: A boilerplate class used in PlayerStats and EnemyStats classes

    Necessary Components: n/A
*/
public class CharacterStats : MonoBehaviour
{
    // used a get set to intiialize currentHealth to a parameterized health
    public float currentHealth {get; set;}

    // isDead is static due to the mechanics of the game; if one dies, game over
    static public bool isDead;


    /*
        Input: float maxHealth => The health that a character starts off with
    
        Functionality: - Initializes currentHealth to the characters maxHealth

        Called In: n/A
 
    */
    protected void InitializeHealth(float maxHealth)  {currentHealth = maxHealth;}


    /*
        Input: float damage => The arbritary damage which will be deducted 
    
        Functionality: - Virtual function that subtracts currentHealth with damage
                       - Handles death if health goes to zero or below

        Called In: Overwritten in another class
 
    */
    public virtual void TakeDamage(float damage)
    {       
        currentHealth -= damage;

        if(currentHealth <= 0){
            Die();
        }
    }

    /*
        Input: n/A
    
        Functionality: - Virtual method that destroys the character at a delayed tiem

        Called In: Overwritten in another class
 
    */
    public virtual void Die()
    {
        Invoke(nameof(DestroyCharacter),1.5f);
    }


    /*
        Input: n/A
    
        Functionality: - Destroys the character

        Called In: Die()
 
    */
    protected void DestroyCharacter()
    {
        Destroy(this.gameObject);
    }


   /*
        Input: n/A
    
        Functionality: - Plays death particle effects conveniently

        Called In: n/A
 
    */
    protected void PlayParticleEffects(GameObject effect)
    {
        Instantiate(effect, transform.position,Quaternion.identity);
    }

}
