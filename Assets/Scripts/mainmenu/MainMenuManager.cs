using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject panel_main;
    [SerializeField] private GameObject panel_quitGame;
    void Start()
    {
        
    }

    public void StartQuickGame()
    {

    }

    public void ShowQuitConfirmation(bool _active)
    {
        panel_quitGame.SetActive(_active);
        panel_main.SetActive(!_active);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
