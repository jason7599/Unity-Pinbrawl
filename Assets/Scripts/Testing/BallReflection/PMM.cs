using UnityEngine;

public class PMM : MonoBehaviour
{
    private Rigidbody _body;

    public float force = 5f;

    private void Start()
    {
        _body = GetComponent<Rigidbody>();
        AddRandomForce();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddRandomForce();
        }
    }

    private void AddRandomForce()
    {
        float randomX = Random.Range(0f, 1f);
        float randomZ = Random.Range(0f, 1f);

        _body.velocity = (Vector3.forward * randomZ + Vector3.right * randomX).normalized * force;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(_body.velocity.sqrMagnitude);
    }
}
