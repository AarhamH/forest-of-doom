using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(this.gameObject.layer == LayerMask.NameToLayer("Water") && other.gameObject.layer == LayerMask.NameToLayer("whatIsPlayer")) {
            other.GetComponent<PlayerStats>().Die();
        }
    }
}
