using UnityEngine;

public class SceneManager_Result : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SceneChange.SceneChangeProcess(ScenesName.Home);
        }
    }
}
