using UnityEngine;

public class AccuseEntry : MonoBehaviour
{

    public void Accuse()
    {
        _ = GameManager.Instance.AccuseCurrentNPC(DialogueManager.Instance.currentDialogueNPC);
    }
}
