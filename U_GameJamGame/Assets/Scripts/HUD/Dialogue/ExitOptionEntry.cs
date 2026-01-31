using UnityEngine;

public class ExitOptionEntry : MonoBehaviour
{

    public void CloseDialogue()
    {
        GameManager.Instance.ExitOnDialogue();
    }
}
