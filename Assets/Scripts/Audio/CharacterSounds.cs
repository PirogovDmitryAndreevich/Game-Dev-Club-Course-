using UnityEngine;

public abstract class CharacterSounds : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] protected AudioManager CurrentAudioManager;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    public void PlaySound(AudioClip clip)
    {
        CurrentAudioManager.PlaySound(clip);
    }

    protected virtual void PlayRandomIndexSound(AudioClip[] clips)
    {
        if (!CurrentAudioManager.IsLoaded)
            return;

        int clipIndex = Random.Range(0, clips.Length);
        CurrentAudioManager.PlaySound(clips[clipIndex]);
    }

    protected virtual void PlayTimedRandomIndexSound(AudioClip[] clips, ref float nextPlayTime)
    {
        if (!CurrentAudioManager.IsLoaded)
            return;

        if (nextPlayTime < Time.time)
        {
            int clipIndex = Random.Range(0, clips.Length);
            AudioClip current = clips[clipIndex];
            CurrentAudioManager.PlaySound(current);
            nextPlayTime = current.length + Time.time;
        }
    }

    protected virtual void PlayRandomPitchSound(AudioClip clip)
    {
        if (!CurrentAudioManager.IsLoaded)
            return;

        CurrentAudioManager.PlayRandomPithSound(clip);
    }

    protected virtual void PlayTimedPitchSound(AudioClip clip, ref float nextPlaytime)
    {
        if (!CurrentAudioManager.IsLoaded)
            return;

        if (CurrentAudioManager.CanBeHeard(_transform.position))
        {
            if (nextPlaytime < Time.time)
            {
                nextPlaytime = clip.length + Time.time;
                CurrentAudioManager.PlayRandomPithSound(clip);
            }
        }
    }
}
