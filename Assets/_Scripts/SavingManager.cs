using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingManager : MonoBehaviour
{
    #region Vars

    // static for singleton
    public static SavingManager Instance;

    public const string MAX_SCORE = "currentScore";
    private float maxScore;

    #endregion


    #region Unity Methods

    // setting up simple singleton as there isn't any scene change
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }


    private void Start()
    {
        // check for MAX_SCORE when first loads the game
        if (PlayerPrefs.HasKey(MAX_SCORE)) return;
        PlayerPrefs.SetFloat(MAX_SCORE, 0);
    }


    #endregion


    #region Public Methods


    public void SaveScore(int currentScore)
    {
        maxScore = PlayerPrefs.GetFloat(MAX_SCORE);

        // performs validation for whether player crossed max score
        if (maxScore >= currentScore) return;
        PlayerPrefs.SetFloat(MAX_SCORE, currentScore);
    }


    // getter for max score
    public int GetMaxScore() => (int)PlayerPrefs.GetFloat(MAX_SCORE);


    #endregion
}
