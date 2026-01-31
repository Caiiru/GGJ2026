using Cysharp.Threading.Tasks.Triggers;
using TMPro;
using UnityEngine;

public class DialogueOptionEntry : MonoBehaviour
{
    public TextMeshProUGUI optionText;
    private string _dialogueUID;
    public void ClickOption()
    {
        DialogueManager.Instance.NextDialogue(_dialogueUID);
    }

    public void PopulateOption(OptionStruct dialogueOption)
    {
        optionText.text = dialogueOption.optionText;
        _dialogueUID = dialogueOption.UIDNextDialogue;
    }
}
