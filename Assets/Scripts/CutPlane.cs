using UnityEngine;

public class CutPlane : MonoBehaviour
{
    private Vector3 point1, point2, point3, point4;

    private void Update()
    {
        point1 = transform.position + transform.right;
        point2 = transform.position - transform.right;
        point3 = point1 + transform.forward * 10;
        point4 = point2 + transform.forward * 10;
    }

    public Plane GetPlane()
    {
        return new Plane(point1, point2, point3);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(point1, point2);
        Gizmos.DrawLine(point2, point4);
        Gizmos.DrawLine(point4, point3);
        Gizmos.DrawLine(point1, point3);
    }
}
