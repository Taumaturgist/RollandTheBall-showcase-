using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 8.0f;
    public int lives = 2;
    public int falls = 0;
    public TextMeshProUGUI livesCounter;
    public GameObject tipPanel;


    //explosion on death
    public GameObject explosionPrefab;

    //all for buffs
    public float pushStrength = 15f;
    public bool hasPowerUp;
    public GameObject powerUpCircle;
    public GameObject powerUpSplashArea;
    public GameObject buttonTAP;
    private MeshRenderer powerUpMeshRend;
    private bool hasRedPowerUp;

    //all for sounds
    public bool isGrounded;
    private AudioSource playerAudio;
    public AudioClip[] playerRoll;
    public AudioClip playerGrounds;    

    private GameManager gameManager;
    private Rigidbody playerRb;    
    private FloatingJoystick joystickFloat;
    public Collider playerCollider;
    private int levelNumber;
    


    //Debug - speed test TODO - transfer here
    private DebugManager debugManager;
    
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            levelNumber = PlayerPrefs.GetInt("Level");
            Debug.Log("Player Scenario: " + levelNumber);
        }

        SetPlayerTip();
    }    
    void Start()
    {
        playerRb = GameObject.Find("Ball").GetComponent<Rigidbody>();
        playerCollider = GameObject.Find("Ball").GetComponent<Collider>();        
        joystickFloat = GameObject.Find("Floating Joystick").GetComponent<FloatingJoystick>();
        debugManager = GameObject.Find("Main Camera").GetComponent<DebugManager>();
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
        playerAudio = GetComponent<AudioSource>();
        powerUpMeshRend = powerUpCircle.GetComponent<MeshRenderer>();
        

        if (levelNumber == 1)
        {
            livesCounter.gameObject.SetActive(false);
            livesCounter.text = "∆»«Õ»: " + lives;
        }           
            
    }
    private void FixedUpdate()
    {
        PCMoveInput();
        MobileMoveInput();
    }
    
    void Update()
    {

        if (playerRb.transform.position.y < -2) Respawn();
        

        if (lives >= 0) livesCounter.text = "∆»«Õ»: " + lives;

        powerUpCircle.transform.position = playerRb.transform.position;
        powerUpSplashArea.transform.position = new Vector3(playerRb.transform.position.x, playerRb.transform.position.y - 0.5f, playerRb.transform.position.z);


    }

    void PCMoveInput()
    {
        float inputUpDown = Input.GetAxis("Vertical");
        float inputLeftRight = Input.GetAxis("Horizontal");

        //roll child ball
        speed = debugManager.speed;
        playerRb.AddForce(transform.forward * speed * inputUpDown);
        playerRb.AddForce(transform.right * speed * inputLeftRight);


        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            if (!playerAudio.isPlaying)
            {
                int playerRandomRoll = Random.Range(0, playerRoll.Length);
                playerAudio.PlayOneShot(playerRoll[playerRandomRoll]);
            }
        }
        if (Input.GetKey(KeyCode.Space)) StartCoroutine(ExplodeRedPowerUp());

        if (Input.anyKeyDown && gameManager.isGameActive) StartCoroutine(RemovePlayerTip());
    }
    void MobileMoveInput()
    {
        //roll child ball        
        Vector3 direction = Vector3.forward * joystickFloat.Vertical + Vector3.right * joystickFloat.Horizontal;
        speed = debugManager.speed;
        playerRb.AddForce(direction * speed);

        if (playerRb.velocity.x > 0.5f || playerRb.velocity.z >0.5f)
        {
            if (!playerAudio.isPlaying)
            {
                int playerRandomRoll = Random.Range(0, playerRoll.Length);
                playerAudio.PlayOneShot(playerRoll[playerRandomRoll]);
            }
        }

        if (Input.touchCount > 0 && gameManager.isGameActive) StartCoroutine(RemovePlayerTip());

    }

    public void Respawn()
    {       
        Instantiate(explosionPrefab, playerRb.transform.position, playerRb.transform.rotation);
        if (levelNumber != 1) lives--;
        if (lives < 0)
        {
            playerRb.transform.position = new Vector3(0, 1000, 0); //crutch - change Y to avoid endless explosion loop. TODO - fix it
            StartCoroutine(WaitAndDefeat());
            
        }
        else
        {
            falls++;
            playerRb.transform.position = new Vector3(0, 1, 0);
            playerRb.velocity = Vector3.zero;
        }          
        
    }

    private IEnumerator WaitAndDefeat()
    {
        yield return new WaitForSeconds(1);
        gameManager.CheckDefeat();
    }
    public void ActivateYellowPowerUp()
    {
        Debug.Log("Yellow PowerUp active!");
        hasPowerUp = true;
        powerUpCircle.gameObject.SetActive(true);
        powerUpMeshRend.material.color = Color.yellow;
        StartCoroutine(PowerupCountdown());
    }       
    IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        hasRedPowerUp = false;
        powerUpCircle.gameObject.SetActive(false);
        buttonTAP.gameObject.SetActive(false);
        Debug.Log("PowerUp expired!");
    }
    public void ActivateRedPowerUp()
    {
        Debug.Log("Red PowerUp is active");
        hasPowerUp = true;
        hasRedPowerUp = true;
        buttonTAP.gameObject.SetActive(true);
        powerUpCircle.gameObject.SetActive(true);
        powerUpMeshRend.material.color = new Color(1, 0.5f, 0, 1); //orange
        StartCoroutine(PowerupCountdown());
    }
    public void TapExplodeRedPowerUp()
    {
        StartCoroutine(ExplodeRedPowerUp());
    }
    IEnumerator ExplodeRedPowerUp()
    {
        if (!hasRedPowerUp) yield break;
        powerUpSplashArea.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        powerUpSplashArea.gameObject.SetActive(false);

    }
    public void PlayerReset()
    {
        playerRb.transform.position = new Vector3(0, 1, 0);
        playerRb.velocity = Vector3.zero;
    } 
    void SetPlayerTip()
    {       
        tipPanel.gameObject.SetActive(true);
        TextMeshProUGUI tipText = tipPanel.GetComponentInChildren<TextMeshProUGUI>();
        switch (Application.platform)
            {
                case RuntimePlatform.Android:
                case RuntimePlatform.IPhonePlayer:
                tipText.text = "SWIPE! SWIPE! SWIPE!";
                break;
                case RuntimePlatform.WebGLPlayer:
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                tipText.text = "USE ARROWS TO MOVE!";
                break;
        }
    }
    IEnumerator RemovePlayerTip()
    {
        yield return new WaitForSeconds(1);
        tipPanel.gameObject.SetActive(false);
    }
}

