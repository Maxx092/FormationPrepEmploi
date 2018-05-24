using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private int coinNumber;
    [SerializeField]
    private int coinGoal = 12;
    private int objectifCount = 0;
    private int playerLife;
    private static float timer;
    private int timerSeconds;

    private int timerMinutes;
    private string zeroSeconds;
    private string zeroMinutes;
    private float timerGoal = 30.0f;
    private bool quest1Completed;
    private bool quest2Completed;
    private bool quest3Completed;

    public Text coinText;
    public Text lifeText;
    public Text timerText;
    public RawImage questStar1;
    public RawImage questStar2;
    public RawImage questStar3;
    public RawImage winImage;
    public RawImage loseImage;
    public Button restartButton;
    public Coin[] coinArray;
    public Enemy[] enemyArray;
    public GameObject player;


    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    void Start()
    {
        Init();
        playerLife = 3;
        questStar1.gameObject.SetActive(false);
        questStar2.gameObject.SetActive(false);
        questStar3.gameObject.SetActive(false);
        winImage.gameObject.SetActive(false);
        loseImage.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    void Update()
    {
        coinText.text = "Coins : " + coinNumber.ToString() + " / " + coinGoal.ToString();
        lifeText.text = "Life : " + playerLife.ToString();
        
        if (!quest1Completed)
        {
            if (coinNumber == coinGoal)
            {
                GetQuest(1);
            }
        }

        Timer();
    }

    public void Init()
    {
        timer = 0.0f;
        coinNumber = 0;

        foreach (Coin aCoin in coinArray)
        {
            aCoin.gameObject.SetActive(true);
        }

        foreach (Enemy aEnemy in enemyArray)
        {
            aEnemy.gameObject.SetActive(true);
        }
    }

    private void Timer()
    {
        timer += Time.deltaTime;

        if (timer >= 60)
        {
            timer -= 60.0f;
            timerMinutes++;
        }

        timerSeconds = (int)timer;

        if (timerSeconds < 10.0f)
        {
            zeroSeconds = "0";
        }
        else
        {
            zeroSeconds = "";
        }

        if (timerMinutes < 10)
        {
            zeroMinutes = "0";
        }
        else
        {
            zeroMinutes = "";
        }
        timerText.text = zeroMinutes + timerMinutes.ToString() + ":" + zeroSeconds + timerSeconds.ToString();
    }

    public void TakeCoin(Collider aCoin)
    {
        aCoin.gameObject.SetActive(false);
        coinNumber++;
    }

    public void LifeLost()
    {
        playerLife--;

        if (playerLife == 0)
        {
            player.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);

            loseImage.gameObject.SetActive(!quest1Completed || !quest2Completed || !quest3Completed);
            winImage.gameObject.SetActive(quest1Completed && quest2Completed && quest3Completed);
        }
    }

    private void GetQuest(int aQuest)
    {
        if (aQuest == 1)
        {
            questStar1.gameObject.SetActive(true);
            quest1Completed = true;
        }
        else if (aQuest == 2)
        {
            questStar2.gameObject.SetActive(true);
            quest2Completed = true;
        }
        else if (aQuest == 3)
        {
            questStar3.gameObject.SetActive(true);
            quest3Completed = true;
        }

        if (quest1Completed && quest2Completed && quest3Completed)
        {
            winImage.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            player.gameObject.SetActive(false);
        }
    }

    public void GoalReach()
    {
        if (timer <= timerGoal)
        {
            GetQuest(3);
        }               // ajouter aussi le 00:00 minute dans le timer
        Init();
        GetQuest(2);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
