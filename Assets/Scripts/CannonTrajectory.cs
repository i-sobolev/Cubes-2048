using UnityEngine;

public class CannonTrajectory : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    private float _maxZPosition;

    private void Start()
    {
        _maxZPosition = _lineRenderer.GetPosition(1).z;
    }

    public void SetLength(float value)
    {
        var target = _lineRenderer.GetPosition(1);
        target.z = Mathf.Lerp(0, _maxZPosition, value);
        
        _lineRenderer.SetPosition(1, target);
    }
}
