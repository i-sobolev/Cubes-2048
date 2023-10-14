using DG.Tweening;
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

        var newCubeLevel = GetRandomCubeLevel();
        newCube.Set(newCubeLevel, _cubeColorsByLevels[newCubeLevel - 1]);
        newCube.gameObject.SetActive(true);

        newCube.CollidedWithCube += MergeCubes;

        return newCube;

        int GetRandomCubeLevel()
        {
            var randomValue = Random.Range(0, 100f);

            return randomValue switch
            {
                < 5 => 3,
                < 15 => 2,
                _ => 1,
            };
        }
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
        newCube.Throw(Vector3.up * 5, true);

        newCube.transform.DOScale(newCube.transform.localScale.x, 0.25f).From(0).SetEase(Ease.OutBack);
    }
}