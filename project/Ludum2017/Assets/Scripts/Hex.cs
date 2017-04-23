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
    River,
    Mountain
}

public class Hex : MonoBehaviour
{
    public bool isCenter = false;
    public HexType currentType = HexType.None;
    public List<GameObject> tileMeshes;

    public static List<Hex> allHexs = new List<Hex>();

    private List<Hex> borderHexs = new List<Hex>();

    void Awake()
    {
        allHexs.Add(this);
    }

    void OnDestroy()
    {
        allHexs.Remove(this);
    }

    private void Start()
    {
        // GetBorder();
    }

    // Fill the selected hex with the 
    public void Fill(HexType playerType)
    {
        // If the hex already has a type, return
        if (currentType != HexType.None)
            return;

        currentType = playerType;
        SetTileMesh();

        foreach(Hex hex in borderHexs)
        {
            if (hex.GetInstanceID() != GetInstanceID())
                hex.gameObject.SetActive(false);
        }
    }

    public void Clear()
    {
        currentType = HexType.None;
    }

    private void GetBorder()
    {
        List<Hex> orderedHex = allHexs.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();

        for(int i = 0; i < 7; i++)
        {
            borderHexs.Add(orderedHex[i]);
        }
    }

    private void SetTileMesh()
    {
        GameObject newMesh = null;

        switch(currentType)
        {
            case HexType.Animal:
                newMesh = tileMeshes[0];
                break;
            case HexType.Mountain:
                newMesh = tileMeshes[1];
                break;
        }

        if(newMesh != null)
            CreateMesh(newMesh);
    }

    private void CreateMesh(GameObject newMesh)
    {
        GameObject hexMesh = Instantiate(newMesh);

        hexMesh.transform.parent = transform;
        hexMesh.transform.localPosition = Vector3.zero;

        hexMesh.transform.forward = transform.up;
    }
}
