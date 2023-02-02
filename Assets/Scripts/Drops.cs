using UnityEngine;

/*
    Drops Class: Handles drop pickups that update health and ammo stats

    Necessary Components: ScoreManager Class => required for the gold score value to update
*/
public class Drops : MonoBehaviour
{
    [Header("Score Manager")]
    [SerializeField]
    private ScoreManager scoreManager;

    /*
        OnTriggerEnter method: - Takes current character as the 'other' collider argument
                               - Based on the tag of the drop and the type of character, will follow 
                                 the following steps:
                                 1) Play a sound effect from AudioManager singleton
                                 2) Update a stat based on the drop type (i.e Mana bottle will update mana)
                                 3) Destroy itself
    */
    private void OnTriggerEnter(Collider other) 
    {
        scoreManager = GetComponent<ScoreManager>();
        int layerMask = LayerMask.NameToLayer("whatIsPlayer");

        // if the collider is a player, the drop can be picked up (can't be picked up by skeles)
        if(other.gameObject.layer == layerMask) 
        {
            if(this.tag == "LargeBag") 
            {
                AudioManager.Instance.PlayEffect("Coins");
                ScoreManager.scoreDisplay+=50;
                Destroy(gameObject);
            }

            else if(this.tag == "SmallBag") 
            {
                AudioManager.Instance.PlayEffect("Coins");
                ScoreManager.scoreDisplay+=10;
                Destroy(gameObject);
            }

            else if(this.tag == "Mana" && other.tag == "Healer") 
            {
                AudioManager.Instance.PlayEffect("ManaSound");
                other.GetComponent<Healing>().currentMana += 100f;
                if(other.GetComponent<Healing>().currentMana > other.GetComponent<Healing>().maxMana) 
                {
                    other.GetComponent<Healing>().currentMana = other.GetComponent<Healing>().maxMana += 100f;
                }
                Destroy(gameObject);
            }

            else if(this.tag == "Bomb" && other.tag == "Thrower") 
            {
                AudioManager.Instance.PlayEffect("BombPickSound");
                other.GetComponent<Throwing>().currentThrows++;
                if(other.GetComponent<Throwing>().currentThrows > other.GetComponent<Throwing>().totalThrows) 
                {
                    other.GetComponent<Throwing>().currentThrows = other.GetComponent<Throwing>().totalThrows;
                }
                Destroy(gameObject);
            }

            else if(this.tag == "Health") 
            {
                AudioManager.Instance.PlayEffect("HealthSound");
                other.GetComponent<PlayerStats>().currentHealth+=20f;
                if(other.GetComponent<PlayerStats>().currentHealth > other.GetComponent<PlayerStats>().maxHealth) 
                {
                    other.GetComponent<PlayerStats>().currentHealth = other.GetComponent<PlayerStats>().maxHealth;
                }
                Destroy(gameObject);
            }
        }
    }
}
