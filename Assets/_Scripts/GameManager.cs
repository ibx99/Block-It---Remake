using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    #region Vars

    [SerializeField] private GameObject ball;           // for accessing the ball
    [SerializeField] private int pointsPerHit;          // for tuneable points per hit

    // one way dependecy on both
    private BallBehaviour ballBehaviour;                // for caching ball behavior script
    private UIManager uiManager;                        // for caching UI Manager script

    // for resetting ball and score management
    private Vector3 ballInitialPos;                     // for resetting ball position
    private int startingScorePerRound = 0;              // for resetting current score per round
    private int currentScore;                           // for managing current score

    // static var for easy accessibility
    public static bool IsGameOver = false;              // useful for further modifications

    #endregion


    #region Unity Methods

    private void Awake()
    {
#if UNITY_EDITOR
        Physics2D.gravity = Vector2.zero;
#endif
    }

    private void OnEnable()
    {
        // CAPPING TARGET FRAME RATE TO 60FPS
        Application.targetFrameRate = 60;

        // subscribing to UI btn callbacks and GameOver
        UIManager.OnStartGameBtnClicked += RoundPlaying;
        UIManager.OnRetryGameBtnClicked += RoundStarting;
        GameOverCollider.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        // for unsubscribing the same
        UIManager.OnStartGameBtnClicked -= RoundPlaying;
        UIManager.OnRetryGameBtnClicked -= RoundStarting;
        GameOverCollider.OnGameOver -= GameOver;
    }

    void Start()
    {
        // caching init ball position for resetting in retry
        ballInitialPos = ball.transform.position;

        // caching dependencies
        ballBehaviour = ball.GetComponent<BallBehaviour>();
        uiManager = FindObjectOfType<UIManager>();

        // initiating round
        RoundStarting();
    }

    #endregion


    #region Public and Private Methods Including Listeners and Helpers

    private void RoundStarting()
    {
        // change sprite color
        ballBehaviour.ChangeBallSprite();

        // resetting and updating UI score
        currentScore = startingScorePerRound;
        UpdateUICurrentScore();

        // resetting ball - position and active state
        ball.transform.position = ballInitialPos;
        ball.SetActive(true);
    }


    // launch the ball
    private void RoundPlaying() => ballBehaviour.LaunchBall();

    // deactivate the ball
    private void GameOver(object sender, EventArgs e)
    {
        // resetting ball velocity and platform pitch value
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ballBehaviour.ResetPitch();
    }
    // called from Bottom Panel script
    public void PointScored()
    {
        currentScore += pointsPerHit;
        SavingManager.Instance.SaveScore(currentScore);
        UpdateUICurrentScore();
    }

    // update current score on screen while playing
    private void UpdateUICurrentScore() => uiManager.UpdateCurrentScore(currentScore);

    #endregion
}
