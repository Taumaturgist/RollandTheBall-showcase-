using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{   
    [Header("For all Episodes")]
    public TextMeshProUGUI briefingText;
    public TextMeshProUGUI victoryText;
    public TextMeshProUGUI defeatText;
    public GameObject pauseMenu;
    public GameObject defeatMenu;
    public GameObject victoryMenu;
    public Image victoryStar1;
    public Image victoryStar2;
    public Image victoryStar3;

    [Header("For Training")]
    public GameObject northCube;

    [Header("For Episode 4")]
    public GameObject arena;    
    private Rotate arenaRotator;

    [Header("For Episode 8")]
    public List<GameObject> propellers;
    private int[] rotationY = { 200, -200 };
    private int randomRotationY;

    [Header("Set Dynamically")]
    public int starsAmount;
    public int levelResult = 0; // -1 for defeat, 1 for victory
    
    //other private fields
    private PlayerController playerController;
    private SpawnManager spawnManager;
    private int levelNumber;
    private int maxCampaignLevelNumber;  


    private void Awake()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            levelNumber = PlayerPrefs.GetInt("Level");
            Debug.Log("Loaded Level: " + levelNumber);
        }

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();               
        arenaRotator = arena.GetComponent<Rotate>();
        spawnManager = gameObject.GetComponent<SpawnManager>();
        if (levelNumber != 1) northCube.gameObject.SetActive(false);

    }
    void Start()
    {
        GameScenarioSwitch();       
        PauseGame();
    }
    void GameScenarioSwitch()
    {
        switch (levelNumber)
        {
            case 1:
                GameScenario1();
                break;
            case 2:
                GameScenario2();
                break;
            case 301:
                GameScenario301();
                break;
            case 311:
                GameScenario311();
                break;
            case 321:
                GameScenario321();
                break;
            case 331:
                GameScenario331();
                break;
            case 341:
                GameScenario341();
                break;
            case 351:
                GameScenario351();
                break;
            case 361:
                GameScenario361();
                break;
            case 371:
                GameScenario371();
                break;
            case 381:
                GameScenario381();
                break;
            case 391:
                GameScenario391();
                break;

        }
    }
    
    void GameScenario1()
    {
        briefingText.text = "Добро пожаловать на Арену!" + "\n" + "\n" +
            "Сейчас ты в безопасности. Освойся с управлением джойстиком и переключением скоростей. Учти, у шарика есть масса и инерция, а вместо тормоза - обратный газ!" + "\n" + "\n" +
            "Когда разберешься с управлением, коснись шариком синего кубика - это вызовет одного соперника. Для победы достаточно скинуть его с Арены - или сделать так, чтобы он слетел с нее сам!";
    
      
    }
    void GameScenario2()
    {
        briefingText.text = "Выживание - продержись столько волн, сколько сможешь!" + "\n" + "\n" +
            "С каждой волной противников все больше! Побей свой лучший результат!";

        if (spawnManager.waveNumber % 5 == 0) RotateArena(new Vector3(0, 10, 0));
        else RotateArena(Vector3.zero);
    }
    void GameScenario301()
    {
        briefingText.text = "ЭПИЗОД 1: Сосчитай до 10" + "\n" + "\n" +
            "Одолей 10 волн простых противников! Столкни их с Арены - или позволь им упасть самим!";
    }
    void GameScenario311()
    {
        briefingText.text = "ЭПИЗОД 2: Размер имеет значение" + "\n" + "\n" +
            "У противников появился Толстый Брат! Сможешь одолеть 10 волн с новой угрозой?";
    }
    void GameScenario321()
    {
        briefingText.text = "ЭПИЗОД 3: Волшебная таблетка" + "\n" + "\n" +
            "Их все больше! Обрати внимание на желтую капсулу - она поможет тебе КАК СЛЕДУЕТ потолкаться";
    }
    void GameScenario331()
    {
        briefingText.text = "ЭПИЗОД 4: Головокружительно" + "\n" + "\n" +
            "Арена под тобой вращается! Постарайся не улететь от центробежного бонуса!";
        
        RotateArena(new Vector3(0, 10, 0));
    }
    void GameScenario341()
    {
        briefingText.text = "ЭПИЗОД 5: Кто здесь Босс?" + "\n" + "\n" +
            "Просто продолжай атаковать!";
    }
    void GameScenario351()
    {
        briefingText.text = "ЭПИЗОД 6: Прием против лома" + "\n" + "\n" +
            "Похоже, Толстые противники не воспринимают желтую таблетку всерьез. А что они скажут про КРАСНУЮ? :)";    
    }
    
    void GameScenario361()
    {
        briefingText.text = "ЭПИЗОД 7: Наброс на вентилятор" + "\n" + "\n" +
            "Будь осторожен - на Арене могут появиться посторонние предметы!";
    }
    public void GameSubScenario361()
    {
        int waveNumber = spawnManager.waveNumber;
        List<GameObject> randomPropellers = propellers;
        for (int i = 0; i < 4; i++)
        {
            randomPropellers[i].gameObject.SetActive(false);
        }
        //шаффлим список пропеллеров по-новой
        int count = randomPropellers.Count;
        int last = count - 1;
        for (int i = 0; i < last; ++i)
        {
            int r = Random.Range(i, count);
            GameObject tmp = randomPropellers[i];
            randomPropellers[i] = randomPropellers[r];
            randomPropellers[r] = tmp;
        }
        //включаем нужное количество случайных пропеллеров
        switch (waveNumber)
        {
            case 2:
                randomPropellers[0].gameObject.SetActive(true);
                break;
            case 3:
                randomPropellers[0].gameObject.SetActive(true);
                break;
            case 4:
                randomPropellers[0].gameObject.SetActive(true);
                randomPropellers[1].gameObject.SetActive(true);
                break;
            case 5:
                randomPropellers[0].gameObject.SetActive(true);
                randomPropellers[1].gameObject.SetActive(true);
                break;
            case 6:
                randomPropellers[0].gameObject.SetActive(true);
                randomPropellers[1].gameObject.SetActive(true);
                break;
            case 7:
                randomPropellers[0].gameObject.SetActive(true);
                randomPropellers[1].gameObject.SetActive(true);
                randomPropellers[2].gameObject.SetActive(true);
                break;
            case 8:
                randomPropellers[0].gameObject.SetActive(true);
                randomPropellers[1].gameObject.SetActive(true);
                randomPropellers[2].gameObject.SetActive(true);
                break;
            case 9:
                randomPropellers[0].gameObject.SetActive(true);
                randomPropellers[1].gameObject.SetActive(true);
                randomPropellers[2].gameObject.SetActive(true);
                randomPropellers[3].gameObject.SetActive(true);
                break;
            case 10:
                randomPropellers[0].gameObject.SetActive(true);
                randomPropellers[1].gameObject.SetActive(true);
                randomPropellers[2].gameObject.SetActive(true);
                randomPropellers[3].gameObject.SetActive(true);
                RotateArena(new Vector3(0, 10, 0));
                break;
            default:
                break;
        }
    }//called from SpawnManager
    void GameScenario371()
    {      
        briefingText.text = "ЭПИЗОД 8: Растроение личности" + "\n" + "\n" +
            "Смотри в оба, эти ребята могут задавить толпой!";
    }
    void GameScenario381()
    {
        briefingText.text = "ЭПИЗОД 9: Полное безумие" + "\n" + "\n" +
            "Все противники, усилители и особенности Арены включены!";
    }
    public void GameSubScenario381()
    {
        int waveNumber = spawnManager.waveNumber;
        List<GameObject> randomPropellers = propellers;
        for (int i = 0; i < 4; i++)
        {
            randomPropellers[i].gameObject.SetActive(false);
        }
        //шаффлим список пропеллеров по-новой
        int count = randomPropellers.Count;
        int last = count - 1;
        for (int i = 0; i < last; ++i)
        {
            int r = Random.Range(i, count);
            GameObject tmp = randomPropellers[i];
            randomPropellers[i] = randomPropellers[r];
            randomPropellers[r] = tmp;
        }
        //шаффлим вращение арены по часовой или против часовой
        int arenaRot = 10;
        int j = Random.Range(0, 2); //0 или 1
        if (j == 0) arenaRot = -arenaRot;
        //включаем нужное количество случайных пропеллеров
        switch (waveNumber)
        {
            case 2:
                randomPropellers[0].gameObject.SetActive(true);
                break;
            case 3:
                randomPropellers[0].gameObject.SetActive(true);
                break;
            case 4:
                randomPropellers[0].gameObject.SetActive(true);
                randomPropellers[1].gameObject.SetActive(true);
                break;
            case 5:
                randomPropellers[0].gameObject.SetActive(true);
                randomPropellers[1].gameObject.SetActive(true);
                RotateArena(new Vector3(0, arenaRot, 0));
                break;
            case 6:
                randomPropellers[0].gameObject.SetActive(true);
                randomPropellers[1].gameObject.SetActive(true);
                RotateArena(new Vector3(0, arenaRot, 0));
                break;
            case 7:
                randomPropellers[0].gameObject.SetActive(true);
                randomPropellers[1].gameObject.SetActive(true);
                RotateArena(new Vector3(0, arenaRot, 0));
                break;
            case 8:
                randomPropellers[0].gameObject.SetActive(true);
                randomPropellers[1].gameObject.SetActive(true);
                RotateArena(new Vector3(0, arenaRot, 0));
                break;
            case 9:
                randomPropellers[0].gameObject.SetActive(true);
                randomPropellers[1].gameObject.SetActive(true);
                randomPropellers[2].gameObject.SetActive(true);
                RotateArena(new Vector3(0, arenaRot, 0));
                break;
            case 10:
                randomPropellers[0].gameObject.SetActive(true);
                randomPropellers[1].gameObject.SetActive(true);
                randomPropellers[2].gameObject.SetActive(true);                
                RotateArena(new Vector3(0, arenaRot, 0));
                break;
            default:
                break;
        }
    }//called from SpawnManager
    void GameScenario391()
    {
        briefingText.text = "ЭПИЗОД 10: Черная дыра" + "\n" + "\n" +
            "Осторожно! В этой схватке упасть можно не только с края Арены!";
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        AudioListener.pause = true;       

    }
    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        AudioListener.pause = false;        

    }
    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitToMainMenu()
    {   
        spawnManager.ChangeSpawnActivity();
        PlayerPrefs.SetInt("Level", 0);
        SceneManager.LoadScene(0);
    }
    public void CheckVictory() //called from SpawnManager on victory conditions meet
    {            
            Time.timeScale = 0f;
            victoryMenu.gameObject.SetActive(true);
            levelResult = 1;
            AudioListener.pause = true;

        //tutorial
        if (levelNumber == 1)
        {
            victoryText.text = "Поздравляю с победой в первом бою!" + "\n" + "\n" +
                "Теперь приступай к настоящим испытаниям!";
        }

        //campaign episode 1
        if (levelNumber == 301)
        {
            victoryText.text = "ЭПИЗОД 1 пройден!" + "\n" + "\n" +
                "Больше испытаний ждет впереди!" + "\n" +
                "ЭПИЗОД 2 разблокирован!";
            
            maxCampaignLevelNumber = 10;
            SetMCLN(maxCampaignLevelNumber);

            CheckStars();

            if (PlayerPrefs.HasKey("E1S"))
            {
                if (starsAmount >= PlayerPrefs.GetInt("E1S")) PlayerPrefs.SetInt("E1S", starsAmount);
            } else PlayerPrefs.SetInt("E1S", starsAmount);
            Debug.Log("Stars Amount in E1: " + starsAmount);
        }

        //campaign episode 2
        if (levelNumber == 311)
        {
            victoryText.text = "ЭПИЗОД 2 пройден!" + "\n" + "\n" +
                "Больше испытаний ждет впереди!" + "\n" +
                "ЭПИЗОД 3 разблокирован!";

            maxCampaignLevelNumber = 20;
            SetMCLN(maxCampaignLevelNumber);

            CheckStars();

            if (PlayerPrefs.HasKey("E2S"))
            {
                if (starsAmount >= PlayerPrefs.GetInt("E2S")) PlayerPrefs.SetInt("E2S", starsAmount);
            }
            else PlayerPrefs.SetInt("E2S", starsAmount);
            Debug.Log("Stars Amount in E2: " + starsAmount);
        }

        //campaign episode 3
        if (levelNumber == 321)
        {
            victoryText.text = "ЭПИЗОД 3 пройден!" + "\n" + "\n" +
                "Больше испытаний ждет впереди!";

            maxCampaignLevelNumber = 30;
            SetMCLN(maxCampaignLevelNumber);

            CheckStars();

            if (PlayerPrefs.HasKey("E3S"))
            {
                if (starsAmount >= PlayerPrefs.GetInt("E3S")) PlayerPrefs.SetInt("E3S", starsAmount);
            }
            else PlayerPrefs.SetInt("E3S", starsAmount);
            Debug.Log("Stars Amount in E3: " + starsAmount);
        }

        //campaign episode 4
        if (levelNumber == 331)
        {
            victoryText.text = "ЭПИЗОД 4 пройден!" + "\n" + "\n" +
                "Больше испытаний ждет впереди!";

            maxCampaignLevelNumber = 40;
            SetMCLN(maxCampaignLevelNumber);

            CheckStars();

            if (PlayerPrefs.HasKey("E4S"))
            {
                if (starsAmount >= PlayerPrefs.GetInt("E4S")) PlayerPrefs.SetInt("E4S", starsAmount);
            }
            else PlayerPrefs.SetInt("E4S", starsAmount);
            Debug.Log("Stars Amount in E4: " + starsAmount);
        }

        //campaign episode 5
        if (levelNumber == 341)
        {
            victoryText.text = "ЭПИЗОД 5 пройден!" + "\n" + "\n" +
                "Больше испытаний ждет впереди!";

            maxCampaignLevelNumber = 50;
            SetMCLN(maxCampaignLevelNumber);

            CheckStars();

            if (PlayerPrefs.HasKey("E5S"))
            {
                if (starsAmount >= PlayerPrefs.GetInt("E5S")) PlayerPrefs.SetInt("E5S", starsAmount);
            }
            else PlayerPrefs.SetInt("E5S", starsAmount);
            Debug.Log("Stars Amount in E5: " + starsAmount);
        }

        //campaign episode 6
        if (levelNumber == 351)
        {
            victoryText.text = "ЭПИЗОД 6 пройден!" + "\n" + "\n" +
                "Больше испытаний ждет впереди!";

            maxCampaignLevelNumber = 60;
            SetMCLN(maxCampaignLevelNumber);

            CheckStars();

            if (PlayerPrefs.HasKey("E6S"))
            {
                if (starsAmount >= PlayerPrefs.GetInt("E6S")) PlayerPrefs.SetInt("E6S", starsAmount);
            }
            else PlayerPrefs.SetInt("E6S", starsAmount);
            Debug.Log("Stars Amount in E6: " + starsAmount);
        }

        //campaign episode 7
        if (levelNumber == 361)
        {
            victoryText.text = "ЭПИЗОД 7 пройден!" + "\n" + "\n" +
                "Больше испытаний ждет впереди!";

            maxCampaignLevelNumber = 70;
            SetMCLN(maxCampaignLevelNumber);

            CheckStars();

            if (PlayerPrefs.HasKey("E7S"))
            {
                if (starsAmount >= PlayerPrefs.GetInt("E7S")) PlayerPrefs.SetInt("E7S", starsAmount);
            }
            else PlayerPrefs.SetInt("E7S", starsAmount);
            Debug.Log("Stars Amount in E7: " + starsAmount);
        }

        //campaign episode 8
        if (levelNumber == 371)
        {
            victoryText.text = "ЭПИЗОД 8 пройден!" + "\n" + "\n" +
                "Больше испытаний ждет впереди!";

            maxCampaignLevelNumber = 80;
            SetMCLN(maxCampaignLevelNumber);

            CheckStars();

            if (PlayerPrefs.HasKey("E8S"))
            {
                if (starsAmount >= PlayerPrefs.GetInt("E8S")) PlayerPrefs.SetInt("E8S", starsAmount);
            }
            else PlayerPrefs.SetInt("E8S", starsAmount);
            Debug.Log("Stars Amount in E8: " + starsAmount);
        }

        //campaign episode 9
        if (levelNumber == 381)
        {
            victoryText.text = "ЭПИЗОД 9 пройден!" + "\n" + "\n" +
                "Больше испытаний ждет впереди!";

            maxCampaignLevelNumber = 90;
            SetMCLN(maxCampaignLevelNumber);

            CheckStars();

            if (PlayerPrefs.HasKey("E9S"))
            {
                if (starsAmount >= PlayerPrefs.GetInt("E9S")) PlayerPrefs.SetInt("E9S", starsAmount);
            }
            else PlayerPrefs.SetInt("E9S", starsAmount);
            Debug.Log("Stars Amount in E9: " + starsAmount);
        }

        //campaign episode 10
        if (levelNumber == 391)
        {
            victoryText.text = "ЭПИЗОД 10 пройден!" + "\n" + "\n" +
                "Больше испытаний ждет впереди!";

            maxCampaignLevelNumber = 100;
            SetMCLN(maxCampaignLevelNumber);            

            CheckStars();

            if (PlayerPrefs.HasKey("E10S"))
            {
                if (starsAmount >= PlayerPrefs.GetInt("E10S")) PlayerPrefs.SetInt("E10S", starsAmount);
            }
            else PlayerPrefs.SetInt("E10S", starsAmount);
            Debug.Log("Stars Amount in E10: " + starsAmount);
        }

    }
    void SetMCLN(int MCLN)
    {
        if (PlayerPrefs.HasKey("MCLN"))
        {
            int currentRecord = PlayerPrefs.GetInt("MCLN");
            if (MCLN >= currentRecord) PlayerPrefs.SetInt("MCLN", MCLN);
        }
        else PlayerPrefs.SetInt("MCLN", MCLN);
    }
    void CheckStars()
    {
        if (playerController.falls == 0)
        {
            victoryStar3.color = Color.yellow;
            victoryStar2.color = Color.yellow;
            victoryStar1.color = Color.yellow;
            starsAmount = 3;

        }
        if (playerController.falls == 1)
        {
            victoryStar2.color = Color.yellow;
            victoryStar1.color = Color.yellow;
            starsAmount = 2;
        }            
        if (playerController.falls >= 2)
        {
            victoryStar1.color = Color.yellow;
            starsAmount = 1;
        }
            
    }
    public void CheckDefeat()
    {   
        if (levelNumber != 1 && playerController.lives < 0)
        {            
            Time.timeScale = 0f;
            defeatMenu.gameObject.SetActive(true);
            levelResult = -1;
            AudioListener.pause = true;
        }
        else return;

        if (levelNumber == 2) defeatText.text = "(Здесь должно быть написано что-то утешающе-одобряющее, мол, ты молодец, вон до какой волны добраться удалось, " +
                "но все же это фейл, ну а что ты хочешь от бесконечного режима, но ты не расстраивайся, попробуй еще)";
        else defeatText.text = "Битва проиграна! Попробуй еще раз!";
    }
    void RotateArena(Vector3 rotation)
    {
        arenaRotator.RotateAmount = rotation;
    }
    
}
