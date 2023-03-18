using UnityEngine;

public class SpherecastMethod : MonoBehaviour
{
    public float speed = 1f;
    public float radius = 0.5f;

    private int _mask = (1 << (int)Layers.Entity) | (1 << (int)Layers.Wall);
    private Rigidbody _body;
    private Vector3 _direction;
    private float _moveDist;

    // private void Start()
    // {
    //     _body = GetComponent<Rigidbody>();
    //     _direction = transform.forward;
    //     _moveDist = speed * Time.fixedDeltaTime;
    // }

    // private void FixedUpdate()
    // {
    //     Vector3 nextPosition;

    //     if (Physics.SphereCast(_body.position, radius, _direction, out RaycastHit hit, _moveDist, _mask))
    //     {
    //         print("HIT");

    //         Vector3 hitCenter = hit.point + hit.normal * radius;
    //         _direction = Vector3.Reflect(_direction, hit.normal);
    //         nextPosition = hitCenter + _direction * (_moveDist - (hitCenter - _body.position).magnitude);
    //     }
    //     else
    //     {
    //         nextPosition = _body.position + _direction * _moveDist;
    //     }

    //     _body.position = nextPosition;
    // }

    // int bounceLimit = 5000;

    // private void UpdateNextPositionAndDirection()
    // {
    //     int bounce = 0;

    //     Vector3 start = _body.position;
    //     Vector3 direction = _direction;
    //     float distance = _moveDist;

    //     while (++bounce < bounceLimit)
    //     {
    //         if (Physics.SphereCast(start, radius, direction, out RaycastHit hit, distance, _mask))
    //         {
    //             Vector3 hitCenter = hit.point + hit.normal * radius;
    //             direction = Vector3.Reflect(direction, hit.normal);
    //             distance -= (hitCenter - start).magnitude;
    //             start = hitCenter;

    //             if (distance <= 0.01f)
    //             {
    //                 print("distance too minute. stopping calculation");
    //                 break;
    //             }
    //         }
    //         else
    //         {
    //             break;
    //         }
    //     }

    //     // move pos
    // }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        int iterationLimit = 500;
        int iteration = 0;

        Vector3 start = transform.position;
        Vector3 direction = transform.forward;
        float distance = speed;

        while (true)
        {
            if (Physics.SphereCast(start, radius, direction, out RaycastHit hit, distance, _mask))
            {
                Vector3 hitCenter = hit.point + hit.normal * radius;
                
                direction = Vector3.Reflect(direction, hit.normal);
                distance -= (hitCenter - start).magnitude;

                Gizmos.DrawRay(start, hitCenter - start);

                start = hitCenter;
            }
            else break;

            if (++iteration >= iterationLimit)
            {
                print("Iteration Limit reached. Stopping..");
                break;
            }
        }

        Gizmos.color = Color.green;
        Gizmos.DrawRay(start, direction * distance);
        Gizmos.DrawWireSphere(start + direction * distance, radius);
    }

    // private void OnDrawGizmos()
    // {
    //     Vector3 position = transform.position;
    //     Vector3 direction = transform.forward;
    //     Vector3 moveVec = direction * speed;
    //     float moveDist = moveVec.magnitude;

    //     if (Physics.SphereCast(position, radius, direction, out RaycastHit hit, moveDist, _mask))
    //     {
    //         Gizmos.color = Color.red;

    //         Vector3 normal = hit.normal;
    //         Vector3 hitCenter = hit.point + normal * radius;

    //         Vector3 reflected = Vector3.Reflect(direction, normal) * (moveDist - (hitCenter - position).magnitude);

    //         Gizmos.DrawRay(position, hitCenter - position);
    //         Gizmos.DrawRay(hitCenter, reflected);
    //         Gizmos.DrawWireSphere(hitCenter + reflected, radius);
    //     }
    //     else
    //     {
    //         Gizmos.color = Color.green;

    //         Gizmos.DrawRay(position, moveVec);
    //         Gizmos.DrawWireSphere(position + moveVec, radius);
    //     }
    // }

}
