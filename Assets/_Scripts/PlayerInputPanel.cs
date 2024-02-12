using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputPanel : MonoBehaviour, IPointerDownHandler
{
    public static EventHandler OnInputButtonPressed;

    // invokes whenever the button is pressed
    public void OnPointerDown(PointerEventData eventData)
    {
        OnInputButtonPressed?.Invoke(this, EventArgs.Empty);
    }

}
