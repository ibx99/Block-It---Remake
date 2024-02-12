using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottomPanel : MonoBehaviour
{
    [Range(1.1f, 2f)]
    [SerializeField] private float velocityAdditive = 1.1f;

    private const string SWITCH_ON_ANIMATION_TRIGGER = "SwitchOn";

    private Animator anim;
    private int animationHashID = 0;

    // one - way dependency over game manager
    private GameManager gameManager;


    #region Unity Methods

    // subscribing and unsubscribing for player tap event

    private void OnEnable()
    {
        PlayerInputPanel.OnInputButtonPressed += PlayerTapReceived;
    }

    private void OnDisable()
    {
        PlayerInputPanel.OnInputButtonPressed -= PlayerTapReceived;
    }


    private void Start()
    {
        // caching animator and gamemanager
        gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();

        // creting animator hash for better perfomance
        animationHashID = Animator.StringToHash(SWITCH_ON_ANIMATION_TRIGGER);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // to prevent false collision after ball corssing the top barrier
        if (GameManager.IsGameOver) return;

        // validating the ball with Player Tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // accessing the RigidBody and adding velocity on every hit
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 v = rb.velocity;
            rb.velocity = new Vector2(v.x + velocityAdditive, v.y + velocityAdditive);

            // calling on game manager
            gameManager.PointScored();
        }
    }


    #endregion


    // event callback for player tapping on screen
    private void PlayerTapReceived(object sender, EventArgs e)
    {
        anim.SetTrigger(animationHashID);
    }


}
