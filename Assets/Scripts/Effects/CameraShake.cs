using Cinemachine;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float _punch = .8f;
    [SerializeField] private float _superPunch = 2.5f;
    [SerializeField] private float _fgSuper;
    [SerializeField] private float _fgPunch;
    [SerializeField] private float _duration = 0.25f;

    private Tween shakeTween;
    private CinemachineBasicMultiChannelPerlin noise;

    private void Awake()
    {
        noise = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        ResetNoise();
    }

    public void ShakePunch() => Shake(_punch);
    public void ShakeSuperPunch() => Shake(_superPunch);

    private void Shake(float intensity)
    {
        shakeTween?.Kill(true);

        noise.m_AmplitudeGain = intensity;
        noise.m_FrequencyGain = intensity > _punch ? _fgSuper : _fgPunch;

        shakeTween = DOTween.To(
            () => noise.m_AmplitudeGain,
            x => noise.m_AmplitudeGain = x,
            0f,
            _duration)
            .SetEase(Ease.OutCubic)
            .Play()
            .OnComplete(() => ResetNoise());
    }

    private void ResetNoise()
    {
        if (noise == null) return;
        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;
    }
}

