using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TipEntry : MonoBehaviour
{
    public bool suspectIsGuilty;
    public bool isGuiltyTip = false;
    [Space] public string guiltyTextTip;
    private string _defaultTextTip;

    [Header("Animation")] public float animationDuration = 0.15f;

    public Color startColor;
    public Color targetColor;

    private Image _tipImage;

    private TextMeshProUGUI _tipText;

    private void Start()
    {
        if (_tipImage == null)
            _tipImage = GetComponent<Image>();

        _tipText = GetComponentInChildren<TextMeshProUGUI>();
        if (_tipImage != null)
            _tipText.color = new Color(0, 0, 0, 0);

        _defaultTextTip = _tipText.text;
    }

    public void Activate()
    {
        _tipText.text = suspectIsGuilty && !isGuiltyTip ? guiltyTextTip : _defaultTextTip;

        transform.DOScale(Vector3.one * 1.1f, animationDuration).SetEase(Ease.InBounce);
        _tipImage.DOColor(targetColor, animationDuration).SetEase(Ease.InBounce);

        if (_tipImage != null)
            _tipText.DOColor(Color.white, 1f).SetEase(Ease.InCirc);
    }

    public void Reset()
    {
        GetComponent<Button>().interactable = true;
        // _tipImage.color = startColor;
        _tipImage.DOColor(startColor, 0.1f);
        if (_tipImage != null)
            _tipText.color = new Color(0, 0, 0, 0);
        transform.localScale = Vector3.one;
        suspectIsGuilty = false;

        if (isGuiltyTip)
            gameObject.SetActive(false);
    }
}