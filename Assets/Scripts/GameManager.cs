using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BlitheFramework;

public class GameManager : BaseClass
{
    #region Initialize
    #region EVENT
    public event EventHandler EVENT_REMOVE;
    #endregion EVENT

    #region Public_field
    public Text text_timer;
    public Button btn_mainButton;
    #endregion Public_field

    #region Pivate_field
    private System.Random random;
    private int timer, nextPlatformSpawnTime;

    private const float PLATFORM_SPEED = 0.05f;
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
        CreateFactoryPlayer();
        //InitPlayers();
        Invoke("InitPlayers", 5);
        CreateFactoryPlatform();
    }
    #region factory
    [SerializeField] private GameObject prefabPlayer;
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
        factoryPlayer.Add(prefabPlayer, new Vector3(-1, 0), Quaternion.identity, 1, true);
        factoryPlayer.Add(prefabPlayer, new Vector3(1, 0), Quaternion.identity, 2, false);
    }
    private void SetPlatformSpawnerCounter()
    {
        nextPlatformSpawnTime = random.Next(MIN_PLATFORM_SPAWN_TIME, MAX_PLATFORM_SPAWN_TIME);
    }
    private void SpawnPlatform()
    {
        float xPos = random.Next(-200, 200);
        //Debug.Log(xPos);
        factoryPlatform.Add(prefabPlatform, new Vector3(xPos/100, 5.3f), Quaternion.identity, PLATFORM_SPEED);
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
        text_timer.text = timer.ToString() + " ; Next Spawn at" + nextPlatformSpawnTime+ "\n"+Input.acceleration;
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
                return true;
            }
        }
        return false;
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
        if (!CheckEndGame())
        {
            CheckSpawnTime();
            UpdateTimer();
            UpdatePlatforms();
            UpdatePlayers();
        }
    }
    #endregion
}