using UnityEngine;

public static class TimeManager 
{
    public static bool IsPause { get; private set; }

    public static void Pause()
    {
        Time.timeScale = 0;
        IsPause = true;
    }

    public static void Run()
    {
        Time.timeScale = 1;
        IsPause = false;
    }
}
