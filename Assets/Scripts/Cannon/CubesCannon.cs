using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CubesCannon : MonoBehaviour
{
    [SerializeField] private CubesCreator _cubesCreator;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private CannonTrajectory _shootTrajectory;
    [Space]
    [SerializeField] private float _xMaxPosition;
    [Space]
    [SerializeField] private float _maxShootForce;
    [SerializeField] private float _minShootForce;

    private Cube _currentCube;
    private Vector3 _cubeTargetPosition;
    private float _shootForce = 1;

    private void Start()
    {
        StartCoroutine(ShootingProcess());
    }

    private IEnumerator ShootingProcess()
    {
        while (true)
        {
            _cubeTargetPosition = transform.position;
            _shootForce = (_minShootForce + _maxShootForce) / 0.5f;

            _currentCube = _cubesCreator.GetCube();
            _currentCube.transform.position = _cubeTargetPosition;

            _currentCube.transform.DOScale(_currentCube.transform.localScale.x, 0.25f).From(0).SetEase(Ease.OutBack);

            HandlePlayerInput();

            yield return new WaitUntil(() => _playerInput.Touched);

            while (_playerInput.Touched || _shootForce < _minShootForce)
            {
                HandlePlayerInput();

                yield return null;
            }

            _shootTrajectory.SetLength(0);
            _currentCube.Throw(Vector3.forward * _shootForce);

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void HandlePlayerInput()
    {
        _currentCube.transform.position = Vector3.Lerp(_currentCube.transform.position, _cubeTargetPosition, 0.15f);

        _cubeTargetPosition += Vector3.right * _playerInput.XInput;
        _cubeTargetPosition.x = Mathf.Clamp(_cubeTargetPosition.x, -_xMaxPosition, _xMaxPosition);

        _shootForce = Mathf.Clamp(_shootForce + _playerInput.YInput, 0, _maxShootForce);

        _shootTrajectory.SetLength(_shootForce > _minShootForce ? Mathf.InverseLerp(0, _maxShootForce, _shootForce) : 0);
        _shootTrajectory.transform.position = _currentCube.transform.position;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * _xMaxPosition);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * _xMaxPosition);
    }
#endif
}