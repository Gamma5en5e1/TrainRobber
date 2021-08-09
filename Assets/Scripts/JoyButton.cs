using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pressed;


    public void OnPointerDown(PointerEventData eventdata)
    {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventdata)
    {
        pressed = false;
    }
}
