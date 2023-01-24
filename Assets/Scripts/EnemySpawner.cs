using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;
    public GameObject enemy;
    public GameObject spawnEffect;
    public int numOfEnemies;
    public bool active;
    float timer;
    float offsetTime = 30f;

    private void Update() {

        if(!this.GetComponent<BoxCollider>().enabled)
        {
            timer += Time.deltaTime;
            if(timer > offsetTime)
            {
                timer = 0f;
                this.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        int i;
        int layerMask = LayerMask.NameToLayer("whatIsPlayer");
        if(other.gameObject.layer == layerMask) {

            for(i=0;i<numOfEnemies;i++) {
                SpawnEnemy();
            }
        this.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void SpawnEnemy() {
        center.x = this.transform.position.x;
        center.y = this.transform.position.y;
        center.z = this.transform.position.z;

        float xComp = this.size.x/2;
        float zComp = this.size.z/2;

        Vector3 pos = center + new Vector3(Random.Range(-xComp-4,xComp+4),2f,Random.Range(-zComp-20,zComp+20));
        Instantiate(spawnEffect,pos,Quaternion.identity);
        Instantiate(enemy,pos,Quaternion.identity);
    }


    IEnumerator wait(float num) { yield return new WaitForSeconds(num); }

    



}
