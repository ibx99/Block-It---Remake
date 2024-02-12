using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCollider : MonoBehaviour
{

    // using C# event handler
    public static EventHandler OnGameOver;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // validating ball with player tag - just in case
        if (collision.CompareTag("Player"))
        {
            GameManager.IsGameOver = true;
            // invoking event with a null check
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
    }
}
