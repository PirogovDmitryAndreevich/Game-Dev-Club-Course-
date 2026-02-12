using UnityEngine;

public abstract class CharacterSounds : MonoBehaviour
{
    private AudioManager _audioManager;
    private Transform _transform;

    private void Awake()
    {
        if (AudioManager.IsLoaded)
            Init();
        else
            AudioManager.OnLoaded += Init;

        _transform = GetComponent<Transform>();
    }

    private void Init()
    {
        AudioManager.OnLoaded -= Init;
        _audioManager = AudioManager.Instance;
    }

    protected virtual void PlayRandomIndexSound(AudioClip[] clips)
    {
        int clipIndex = Random.Range(0, clips.Length);
        AudioManager.Instance.PlaySound(clips[clipIndex]);
    }

    protected virtual void PlayTimedRandomIndexSound(AudioClip[] clips, ref float nextPlayTime)
    {
        if (nextPlayTime < Time.time)
        {
            int clipIndex = Random.Range(0, clips.Length);
            AudioClip current = clips[clipIndex];
            _audioManager.PlaySound(current);
            nextPlayTime = current.length + Time.time;
        }
    }

    protected virtual void PlayRandomPitchSound(AudioClip clip)
    {
        _audioManager.PlayRandomPithSound(clip);
    }

    protected virtual void PlayTimedPitchSound(AudioClip clip, ref float nextPlaytime)
    {
        if (_audioManager.CanBeHeard(_transform.position))
        {
            if (nextPlaytime < Time.time)
            {
                nextPlaytime = clip.length + Time.time;
                _audioManager.PlayRandomPithSound(clip);
            }
        }
    }
}
