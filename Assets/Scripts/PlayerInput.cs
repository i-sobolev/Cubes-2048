using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _sensitivityX;
    [SerializeField] private float _sensitivityY;

    private Vector2 _deltaInput;
    private Vector2 _lastTouchPosition;

    private bool _touched;

    public float XInput => _deltaInput.x * 0.01f * _sensitivityX;
    public float YInput => _deltaInput.y * 0.01f * _sensitivityY;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var currentTouch = Input.mousePosition;

            if (!_touched)
            {
                _lastTouchPosition = currentTouch;
                _touched = true;
            }

            _deltaInput = (Vector2)currentTouch - _lastTouchPosition;

            _lastTouchPosition = currentTouch;
        }
        else
        {
            _deltaInput = Vector2.zero;
            _touched = false;
        }
    }
}