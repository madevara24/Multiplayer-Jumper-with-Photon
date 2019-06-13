using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

namespace Sepay
{
    public class RoomController : MonoBehaviourPunCallbacks
    {

        public UnityEngine.UI.Text ping;
        public GameObject gm;

        //public Transform spawnPoint;
        //public Text countRoom;

        // Start is called before the first frame update
        void Start()
        {
            if(PhotonNetwork.CurrentRoom == null)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                return;
            }
        }

        // Update is called once per frame
        void Update()
        {
            ping.text = "Ping : " + PhotonNetwork.GetPing().ToString();
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                gm.SetActive(true);
            }
        }
    }

}
