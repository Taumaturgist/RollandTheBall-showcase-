using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleEnemy : Enemy
{
    [Header("Triple Enemy: Set in Inspector")]
    public GameObject[] tripleEnemyPrefabs;
    public Material[] tripleEnemyMaterials;

    private int tripleEnemyHealth = 3;    
    private MeshRenderer tripleEnemyMesh;
    private int index;

    private void Start()
    {
        speed = 10f;
        name = gameObject.name;
        tripleEnemyMesh = GetComponent<MeshRenderer>();
        index = int.Parse(name.Substring(11, 1)); //get the prefab version number
    }

    public override void EnemyMove()
    {
        Vector3 lookDirection = (playerBall.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
        
        if (transform.position.y < -2)
        {
            if (index == 1)
            {
                transform.position = new Vector3(0, 1, 0);
                enemyRb.velocity = Vector3.zero;
            }
            else
            {
                spawnManager.BodyCount();
                Debug.Log("Enemy killed");
                Instantiate(explosionPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Environment")) enemyAudio.PlayOneShot(impact); //don't cling when contact floor

        if (collision.gameObject.CompareTag("Player"))
        {
            //check damage only for 'senior' prefab version
            

            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (index == 1 && (playerRigidbody.velocity.x > 0.5 || playerRigidbody.velocity.z > 0.5))
            {
                Debug.Log("TripleEnemy1 takes damage from Collision");
                tripleEnemyHealth--;
                if (tripleEnemyHealth == 2) tripleEnemyMesh.material = tripleEnemyMaterials[0];
                if (tripleEnemyHealth == 1) tripleEnemyMesh.material = tripleEnemyMaterials[1];
                Debug.Log(name + " health is " + tripleEnemyHealth);
                if (tripleEnemyHealth <= 0)
                {                  
                   
                    SpawnSmallerCopy(1);
                    SpawnSmallerCopy(1);
                    SpawnSmallerCopy(1);
                    Destroy(gameObject);                 

                }
            }

            if (index != 1 && playerController.hasPowerUp)
            {
                Debug.Log("Collided with Player under yellow pill");                
                Vector3 awayFromPlayer = (transform.position - collision.gameObject.transform.position);
                enemyRb.AddForce(awayFromPlayer * playerController.pushStrength, ForceMode.Impulse);
            }



        }
    }
    void SpawnSmallerCopy(int prefabNumber)
    {
        Instantiate(tripleEnemyPrefabs[prefabNumber], transform.position, transform.rotation);
    }
    public override void OnDestroy()
    {
        if (index != 1) spawnManager.CheckEnemyPresence();
    }
}
