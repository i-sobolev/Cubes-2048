using UnityEngine;

public class CannonTrajectory : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    private float _maxZPosition;

    private float _targetValue = 0;
    private float _currentValue = 0;

    private void Start()
    {
        _maxZPosition = _lineRenderer.GetPosition(1).z;
    }

    private void Update()
    {
        _currentValue = Mathf.Lerp(_currentValue, _targetValue, 0.15f);

        RefreshTrajectory();
    }

    private void RefreshTrajectory()
    {
        var target = _lineRenderer.GetPosition(1);
        target.z = Mathf.Lerp(0, _maxZPosition, _currentValue);

        _lineRenderer.SetPosition(1, target);
    }

    public void SetLength(float value)
    {
        _lineRenderer.enabled = value != 0;

        _targetValue = value;
    }
}
