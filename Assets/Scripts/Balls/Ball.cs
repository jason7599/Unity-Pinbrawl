using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed = 7f;
    [SerializeField] protected int _damage = 10;

    private static Transform _ballHolder;

    private float _radius = 0.125f;
    private float _moveDist;
    private int _mask = (1 << (int)Layers.Entity) | (1 << (int)Layers.Wall);
    private Rigidbody _body;
    private Vector3 _direction;

    private void Start()
    {
        _body = GetComponent<Rigidbody>();
        _moveDist = _speed * Time.fixedDeltaTime;

        if (_ballHolder == null)
            _ballHolder = new GameObject("@Balls").transform;
        transform.parent = _ballHolder;

        gameObject.SetActive(false);
    }
    
    private void FixedUpdate()
    {
        Vector3 start = _body.position;
        float distance = _moveDist;

        int iterationLimit = 10; // just to be safe
        int iteration = 0;

        while (iteration++ < iterationLimit)
        {
            if (Physics.SphereCast(start, _radius, _direction, out RaycastHit hit, distance, _mask))
            {
                GameObject hitObject = hit.transform.gameObject;

                Vector3 hitCenter = hit.point + hit.normal * _radius;
                
                _direction = Vector3.Reflect(_direction, hit.normal);
                distance -= (hitCenter - start).magnitude;
                start = hitCenter;
            
                if (hitObject.layer == (int)Layers.Entity)
                {
                    // TODO: maybe a dictionary for optimization
                    OnEntityHit(hitObject.GetComponent<Entity>());
                }
            }
            else 
            {
                break;
            }
        }

        _body.MovePosition(start + _direction * distance);
    }

    protected virtual void OnEntityHit(Entity entity)
    {
        entity.OnHit(_damage);
    }

    public void Activate(Transform shootPoint)
    {
        gameObject.SetActive(true);
        transform.position = shootPoint.position;
        transform.rotation = shootPoint.rotation;
        _direction = transform.forward;
    }

    public void ReturnToPlayer()
    {
        GameManager.player.OnBallReturn(_body.position.x);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == (int)Layers.Powerup)
        {
            collider.gameObject.GetComponent<Entity>().OnHit();
        }
        else
        {    // PlayerZone
            ReturnToPlayer();
        }   
    }

}
