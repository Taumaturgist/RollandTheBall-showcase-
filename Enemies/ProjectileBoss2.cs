using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBoss2 : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed; //от 2 до 4
    public GameObject blackHolePrefab;

    private float zDestroy = -10.0f;
    private float xDestroy = 20.0f;

    private GameObject player;
    private PlayerController playerController;
    public Vector3 landingSpot;
    

    
    void Start()
    {
        player = GameObject.Find("Ball");
        if (player)
        {
            transform.LookAt(player.transform);
            landingSpot = player.transform.position;
        }

        speed = Random.Range(2, 4);

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    
    void FixedUpdate()
    {
        MoveProjectile();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.Respawn();
            Destroy(gameObject);
        }
    }
    void MoveProjectile()
    {

        if (transform.position.x <= landingSpot.x + 0.5f && transform.position.x >= landingSpot.x - 0.5f 
            && transform.position.z >= landingSpot.z - 0.5f && transform.position.z <= landingSpot.z + 0.5f)  
        {
            Instantiate(blackHolePrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if ((transform.position.z < zDestroy) || (transform.position.z > -zDestroy * 2) || (transform.position.x > xDestroy) || (transform.position.x < -xDestroy))
        {
            Destroy(gameObject);
        }
    }
}
