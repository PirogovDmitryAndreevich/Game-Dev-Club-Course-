using UnityEngine;

public abstract class CharacterSounds : MonoBehaviour
{
    protected AudioHandler CurrentAudioManager;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    public void PlaySound(AudioClip clip)
    {
        CurrentAudioManager.PlaySound(clip);
    }

    public void Construct(AudioHandler manager)
    {
        CurrentAudioManager = manager;
    }

    protected virtual void PlayRandomIndexSound(AudioClip[] clips)
    {

        int clipIndex = Random.Range(0, clips.Length);
        CurrentAudioManager.PlaySound(clips[clipIndex]);
    }

    protected virtual void PlayTimedRandomIndexSound(AudioClip[] clips, ref float nextPlayTime)
    {
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
        CurrentAudioManager.PlayRandomPithSound(clip);
    }

    protected virtual void PlayTimedPitchSound(AudioClip clip, ref float nextPlaytime)
    {
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
