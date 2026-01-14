using UnityEngine;

public class ConstantsData
{
    public static class AnimatorParameters
    {
        public static readonly int IsWalk = Animator.StringToHash(nameof(IsWalk));
        public static readonly int IsOpen = Animator.StringToHash(nameof(IsOpen));
        public static readonly int IsActivated = Animator.StringToHash(nameof(IsActivated));
        public static readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));
        public static readonly int Hit = Animator.StringToHash(nameof(Hit));
        public static readonly int IsDash = Animator.StringToHash(nameof(IsDash));

    }

    public static class InputAxis
    {
        public const string HorizontalAxis = "Horizontal";
        public const string VerticalAxis = "Vertical";
    }
}
