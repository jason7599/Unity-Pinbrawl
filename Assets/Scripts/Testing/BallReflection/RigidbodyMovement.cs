using UnityEngine;

public class RigidbodyMovement : MonoBehaviour
{
    [SerializeField] float _speed = 1f;
    
    Rigidbody _body;
    Vector3 _direction;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _direction = transform.forward;
    }

    void FixedUpdate()
    {
        _body.velocity = _direction * Time.fixedDeltaTime * _speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        _direction = Vector3.Reflect(_direction, collision.GetContact(0).normal);
    }
}
