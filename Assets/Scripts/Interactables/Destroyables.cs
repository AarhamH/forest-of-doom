using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyables : MonoBehaviour
{
    public GameObject explosion;
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "BombThrow" && this.tag == "Gate-Breakable") {
            Instantiate(explosion,new Vector3(transform.position.x, 2.5f,transform.position.z),Quaternion.identity);
            Destroy(gameObject);
        }
        

    }

    // do water later
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("whatIsPlayer") && this.gameObject.layer == LayerMask.NameToLayer("Water")) {
            Destroy(other.gameObject);
        }        
    }
}
