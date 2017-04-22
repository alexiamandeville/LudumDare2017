using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkedPlayer : Photon.MonoBehaviour
{
    public HexType hexType;

    public Sprite[] playerAvatar = new Sprite[4];
    //objectives
    //

    // Use this for initialization
    void Awake()
    {

    }

    void Update()
    {
        //if (!photonView.isMine)
        //{
        if (Input.GetMouseButtonDown(0))
            CreateRay();
        //}
    }

    private void CreateRay()
    {
        // Create
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
}
