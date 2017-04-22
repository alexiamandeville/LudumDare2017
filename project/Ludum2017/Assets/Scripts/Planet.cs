using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject mHex;

    private int index = 0;

    public List<Vector3> allHexs = new List<Vector3>();

    void Awake()
    {
        DrawNormals();
    }

    void OnDrawGizmosSelected()
    {
        /*
        Gizmos.color = Color.blue;

        Gizmos.DrawSphere(transform.position, 0.5f);
    
        foreach (Vector3 vertexPosition in GetVertices())
        {
            // World position
            Vector3 targetPosition = vertexPosition + transform.position;

            // Factor in rotation to world position
            targetPosition = transform.rotation * (targetPosition - transform.position) + transform.position;
        }
        */
    }

    private void DrawNormals()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

        List<Vector3> allNormals = new List<Vector3>();
        allNormals.AddRange(mesh.normals);

        for (int i = 0; i < allNormals.Count; i++)
        {
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

            GameObject newObject = Instantiate(mHex);

            newObject.transform.up = normal;
            newObject.transform.position = targetPosition;

            allHexs.Add(targetPosition);

            // Remove relevant
            foreach (Vector3 otherNormal in relevantNormals)
            {
                allNormals.Remove(otherNormal);
            }
        }
    }

    private List<Vector3> GetVertices()
    {
        List<Vector3> vertices = new List<Vector3>();

        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

        vertices.AddRange(mesh.vertices);

        return vertices;
    }
}
