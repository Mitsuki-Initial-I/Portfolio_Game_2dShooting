using UnityEngine;

public class SelectStageButton : MonoBehaviour
{
    [SerializeField]
    StagenameEnum selectStageName;

    public void OnButtonClicked()
    {
        UserObject.Instance.selectStageName = selectStageName;
    }

}