using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 RotateAmount;
    public bool isRotationRandom;

    private void Start()
    {
        if (isRotationRandom) RotateRandomly();
    }

    void Update()
    {
        transform.Rotate(RotateAmount * Time.deltaTime);
    }
    void RotateRandomly()
    {
        int randomizer = Random.Range(0, 2);
        Debug.Log(gameObject.name + " rotation is set to " + randomizer);
        if (randomizer == 0) RotateAmount = -RotateAmount;
        
        
    }
}
