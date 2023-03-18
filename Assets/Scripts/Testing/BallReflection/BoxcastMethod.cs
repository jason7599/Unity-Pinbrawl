using UnityEngine;

public class BoxcastMethod : MonoBehaviour
{
    public float speed = 1f;
    public float boxHalfSize = .125f;

    int _mask = (1 << (int)Layers.Entity) | (1 << (int)Layers.Wall);

    private void OnDrawGizmos()
    {
        Vector3 start = transform.position;
        Vector3 direction = transform.forward;
        Vector3 halfExtents = Vector3.one * boxHalfSize;
        float distance = speed;

        int iterationLimit = 1;
        int iteration = 0;

        Gizmos.color = Color.red;

        while (iteration++ < iterationLimit)
        {
            if (Physics.BoxCast(start, halfExtents, direction, out RaycastHit hit, Quaternion.identity, distance, _mask))
            {
                Vector3 hitCenter = hit.point + hit.normal * boxHalfSize;
                
                Gizmos.DrawRay(start, hitCenter - start);
                Gizmos.DrawWireCube(hitCenter, halfExtents * 2);
            }
            else
                break;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawRay(start, direction * distance);
        Gizmos.DrawWireCube(start + direction * distance, halfExtents * 2);
    }
}
