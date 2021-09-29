using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject Transporter;
    public GameObject Miner;
    public GameObject SuperWorker;
    public GameObject Parent;
    public GameObject finishScreen;
    public GameObject mainScreen;
    public GameObject startScreen;
    public GameObject Build;
    public GameObject TransporterParent;
    public GameObject MinerParent;

    public float waitfor = 5f;
    public float percent;
    public int ObjectCount=0;
    public int TransporterCount=1;
    public int MinerCount=1;
    public int currentMoney;
    
    public Text LevelText;

    public Button superworkerButton;
    
    public List <GameObject> Object;

    public Image levelBar;

    void Awake() 
    {
        instance = this;
    }
    public static bool isGameStarted = false;
    public static bool isGameEnded= false;

    void Start()
    {
        //ObjectCount=0;
        //PlayerPrefs.SetInt("LevelID", ObjectCount); 
        GetLevel();

    }

    void Update()
    {
        LevelText.text="Level "+(ObjectCount+1).ToString();
        //MoneyManager.instance.SuperworkerCheck();
    }

    public void OnLevelStarted()
    {
        isGameStarted = true;
        mainScreen.SetActive(true);
        startScreen.SetActive(false);
        SpawnTransporter();
        SpawnMiner();
        Load();
    }

    public void OnLevelEnded() // Game Over?
    {
        
    }

    public void OnLevelCompleted() // Loads the next level
    {
        mainScreen.SetActive(false);
        finishScreen.SetActive(true);
        isGameEnded = true;
    }

    public void OnLevelFailed() // Loads the current scene back
    {
        
    }

    public void SpawnTransporter()
    {
        Instantiate(Transporter,new Vector3(6.5f,0.05268812f,10f),Quaternion.Euler(0,190,0),TransporterParent.transform);
    }

    public void SpawnMiner()
    {
        Instantiate(Miner,new Vector3(-16f,0f,-6f),Quaternion.identity,MinerParent.transform);
    }

    public void PowerButton()
    {
        waitfor-=0.1f;
        MoneyManager.instance.PowerCheck();
    }

    public void SpawnSuperWorker()
    {
        Instantiate(SuperWorker,new Vector3(6.5f,0f,10f),Quaternion.identity);
    }

    public void NextLevel ()
    {
        Save();
        ObjectCount++;
        PlayerPrefs.SetInt("LevelID", ObjectCount); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isGameEnded=false;
    }

    public void GetLevel()
    {
        ObjectCount = PlayerPrefs.GetInt("LevelID", 0); 
        if (ObjectCount> Object.Count -1 || ObjectCount <0) 
        {
            TransporterCount=1;
            MinerCount=1;
            currentMoney=0;
            PlayerPrefs.SetInt("currentMoney", currentMoney);
            PlayerPrefs.SetInt("TransporterCount", TransporterCount);
            PlayerPrefs.SetInt("MinerCount", MinerCount);
            ObjectCount = 0;
            PlayerPrefs.SetInt("LevelID", ObjectCount);
        }

        Instantiate(Object[ObjectCount],Vector3.zero, Quaternion.identity,Build.transform);

        if (!(ObjectCount == 4 || ObjectCount == 9))
        {
            superworkerButton.gameObject.SetActive(false);
        }

        if (ObjectCount == 4 || ObjectCount == 9)
        {
            superworkerButton.gameObject.SetActive(true);
            superworkerButton.interactable = false;
        }
    }

    public void Save()
    {
        currentMoney=MoneyManager.instance.currentMoney;
        TransporterCount=TransporterParent.GetComponent<Transform>().gameObject.transform.childCount;
        //Debug.Log("transporter sayısı"+TransporterCount);
        PlayerPrefs.SetInt("TransporterCount", TransporterCount);
        MinerCount=MinerParent.GetComponent<Transform>().gameObject.transform.childCount;
        PlayerPrefs.SetInt("MinerCount", MinerCount);
        PlayerPrefs.SetInt("currentMoney", currentMoney);
    }
    public void Load()
    {
        MoneyManager.instance.currentMoney=PlayerPrefs.GetInt("currentMoney", 0); 
        TransporterCount = PlayerPrefs.GetInt("TransporterCount", 0); 
        MinerCount = PlayerPrefs.GetInt("MinerCount", 0); 
        for(int i=0;i<TransporterCount/3-1;i++){
            SpawnTransporter();
        }
        for(int i=0;i<MinerCount/3-1;i++){
            SpawnMiner();
        }
    }

    public void level(float fillAmount)
    {
        levelBar.fillAmount = fillAmount;
        percent = fillAmount;
        if(percent>=0.5f)
        {
            MoneyManager.instance.CheckMoney();
        }
    }
}
