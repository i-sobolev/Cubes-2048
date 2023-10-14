using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubesCreator : MonoBehaviour
{
    [SerializeField] private Cube _cubeTemplate;
    [Space]
    [SerializeField] private Color[] _cubeColorsByLevels;

    private List<Cube> _createdCubes = new();

    public Cube GetCube()
    {
        var newCube = _createdCubes.FirstOrDefault(x => !x.gameObject.activeInHierarchy) ?? Instantiate(_cubeTemplate, Vector3.zero, Quaternion.identity);

        var newCubeLevel = 1;
        newCube.Set(newCubeLevel, _cubeColorsByLevels[newCubeLevel - 1]);
        newCube.gameObject.SetActive(true);

        newCube.CollidedWithCube += MergeCubes;

        return newCube;
    }

    private void MergeCubes(Cube.CubesCollisionEventArgs args)
    {
        args.Cube1.gameObject.SetActive(false);
        args.Cube2.gameObject.SetActive(false);

        var newCubeTargetPosition = (args.Cube1.transform.position + args.Cube2.transform.position) / 2;

        var newCube = GetCube();
        var newCubeLevel = args.Cube1.Level + 1;
        
        newCube.Set(newCubeLevel, _cubeColorsByLevels[newCubeLevel - 1]);
        newCube.transform.position = newCubeTargetPosition;
        newCube.Throw(Vector3.up * 5);
    }
}