using UnityEngine;

public class InputTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            print("Hey");
            enabled = false;
        }
    }
}
