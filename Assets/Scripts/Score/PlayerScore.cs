using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public event Action<int> CurrentScoreUpdated;
    public event Action<int> HighScoreUpdated;

    [SerializeField] private CubesCreator _cubesCreator;

    private int _currentScore = 0;

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

    private void OnCubesMerged(int score)
    {
        _currentScore += score;

        CurrentScoreUpdated?.Invoke(_currentScore);

        if (_currentScore > HighScore)
        {
            HighScore = _currentScore;
            HighScoreUpdated?.Invoke(HighScore);
        }
    }
}