using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
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
        inLevelTimerString = Mathf.Ceil(inLevelTimerInt / 60).ToString("00") + ":" + (inLevelTimerInt % 60).ToString("00");
        timerText.text = inLevelTimerString;
    }
    //called on any level quit button press (victory, defeat, interrupt). sends string to statistics server
    //---TO DO: send build version as well, now sends '???'---
    public void MakeStatSnapShot()
    {
        string statTimeNow = DateTime.UtcNow.ToString();
        string statLevelNum = levelNumber.ToString();
        string statLevelTimer = inLevelTimerString;
        string statLevelResult = gameManager.levelResult.ToString();
        string statPlayerLives = playerController.lives.ToString();
        string statWaveNum = spawnManager.waveNumber.ToString();
        string statStarsAmount = gameManager.starsAmount.ToString();
        string statBuildVersion = Application.version.ToString();

#if UNITY_EDITOR      

        Debug.Log("%playerID at: " + statTimeNow + "; has completed Level: " + statLevelNum + "; in " + statLevelTimer +
             "; with Result " + statLevelResult + "; Lives left: " + statPlayerLives + "; Wave reached: " + statWaveNum +
             "; Stars earned: " + statStarsAmount + ";");
        Debug.Log("Build version is: " + statBuildVersion);
#else
        StartCoroutine(GetRequest("https://gideon-smart.ru/tau/logger/rollo.php?"
            + "&build_version=" + UnityWebRequest.EscapeURL(statBuildVersion) //sends '???'
            + "&level_num=" + UnityWebRequest.EscapeURL(statLevelNum)
            + "&level_timer=" + UnityWebRequest.EscapeURL(statLevelTimer)
            + "&level_result=" + UnityWebRequest.EscapeURL(statLevelResult)
            + "&player_lives=" + UnityWebRequest.EscapeURL(statPlayerLives)
            + "&wave_num=" + UnityWebRequest.EscapeURL(statWaveNum)
            + "&stars_amount=" + UnityWebRequest.EscapeURL(statStarsAmount)));
#endif
    }
    private IEnumerator GetRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            //Debug.Log("Received: " + uwr.downloadHandler.text);
        }
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
