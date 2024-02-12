using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetList : MonoBehaviour
{
    public Button getListButton;

    // Start is called before the first frame update
    void Start()
    {
        getListButton.onClick.AddListener(GetListMethod);
    }

    private void GetListMethod()
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject pluginReference = new AndroidJavaObject("com.example.thisthat");
    }
}
