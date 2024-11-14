using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PhotonExamples : MonoBehaviourPunCallbacks
{
    private PhotonView pv;
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
        Hashtable customProperties = pv.Owner.CustomProperties;

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

    void RPCExample()
    {
        //Call Print() on the other client
        pv.RPC("Print", /*enemy*/pv.Owner, new object[] {
            "awesome sauce",
            "epic eggs",
            123
        });
    }

    [PunRPC]
    private void Print(string a, string b, int c)
    {
        Debug.Log(a + " " + b + " " + c);
    }
}
