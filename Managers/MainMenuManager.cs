using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenuManager : MonoBehaviour
{
    [Header("Titles and Menus")]
    //TODO - put into list/array
    public GameObject gameTitle;
    public GameObject startMenu;
    public GameObject levelSelectMenu;
    public GameObject campaignMenu1;
    public GameObject campaignMenu2;
    public GameObject settingsMenu;
    public GameObject statisticsMenu;
    public TextMeshProUGUI maxWaveText;
    public Button episode02Button;
    public Button episode03Button;
    public Button episode04Button;
    public Button episode05Button;
    public Button episode06Button;
    public Button episode07Button;
    public Button episode08Button;
    public Button episode09Button;
    public Button episode10Button;

    private int maxWave;    
    private GameObject Star1;
    private GameObject Star2;
    private GameObject Star3;
    
    

    private void Awake()
    {
        CheckMaxWave();
        CheckMCLN();
        CheckLastLevel();
        
    }
    
    //-----menus switching methods-----

    public void FromStartToLevel()
    {
        startMenu.gameObject.SetActive(false);
        levelSelectMenu.gameObject.SetActive(true);
        CheckEpisodesStars();
    }
    
    public void FromLevelToStart()
    {
        levelSelectMenu.gameObject.SetActive(false);
        startMenu.gameObject.SetActive(true);
    }       
    
    public void FromStartToSettings()
    {
        startMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
    }
    public void FromSettingsToStart()
    {
        settingsMenu.gameObject.SetActive(false);
        startMenu.gameObject.SetActive(true);
    }
    public void LoadLastCampaignLevel()
    {
        int maxCampaignLevelNumber = 0;
        if (PlayerPrefs.HasKey("MCLN"))
        {
            maxCampaignLevelNumber = PlayerPrefs.GetInt("MCLN");
        }

        //crunch to avoid OutOfRange (yes, it's a magic number equal to last level achieved)
        if (maxCampaignLevelNumber >= 90) maxCampaignLevelNumber = 90;
        PlayerPrefs.SetInt("Level", 301 + maxCampaignLevelNumber);
        SceneManager.LoadScene(1);
    }
    
    //-----level start methods (change scene)-----
    public void SetLevel1() //start tutorial
    {
        PlayerPrefs.SetInt("Level", 1);
        SceneManager.LoadScene(1);
    }
    public void SetLevel2() //start endless survival
    {
        PlayerPrefs.SetInt("Level", 2);
        SceneManager.LoadScene(1);
    }
    public void SetLevel301() //start campaign episode 1
    {
        PlayerPrefs.SetInt("Level", 301);
        SceneManager.LoadScene(1);
    }
    public void SetLevel311() //start campaign episode 2
    {
        PlayerPrefs.SetInt("Level", 311);
        SceneManager.LoadScene(1);
    }
    public void SetLevel321() //start campaign episode 3
    {
        PlayerPrefs.SetInt("Level", 321);
        SceneManager.LoadScene(1);
    }
    public void SetLevel331() //start campaign episode 4
    {
        PlayerPrefs.SetInt("Level", 331);
        SceneManager.LoadScene(1);
    }
    public void SetLevel341() //start campaign episode 5 - BOSS 1
    {
        PlayerPrefs.SetInt("Level", 341);
        SceneManager.LoadScene(1);
    }
    public void SetLevel351() //start campaign episode 6 
    {
        PlayerPrefs.SetInt("Level", 351);
        SceneManager.LoadScene(1);
    }
    public void SetLevel361() //start campaign episode 7 
    {
        PlayerPrefs.SetInt("Level", 361);
        SceneManager.LoadScene(1);
    }
    public void SetLevel371() //start campaign episode 8 
    {
        PlayerPrefs.SetInt("Level", 371);
        SceneManager.LoadScene(1);
    }
    public void SetLevel381() //start campaign episode 9 
    {
        PlayerPrefs.SetInt("Level", 381);
        SceneManager.LoadScene(1);
    }
    public void SetLevel391() //start campaign episode 10 - BOSS 2
    {
        PlayerPrefs.SetInt("Level", 391);
        SceneManager.LoadScene(1);
    }

    //-----achievements display methods-----
    void CheckLastLevel()
    {
        int maxCampaignLevelNumber = 0;
        Button lastLevelButton = GameObject.Find("LastLevelButton").GetComponent<Button>();
        if (PlayerPrefs.HasKey("MCLN"))
        {
            maxCampaignLevelNumber = PlayerPrefs.GetInt("MCLN");
        }
        if (maxCampaignLevelNumber >= 10)
        {
            lastLevelButton.interactable = true;
        }   
            
    }
    void CheckMaxWave() //����������� ����������� ����� � ���������
    {
        if (PlayerPrefs.HasKey("MaxWave"))
        {
            maxWave = PlayerPrefs.GetInt("MaxWave");
            maxWaveText.text = ": " + maxWave;
        }
    }
    void CheckMCLN() //max campaign episode reached (unlocks next episode) TODO - switch
    {
        if (PlayerPrefs.HasKey("MCLN"))
        {
            int maxCampaignLevelNumber = PlayerPrefs.GetInt("MCLN");
            if (maxCampaignLevelNumber >= 10)
            {
                episode02Button.image.color = Color.white;
                episode02Button.interactable = true;
            }
            if (maxCampaignLevelNumber >= 20)
            {
                episode03Button.image.color = Color.white;
                episode03Button.interactable = true;
            }
            if (maxCampaignLevelNumber >= 30)
            {
                episode04Button.image.color = Color.white;
                episode04Button.interactable = true;
            }
            if (maxCampaignLevelNumber >= 40)
            {
                episode05Button.image.color = Color.white;
                episode05Button.interactable = true;
            }
            if (maxCampaignLevelNumber >= 50)
            {
                episode06Button.image.color = Color.white;
                episode06Button.interactable = true;
            }
            if (maxCampaignLevelNumber >= 60)
            {
                episode07Button.image.color = Color.white;
                episode07Button.interactable = true;
            }
            if (maxCampaignLevelNumber >= 70)
            {
                episode08Button.image.color = Color.white;
                episode08Button.interactable = true;
            }
            if (maxCampaignLevelNumber >= 80)
            {
                episode09Button.image.color = Color.white;
                episode09Button.interactable = true;
            }
            if (maxCampaignLevelNumber >= 90)
            {
                episode10Button.image.color = Color.white;
                episode10Button.interactable = true;
            }

            Debug.Log("MaxCampaignLevenNumber is " + PlayerPrefs.GetInt("MCLN"));
        }
    }  
    void CheckEpisodesStars()  //amount of golden stars awarded for each episode
        
    {
        if (PlayerPrefs.HasKey("E1S"))
        {
            int starsAmount = PlayerPrefs.GetInt("E1S");
            Debug.Log("Got Stars Amount for E1: " + starsAmount);
            Star1 = GameObject.Find("E1Star1");
            Star2 = GameObject.Find("E1Star2");
            Star3 = GameObject.Find("E1Star3");

            LightTheStars(starsAmount);
            
        }

        if (PlayerPrefs.HasKey("E2S"))
        {
            int starsAmount = PlayerPrefs.GetInt("E2S");
            Debug.Log("Got Stars Amount for E2: " + starsAmount);
            Star1 = GameObject.Find("E2Star1");
            Star2 = GameObject.Find("E2Star2");
            Star3 = GameObject.Find("E2Star3");

            LightTheStars(starsAmount);
        }

        if (PlayerPrefs.HasKey("E3S"))
        {
            int starsAmount = PlayerPrefs.GetInt("E3S");
            Debug.Log("Got Stars Amount for E3: " + starsAmount);
            Star1 = GameObject.Find("E3Star1");
            Star2 = GameObject.Find("E3Star2");
            Star3 = GameObject.Find("E3Star3");

            LightTheStars(starsAmount);
        }

        if (PlayerPrefs.HasKey("E4S"))
        {
            int starsAmount = PlayerPrefs.GetInt("E4S");
            Debug.Log("Got Stars Amount for E4: " + starsAmount);
            Star1 = GameObject.Find("E4Star1");
            Star2 = GameObject.Find("E4Star2");
            Star3 = GameObject.Find("E4Star3");

            LightTheStars(starsAmount);
        }

        if (PlayerPrefs.HasKey("E5S"))
        {
            int starsAmount = PlayerPrefs.GetInt("E5S");
            Debug.Log("Got Stars Amount for E5: " + starsAmount);
            Star1 = GameObject.Find("E5Star1");
            Star2 = GameObject.Find("E5Star2");
            Star3 = GameObject.Find("E5Star3");

            LightTheStars(starsAmount);
        }

        if (PlayerPrefs.HasKey("E6S"))
        {
            int starsAmount = PlayerPrefs.GetInt("E6S");
            Debug.Log("Got Stars Amount for E6: " + starsAmount);
            Star1 = GameObject.Find("E6Star1");
            Star2 = GameObject.Find("E6Star2");
            Star3 = GameObject.Find("E6Star3");

            LightTheStars(starsAmount);
        }

        if (PlayerPrefs.HasKey("E7S"))
        {
            int starsAmount = PlayerPrefs.GetInt("E7S");
            Debug.Log("Got Stars Amount for E7: " + starsAmount);
            Star1 = GameObject.Find("E7Star1");
            Star2 = GameObject.Find("E7Star2");
            Star3 = GameObject.Find("E7Star3");

            LightTheStars(starsAmount);
        }

        if (PlayerPrefs.HasKey("E8S"))
        {
            int starsAmount = PlayerPrefs.GetInt("E8S");
            Debug.Log("Got Stars Amount for E8: " + starsAmount);
            Star1 = GameObject.Find("E8Star1");
            Star2 = GameObject.Find("E8Star2");
            Star3 = GameObject.Find("E8Star3");

            LightTheStars(starsAmount);
        }

        if (PlayerPrefs.HasKey("E9S"))
        {
            int starsAmount = PlayerPrefs.GetInt("E9S");
            Debug.Log("Got Stars Amount for E9: " + starsAmount);
            Star1 = GameObject.Find("E9Star1");
            Star2 = GameObject.Find("E9Star2");
            Star3 = GameObject.Find("E9Star3");

            LightTheStars(starsAmount);
        }

        if (PlayerPrefs.HasKey("E10S"))
        {
            int starsAmount = PlayerPrefs.GetInt("E10S");
            Debug.Log("Got Stars Amount for E10: " + starsAmount);
            Star1 = GameObject.Find("E10Star1");
            Star2 = GameObject.Find("E10Star2");
            Star3 = GameObject.Find("E10Star3");

            LightTheStars(starsAmount);
        }

    }
    void LightTheStars(int starsAmount)
    {
        if (starsAmount >= 1) Star1.GetComponent<Image>().color = Color.yellow;
        if (starsAmount >= 2) Star2.GetComponent<Image>().color = Color.yellow;
        if (starsAmount == 3) Star3.GetComponent<Image>().color = Color.yellow;
    }
    public void OpenPolicy()
    {
        Application.OpenURL("https://pages.flycricket.io/rolland-the-ball/privacy.html");
    }
    public void OpenTerms()
    {
        Application.OpenURL("https://pages.flycricket.io/rolland-the-ball/terms.html");
    }
    public void QuitGame()
    {
        PlayerPrefs.SetInt("Level", 0);
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }    
}
