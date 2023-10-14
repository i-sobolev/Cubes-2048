using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public event Action<int> CurrentScoreUpdated;
    public event Action<int> HighScoreUpdated;

    [SerializeField] private CubesCreator _cubesCreator;
    [SerializeField] private ScorePointsParticles _scorePointsParticles;

    public int CurrentScore { get; private set; } = 0;

    private const string _prefsKeyHighScore = "high_score";
    public int HighScore
    {
        get
        {
            return PlayerPrefs.HasKey(_prefsKeyHighScore) ? PlayerPrefs.GetInt(_prefsKeyHighScore) : 0;
        }
        set
        {
            PlayerPrefs.SetInt(_prefsKeyHighScore, value);
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        _cubesCreator.CubesMerged += OnCubesMerged;
    }

    private void OnCubesMerged(Cube cube)
    {
        var points = (int)Mathf.Pow(2, cube.Level);
        CurrentScore += points;

        CurrentScoreUpdated?.Invoke(CurrentScore);

        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
            HighScoreUpdated?.Invoke(HighScore);
        }

        var scoreParticles = Instantiate(_scorePointsParticles, cube.transform.position, Quaternion.identity);
        scoreParticles.Play(points);
    }
}