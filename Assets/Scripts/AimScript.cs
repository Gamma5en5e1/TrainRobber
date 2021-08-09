using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{
    private GameObject player;
    private PlayerController playercontroller;
    public JoyButton joystickButtton;
    bool btPressed = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playercontroller = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (joystickButtton.pressed)
        {
            btPressed = true;
        }
        else if(!joystickButtton.pressed)
        {
            if(btPressed==true)
            {
                ButtonReleased();
                btPressed = false;
            }
        }
    }

    public void ButtonReleased()
    {
        //Debug.Log(playercontroller.Aim.Vertical + playercontroller.Aim.Horizontal);
        playercontroller.AimButtonReleased = true;
        Debug.Log("Pressed");
    }
}
