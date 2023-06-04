using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{

    GameObject pauseMenu;
    GameObject hud;
    UnitController unitController;

    GameObject[] smallPortraits;

    Text activeHealth;

    public static readonly int MAX_UNITS_SELECTED = 14;


    private class NumericNameSorter : IComparer<GameObject>
    {
  

        public int Compare(GameObject x, GameObject y)
        {
            int xName = Convert.ToInt32(x.name);
            int yName = Convert.ToInt32(y.name);

            if (xName  == yName)
            {
                return 0;
            }
            if (xName < yName)
            {
                return -1;
            }
            return 0;
        }
    }


    private class UnitNameSorter : IComparer<Unit>
    {


        public int Compare(Unit x, Unit y)
        {
            return String.Compare(x.fields.name, y.fields.name);
        }



    }




    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        hud = GameObject.FindGameObjectWithTag("HUD");
        smallPortraits = GameObject.FindGameObjectsWithTag("SmallPortrait");
        activeHealth = GameObject.FindGameObjectWithTag("ActiveHealth").GetComponent<Text>();
        unitController = GameObject.FindGameObjectWithTag("UnitController").GetComponent<UnitController>();


        Array.Sort(smallPortraits, new NumericNameSorter());

        ClearActivePortrait();
        ClearSmallPortraits();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateActivePortrait();
        UpdateSmallPortraits();
    }


    private void UpdateActivePortrait()
    {
        Unit[] selectedUnits = SelectedUnitsShallowCopy();

        ClearActivePortrait();

        if (selectedUnits.Length != 0)
        {
             hud.transform.Find("Portrait").GetComponent<Image>().sprite = selectedUnits[0].GetComponent<SpriteRenderer>().sprite;
            hud.transform.Find("Portrait").GetComponent<Image>().color = Color.white;
           // activeHealth.text = $"Health: {selectedUnits[0].fields.health})";


        }
        else
        {
            ClearActivePortrait();
        }
    }

    // ugly looking function

    private void UpdateSmallPortraits()
    {
        Unit[] selectedUnits = SelectedUnitsShallowCopy();

        ClearSmallPortraits();

        if (selectedUnits.Length != 0)
        {
            for (int i = 0; i < MAX_UNITS_SELECTED && i < selectedUnits.Length; ++i)
            {
                smallPortraits[i].GetComponent<Image>().sprite = selectedUnits[i].GetComponent<SpriteRenderer>().sprite;
                smallPortraits[i].GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            ClearSmallPortraits();
        }
    }


    private void ClearSmallPortraits()
    {
        for (int i = 0; i < smallPortraits.Length; ++i)
        {
            smallPortraits[i].GetComponent<Image>().sprite = null;
            smallPortraits[i].GetComponent<Image>().color = Color.black;
        }
    }


    private void ClearActivePortrait()
    {
        hud.transform.Find("Portrait").GetComponent<Image>().sprite = null;
        hud.transform.Find("Portrait").GetComponent<Image>().color = Color.black;
    }

    private void ClearActiveHealth()
    {
        activeHealth.text = "";
    }




    private Unit[] SelectedUnitsShallowCopy()
    {

        // make the array
        List<Unit> list = unitController.selectedUnits;
        Unit[] copy = new Unit[list.Count];

        // copy references over
        for (int i = 0; i < copy.Length; ++i)
        {
            copy[i] = list[i];
        }

        // return array of units sorted by name
        Array.Sort(copy, new UnitNameSorter());
        return copy;

    }


}
