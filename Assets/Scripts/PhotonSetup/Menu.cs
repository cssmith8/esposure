using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviourPunCallbacks
{
    private int creationAttempts = 0;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject joinButton;
    //public string roomName = "";

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.NickName = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateRoom()
    {
        //RoomOptions roomOptions = new RoomOptions();
        //roomOptions.CustomRoomPropertiesForLobby = new string[] { "started" };
        //roomOptions.IsVisible = false;
        //PhotonNetwork.CreateRoom("test10", roomOptions);
        //roomName = CreateRandomName();
        
        PhotonNetwork.CreateRoom(CreateRandomName());
    }

    public override void OnCreatedRoom()
    {
        var hash = PhotonNetwork.CurrentRoom.CustomProperties;
        hash["started"] = false;
        hash["challenge"] = Random.Range(0, 150);
        hash["submitted"] = 0;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }

    public void OnCreateRoomFailed()
    {
        creationAttempts++;
        if (creationAttempts < 10)
        {
            CreateRoom();
        }
        else
        {
            Debug.Log("Failed to create room");
        }
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(inputField.text.Trim().ToLower());
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room. Error " + returnCode.ToString() + " | message: " + message);
    }

    public override void OnJoinedRoom()
    {
        //Debug.Log(PhotonNetwork.NickName);
        var hash = PhotonNetwork.CurrentRoom.CustomProperties;
        if ((bool)hash["started"] == false)
        {
            GameObject go = PhotonNetwork.Instantiate(gameCanvas.name, transform.position, transform.rotation);
        } else
        {
            //join an in progress game
            PhotonNetwork.LoadLevel("Game");
        }
        
    }

    public void onInputFieldUpdate()
    {
        joinButton.GetComponent<UnityEngine.UI.Button>().interactable = inputField.text.Length > 0;
    }

    private string CreateRandomName(int length = 3)
    {
        string name = "";

        for (int counter = 1; counter <= length; ++counter) {
            int rand = Random.Range(48, 57);
            name += (char)rand;
        }

        return name;
    }

    
}
