using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool canInteract = true;
    public string interactText;


    void OnEnterInteractRange()
    {
        if (canInteract)
        {
            HudManager.GetInstance().EnterInteractRange(interactText);
        }
    }

    void OnLeaveInteractRange()
    {
        HudManager.GetInstance().LeaveInteractRange();
    }

    public virtual void Interact()
    {
        Debug.Log($"Interact with " + interactText);
    }

    internal void SetInteractText(string text)
    {
        this.interactText = text;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInteract>().OnInteractRangeEnter(interactText, this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInteract>().OnInteractRangeExit();
        }
    }
}