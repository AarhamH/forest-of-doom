using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAddon : MonoBehaviour
{
    public int damage = 20;

    public GameObject hitEffect;

    public Collider[] targets;

    SwordAttack swordAttack;

    bool inRange;

    private void Awake() {
        swordAttack = GetComponent<SwordAttack>();
    }
    
}
