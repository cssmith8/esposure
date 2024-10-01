using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    void Start()
    {
        PhotonNetwork.Instantiate("GameManager", new Vector3(0,0,0), transform.rotation);
    }
}
