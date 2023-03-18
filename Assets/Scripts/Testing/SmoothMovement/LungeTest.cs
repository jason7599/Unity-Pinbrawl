using System.Collections;
using UnityEngine;

public class LungeTest : MonoBehaviour
{

    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray cursorRay = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cursorRay, out RaycastHit hit))
            {
                StopAllCoroutines();
                StartCoroutine(LungeTowards(hit.point));
            }
        }
    }

    private IEnumerator LungeTowards(Vector3 targetPosition)
    {
        Vector3 originalPosition = transform.position;
        
        float percent = 0f;
        while (percent < 1f)
        {
            percent += Time.deltaTime;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, targetPosition, interpolation);

            yield return null;
        }
    }

}
