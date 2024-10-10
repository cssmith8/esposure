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

    

    void PlayerVariableSetExample()
    {
        Hashtable customProperties = PhotonNetwork.LocalPlayer.CustomProperties;

        //set player-specific variable
        customProperties["variablename"] = "can put any primitive type here";
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);

        //changes are inconsistent if you set the hash multiple times in one frame. dont set player variables every frame.
        //this isnt stated anywhere in the documentation
        //i had to post help on the photon forums to figure this out
        //and even then nobody knew and i had to figure it out myself
    }

    void PlayerVariableGetExample()
    {
        Hashtable customProperties = enemypv.Owner.CustomProperties;

        //get player-specific variable
        string real = (string)customProperties["variablename"];
    }

    void RoomVariableExample()
    {
        Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;

        //set room variable
        hash["variablename"] = "can put any primitive type here";
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);

        //get room variable
        string real = (string)hash["variablename"];

        //see above comments on not changing variables every frame. I assume it's the same for room variables.
    }


    // -------------


    //RPCs - used to call functions across clients

    void Update()
    {
        //when r key is pressed. just for example
        if (Input.GetKeyDown(KeyCode.R))
        {
            RPCExample();
        }

        //when 1 key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject.FindGameObjectWithTag("rc1").GetComponent<LocalRoleCard>().MoveUp();
            GameObject.FindGameObjectWithTag("rc2").GetComponent<LocalRoleCard>().MoveDown();
            GameObject.FindGameObjectWithTag("rc3").GetComponent<LocalRoleCard>().MoveDown();
            pv.RPC("MoveUp", enemypv.Owner, new object[] { 1 });
        }

        //when 2 key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject.FindGameObjectWithTag("rc1").GetComponent<LocalRoleCard>().MoveDown();
            GameObject.FindGameObjectWithTag("rc2").GetComponent<LocalRoleCard>().MoveUp();
            GameObject.FindGameObjectWithTag("rc3").GetComponent<LocalRoleCard>().MoveDown();
            pv.RPC("MoveUp", enemypv.Owner, new object[] { 2 });
        }

        //when 3 key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameObject.FindGameObjectWithTag("rc1").GetComponent<LocalRoleCard>().MoveDown();
            GameObject.FindGameObjectWithTag("rc2").GetComponent<LocalRoleCard>().MoveDown();
            GameObject.FindGameObjectWithTag("rc3").GetComponent<LocalRoleCard>().MoveUp();
            pv.RPC("MoveUp", enemypv.Owner, new object[] { 3 });
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

    [PunRPC]
    private void MoveUp(int card)
    {
        switch (card)
        {
            case 1:
                GameObject.FindGameObjectWithTag("rc4").GetComponent<EnemyRoleCard>().MoveUp();
                GameObject.FindGameObjectWithTag("rc5").GetComponent<EnemyRoleCard>().MoveDown();
                GameObject.FindGameObjectWithTag("rc6").GetComponent<EnemyRoleCard>().MoveDown();
                break;
            case 2:
                GameObject.FindGameObjectWithTag("rc4").GetComponent<EnemyRoleCard>().MoveDown();
                GameObject.FindGameObjectWithTag("rc5").GetComponent<EnemyRoleCard>().MoveUp();
                GameObject.FindGameObjectWithTag("rc6").GetComponent<EnemyRoleCard>().MoveDown();
                break;
            case 3:
                GameObject.FindGameObjectWithTag("rc4").GetComponent<EnemyRoleCard>().MoveDown();
                GameObject.FindGameObjectWithTag("rc5").GetComponent<EnemyRoleCard>().MoveDown();
                GameObject.FindGameObjectWithTag("rc6").GetComponent<EnemyRoleCard>().MoveUp();
                break;
        }
    }
}
