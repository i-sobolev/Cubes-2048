using UnityEngine;

public class GameProcess : MonoBehaviour
{
    [SerializeField] private LoseArea _loseArea;
    [SerializeField] private LoseScreen _loseScreen;
    [SerializeField] private PlayerScore _playerScore;
    [Space]
    [SerializeField] private GameObject _gameplayUI;
    [SerializeField] private GameObject _loseScreenUI;

    private void Start()
    {
        _loseArea.CubeEnteredLoseArea += OnLoseAreaTriggered;
    }

    private void OnLoseAreaTriggered()
    {
        _gameplayUI.SetActive(false);
        _loseScreenUI.SetActive(true);

        _loseScreen.Show(_playerScore.CurrentScore);
    }
}