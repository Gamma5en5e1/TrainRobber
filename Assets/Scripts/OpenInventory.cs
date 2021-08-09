using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject closeButton;

    private void Start()
    {
        inventory.SetActive(false);
        closeButton.SetActive(false);
    }

    public void OpenUp()
    {
        inventory.SetActive(true);
        closeButton.SetActive(true);
    }
}
