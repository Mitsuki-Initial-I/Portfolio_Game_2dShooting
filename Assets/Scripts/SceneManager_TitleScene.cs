using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_TitleScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SceneChange.SceneChangeProcess(ScenesName.Home);
        }
    }
}