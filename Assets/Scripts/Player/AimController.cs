using UnityEngine;
using UnityEditor;

public class AimController : MonoBehaviour
{
    [SerializeField] private Transform _aimStartPoint;
    [SerializeField] private Transform _aimCircle;
    [SerializeField] private float _aimCircleRadius = 0.0625f;
    [SerializeField] private float _aimZLimit = 1f;

    private LineRenderer _aimLine;
    private Plane _aimPlane;
    private Camera _cam;

    private int _rayCollideMask = (1 << (int)Layers.Entity) | (1 << (int)Layers.Wall);

    private void Awake()
    {
        _cam = Camera.main;
        _aimPlane = new Plane(Vector3.up, Vector3.up * _aimStartPoint.position.y);
        _aimLine = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Vector3 aimStartPos = _aimStartPoint.position;
        Vector3 aimDirection = GetAimPosition() - aimStartPos;

        _aimLine.SetPosition(0, aimStartPos);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDirection), 0.5f);
        
        if (Physics.SphereCast(aimStartPos, _aimCircleRadius, aimDirection, out RaycastHit hit, 15f, _rayCollideMask))
        {
            _aimLine.SetPosition(1, _aimCircle.position = hit.point - hit.normal * _aimCircleRadius);
        }
    }

    private Vector3 GetAimPosition()
    {
        Ray camRay = _cam.ScreenPointToRay(Input.mousePosition);
        _aimPlane.Raycast(camRay, out float rayDist);
        Vector3 aimPos = camRay.GetPoint(rayDist);
        aimPos.z = Mathf.Max(aimPos.z, _aimZLimit);
        return aimPos;
    }

    private void OnEnable()
    {
        _aimLine.enabled = true;
        _aimCircle.gameObject.SetActive(true);
    }
    
    private void OnDisable()
    {
        _aimLine.enabled = false;
        _aimCircle.gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 pos = Vector3.forward * _aimZLimit + Vector3.up;
        Gizmos.DrawLine(pos - Vector3.right, pos + Vector3.right);
        Handles.Label(pos, "aim Z limit");
    }
}
