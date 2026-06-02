using DG.Tweening;
using UnityEngine;

public class Gem : Interactable
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _flyDistance = 1.5f;
    [SerializeField] private float _flyDuration = 0.35f;
    [SerializeField] private int _reward = 10;
    [SerializeField] private AudioClip _spawnSound;
    [SerializeField] private AudioClip _collectSound;

    private ISaveLoadService _save;
    private IPersistentProgressService _progress;
    private IPoolService _pool;
    private AudioHandler _audio;
    private Sequence seq;

    private void OnDisable()
    {
        Animation?.Kill();
        seq?.Kill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            Collect();
    }

    public void Construct(IPoolService poolService, IPersistentProgressService progress, ISaveLoadService save, AudioHandler audio)
    {
        _save = save;
        _progress = progress;
        _pool = poolService;
        _audio = audio;
    }

    private void Collect()
    {
        _audio.PlaySound(_collectSound);
        _progress.Progress.PlayerData.SetStat(StatsType.Gem, _reward);
        _save.SaveProgress();
        _pool.Return(this);
    }

    public void Spawn(Vector2 position)
    {
        _collider.enabled = false;
        Animation?.Kill();
        transform.position = position;

        _audio.PlaySound(_spawnSound);

        Vector2 randomDir = Random.insideUnitCircle.normalized;

        Vector3 targetPos = transform.position +
                            (Vector3)(randomDir * _flyDistance);

        seq = DOTween.Sequence();

        seq.Append(
            transform.DOScale(1f, 0.2f).From(0f)
                .SetEase(Ease.OutBack)
        );

        seq.Join(
            transform.DOMove(targetPos, _flyDuration)
                .SetEase(Ease.OutQuad)
        );

        seq.Play();

        seq.OnComplete(() =>
        {
            _collider.enabled = true;
            seq.Kill();
            Moving();
        });
    }
}