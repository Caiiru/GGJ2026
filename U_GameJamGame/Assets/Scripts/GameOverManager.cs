using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public float gameOverAnimationDuration = 3.75f;
    [Space] public float feedbackAnimaionDuration = 0.75f;
    [Header("Buttons")]
    public GameObject retryButton;
    public GameObject backToMenuButton;


    [Header("Victory Screen")] public GameObject victoryScreen;
    public Color victoryTextColor;
    public TextMeshProUGUI victoryText;

    [Header("Lose Screen")] public GameObject loseScreen;
    public Color loseTextColor;
    public TextMeshProUGUI looseText;

    private void Start()
    {
        victoryScreen.SetActive(false);
        loseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        BindEvents();
    }

    private void BindEvents()
    {
        GameManager.Instance.OnVictory += CallVictory;
        GameManager.Instance.OnLoose += CallLoose;
    }

    private void CallLoose(object sender, EventArgs e)
    {
        Loose();
    }

    void CallVictory(object o, EventArgs args)
    {
        Victory();
    }

    private async UniTask Victory()
    {
        await GameOver();

        victoryText.DOColor(new Color(0, 0, 0, 0), 0);
        victoryScreen.SetActive(true);
        loseScreen.SetActive(false);

        victoryText.DOColor(victoryTextColor, feedbackAnimaionDuration);
        await UniTask.WaitForSeconds(feedbackAnimaionDuration);
        await ShowButtons();
    }

    private async UniTask Loose()
    {
        await GameOver();

        looseText.DOColor(new Color(0, 0, 0, 0), 0);
        victoryScreen.SetActive(false);
        loseScreen.SetActive(true);


        looseText.DOColor(loseTextColor, feedbackAnimaionDuration);
        await UniTask.WaitForSeconds(feedbackAnimaionDuration);
        await ShowButtons();
    }

    private async UniTask GameOver()
    {
        HideButtons();
        gameOverScreen.SetActive(true);
        TextMeshProUGUI gameOverText = gameOverScreen.GetComponentInChildren<TextMeshProUGUI>();
        gameOverText.color = Color.black;
        gameOverText.DOScale(1.2f, gameOverAnimationDuration).SetEase(Ease.OutBack);
        gameOverText.DOColor(Color.whiteSmoke, gameOverAnimationDuration);
        await UniTask.WaitForSeconds(gameOverAnimationDuration);
        gameOverText.DOColor(Color.black, feedbackAnimaionDuration);
        await UniTask.WaitForSeconds(feedbackAnimaionDuration);
    }

    private void HideButtons()
    {
        backToMenuButton.SetActive(false);
        retryButton.SetActive(false);
    }
    private async UniTask ShowButtons()
    {
        backToMenuButton.SetActive(true);
        backToMenuButton.transform.localScale = Vector3.zero;
        backToMenuButton.transform.DOScale(1, gameOverAnimationDuration).SetEase(Ease.OutElastic);
        
        
        retryButton.SetActive(true);
        retryButton.transform.localScale = Vector3.zero;
        retryButton.transform.DOScale(1, gameOverAnimationDuration).SetEase(Ease.OutElastic);
    }
}