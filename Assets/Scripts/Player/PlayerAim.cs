using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private float _aimPlaneYLevel = 1f;
    [SerializeField] private Transform _aimCircle;
    [SerializeField] private float _aimCircleRadius = 0.0625f;
    
    private Camera _cam;
    private Plane _aimPlane;
    private LineRenderer _aimLine;

    private int _aimRayMask = (1 << (int)Layers.Entity) | (1 << (int)Layers.Wall);

    private void Awake()
    {
        _cam = Camera.main;
        _aimPlane = new Plane(Vector3.up, Vector3.up * _aimPlaneYLevel);
        _aimLine = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Vector3 aimStartPoint = new Vector3(transform.position.x, _aimPlaneYLevel, transform.position.z);

        Vector3 aimDirection = GetMousePosition() - aimStartPoint;

        if (Physics.SphereCast(aimStartPoint, _aimCircleRadius, aimDirection, out RaycastHit hit, 15f, _aimRayMask))
        {
            _aimLine.SetPosition(0, aimStartPoint);
            
            _aimLine.SetPosition(1, _aimCircle.position = hit.point - hit.normal * _aimCircleRadius);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDirection), 0.5f);
        }

    }

    private Vector3 GetMousePosition()
    {
        Ray cursorRay = _cam.ScreenPointToRay(Input.mousePosition);

        _aimPlane.Raycast(cursorRay, out float rayDist);

        Vector3 mousePos = cursorRay.GetPoint(rayDist);

        return mousePos; // TODO: additional validation
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

}
