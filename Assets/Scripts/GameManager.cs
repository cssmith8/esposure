using DefaultNamespace;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

//set hashtable
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviourPunCallbacks
{
    //Singleton
    [HideInInspector] public static GameManager localInstance;
    
    [HideInInspector] private static PhotonView pv;
    [HideInInspector] private static PhotonView enemypv;

    [HideInInspector] public bool submitted = false;

    [HideInInspector] public static bool playerOne = true;

    void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            //This is the local player
            pv = GetComponent<PhotonView>();
            localInstance = this;
            playerOne = PhotonNetwork.IsMasterClient;
        } else
        {
            //This is the enemy player
            enemypv = GetComponent<PhotonView>();
        }

    }

    void Update()
    {
        //if esc key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GetComponent<PhotonView>().IsMine) return;
            //quit the game
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
            //quit the application
            Application.Quit();
        }
    }

    public void SelectCard(int index)
    {
        pv.RPC("EnemySelect", enemypv.Owner, index);
    }

    [PunRPC]
    private void EnemySelect(int index)
    {
        EnemyHand.instance.SelectCard(index);
    }

    public void Submit()
    {
        submitted = true;
        int localid = LocalHand.instance.GetSelectedCard().GetComponent<LocalCard>().RoleID;
        pv.RPC("OnEnemySubmit", enemypv.Owner, localid);
        //update the room variable
        Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        hash["submitted"] = (int) hash["submitted"] + 1;
        
        //if both players have submitted
        if ((int)hash["submitted"] == 2)
        {
            pv.RPC("OnBothSubmit", RpcTarget.All);
            hash["submitted"] = 0;
            hash["challenge"] = Random.Range(0, 150);
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    [PunRPC]
    private void OnEnemySubmit(int localid)
    {
        EnemyHand.instance.TransformCards(localid);
        EnemyHand.instance.HideUnselected();
    }

    [PunRPC]
    private void OnBothSubmit()
    {
        /*
        //GameObject.FindWithTag("EnemyHand").GetComponent<EnemyHand>().ResetCards();
        GameObject hand = HandManager.localInstance.gameObject;
        for (int i = 0; i < hand.transform.childCount; i++)
        {
             hand.transform.GetChild(i).GetChild(1).GetComponent<LocalCardSlotSlot>().Deselect();
        }
        */
        ReviewArea.instance.ReviewSequence();
    }

    public void AddEnemyScore(int points)
    {
        pv.RPC("EnemyScore", enemypv.Owner, points);
    }

    [PunRPC]
    private void EnemyScore(int points)
    {
        if (points == 1)
        {
            Instantiate(ReviewArea.instance.enemyPlusOne, ReviewArea.instance.GetEnemyPosition(), Quaternion.identity);
        } else if (points == 2)
        {
            Instantiate(ReviewArea.instance.enemyPlusTwo, ReviewArea.instance.GetEnemyPosition(), Quaternion.identity);
        }
    }
}
