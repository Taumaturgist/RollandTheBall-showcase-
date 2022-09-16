using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// ����� ��� ���������� ������� ������.
/// ������� ���� ������ ��������� ������
/// ����� ������ ��������� ������, ������������ �� ��� ���� ��������
/// </summary>
public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    public GameObject enemyBoss1;    
    public GameObject enemyBoss2;
    public TextMeshProUGUI waveNumberText;
    public int waveNumber = 1;

    private int spawnScenario;    
    private NorthCube northCube;
    private float spawnRange = 7.0f;     
    private bool spawnIsActive;
    private int bodyCount;
    private int bossBodyCount;
    private int maxWave;


    private void Awake()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            spawnScenario = PlayerPrefs.GetInt("Level");
            Debug.Log("Spawn Scenario: " + spawnScenario);
        }
        if (PlayerPrefs.HasKey("MaxWave")) maxWave = PlayerPrefs.GetInt("MaxWave");
    }    
    void Start()
    {        
        if (spawnScenario == 1)
        {
            northCube = GameObject.Find("NorthCube").GetComponent<NorthCube>();
            ChangeSpawnActivity();
        } 
        else Invoke("ChangeSpawnActivity", 3);
        
    }
    void ChangeSpawnActivity()
    {
        spawnIsActive = !spawnIsActive;
        if (waveNumber == 1) SpawnScenarioSwitch();
    }
    public void BodyCount()
    {
        bodyCount++;
        if (spawnScenario == 1) SpawnScenario1();

    }
    public void BossBodyCount()
    {
        bossBodyCount++;
        Debug.Log("Boss clones killed: " + bossBodyCount);
    }
    public void CheckEnemyPresence()
    {
        int enemyCount = FindObjectsOfType<Enemy>().Length;
        Debug.Log("Enemies left on Arena: " + enemyCount);
        if (enemyCount == 0) SpawnScenarioSwitch();
    }
    void SpawnScenarioSwitch()
    {
        switch (spawnScenario)
        {
            case 1:
                SpawnScenario1();
                break;
            case 2:
                SpawnScenario2();
                break;
            case 301:
                SpawnScenario301();
                break;
            case 311:
                SpawnScenario311();
                break;
            case 321:
                SpawnScenario321();
                break;
            case 331:
                SpawnScenario331();
                break;
            case 341:
                SpawnScenario341();
                break;
            case 351:
                SpawnScenario351();
                break;
            case 361:
                SpawnScenario361();
                break;
            case 371:
                SpawnScenario371();
                break;
            case 381:
                SpawnScenario381();
                break;
            case 391:
                SpawnScenario391();
                break;
        }
    }
    //-----������ ������ ���������� ������-----
    void SpawnEnemyWave(int enemiesToSpawn) //������� ������� ������ �� �����������
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefabs[0], GenerateSpawnPosition(), enemyPrefabs[0].transform.rotation);
        }
    }
    void SpawnFatEnemy() //������� ��������������� �������� �����
    {
        Instantiate(enemyPrefabs[1], GenerateSpawnPosition(), enemyPrefabs[1].transform.rotation);
    }
    void SpawnFatEnemy(int enemiesToSpawn) //������� ��������������� �������� - ����������
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefabs[1], GenerateSpawnPosition(), enemyPrefabs[1].transform.rotation);
        }

    }
    void SpawnBasicFatEnemies(int enemiesToSpawn) //������� ���������� ����� �� �������
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemyIndex = Random.Range(0, 1);
            Instantiate(enemyPrefabs[randomEnemyIndex], GenerateSpawnPosition(), enemyPrefabs[randomEnemyIndex].transform.rotation);
        }
    }
    void SpawnTripleEnemy() //������� ��������������� ����� � �����������
    {
        Instantiate(enemyPrefabs[2], GenerateSpawnPosition(), enemyPrefabs[2].transform.rotation);
    }
    void SpawnTripleEnemy(int enemiesToSpawn) //������� ��������������� ����� � ����������� - ����������
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefabs[2], GenerateSpawnPosition(), enemyPrefabs[2].transform.rotation);
        }
        
    }
    void SpawnBoss1Enemy() //������� ��������������� ����� 1
    {
        Instantiate(enemyBoss1, GenerateSpawnPosition(), enemyBoss1.transform.rotation);
    }
    void SpawnBoss2Enemy() //������� ��������������� ����� 2
    {
        Instantiate(enemyBoss2, GenerateSpawnPosition(), enemyBoss2.transform.rotation);
    }
    void SpawnSurvival(int enemiesToSpawn) //������� ���������� ����� �� �������
    {                 
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
                Instantiate(enemyPrefabs[randomEnemyIndex], GenerateSpawnPosition(), enemyPrefabs[randomEnemyIndex].transform.rotation);                
            }        
    }
    void SpawnRandomPowerUp()
    {
        int randomPowerUpIndex = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerUpIndex], GenerateSpawnPosition(), powerupPrefabs[randomPowerUpIndex].transform.rotation);
    }
    void SpawnYellowPowerUp()
    {
        Instantiate(powerupPrefabs[0], GenerateSpawnPosition(), powerupPrefabs[0].transform.rotation);
    }
    void SpawnRedPowerUp()
    {        
        Instantiate(powerupPrefabs[1], GenerateSpawnPosition(), powerupPrefabs[1].transform.rotation);
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
    //-----������ ��� ���������� ��������� ������-----
    public void SpawnScenario1() //����������
    {
        if (northCube.isTriggered && spawnIsActive && spawnScenario == 1) 
        {            
            SpawnEnemyWave(waveNumber);            
            spawnIsActive = false;
            
            
        }
        if (bodyCount > 0 && spawnScenario == 1)
        {
            SendMessage("CheckVictory");
            Debug.Log("SpawnManager checks Victory Condition");
        }      

    }
    
    void SpawnScenario2() //���������
    {
        waveNumberText.gameObject.SetActive(true);       
            
              
        if (spawnIsActive)
        {
            SendMessage("GameScenario2");
            SpawnSurvival(waveNumber);
            if (waveNumber % 2 == 0) SpawnRandomPowerUp();
            if (waveNumber > maxWave) PlayerPrefs.SetInt("MaxWave", waveNumber);
            waveNumberText.text = "�����: " + waveNumber;
            waveNumber++;
        }  
        
    }
    void SpawnScenario301() //������ 1 �������� "�������� �� 10"
    {
        waveNumberText.gameObject.SetActive(true);
        if (waveNumber == 11)
        {
            SendMessage("CheckVictory");
            Debug.Log("SpawnManager checks Victory Condition");

        }
        if (spawnIsActive)
        {
            SpawnEnemyWave(waveNumber);
            waveNumberText.text = "�����: " + waveNumber;
            waveNumber++;
            if (waveNumber == 11)
            {
                spawnIsActive = false;
                Debug.Log("Spawn is stopped due to last wave: " + waveNumber);
            }
        }
    }
    void SpawnScenario311() //������ 2 ��������
    {
        waveNumberText.gameObject.SetActive(true);
        if (waveNumber == 11)
        {
            SendMessage("CheckVictory");
            Debug.Log("SpawnManager checks Victory Condition");

        }
        if (spawnIsActive)
        {
            SpawnEnemyWave(waveNumber);
            SpawnFatEnemy();
            waveNumberText.text = "�����: " + waveNumber;
            waveNumber++;
            if (waveNumber == 11)
            {
                spawnIsActive = false;
                Debug.Log("Spawn is stopped due to last wave: " + waveNumber);
            }    
        }
    }
    void SpawnScenario321() //������ 3 �������� "��������� ��������"
    {
        waveNumberText.gameObject.SetActive(true);
        if (waveNumber == 11)
        {
            SendMessage("CheckVictory");
            Debug.Log("SpawnManager checks Victory Condition");

        }
        if (spawnIsActive)
        {
            SpawnEnemyWave(waveNumber * 2);
            SpawnFatEnemy();
            SpawnYellowPowerUp();
            waveNumberText.text = "�����: " + waveNumber;
            waveNumber++;
            if (waveNumber == 11)
            {
                spawnIsActive = false;
                Debug.Log("Spawn is stopped due to last wave: " + waveNumber);
            } 
        }
    }
    void SpawnScenario331() //������ 4 �������� "�����������������"
    {
        waveNumberText.gameObject.SetActive(true);
        if (waveNumber == 11)
        {
            SendMessage("CheckVictory");
            Debug.Log("SpawnManager checks Victory Condition");

        }
        if (spawnIsActive)
        {
            SpawnEnemyWave(waveNumber * 2);
            SpawnFatEnemy();
            SpawnYellowPowerUp();
            waveNumberText.text = "�����: " + waveNumber;
            waveNumber++;
            if (waveNumber == 11)
            {
                spawnIsActive = false;
                Debug.Log("Spawn is stopped due to last wave: " + waveNumber);
            } 

        }
    }
    void SpawnScenario341() //������ 5 "��� ����� ����?"
    {
        if (spawnIsActive)
        {
            SpawnBoss1Enemy();
        }
            

        if (bossBodyCount >= 31)
        {
            SendMessage("CheckVictory");
            Debug.Log("SpawnManager checks Victory Condition");
        }
    }
    void SpawnScenario351() //������ 6 "����� ������ ����"
    {
        waveNumberText.gameObject.SetActive(true);
        if (waveNumber == 11)
        {
            SendMessage("CheckVictory");
            Debug.Log("SpawnManager checks Victory Condition");

        }

        if (spawnIsActive)
        {
            SpawnFatEnemy(waveNumber + 1);
            SpawnRedPowerUp();
            waveNumberText.text = "�����: " + waveNumber;
            waveNumber++;
            if (waveNumber == 11)
            {
                spawnIsActive = false;
                Debug.Log("Spawn is stopped due to last wave: " + waveNumber);
            }
        }
        
    }
    void SpawnScenario361() //������ 7 "������ �� ����������"
    {
        waveNumberText.gameObject.SetActive(true);
        if (waveNumber == 11)
        {
            SendMessage("CheckVictory");
            Debug.Log("SpawnManager checks Victory Condition");
        }
        if (spawnIsActive)
        {
            SpawnBasicFatEnemies(waveNumber);
            SpawnRandomPowerUp();
            waveNumberText.text = "�����: " + waveNumber;
            if (waveNumber > 1) SendMessage("GameSubScenario361");
            waveNumber++;
        }

    }

    void SpawnScenario371() //������ 8 "���������� ��������"
    {
        waveNumberText.gameObject.SetActive(true);
        if (waveNumber == 11)
        {
            SendMessage("CheckVictory");
            Debug.Log("SpawnManager checks Victory Condition");
        }       

        if (spawnIsActive)
        {
            SpawnTripleEnemy(waveNumber);
            SpawnRandomPowerUp();
            waveNumberText.text = "�����: " + waveNumber;
            waveNumber++;
            if (waveNumber == 11)
            {
                spawnIsActive = false;
                Debug.Log("Spawn is stopped due to last wave: " + waveNumber);
            }
        }
    }
    void SpawnScenario381() //������ 9 "������ �������"
    {
        waveNumberText.gameObject.SetActive(true);
        if (waveNumber == 11)
        {
            SendMessage("CheckVictory");
            Debug.Log("SpawnManager checks Victory Condition");
        }
        if (spawnIsActive)
        {
            SpawnSurvival(waveNumber);
            SpawnRandomPowerUp();
            waveNumberText.text = "�����: " + waveNumber;
            if (waveNumber > 1) SendMessage("GameSubScenario381");
            waveNumber++;
            if (waveNumber == 11)
            {
                spawnIsActive = false;
                Debug.Log("Spawn is stopped due to last wave: " + waveNumber);
            } 
        }
    }
    void SpawnScenario391() //������ 10 "������ ����"
    {
        if (spawnIsActive)
        {
            SpawnBoss2Enemy();
        }

        if (bossBodyCount >= 1)
        {
            SendMessage("CheckVictory");
            Debug.Log("SpawnManager checks Victory Condition");
        }
    }


}
