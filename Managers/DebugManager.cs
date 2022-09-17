using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
    [Header("Set in Scene 0")]    
    public TMP_InputField cheatInputField;    

    [Header("Set in Scene 1")]
    public float speed = 8f; //player speed
    public TextMeshProUGUI timerText; //in-game timer

    //todo - transfer to Player Controller
    private Button x4;
    private Button x8;
    private Button x16;

    //cheat store field
    private string cheatString;

    //count in-game timer. timer is stopped when game is paused
    private int levelNumber;
    private int inLevelTimerInt;
    private string inLevelTimerString;
    private bool gameIsActive = true;

    private GameManager gameManager;
    private PlayerController playerController;
    private SpawnManager spawnManager;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Level")) levelNumber = PlayerPrefs.GetInt("Level");
        else levelNumber = 0;
        
    }
    private void Start()
    {
       if (SceneManager.GetActiveScene().buildIndex == 1)
       {
            gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            spawnManager = GameObject.Find("Main Camera").GetComponent<SpawnManager>();


            x4 = GameObject.Find("x4").GetComponent<Button>();
            x8 = GameObject.Find("x8").GetComponent<Button>();
            x16 = GameObject.Find("x16").GetComponent<Button>();
            speed = 8;

            StartCoroutine(CountTimeInLevel());
       }            
    }   

    public void SetSpeedX4()
    {
        
        x4.image.color = Color.red;
        x8.image.color = Color.white;
        x16.image.color = Color.white;

        speed = 4;
    }
    public void SetSpeedX8()
    {
        x4.image.color = Color.white;
        x8.image.color = Color.red;
        x16.image.color = Color.white;

        speed = 8;
    }
    public void SetSpeedX16()
    {
        x4.image.color = Color.white;
        x8.image.color = Color.white;
        x16.image.color = Color.red;

        speed = 16;
    }
    IEnumerator CountTimeInLevel()
    {
        while (gameIsActive)
        {
            yield return new WaitForSeconds(1);
            inLevelTimerInt++;
            UpdateTimer();
        }
    }
    void UpdateTimer()
    {
        inLevelTimerString = Mathf.Ceil(inLevelTimerInt / 60).ToString("00") + " : " + (inLevelTimerInt % 60).ToString("00");
        timerText.text = inLevelTimerString;
    }
    //called on any level quit button press (victory, defeat, interrupt). should send string to statistics server
    public void MakeStatSnapShot()
    {
        string statTimeNow = DateTime.UtcNow.ToString();
        int statLevelNum = levelNumber;
        string statLevelTimer = inLevelTimerString;
        int statLevelResult = gameManager.levelResult;
        int statPlayerLives = playerController.lives;
        int statWaveNum = spawnManager.waveNumber;
        int statStarsAmount = gameManager.starsAmount;

        Debug.Log("%playerID at: " + statTimeNow + "; has completed Level: " + statLevelNum + "; in " + statLevelTimer +
             "; with Result " + statLevelResult + "; Lives left: " + statPlayerLives + "; Wave reached: " + statWaveNum + 
             "; Stars earned: " + statStarsAmount + ";");
    }
    
    //CHEATS
    public void ConfirmCheat()
    {
        cheatString = cheatInputField.text;
        CheckCheatInput();
    }
    void CheckCheatInput() //TODO - switch
    {
        
        if (cheatString == "1987")
        {
            SetMCLN();
            Debug.Log("Cheater! 1987 - all episodes UNlocked!");
        }         
            
            
        if (cheatString == "7891")
        {
            ClearMCLN();
            Debug.Log("Cheater! 7891 - all episodes Locked!");
        }            
    }

    private void SetMCLN()
    {
        PlayerPrefs.SetInt("MCLN", 300);
        SceneManager.LoadScene(0);
    }
    private void ClearMCLN()
    {
        PlayerPrefs.SetInt("MCLN", 0);
        SceneManager.LoadScene(0);
    }
    

}
