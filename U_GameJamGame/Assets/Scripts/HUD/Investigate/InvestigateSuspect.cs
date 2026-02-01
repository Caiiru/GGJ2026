using System;
using UnityEngine;
using UnityEngine.UI;

public class InvestigateSuspect : MonoBehaviour
{
    public SuspectNPC refNPC;
    public Transform tipsContainer;
    public bool isGuilty = false;
    private Image _displayImage;
    
    private void OnEnable()
    {
        _displayImage = GetComponent<Image>();
        if (GameManager.Instance.assassinNPC == refNPC)
        {
            Debug.Log(refNPC);
            _displayImage.sprite = refNPC.guiltySprite;
            isGuilty = true;
        }
        
    }

    public void Reset()
    {
        Debug.Log("Reset Boy boy oby");
        for (int i = 0; i < tipsContainer.childCount; i++)
        {
            tipsContainer.GetChild(i).GetComponent<TipEntry>().Reset();
        }
    }
}