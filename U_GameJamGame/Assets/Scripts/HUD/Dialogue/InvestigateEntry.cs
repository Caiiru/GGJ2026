using UnityEngine;

public class InvestigateEntry : MonoBehaviour
{
    public void Investigate()
    {
        SuspectNPC currentTalkingNpc = DialogueManager.Instance.currentDialogueNPC;
        HudManager.GetInstance().InvestigateSuspect(currentTalkingNpc);
    }

    
}