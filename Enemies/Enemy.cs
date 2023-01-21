using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy: Set in Inspector")]
    public GameObject explosionPrefab;
    public AudioClip impact;
    public float speed;

    [Header("Enemy: Set Dynamically")]    
    public Rigidbody enemyRb;
    public GameObject playerEmpty;
    public GameObject playerBall;    
    public SpawnManager spawnManager;
    public PlayerController playerController;
    public AudioSource enemyAudio;
  
    protected virtual void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerBall = GameObject.Find("Ball");
        playerEmpty = GameObject.Find("Player");
        spawnManager = GameObject.Find("Main Camera").GetComponent<SpawnManager>();
        playerController = playerEmpty.GetComponent<PlayerController>();
        enemyAudio = GetComponent<AudioSource>();
        
    }
    public virtual void FixedUpdate()
    {
        EnemyMove();
    }

    public virtual void OnCollisionEnter(Collision collision)
    {

        if (!collision.gameObject.CompareTag("Environment")) enemyAudio.PlayOneShot(impact); //don't cling when contact floor


        if (collision.gameObject.CompareTag("Player") && playerController.hasPowerUp)
        {
            Debug.Log("Collided with Player under yellow pill");
            Vector3 awayFromPlayer = (transform.position - collision.gameObject.transform.position);
            enemyRb.AddForce(awayFromPlayer * playerController.pushStrength, ForceMode.Impulse);
        }
    }
    public virtual void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PowerUpSplash") && playerController.hasPowerUp)
        {
            Debug.Log("Collided with Player under red pill");
            Vector3 awayFromPlayer = (transform.position - other.gameObject.transform.position);
            enemyRb.AddForce(awayFromPlayer * playerController.pushStrength * 2, ForceMode.Impulse);
        }
    }
    public virtual void EnemyMove()
    {
        Vector3 lookDirection = (playerBall.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
        if (transform.position.y < -2) KillEnemy();       
    }   
    void KillEnemy()
    {
        spawnManager.BodyCount();
        spawnManager.CheckEnemyPresence();
        Debug.Log("Enemy killed");
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
