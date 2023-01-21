using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum ePowerUpType { yellow, red }
    public ePowerUpType itemType;
    private PlayerController playerController;
    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (itemType == ePowerUpType.yellow && other.tag == "Player" && !playerController.hasPowerUp)
        {
            playerController.ActivateYellowPowerUp();
            Destroy(gameObject);
        }

        if (itemType == ePowerUpType.red && other.tag == "Player" && !playerController.hasPowerUp)
        {
            playerController.ActivateRedPowerUp();
            Destroy(gameObject);
        }
    }

}
