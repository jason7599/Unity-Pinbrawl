using UnityEngine;

public class RaycastMethod : MonoBehaviour
{
    public float speed = 1f;
    public float radius = 0.5f;

    int _wallLayer = (1 << (int)Layers.Wall);

    // void Update()
    // {
    //     transform.position = _nextPosition;

    //     Vector3 moveVec = _direction * Time.deltaTime * speed;
    //     float moveDist = moveVec.magnitude;

    //     if (Physics.Raycast(transform.position, _direction, out RaycastHit hit, moveDist, _wallLayer))
    //     {
    //         _direction = Vector3.Reflect(_direction, hit.normal);
    //         _nextPosition = hit.point + _direction * (moveDist - hit.distance);
    //     }
    //     else
    //     {
    //         _nextPosition = transform.position + moveVec;
    //     }
    // }

    void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        Vector3 _direction = transform.forward;
        float moveDistance = _direction.sqrMagnitude;
        Vector3 nextPosition;

        if (Physics.Raycast(origin , _direction, out RaycastHit hit, moveDistance, _wallLayer))
        {
            Vector3 reflected = Vector3.Reflect(_direction, hit.normal).normalized * (moveDistance - hit.distance);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(origin, hit.point - origin);
            Gizmos.DrawRay(hit.point, reflected);

            nextPosition = hit.point + reflected;
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(origin, _direction);

            nextPosition = origin + _direction;
        }

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(nextPosition, radius);
    }

}
