using UnityEngine;
using System.Collections.Generic;

public class MeshBuilder
{
    private List<Vector3> vertices = new List<Vector3>();
    private List<Vector3> normals = new List<Vector3>();
    private List<int> indices = new List<int>();

    public void AddTriangle(Vector3[] vertices, Vector3[] normals)
    {
        indices.Add(this.vertices.Count);
        indices.Add(this.vertices.Count + 1);
        indices.Add(this.vertices.Count + 2);

        this.vertices.AddRange(vertices);
        this.normals.AddRange(normals);
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        Debug.Log("Create mesh, vertices.Count: " + vertices.Count);
        mesh.SetVertices(vertices);
        mesh.SetTriangles(indices, 0);
        if (normals.Count == vertices.Count)
            mesh.SetNormals(normals);

        mesh.RecalculateBounds();

        return mesh;
    }
}