using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScorePointsParticles : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }

    public void Play(int points)
    {
        _text.text = $"+{points}";

        var randomOffset = Random.insideUnitCircle * Random.Range(0.5f, 1.5f);
        var jumpTarget = new Vector3
        {
            x = randomOffset.x,
            z = randomOffset.y
        };

        var duration = 0.75f;
        transform.DOJump(transform.position + jumpTarget, Random.Range(2f, 3f), 1, duration).SetEase(Ease.Linear);

        DOTween.Sequence()
            .Join(transform.DOScale(1, duration / 2).SetEase(Ease.OutSine))
            .Append(_text.DOFade(0, duration / 2))
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}