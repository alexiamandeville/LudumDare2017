using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkedPlayer : Photon.PunBehaviour
{

    public HexType hexType;
    public int playerId;
    public Quest mySoloQuest;
    public Quest myGroupQuest;
    public int myPoints;

    void Start()
    {
        if (photonView.isMine)
        {
            UpdateQuest();
        }
    }

    void Update()
    {

        CheckSoloComplete(); //is quest complete?
        CheckGroupComplete(); //is quest complete?

        if (photonView.isMine) //only allow input for my network view
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0)) //mouse for desktop
                CreateRay(Input.mousePosition);
        }
#endif
#if UNITY_ANDROID || UNITY_IPHONE
        foreach (Touch touch in Input.touches) //touch with phone
        {
            Touch touch0 = Input.GetTouch(0);
            Vector3 myTouch = new Vector3(touch0.position.x, touch0.position.y, 0);
            CreateRay(myTouch);
        }
#endif
    }

    //TODO: need to make PunRPC
    private void CreateRay(Vector3 inputType)
    {
        // Create
        Ray ray = Camera.main.ScreenPointToRay(inputType);
        RaycastHit hit;

        // Did we hit something?
        if (!Physics.Raycast(ray, out hit))
            return;

        // Was it a Hex?
        if (hit.collider.gameObject.tag != "Hex")
            return;

        // Place hex stuff here for now
        Hex hex = hit.collider.gameObject.GetComponent<Hex>();

        // If we fill successfully, remove value from player for this round
        hex.Fill(hexType);
    }

    void UpdateQuest()
    {
        mySoloQuest = QuestManager.Instance.currentSoloQuest;
        myGroupQuest = QuestManager.Instance.currentGroupQuest;
    }

    public void CheckSoloComplete()
    {
        //TODO: if quest completed
        photonView.RPC("CompleteSoloQuest", PhotonTargets.All, mySoloQuest.pointReward, mySoloQuest.id); //call network update player
        QuestManager.Instance.NextSoloQuest();
        UpdateQuest();
    }

    public void CheckGroupComplete()
    {
        //TODO: if quest completed
        photonView.RPC("CompleteGroupQuest", PhotonTargets.All, myGroupQuest.pointReward, myGroupQuest.id); //call network update player
        QuestManager.Instance.NextGroupQuest();
        UpdateQuest();
    }

    [PunRPC]
    void CompleteSoloQuest(int reward, int id)
    {
        if (photonView.isMine)
        {
            myPoints += reward; //add points to player
        }
    }

    [PunRPC]
    void CompleteGroupQuest(int reward, int id)
    {
        myPoints += reward; //add points to player

    }
}
