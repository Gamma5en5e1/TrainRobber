using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject closeButton;

    public void OnClose()
    {
        inventory.SetActive(false);
        closeButton.SetActive(false);
    }
}
