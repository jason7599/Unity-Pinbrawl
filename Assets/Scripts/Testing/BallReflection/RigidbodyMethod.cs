using UnityEngine;

public class RigidbodyMethod : MonoBehaviour
{
    // public float speed = 2f;

    // int _wallLayer = (1 << (int)Layers.Wall);

    // Rigidbody _body;
    // Vector3 _direction;

    // void Start()
    // {
    //     _body = GetComponent<Rigidbody>();
    //     _direction = transform.forward;
    //     _body.velocity = _direction * speed;
    // }

    // void OnCollisionEnter(Collision collision)
    // {
    //     Vector3 normal = Utils.ConvertNormalToBoxNormal(collision.GetContact(0).normal);
    //     _direction = Vector3.Reflect(_direction, normal);
    //     _body.velocity = _direction * speed;
    //     _body.rotation = Quaternion.LookRotation(_direction);
    // }

    // void OnCollisionStay(Collision collision)
    // {
        


    //     // if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f, _wallLayer))
    //     // {
    //     //     Vector3 reflected = Vector3.Reflect(transform.forward, hit.normal);
    //     //     Debug.DrawRay(hit.point, reflected, Color.red);
    //     // }
        
    // }

    // void OnTriggerStay(Collider collider)
    // {
    //     Debug.DrawRay(transform.position, collider.Clo - transform.position, Color.red);

    //     // if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f, _wallLayer))
    //     // {
    //     //     Vector3 reflected = Vector3.Reflect(transform.forward, hit.normal);
    //     //     Debug.DrawRay(hit.point, reflected, Color.red);
    //     // }
    // }
}
