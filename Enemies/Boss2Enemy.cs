using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Enemy : Enemy
{
    [Header("Boss2: Set in Inspector")]    
    public GameObject projectile;
    public float timeUntilNextShot = 3;
    public float delayBetweenShots = 3;
    public int bossLives = 3;

    private float presetSpeed;

    protected override void Awake()
    {
        base.Awake();
        presetSpeed = speed;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Boss2Fire();
    }
    void Boss2Fire()
    {
        timeUntilNextShot -= Time.deltaTime;
        
        if (timeUntilNextShot <= 0.0f)
        {
            Vector3 shotSpawnPos = transform.position;
            Instantiate(projectile, shotSpawnPos, projectile.transform.rotation);
            timeUntilNextShot = delayBetweenShots + Random.Range(-3, 3);
        }
    }
    public override void EnemyMove()
    {
        //give stronger impulse near the edge of arena
        if (transform.position.x > 5 || transform.position.x < -5 || transform.position.z > 5 || transform.position.z < -5) speed = 5f;
        else if (transform.position.x > 6 || transform.position.x < -6 || transform.position.z > 6 || transform.position.z < -6) speed = 10f;
        else if (transform.position.x > 7 || transform.position.x < -7 || transform.position.z > 7 || transform.position.z < -7) speed = 15f;
        else speed = presetSpeed;
        Vector3 lookDirection = (playerBall.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
        if (transform.position.y < -2)
        {
            transform.position = new Vector3(0, 1, 0);
            enemyRb.velocity = Vector3.zero;
            Respawn();
        }
    }
    public void Respawn()
    {        
        bossLives--;
        Debug.Log("Boss lives left: " + bossLives);
        if (bossLives <= 0)
        {
            spawnManager.BossBodyCount();
            Debug.Log("Enemy killed");
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {            
            transform.position = new Vector3(0, 1, 0);
            enemyRb.velocity = Vector3.zero;
        }

    }
}
