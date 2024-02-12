using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables 


    public delegate void ClickAction();
    
    // For events to fired by UI
    public static event ClickAction OnStartGameBtnClicked;
    public static event ClickAction OnRetryGameBtnClicked;


    [Header("FOR UI PROPS")]
    [SerializeField] private Button startGameBtn;
    [SerializeField] private Button retryGameBtn;
    [SerializeField] private GameObject playerInputPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI currentScoreText;


    [Header("TEXTS FOR GAME OVER PANEL")]
    [SerializeField] private TextMeshProUGUI currentScoreGO, maxScoreGO;


    #endregion


    #region Unity Methods


    // Subscribing to the GameOver event
    private void OnEnable()
    {
        GameOverCollider.OnGameOver += GameOver;
    }


    // Unsubscribing to the GameOver event
    private void OnDisable()
    {
        GameOverCollider.OnGameOver -= GameOver;
    }


    private void Start()
    {
        // adding listeners to the btns
        startGameBtn.onClick.AddListener(StartGameBtnClicked);
        retryGameBtn.onClick.AddListener(RetryGameBtnClicked);
    }


    #endregion


    #region Private Methods - Helps and Event Listeners


    // In-Game Start Button Listener Method
    private void StartGameBtnClicked()
    {
        GameManager.IsGameOver = false;
        OnStartGameBtnClicked?.Invoke();

        startGameBtn.gameObject.SetActive(false);
        playerInputPanel.SetActive(true);
    }


    // Game-Over Retry Method
    private void RetryGameBtnClicked()
    {
        OnRetryGameBtnClicked?.Invoke();

        gameOverPanel.SetActive(false);
        startGameBtn.gameObject.SetActive(true);
    }


    // Event lister method for Game Over Event
    private void GameOver(object sender, EventArgs e)
    {
        playerInputPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        // updating game over panel texts
        UpdateCurrentAndMaxScoreForGameOverPanel();
    }


    // For Updating Current and MaxScore for game over panel
    private void UpdateCurrentAndMaxScoreForGameOverPanel()
    {
        currentScoreGO.text = this.currentScore.ToString();
        maxScoreGO.text = SavingManager.Instance.GetMaxScore().ToString();
    }


    #endregion


    #region Public Methods

    // impltementing this to prevent the dependency cycle. [sort of improvisation]
    int currentScore = 0;
    public void UpdateCurrentScore(int currentScore)
    {
        this.currentScore = currentScore;
        currentScoreText.text = currentScore.ToString();
    }

    #endregion
}
