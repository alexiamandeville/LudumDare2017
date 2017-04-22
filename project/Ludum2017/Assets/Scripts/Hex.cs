using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HexType
{
    None,
    
    Animal,
    Road,
    City,
    River
}

public class Hex : MonoBehaviour
{
    public HexType currentType = HexType.None;

    // Fill the selected hex with the 
    public void Fill(HexType playerType)
    {
        // If the hex already has a type, return
        if (currentType != HexType.None)
            return;

        currentType = playerType;

        GetComponentInChildren<MeshRenderer>().material.color = Color.red;
    }

    public void Clear()
    {
        currentType = HexType.None;
    }

    private void CheckNeighbors()
    {
        // When filled, check objects near this one to create special objects
    }
}
