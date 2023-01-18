using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    public ScoreManager scoreManager;

    private void OnTriggerEnter(Collider other) {
        scoreManager = GetComponent<ScoreManager>();
        int layerMask = LayerMask.NameToLayer("whatIsPlayer");
        if(other.gameObject.layer == layerMask) {
            if(this.tag == "LargeBag") {
                ScoreManager.scoreDisplay+=50;
            }

            else if(this.tag == "SmallBag") {
                ScoreManager.scoreDisplay+=10;
            }

            else if(this.tag == "Mana" && other.tag == "Healer") {
                other.GetComponent<Healing>().currentMana += 100f;
                if(other.GetComponent<Healing>().currentMana > other.GetComponent<Healing>().maxMana) {
                    other.GetComponent<Healing>().currentMana = other.GetComponent<Healing>().maxMana += 100f;
                }
            }

            else if(this.tag == "Bomb" && other.tag == "Thrower") {
                other.GetComponent<Throwing>().currentThrows++;
                if(other.GetComponent<Throwing>().currentThrows > other.GetComponent<Throwing>().totalThrows) {
                    other.GetComponent<Throwing>().currentThrows = other.GetComponent<Throwing>().totalThrows;
                }
            }

            else if(this.tag == "Health") {
                other.GetComponent<PlayerStats>().currentHealth+=20f;
                if(other.GetComponent<PlayerStats>().currentHealth > other.GetComponent<PlayerStats>().maxHealth) {
                    other.GetComponent<PlayerStats>().currentHealth = other.GetComponent<PlayerStats>().maxHealth;
                }
            }

            Destroy(gameObject);
        }


    }

}
