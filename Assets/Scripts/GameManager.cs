using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    //Singleton
    [HideInInspector] public static GameManager localInstance;
    
    [HideInInspector] private static PhotonView pv;
    [HideInInspector] private static PhotonView enemypv;

    void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            //This is the local player
            pv = GetComponent<PhotonView>();
            localInstance = this;
        } else
        {
            //This is the enemy player
            enemypv = GetComponent<PhotonView>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //when r key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            RPCExample();
        }
    }

    void RPCExample()
    {
        //Call Print() on the other client
        pv.RPC("Print", enemypv.Owner, new object[] {
            "awesome sauce",
            "epic eggs",
            123
        });
    }

    //RPC (Function that can be called across clients)
    [PunRPC]
    private void Print(string this1, string this2, int this3)
    {
        Debug.Log(this1 + " " + this2 + " " + this3);
    }
}
