using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    private static List<Hex> allHexs = new List<Hex>();
    private List<Hex> borderHexs = new List<Hex>();

    void Awake()
    {
        allHexs.Add(this);
    }

    private void Start()
    {
        GetBorder();

        print(borderHexs.Count);
    }

    // Fill the selected hex with the 
    public void Fill(HexType playerType)
    {
        // If the hex already has a type, return
        if (currentType != HexType.None)
            return;

        currentType = playerType;

        foreach(Hex hex in borderHexs)
        {
            hex.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        }
    }

    public void Clear()
    {
        currentType = HexType.None;
    }

    private void GetBorder()
    {
        List<Hex> orderedHex = allHexs.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();

        for(int i = 0; i < 6; i++)
        {
            borderHexs.Add(orderedHex[i]);
        }
    }
}
