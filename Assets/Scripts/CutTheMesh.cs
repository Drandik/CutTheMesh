using UnityEngine;

public class CutTheMesh : MonoBehaviour
{
    [SerializeField] private CutPlane CutPlane;

    private Plane cutPlane;

    private MeshBuilder positiveMesh;
    private MeshBuilder negativeMesh;

    private void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            cutPlane = CutPlane.GetPlane();

            StartCutting();
        }
    }

    private void StartCutting()
    {
        foreach (var cutMesh in CuttingMesh.CuttingMeshes)
            CutMesh(cutMesh);
    }

    private void CutMesh(CuttingMesh cuttingMesh)
    {
        positiveMesh = new MeshBuilder();
        negativeMesh = new MeshBuilder();

        bool[] sides = new bool[3];
        int[] indices;
        int p1, p2, p3;

        indices = cuttingMesh.GetMesh().triangles;

        for (int i = 0; i< indices.Length; i+=3)
        {
            p1 = indices[i];
            p2 = indices[i + 1];
            p3 = indices[i + 2];

            sides[0] = cutPlane.GetSide(cuttingMesh.GetMesh().vertices[p1]);
            sides[1] = cutPlane.GetSide(cuttingMesh.GetMesh().vertices[p2]);
            sides[2] = cutPlane.GetSide(cuttingMesh.GetMesh().vertices[p3]);

            if (sides[0] == sides[1] && sides[0] == sides[2])
            {
                if (sides[0])
                {

                    positiveMesh.AddTriangle(
                        new Vector3[] { cuttingMesh.GetMesh().vertices[p1], cuttingMesh.GetMesh().vertices[p2], cuttingMesh.GetMesh().vertices[p3] },
                        new Vector3[] { cuttingMesh.GetMesh().normals[p1], cuttingMesh.GetMesh().normals[p2], cuttingMesh.GetMesh().normals[p3] });
                }
                else
                {

                    negativeMesh.AddTriangle(
                        new Vector3[] { cuttingMesh.GetMesh().vertices[p1], cuttingMesh.GetMesh().vertices[p2], cuttingMesh.GetMesh().vertices[p3] },
                        new Vector3[] { cuttingMesh.GetMesh().normals[p1], cuttingMesh.GetMesh().normals[p2], cuttingMesh.GetMesh().normals[p3] });
                }
            }
        }

        Material[] mats = cuttingMesh.Materials;

        Mesh positiveHalfMesh = positiveMesh.CreateMesh();
        positiveHalfMesh.name = "Split Mesh Positive";

        Mesh negativeHalfMesh = negativeMesh.CreateMesh();
        negativeHalfMesh.name = "Split Mesh Negative";

        cuttingMesh.SetMesh(positiveHalfMesh);

        GameObject negativeSideObj = new GameObject("negative side", typeof(MeshFilter), typeof(MeshRenderer), typeof(CuttingMesh));
        negativeSideObj.transform.position = cuttingMesh.ObjTransform.position;
        negativeSideObj.transform.rotation = cuttingMesh.ObjTransform.rotation;
        negativeSideObj.GetComponent<MeshFilter>().mesh = negativeHalfMesh;
        negativeSideObj.GetComponent<MeshRenderer>().materials = mats;

        negativeSideObj.transform.localScale = cuttingMesh.ObjTransform.localScale;
    }
}
