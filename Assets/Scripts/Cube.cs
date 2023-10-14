using UnityEngine;

public class Cube : MonoBehaviour
{
    public void Throw(Vector3 direction)
    {
        var rigidBody = GetComponent<Rigidbody>();

        rigidBody.isKinematic = false;
        rigidBody.AddForce(direction, ForceMode.Impulse);
    }
}
