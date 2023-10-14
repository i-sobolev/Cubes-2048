using System;
using System.Collections;
using UnityEngine;

public class LoseArea : MonoBehaviour
{
    public event Action CubeEnteredLoseArea;

    [SerializeField] private float _loseDetectionDelay;

    private int _enteredCubes = 0;

    private Coroutine _loseDetectionCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Cube>(out var cube) && cube.Thrown)
        {
            _loseDetectionCoroutine = StartCoroutine(LoseDetection());

            ++_enteredCubes;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Cube>(out var cube) && cube.Thrown)
        {
            --_enteredCubes;

            if (_enteredCubes == 0)
                StopCoroutine(_loseDetectionCoroutine);
        }
    }

    private IEnumerator LoseDetection()
    {
        yield return new WaitForSeconds(_loseDetectionDelay);

        enabled = false;

        CubeEnteredLoseArea?.Invoke();
    }
}