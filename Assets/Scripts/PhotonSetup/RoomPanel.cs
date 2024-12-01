using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomPanel : MonoBehaviour
{
    [SerializeField] private GameObject startButtonObject;
    private TMP_Text roomCode;
    private PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();

        if (!pv.IsMine)
        {
            gameObject.SetActive(false);
        }

        roomCode = transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>();

        roomCode.text = "Join code: " + PhotonNetwork.CurrentRoom.Name;

        InvokeRepeating("UpdatePlayerList", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //if esc key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("Join Menu");
        }
        //if 0 key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            startButtonObject.SetActive(true);
        }
    }

    void UpdatePlayerList()
    {
        //if there are 2 players in the room
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Invoke("StartButtonPress", 1f);
            }
        }
    }

    public void StartButtonPress()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //var hash = PhotonNetwork.CurrentRoom.CustomProperties;
            //hash["started"] = true;
            //PhotonNetwork.CurrentRoom.SetCustomProperties(hash);

            var hash = PhotonNetwork.CurrentRoom.CustomProperties;
            hash["started"] = true;
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);

            pv.RPC("EnterGame", RpcTarget.All);
        }
    }

    [PunRPC]
    void EnterGame()
    {
        //SceneData.Instance.started = false;
        PhotonNetwork.LoadLevel("Game");
    }
}
