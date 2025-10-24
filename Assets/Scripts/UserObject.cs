using UnityEngine;

public class UserObject : MonoBehaviour
{
    [SerializeField]
    ScenesName scenesName;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))    
            SceneChange.SceneChangeProcess(scenesName);
    }
}