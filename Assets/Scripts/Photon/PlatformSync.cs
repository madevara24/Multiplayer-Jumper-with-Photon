using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlatformSync : MonoBehaviourPun, IPunObservable
{
    public List<MonoBehaviour> localScripts;
    public GameObject[] localObject;

    [SerializeField] GameManager gameManager;

    private Vector3 latestPos;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (photonView.IsMine)
        {

        }
        else
        {
            for (int i = 0; i < localScripts.Capacity; i++)
            {
                localScripts[i].enabled = false;
            }
            for (int i = 0; i < localObject.Length; i++)
            {
                localObject[i].SetActive(false);
            }
        }
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector2.Lerp(transform.position, latestPos, Time.deltaTime * 5);
            if (gameManager.Endgame)
            {
                Destroy(this);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            latestPos = (Vector3)stream.ReceiveNext();
        }
    }
}
