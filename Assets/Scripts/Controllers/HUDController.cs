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

    Unit[] group1;
    Unit[] group2;
    Unit[] group3;
    Unit[] group4;
    Unit[] group5;
    Unit[] group6;
    Unit[] group7;
    Unit[] group8;
    Unit[] group9;
    Unit[] group0;

    bool[] activeGroup;
    int lastSelected;

    public static readonly int MAX_UNITS_SELECTED = 14;
    public static readonly double DOUBLE_CLICK_DELAY = 0.5f;





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

        lastSelected = 0;

        // one for each control group
        this.activeGroup = new bool[10];




        InitControlGroups();
        Array.Sort(smallPortraits, new NumericNameSorter());

        ClearActivePortrait();
        ClearSmallPortraits();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateActivePortrait();
        UpdateSmallPortraits();
       //SetControlGroup();
       
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


    private void InitControlGroups()
    {

        Unit[] group1= new Unit[MAX_UNITS_SELECTED];
        Unit[] group2= new Unit[MAX_UNITS_SELECTED];
        Unit[] group3= new Unit[MAX_UNITS_SELECTED];
        Unit[] group4= new Unit[MAX_UNITS_SELECTED];
        Unit[] group5= new Unit[MAX_UNITS_SELECTED];
        Unit[] group6= new Unit[MAX_UNITS_SELECTED];
        Unit[] group7= new Unit[MAX_UNITS_SELECTED];
        Unit[] group8= new Unit[MAX_UNITS_SELECTED];
        Unit[] group9= new Unit[MAX_UNITS_SELECTED];
        Unit[] group0= new Unit[MAX_UNITS_SELECTED];
    }



    //private void SetControlGroup()
    //{
    //    Unit[] selectedUnits = SelectedUnitsShallowCopy();
    //    int lengthDiff = MAX_UNITS_SELECTED - selectedUnits.Length;

    //    if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        Array.Copy(selectedUnits, group1, MAX_UNITS_SELECTED - lengthDiff);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        Array.Copy(selectedUnits, group2, MAX_UNITS_SELECTED - lengthDiff);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        Array.Copy(selectedUnits, group3, MAX_UNITS_SELECTED - lengthDiff);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha4))
    //    {
    //        Array.Copy(selectedUnits, group4, MAX_UNITS_SELECTED - lengthDiff);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha5))
    //    {
    //        Array.Copy(selectedUnits, group5, MAX_UNITS_SELECTED - lengthDiff);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha6))
    //    {
    //        Array.Copy(selectedUnits, group6, MAX_UNITS_SELECTED - lengthDiff);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha7))
    //    {
    //        Array.Copy(selectedUnits, group7, MAX_UNITS_SELECTED - lengthDiff);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha8))
    //    {
    //        Array.Copy(selectedUnits, group8, MAX_UNITS_SELECTED - lengthDiff);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha9))
    //    {
    //        Array.Copy(selectedUnits, group9, MAX_UNITS_SELECTED - lengthDiff);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha0))
    //    {
    //        Array.Copy(selectedUnits, group0, MAX_UNITS_SELECTED - lengthDiff);
    //    }
    //}


    //private Unit[] GetSelectedGroup()
    //{
    //    if (activeGroup[0])
    //    {
    //        return group1;
    //    }
    //    if (activeGroup[1])
    //    {
    //        return group2;
    //    }
    //    if (activeGroup[2])
    //    {
    //        return group3;
    //    }
    //    if (activeGroup[3])
    //    {
    //        return group4;
    //    }
    //    if (activeGroup[4])
    //    {
    //        return group5;
    //    }
    //    if (activeGroup[5])
    //    {
    //        return group6;
    //    }
    //    if (activeGroup[6])
    //    {
    //        return group7;
    //    }
    //    if (activeGroup[7])
    //    {
    //        return group8;
    //    }
    //    if (activeGroup[8])
    //    {
    //        return group9;
    //    }
    //    if (activeGroup[9])
    //    {
    //        return group0;
    //    }
    //    return SelectedUnitsShallowCopy();
    //}


    //private void SelectGroup()
    //{

    //    if (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        activeGroup[0] = true;
    //        activeGroup[lastSelected] = false;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        activeGroup[1] = true;
    //        activeGroup[lastSelected] = false;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        activeGroup[2] = true;
    //        activeGroup[lastSelected] = false;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha4))
    //    {
    //        activeGroup[3] = true;
    //        activeGroup[lastSelected] = false;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha5))
    //    {
    //        activeGroup[4] = true;
    //        activeGroup[lastSelected] = false;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha6))
    //    {
    //        activeGroup[5] = true;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha7))
    //    {
    //        activeGroup[6] = true;
    //        activeGroup[lastSelected] = false;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha8))
    //    {
    //        activeGroup[7] = true;
    //        activeGroup[lastSelected] = false;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha9))
    //    {
    //        activeGroup[8] = true;
    //        activeGroup[lastSelected] = false;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha0))
    //    {
    //        activeGroup[9] = true;
    //        activeGroup[lastSelected] = false;
    //    }

    //}

}
