using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange
{
    public static void SceneChangeProcess(ScenesName scenes)
    {
        SceneManager.LoadScene((int)scenes);
    }    
}
public enum ScenesName
{
    Start,
    Home,
    Main,
    Result
}