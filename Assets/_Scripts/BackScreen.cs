using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackScreen : MonoBehaviour
{
    [Range(0.1f, 1f)]
    [SerializeField] private float v_ValueInHSV = 0.8f;               // to set the "V" value in hsv

    [Range(0.1f, 1f)]
    [SerializeField] private float s_ValueInHSV = 0.8f;               // to set the "S" value in hsv

    private SpriteRenderer spriteRenderer;                            // for caching sprite renderer

    // globals to prevent GC allocs over iterative calling
    int hueLimit_UpRange;
    int hueLimit_LowerRange;
    int result;

    private void OnEnable()
    {
        UIManager.OnRetryGameBtnClicked += ChangeBackScreenToRandomColor;
    }

    private void OnDisable()
    {
        UIManager.OnRetryGameBtnClicked -= ChangeBackScreenToRandomColor;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeBackScreenToRandomColor();
    }


    // runs on start of the game as well as every retry
    private void ChangeBackScreenToRandomColor()
    {
        hueLimit_UpRange = Random.Range(200, 360);
        hueLimit_LowerRange = Random.Range(0, 40);

        // getting random between 0, 200 and using 40 as validation to create 
        // best random results
        result = Random.Range(0, 200) <= 40 ? hueLimit_LowerRange : hueLimit_UpRange;

        // dividing result with 360 as the color represents 0 - 360 as 0 - 1
        spriteRenderer.color = Color.HSVToRGB((result / 360f), s_ValueInHSV, v_ValueInHSV);

    }
}
