using UnityEngine;

public class AccuseEntry : MonoBehaviour
{

    public void Accuse()
    {
        GameManager.Instance.AcusseCurrentNPC(DialogueManager.Instance.currentDialogueNPC);
    }
}
