using UnityEngine;

public class CubesCannon : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Transform _testCube;
    [SerializeField] private CannonTrajectory _shootTrajectory;
    [Space]
    [SerializeField] private float _xMaxPosition;
    [SerializeField] private float _maxShootForce;

    private Vector3 _cubeTargetPosition = Vector3.zero;
    private float _shootForce = 1;

    private void Update()
    {
        _testCube.localPosition = Vector3.Lerp(_testCube.localPosition, _cubeTargetPosition, 0.15f);

        _cubeTargetPosition += Vector3.right * _playerInput.XInput;
        _cubeTargetPosition.x = Mathf.Clamp(_cubeTargetPosition.x, -_xMaxPosition, _xMaxPosition);

        _shootForce = Mathf.Clamp(_shootForce + _playerInput.YInput, 0, _maxShootForce);
        _shootTrajectory.SetLength(Mathf.InverseLerp(0, _maxShootForce, _shootForce));
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * _xMaxPosition);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * _xMaxPosition);
    }
#endif
}