using UnityEngine;

public class InputTest : MonoBehaviour
{
    [SerializeField] private GameObject thing;

    private void ActivateThing()
    {
        if (thing.activeInHierarchy)
        {
            print("was already ACTIVE");
        }
        else
        {
            thing.SetActive(true);
        }
    }

    private void DeactivateThing()
    {
        if (!thing.activeInHierarchy)
        {
            print("was already INACTIVE");
        }
        else
        {
            thing.SetActive(false);
        }
    }

    private void Start()
    {
        DeactivateThing();
    }

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(1))
    //     {
    //         ActivateThing();
    //     }
    //     else if (Input.GetMouseButtonUp(1))
    //     {
    //         DeactivateThing();
    //     }
    // }

    // [SerializeField] private Transform _playerTransform;
    // [SerializeField] private float _attackDistance = 2f;
    // [SerializeField] private float _speed = 1f;

    // private void Update()
    // {
    //     if ((_playerTransform.position - transform.position).sqrMagnitude <= Mathf.Pow(_attackDistance, 2))
    //     {
    //         Attack();
    //     }
    // }

    // private void Attack() { }
}
