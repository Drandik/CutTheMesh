using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CuttingMesh : MonoBehaviour
{
    public static List<CuttingMesh> CuttingMeshes { get; private set; }

    public Transform ObjTransform { get; set; }
    public Material[] Materials { get; private set; }

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        if (CuttingMeshes == null)
            CuttingMeshes = new List<CuttingMesh>();
    }

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        ObjTransform = transform;
        Materials = meshRenderer.sharedMaterials;
        CuttingMeshes.Add(this);
    }

    public Mesh GetMesh()
    {
        return meshFilter.mesh;
    }

    public void SetMesh(Mesh newMesh)
    {
        meshFilter.mesh = newMesh;
    }
}
