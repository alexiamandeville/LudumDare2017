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
    Mountain, 
    Forest
}

public class Hex : MonoBehaviour
{
    public HexType currentType = HexType.None;
    public List<GameObject> tileMeshes;

    public static List<Hex> allHexs = new List<Hex>();

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

        GetComponent<MeshRenderer>().enabled = false;
    }

    public void Clear()
    {
        currentType = HexType.None;
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
        print(newMesh);
        if (newMesh != null)
        {
            CreateMesh(newMesh);
        }
    }

    private void CreateMesh(GameObject newMesh)
    {
        GameObject hexMesh = Instantiate(newMesh);

        hexMesh.transform.parent = transform;
        hexMesh.transform.localPosition = Vector3.zero;

        hexMesh.transform.forward = transform.up;
    }
}
