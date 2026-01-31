using Cysharp.Threading.Tasks.Triggers;
using TMPro;
using UnityEngine;

public class DialogueOptionEntry : MonoBehaviour
{
    public TextMeshProUGUI optionText;
    private string dialogueUID;
    public void ClickOption()
    {
        DialogueManager.Instance.NextDialogue(dialogueUID);
    }

    public void PopulateOption(OptionStruct dialogueOption)
    {
        optionText.text = dialogueOption.optionText;
        dialogueUID = dialogueOption.UIDNextDialogue;
    }
}
