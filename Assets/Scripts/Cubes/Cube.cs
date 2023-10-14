using System;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action<CubesCollisionEventArgs> CollidedWithCube;

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private TextMeshPro[] numbers;

    public int Level { get; private set; }

    private Rigidbody _rigidbody;
    private Vector3 _baseScale;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _baseScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = _baseScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (collision.gameObject.TryGetComponent<Cube>(out var cube) && cube.Level == Level)
            CollidedWithCube?.Invoke(new (this, cube, Level));
    }

    public void Set(int level, Color color)
    {
        transform.localRotation = Quaternion.identity;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;

        Level = level;

        foreach (var text in numbers)
            text.text = Mathf.Pow(2, level).ToString();

        _meshRenderer.material.color = color;
    }

    public void Throw(Vector3 direction, bool addTorque = false)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(direction, ForceMode.Impulse);

        if (addTorque)
            _rigidbody.AddTorque(UnityEngine.Random.onUnitSphere * 50);
    }

    public class CubesCollisionEventArgs
    {
        public readonly Cube Cube1;
        public readonly Cube Cube2;
        public readonly int Level;

        public CubesCollisionEventArgs(Cube cube1, Cube cube2, int level)
        {
            Cube1 = cube1;
            Cube2 = cube2;
            Level = level;
        }
    }
}