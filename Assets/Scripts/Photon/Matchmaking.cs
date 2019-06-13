using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Matchmaking : MonoBehaviour
{
    public Text countPlayer;
    public GameObject[] player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countPlayer.text = PhotonNetwork.CurrentRoom.PlayerCount + " Player In Room";

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            player[0].SetActive(true);
        } else
        {
            player[0].SetActive(true);
            player[1].SetActive(true);
            StartCoroutine(AfterTwoPlayer());
        }
    }

    IEnumerator AfterTwoPlayer()
    {
        yield return new WaitForSeconds(3f);
        PhotonNetwork.LoadLevel("Core");
    }

}
