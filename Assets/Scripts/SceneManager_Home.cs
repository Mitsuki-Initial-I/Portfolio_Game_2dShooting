using UnityEngine;

public class SceneManager_Home : MonoBehaviour
{
    public void GotoBattle()
    {
        SceneChange.SceneChangeProcess(ScenesName.Main);
    }
    public void GotoTitle()
    {
        SceneChange.SceneChangeProcess(ScenesName.Main);
    }
}
