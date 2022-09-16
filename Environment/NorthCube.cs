using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorthCube : MonoBehaviour
{
    public SpawnManager spawnManager;
    public bool isTriggered = false;
    private bool isTriggerable = true;

    private void Awake()
    {
        spawnManager = GameObject.Find("Main Camera").GetComponent<SpawnManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isTriggerable)
        {
            isTriggered = true;
            spawnManager.SpawnScenario1();
            isTriggerable = false;
        }

    }
}
