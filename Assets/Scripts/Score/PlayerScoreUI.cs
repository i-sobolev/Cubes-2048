using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerScoreUI : MonoBehaviour
{
    [SerializeField] private PlayerScore _playerScore;
    [Space]
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _highScore;

    private void Awake()
    {
        _playerScore.CurrentScoreUpdated += SetCurrentScore;
        _playerScore.HighScoreUpdated += SetHighScore;

        SetHighScore(_playerScore.HighScore);
    }

    public void SetCurrentScore(int score)
    {
        _currentScore.text = score.ToString();

        _currentScore.transform.localScale = Vector3.one;
        _currentScore.transform.DOPunchScale(-Vector3.one * 0.2f, 0.15f);
    }

    public void SetHighScore(int score)
    {
        _highScore.text = score.ToString();
    }
}