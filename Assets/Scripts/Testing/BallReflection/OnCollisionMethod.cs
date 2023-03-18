using UnityEngine;

public class OnCollisionMethod : MonoBehaviour
{
    public float speed = 1f;

    Vector3 ConvertNormalToBoxNormal(Vector3 vector)
    {
        float x = vector.x;

        if (x == -1f || x == 0f || x == 1f) return vector;

        float z = vector.z;

        if (x < 0)
        {
            if (z < 0)
            {
                if (-x < -z) return Vector3.back;
                return Vector3.left;
            }
            if (-x < z) return Vector3.forward;
            return Vector3.left;
        }
        if (z < 0)
        {
            if (x < -z) return Vector3.back;
            return Vector3.right;
        }
        if (x < z) return Vector3.forward;
        return Vector3.right;
    }

    void OnCollisionStay(Collision collision)
    {
        ContactPoint cp = collision.GetContact(0);
        Vector3 normal = ConvertNormalToBoxNormal(cp.normal);
        Debug.DrawRay(cp.point, normal * 3f, Color.red);

        Vector3 reflected = Vector3.Reflect(transform.forward, normal);
        Debug.DrawRay(cp.point, reflected * 3f, Color.green);
    }

}
