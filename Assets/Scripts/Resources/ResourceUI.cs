using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mono.Cecil;

public class ResourceUI : MonoBehaviour
{
    public GameObject popupPanel;
    public TextMeshProUGUI resourceQuantityText;
    public ResourceBase resource;


    private void Update()
    {
       OnResourceQuantityChange();
    }

    private void OnMouseEnter()
    {
        popupPanel.SetActive(true);
    }

    private void OnMouseExit()
    {
        popupPanel.SetActive(false);
    }

    public void OnResourceQuantityChange ()
    {
        resourceQuantityText.text = resource.remainingResources.ToString();
    }
}
