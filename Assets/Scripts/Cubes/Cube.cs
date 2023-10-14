using System;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action<CubesCollisionEventArgs> CollidedWithCube;

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private TextMeshPro[] numbers;

    public int Level { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (collision.gameObject.TryGetComponent<Cube>(out var cube) && cube.Level == Level)
        {
            CollidedWithCube?.Invoke(new (this, cube));
        }
    }

    public void Set(int level, Color color)
    {
        Level = level;

        foreach (var text in numbers)
            text.text = Mathf.Pow(2, level).ToString();

        _meshRenderer.material.color = color;
    }

    public void Throw(Vector3 direction, bool addTorque = false)
    {
        var rigidBody = GetComponent<Rigidbody>();

        rigidBody.isKinematic = false;
        rigidBody.AddForce(direction, ForceMode.Impulse);

        if (addTorque)
            rigidBody.AddTorque(UnityEngine.Random.onUnitSphere * 50);
    }

    public class CubesCollisionEventArgs
    {
        public readonly Cube Cube1;
        public readonly Cube Cube2;

        public CubesCollisionEventArgs(Cube cube1, Cube cube2)
        {
            Cube1 = cube1;
            Cube2 = cube2;
        }
    }
}