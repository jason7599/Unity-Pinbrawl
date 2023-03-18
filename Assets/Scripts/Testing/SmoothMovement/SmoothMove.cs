using System.Collections;
using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    // public float speed = 2f;
    public float movementTime = 1f;
    Camera _cam;

    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            StartCoroutine(MoveTo(GetMouseWorldPosition() + Vector3.up / 2));
        }
    }

    IEnumerator MoveTo(Vector3 dest)
    {
        if (movementTime <= 0f)
        {
            print("THE FUCK");
            yield break;
        }

        Vector3 start = transform.position;

        float elapsed = 0f;
        while (elapsed <= movementTime)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(start, dest, elapsed / movementTime);

            yield return null;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        return hit.point;
    }

}
