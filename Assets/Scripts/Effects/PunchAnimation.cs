using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PunchAnimation 
{
    public  void Play(Vector2 point)
    {
        //transform.position = point;
    }    

    // Called in animator
    private void AnimationEnded()
    {
       // ReturnToPool?.Invoke(this);
    }
}
