using UnityEngine;

public class ConstantsData
{
    public static class AnimatorParameters
    {
        public static readonly int IsWalk = Animator.StringToHash(nameof(IsWalk));
    }

    public static class InputAxis
    {
        public const string HorizontalAxis = "Horizontal";
        public const string VerticalAxis = "Vertical";
    }
}
