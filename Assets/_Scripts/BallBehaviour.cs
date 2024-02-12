using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallBehaviour : MonoBehaviour
{
    [Range(-5f, 5f)]
    [SerializeField] private float leftRange, rightRange;

    [SerializeField] private float initialSpeed = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] ballSprites;
    
    [Header("For Audio")]
    [SerializeField] private AudioSource wallAudioSource, platformAudioSource;
    [SerializeField] private string wallTag, platformTag;

    private Rigidbody2D rb;

    private float initialPitchValue;

    private void Start()
    {
        // caching RigidBody
        rb = GetComponent<Rigidbody2D>();
        initialPitchValue = platformAudioSource.pitch;
    }

    // called from game manager
    public void LaunchBall()
    {
        float randomXDir = Random.Range(leftRange, rightRange);
        rb.velocity = new Vector2(randomXDir, initialSpeed);
    }


    // for changing color of sprite -- called from game manager during round starting
    public void ChangeBallSprite()
    {
        spriteRenderer.sprite = GetRandomSprite();
    }


    // for getting a random sprite via recursion method
    // to get any sprite other that what currently is selected already
    private Sprite GetRandomSprite()
    {
        Sprite sprite = ballSprites[Random.Range(0, ballSprites.Length)];
        return (sprite == spriteRenderer.sprite) ? GetRandomSprite() : sprite;
    }


    // handling audio after collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // since the will come from only two form of collisions:
        // walls and bottom platform

        if (collision.gameObject.CompareTag(wallTag))
        {
            wallAudioSource.Play();
        }
        else if (collision.gameObject.CompareTag(platformTag))
        {
            platformAudioSource.pitch += 0.1f;
            platformAudioSource.Play();
        }
    }

    public void ResetPitch()
    {
        platformAudioSource.pitch = initialPitchValue;
    }
}
