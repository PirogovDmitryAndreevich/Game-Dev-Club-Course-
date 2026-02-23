using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PunchAnimation : FXBase
{
    public override FXType Type => FXType.Punch;

    public override void Play(Vector2 point)
    {
        transform.position = point;
    }    

    // Called in animator
    private void AnimationEnded()
    {
        ReturnToPool?.Invoke(this);
    }
}
