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

    }

    public void SelectCard(int index)
    {
        pv.RPC("EnemySelect", enemypv.Owner, index);
    }

    [PunRPC]
    private void EnemySelect(int index)
    {
        //EnemyHand.instance.SelectCard(index);
    }

    public void Submit()
    {
        //update the room variable
        Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
        hash["submitted"] = (int) hash["submitted"] + 1;
        
        //if both players have submitted
        if ((int)hash["submitted"] == 2)
        {
            pv.RPC("OnBothSubmit", RpcTarget.All);
            hash["submitted"] = 0;

        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    [PunRPC]
    private void OnEnemySubmit()
    {
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
    }
}
