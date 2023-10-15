using UnityEngine;

public static class FramerateFixer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void FixFramerate()
    {
        Application.targetFrameRate = 60;
    }
}
