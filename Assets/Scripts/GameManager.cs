using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BlitheFramework;
using Photon.Pun;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GameManager : BaseClass
{
    #region Initialize
    #region EVENT
    public event EventHandler EVENT_REMOVE;
    #endregion EVENT

    #region Public_field
    public Text text_timer;
    public Button btn_mainButton;
    public bool Endgame { get => endgame; set => endgame = value; }

    [SerializeField] GameObject canvasEndgame;
    [SerializeField] Text popUp_Score, popUp_Status;
    #endregion Public_field

    #region Pivate_field
    private System.Random random;
    private int timer, nextPlatformSpawnTime, score, winner, homeScore, awayScore;
    private bool endgame;

    private const float PLATFORM_SPEED = 0.03f;
    private const int MIN_PLATFORM_SPAWN_TIME = 50;
    private const int MAX_PLATFORM_SPAWN_TIME = 70;
    #endregion Pivate_field
    #endregion Initialize

    public override void Init()
    {

    }

    void Start()
    {
        random = new System.Random();
        timer = nextPlatformSpawnTime = 0;
        endgame = false;
        canvasEndgame.SetActive(false);
        CreateFactoryPlayer();
        Invoke("InitPlayers", 5);
        if (PhotonNetwork.IsMasterClient)
        {
            CreateFactoryPlatform();
        }
        
    }
    #region factory
    [SerializeField] private GameObject[] prefabPlayer;
    FactoryPlayer factoryPlayer;
    private void CreateFactoryPlayer()
    {
        var go = new GameObject();
        go.name = "FactoryPlayer";
        factoryPlayer = new FactoryPlayer();
        factoryPlayer = go.AddComponent<FactoryPlayer>() as FactoryPlayer;
        #region EVENT_LISTENER_ADD_FactoryPlayer
        factoryPlayer.EVENT_REMOVE += OnRemoveFactoryPlayer;
        #endregion EVENT_LISTENER_ADD_FactoryPlayer    
}

    [SerializeField] private GameObject prefabPlatform;
    FactoryPlatform factoryPlatform;


    private void CreateFactoryPlatform()
    {
        var go = new GameObject();
        go.name = "FactoryPlatform";
        factoryPlatform = new FactoryPlatform();
        factoryPlatform = go.AddComponent<FactoryPlatform>() as FactoryPlatform;
        #region EVENT_LISTENER_ADD_FactoryPlatform
        factoryPlatform.EVENT_REMOVE += OnRemoveFactoryPlatform;
        #endregion EVENT_LISTENER_ADD_FactoryPlatform    
}

    #region EVENT_LISTENER_ADD
    #endregion EVENT_LISTENER_ADD
    #region EVENT_LISTENER_METHOD
    private void OnRemoveFactoryPlayer(object _sender, EventArgs e)
    {
        GameObject sender = (GameObject)_sender;
        #region EVENT_LISTENER_REMOVE_FactoryPlayer
        sender.GetComponent<FactoryPlayer>().EVENT_REMOVE -= OnRemoveFactoryPlayer;
        #endregion EVENT_LISTENER_REMOVE_FactoryPlayer
        Destroy(sender);
    }

    private void OnRemoveFactoryPlatform(object _sender, EventArgs e)
    {
        GameObject sender = (GameObject)_sender;
        #region EVENT_LISTENER_REMOVE_FactoryPlatform
        sender.GetComponent<FactoryPlatform>().EVENT_REMOVE -= OnRemoveFactoryPlatform;
        #endregion EVENT_LISTENER_REMOVE_FactoryPlatform
        Destroy(sender);
    }

    #endregion EVENT_LISTENER_METHOD
    #endregion factory
    #region private method
    private void InitPlayers()
    {
        score = winner = 0;
        if (PhotonNetwork.IsMasterClient)
        {
            factoryPlayer.Add(prefabPlayer[0], new Vector3(-1, 4), Quaternion.identity, 1);
        }
        else
        {
            factoryPlayer.Add(prefabPlayer[1], new Vector3(1, 4), Quaternion.identity, 2);
        }
    }
    private void SetPlatformSpawnerCounter()
    {
        nextPlatformSpawnTime = random.Next(MIN_PLATFORM_SPAWN_TIME, MAX_PLATFORM_SPAWN_TIME);
    }
    private void SpawnPlatform()
    {
        float xPos = random.Next(-200, 200);
        factoryPlatform.Add(prefabPlatform, new Vector3(xPos / 100, 5.3f), Quaternion.identity, PLATFORM_SPEED);
    }
    private void UpdatePlatforms()
    {
        for (int i = 0; i < factoryPlatform.GetNumberOfObjectFactories(); i++)
        {
            factoryPlatform.Get(i).UpdateMethod();
        }
    }
    private void UpdateTimer()
    {
        timer++;
        //text_timer.text = timer.ToString() + " ; Next Spawn at" + nextPlatformSpawnTime+ "\n"+Input.acceleration;
    }

    private void CheckSpawnTime()
    {
        if (timer == nextPlatformSpawnTime)
        {
            SpawnPlatform();
            SetPlatformSpawnerCounter();
            timer = 0;
        }
    }

    private void UpdatePlayers()
    {
        for (int i = 0; i < factoryPlayer.GetNumberOfObjectFactories(); i++)
        {
            factoryPlayer.Get(i).UpdateMethod();
        }
    }

    private bool CheckEndGame()
    {
        for (int i = 0; i < factoryPlayer.GetNumberOfObjectFactories(); i++)
        {
            if (factoryPlayer.Get(i).transform.position.y < -5.4f)
            {
                InitEndgame(PhotonNetwork.IsMasterClient ? 2 : 1);
                return true;
            }
        }
        return false;
    }

    public void InitEndgame(int _winner)
    {
        if(!endgame)
        {
            winner = _winner;
            endgame = true;
            if (_winner == 1)
            {
                homeScore = score;
                awayScore = score / 2;
            }
            else
            {
                homeScore = score / 2;
                awayScore = score;
            }
            ClearAllObjects();
            InitPopUpEndgame();
            UpdateLeaderboard();
        }
    }

    private void ClearAllObjects()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < factoryPlatform.GetNumberOfObjectFactories(); i++)
            {
                factoryPlatform.Get(i).Remove();
            }
        }

        for (int i = 0; i < factoryPlayer.GetNumberOfObjectFactories(); i++)
        {
            factoryPlayer.Get(i).Remove();
        }
    }
    private void InitPopUpEndgame()
    {
        Debug.Log("winner : " + winner);
        canvasEndgame.SetActive(true);
        if (PhotonNetwork.IsMasterClient)
        {
            popUp_Score.text = homeScore.ToString();
            if (winner == 1)
                popUp_Status.text = "Victory";
            else
                popUp_Status.text = "Defeat";
        }
        else
        {
            popUp_Score.text = awayScore.ToString();
            if (winner == 2)
                popUp_Status.text = "Victory";
            else
                popUp_Status.text = "Defeat";
        }
    }

    private void UpdateLeaderboard()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PlayGamesPlatform.Instance.ReportScore(homeScore, GPGSIds.leaderboardScore, (bool success) =>
             {

             });
        }
        else
        {
            PlayGamesPlatform.Instance.ReportScore(awayScore, GPGSIds.leaderboardScore, (bool success) =>
            {

            });
        }
    }

    
    #endregion
    #region public method

    public void Remove()
    {
       dispatchEvent(EVENT_REMOVE, this.gameObject, EventArgs.Empty);
    }
    #endregion
    #region update
    public void FixedUpdate()
    {
        if (!endgame)
        {
            CheckEndGame();
            if (PhotonNetwork.IsMasterClient)
            {
                CheckSpawnTime();
                UpdatePlatforms();
            }
            UpdateTimer();
            UpdatePlayers();
            score++;
        }
    }

    public void OnClikContinueButton()
    {
        PhotonNetwork.LeaveRoom();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    #endregion
}