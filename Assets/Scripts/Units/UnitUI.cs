using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitUI : MonoBehaviour
{
    public GameObject healthPanel;
    public TextMeshProUGUI healthText;
    public Unit unit;


    private void Update()
    {
        OnHealthChange();
    }

    private void OnMouseEnter()
    {
        healthPanel.SetActive(true);
    }
    
    private void OnMouseExit()
    {
        healthPanel.SetActive(false);
    }

    public void OnHealthChange()
    {
        healthText.text = unit.fields.health.ToString();
    }
}
