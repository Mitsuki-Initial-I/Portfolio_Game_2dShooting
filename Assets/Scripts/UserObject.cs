using UnityEngine;

public class UserObject : MonoBehaviour
{
    private static UserObject instance = null;
    public static UserObject Instance => instance ?? (instance=GameObject.Find("UserObject").GetComponent<UserObject>());

    private void Awake()
    {
        if (this != Instance) 
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public StagenameEnum selectStageName;
}