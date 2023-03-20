using UnityEngine;
using System.Collections;

public class JitterBehaviour : MonoBehaviour
{
    public float jitterAmount = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(Jitter());
        }
    }

    private IEnumerator Jitter()
    {
        // only x and y 
        Vector3 shake = Random.insideUnitCircle.normalized * jitterAmount;
        Quaternion shookRotation = Quaternion.LookRotation(transform.forward + shake);

        float percent = 0f;

        while (percent < 1f)
        {
            percent += Time.deltaTime;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.rotation = Quaternion.Lerp(Quaternion.identity, shookRotation, interpolation);

            yield return null;
        }
        
    }
}
