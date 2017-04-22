using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public static Planet Instance;

    public GameObject mHex;

    void Awake()
    {
        Instance = this;

        DrawSpots();
    }

    public void GetClosestVertex(Vector3 hitPoint)
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

        int closestIndex = 0;
        float minimumDistance = Mathf.Infinity;
        Vector3 nearestVertex = Vector3.zero;

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Vector3 difference = hitPoint - mesh.vertices[i];
            float distance = difference.sqrMagnitude;

            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                nearestVertex = mesh.vertices[i];
                closestIndex = i;
            }
        }

        Color[] colors = new Color[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            colors[i] = Color.red;
        }

        // convert nearest vertex back to world space
        // return transform.TransformPoint(nearestVertex);
    }

    private void DrawSpots()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            if (CheckForHex(mesh.vertices[i]))
                continue;

            GameObject newObject = Instantiate(mHex);

            newObject.transform.up = transform.TransformDirection(mesh.vertices[i]);
            newObject.transform.position = mesh.vertices[i] + transform.position;

            newObject.transform.parent = transform;
        }
    }

    private bool CheckForHex(Vector3 target)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, target - transform.position);

        if(Physics.Raycast(ray, out hit, 10.0f))
        {
            if(hit.collider.gameObject.tag == "Hex")
            {
                return true;
            }
        }

        return false;
    }

    private List<Vector3> GetVertices()
    {
        List<Vector3> vertices = new List<Vector3>();

        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

        vertices.AddRange(mesh.vertices);

        return vertices;
    }

    /*
            // Position, rotation
            Vector3 normal = transform.TransformDirection(allNormals[i]);
            List<Vector3> relevantNormals = new List<Vector3>();

            // Get all relevant normals

            foreach (Vector3 meshNormal in allNormals)
            {
                if (normal == transform.TransformDirection(meshNormal))
                {
                    relevantNormals.Add(meshNormal);
                }
            }

            float distance = 0.0f;
            Vector3 furthestPoint = Vector3.zero;

            // Get furthest normal
            foreach (Vector3 otherNormal in relevantNormals)
            {
                float tempDistance = Vector3.Distance(normal, otherNormal);

                if (tempDistance > distance)
                {
                    distance = tempDistance;
                    furthestPoint = otherNormal;
                }
            }

            Vector3 targetPosition = Vector3.Lerp(normal, furthestPoint, 0.5f);
            */
}
