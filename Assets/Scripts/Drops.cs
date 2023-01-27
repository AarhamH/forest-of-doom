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
                AudioManager.Instance.PlayEffect("Coins");
                ScoreManager.scoreDisplay+=50;
                Destroy(gameObject);
            }

            else if(this.tag == "SmallBag") {
                AudioManager.Instance.PlayEffect("Coins");
                ScoreManager.scoreDisplay+=10;
                Destroy(gameObject);
            }

            else if(this.tag == "Mana" && other.tag == "Healer") {
                AudioManager.Instance.PlayEffect("ManaSound");
                other.GetComponent<Healing>().currentMana += 100f;
                if(other.GetComponent<Healing>().currentMana > other.GetComponent<Healing>().maxMana) {
                    other.GetComponent<Healing>().currentMana = other.GetComponent<Healing>().maxMana += 100f;
                }
                Destroy(gameObject);
            }

            else if(this.tag == "Bomb" && other.tag == "Thrower") {
                AudioManager.Instance.PlayEffect("BombPickSound");
                other.GetComponent<Throwing>().currentThrows++;
                if(other.GetComponent<Throwing>().currentThrows > other.GetComponent<Throwing>().totalThrows) {
                    other.GetComponent<Throwing>().currentThrows = other.GetComponent<Throwing>().totalThrows;
                }
                Destroy(gameObject);
            }

            else if(this.tag == "Health") {
                AudioManager.Instance.PlayEffect("HealthSound");
                other.GetComponent<PlayerStats>().currentHealth+=20f;
                if(other.GetComponent<PlayerStats>().currentHealth > other.GetComponent<PlayerStats>().maxHealth) {
                    other.GetComponent<PlayerStats>().currentHealth = other.GetComponent<PlayerStats>().maxHealth;
                }
                Destroy(gameObject);
            }

        }


    }

}
