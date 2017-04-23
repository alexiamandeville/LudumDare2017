using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkedPlayer : Photon.PunBehaviour
{

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    public HexType hexType;
    public static string[] myHexes = new string[5];
    public int playerId;
    public Quest mySoloQuest;
    public Quest myGroupQuest;
    public int myPoints;

    void Awake()
    {
        if (photonView.isMine)
        {
            InitAssets(); //initialize the assets/tokens the player has
            NetworkedPlayer.LocalPlayerInstance = this.gameObject; //may need for later
        }

    }

    void InitAssets() //add assets to player
    {
        for (int i = 0; i < myHexes.Length; i++)
        {
            myHexes[i] = ((HexType)UnityEngine.Random.Range(1, 5)).ToString();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //mouse for desktop
            CreateRay(Input.mousePosition);


#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0)) //mouse for desktop
                CreateRay(Input.mousePosition);
        
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

    //TO DO: need to make PunRPC
    private void CreateRay(Vector2 startPoint)
    {
        // Create
        Ray ray = Camera.main.ScreenPointToRay(startPoint);
        RaycastHit hit;

        // Did we hit something?
        if (!Physics.Raycast(ray, out hit))
            return;

        // Did we hit the world
        if (hit.collider.gameObject.tag != "Hex")
            return;

        // Place hex stuff here for now
        Hex hex = hit.collider.gameObject.GetComponent<Hex>();

        // If we fill successfully, remove value from player for this round
        hex.Fill(hexType);
    }

    [PunRPC]
    void CompleteSoloQuest(int reward, int id)
    {
        if (photonView.isMine)
        {
            myPoints += reward; //add points to player
            mySoloQuest.id = id; //update quest
            InitAssets(); //update player pieces
        }
    }

    [PunRPC]
    void CompleteGroupQuest(int reward, int id)
    {
        myPoints += reward; //add points to player
        myGroupQuest.id = id; //update quest

    }
}
