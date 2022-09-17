using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Enemy : Enemy
{
    [Header("Boss: Set in Inspector")]
    public GameObject[] bossPrefabs;
    public Material[] bossMaterials;

    private int bossHealth = 3;    
    private MeshRenderer bossMesh;
    private new string name;
    private int index;

    private void Start()
    {
        speed = 10f;
        name = gameObject.name;           
        bossMesh = GetComponent<MeshRenderer>();
        index = int.Parse(name.Substring(5, 1));
    }
   
    

    public override void EnemyMove()
    {
        Vector3 lookDirection = (playerBall.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
        if (transform.position.y < -2)
        {
            transform.position = new Vector3(0, 1, 0);
            enemyRb.velocity = Vector3.zero;
        }
    }
    public override void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Environment")) enemyAudio.PlayOneShot(impact); //don't cling when contact floor

        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody.velocity.x > 0.5 || playerRigidbody.velocity.z > 0.5)
            {
                Debug.Log("Boss takes damage from Collision");
                bossHealth--;
                if (bossHealth == 2) bossMesh.material = bossMaterials[0];
                if (bossHealth == 1) bossMesh.material = bossMaterials[1];
                Debug.Log(name + " health is " + bossHealth);
                if (bossHealth <=0)
                {
                    spawnManager.BossBodyCount();              
                    
                    if (index == 5)
                    {
                        Instantiate(explosionPrefab, transform.position, transform.rotation);
                        Destroy(gameObject);
                    }                        
                    else
                    {
                        SpawnSmallerCopy(index);
                        SpawnSmallerCopy(index);                                               
                        Destroy(gameObject);
                    }                   
                }
            }      
        }
    }
    private void SpawnSmallerCopy(int prefabNumber)
    {        
        Instantiate(bossPrefabs[prefabNumber], transform.position, transform.rotation);
    }
    public override void OnDestroy()
    {
        if (index == 5) spawnManager.CheckEnemyPresence();
    }
}
