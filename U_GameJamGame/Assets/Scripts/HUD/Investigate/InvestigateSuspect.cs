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
        isGuilty = false;

        for (int i = 0; i < tipsContainer.childCount; i++)
        {
            if (tipsContainer.GetChild(i).GetComponent<TipEntry>().isGuiltyTip)
            {
                Debug.Log("Found some guilty tip active");
                if (tipsContainer.GetChild(i).gameObject.activeSelf)
                    tipsContainer.GetChild(i).gameObject.SetActive(false);
            }
        }

        _displayImage.sprite = refNPC.defaultSprite;

        if (GameManager.Instance.assassinNPC != refNPC) return;
        // if is guilty

        _displayImage.sprite = refNPC.guiltySprite;
        isGuilty = true;


        for (int i = 0; i < tipsContainer.childCount; i++)
        {
            if (tipsContainer.GetChild(i).GetComponent<TipEntry>().isGuiltyTip)
            {
                tipsContainer.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void Reset()
    {
        for (int i = 0; i < tipsContainer.childCount; i++)
        {
            tipsContainer.GetChild(i).GetComponent<TipEntry>().Reset();
        }

        for (int i = 0; i < tipsContainer.childCount; i++)
        {
            if (tipsContainer.GetChild(i).GetComponent<TipEntry>().isGuiltyTip)
            {
                tipsContainer.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}