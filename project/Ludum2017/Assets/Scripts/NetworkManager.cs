using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class NetworkManager : Photon.MonoBehaviour
{
    string _room = "smallWorld"; //need to update later for players to create their own rooms
    public Image[] playerAvatarSlot;
    public Text[] playerTextSlot;


    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public void Join()
    {
        PhotonNetwork.JoinLobby();
    }

    void OnJoinedLobby()
    {
        
        RoomOptions roomOptions = new RoomOptions() { };
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(_room, roomOptions, TypedLobby.Default);
    }

    /// <summary>
    /// Instantiate each player separately when someone joins.
    /// </summary>
    void OnJoinedRoom()
    {
        //limit players to 4
        if (PhotonNetwork.playerList.Length <= 4)
        {
            for(int i=0; i < PhotonNetwork.playerList.Length; i++)
            {
                playerAvatarSlot[i].gameObject.SetActive(true);
            }
            //playerAvatarSlot[PhotonNetwork.playerList.Length - 1].gameObject.SetActive(true);
            playerTextSlot[PhotonNetwork.playerList.Length - 1].gameObject.SetActive(true);
            PhotonNetwork.Instantiate("MyPlayer", new Vector3(0, 0, 0), Quaternion.identity, 0);
        }

        print(NetworkedPlayer.LocalPlayerInstance.GetPhotonView().viewID);


    }

    void OnPhotonPlayerConnected()
    {
        //limit players to 4
        if (PhotonNetwork.playerList.Length <= 4)
        {
            for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
            {
                playerAvatarSlot[i].gameObject.SetActive(true);
            }

        }
    }

}