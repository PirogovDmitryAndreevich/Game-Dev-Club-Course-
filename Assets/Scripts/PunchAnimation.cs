using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PunchAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Punch(Vector2 point)
    {
        Instantiate(this, point, Quaternion.identity);        
    }

    private void AnimationEnded()
    {
        Destroy(gameObject);
    }
}
