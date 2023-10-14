using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private Image _overlay;
    [SerializeField] private Transform _title;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private Transform _restartButton;

    public void Show(int currentScore)
    {
        _score.text = currentScore.ToString();

        DOTween.Sequence()
            .Append(_overlay.DOFade(_overlay.color.a, 0.35f))
            .Join(_title.DOScale(1, 0.25f).From(0).SetEase(Ease.OutBack))
            .Append(_score.DOScale(1, 0.25f).From(0).SetEase(Ease.OutBack))
            .Append(_restartButton.DOScale(1, 0.25f).From(0).SetEase(Ease.OutBack));
    }
}
