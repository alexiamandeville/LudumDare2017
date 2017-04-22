using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject mCell;

    void Awake()
    {
        DrawNormals();
    }

    void Update()
    {
        
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

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            // Position, rotation
            Vector3 normal = transform.TransformDirection(mesh.normals[i]);
            Vector3 vertex = transform.TransformPoint(mesh.vertices[i]);

            // Create object
            GameObject newObject = Instantiate(mCell, vertex, Quaternion.identity);
            newObject.transform.up = normal;

            Debug.DrawRay(vertex, normal * 1.0f, Color.red);
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
