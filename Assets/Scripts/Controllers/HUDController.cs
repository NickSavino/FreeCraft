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
        SetControlGroup();
        SelectGroup();
       
    }


    private void UpdateActivePortrait()
    {
        // Unit[] selectedUnits = SelectedUnitsShallowCopy();
        Unit[] selectedUnits = GetSelectedGroup();

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
        // Unit[] selectedUnits = SelectedUnitsShallowCopy();
        Unit[] selectedUnits = GetSelectedGroup();
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

        Unit[] group1 = new Unit[MAX_UNITS_SELECTED];
        Unit[] group2 = new Unit[MAX_UNITS_SELECTED];
        Unit[] group3 = new Unit[MAX_UNITS_SELECTED];
        Unit[] group4 = new Unit[MAX_UNITS_SELECTED];
        Unit[] group5 = new Unit[MAX_UNITS_SELECTED];
        Unit[] group6 = new Unit[MAX_UNITS_SELECTED];
        Unit[] group7 = new Unit[MAX_UNITS_SELECTED];
        Unit[] group8 = new Unit[MAX_UNITS_SELECTED];
        Unit[] group9 = new Unit[MAX_UNITS_SELECTED];
        Unit[] group0 = new Unit[MAX_UNITS_SELECTED];
    }



    private void SetControlGroup()
    {
        Unit[] selectedUnits = SelectedUnitsShallowCopy();
        int lengthDiff = MAX_UNITS_SELECTED - selectedUnits.Length;

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            group1 = CopySelectedIntoGroup(selectedUnits, lengthDiff);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            group2 = CopySelectedIntoGroup(selectedUnits, lengthDiff);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            group3 = CopySelectedIntoGroup(selectedUnits, lengthDiff);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha4))
        {
            group4 = CopySelectedIntoGroup(selectedUnits, lengthDiff);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha5))
        {
            group5 = CopySelectedIntoGroup(selectedUnits, lengthDiff);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha6))
        {
            group6 = CopySelectedIntoGroup(selectedUnits, lengthDiff);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha7))
        {
            group7 = CopySelectedIntoGroup(selectedUnits, lengthDiff);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha8))
        {
            group8 = CopySelectedIntoGroup(selectedUnits, lengthDiff);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha9))
        {
            group9 = CopySelectedIntoGroup(selectedUnits, lengthDiff);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha0))
        {
            group0 = CopySelectedIntoGroup(selectedUnits, lengthDiff);
        }
    }


    private Unit[] GetSelectedGroup()
    {
        if (activeGroup[0])
        {
            return group1;
        }
        if (activeGroup[1])
        {
            return group2;
        }
        if (activeGroup[2])
        {
            return group3;
        }
        if (activeGroup[3])
        {
            return group4;
        }
        if (activeGroup[4])
        {
            return group5;
        }
        if (activeGroup[5])
        {
            return group6;
        }
        if (activeGroup[6])
        {
            return group7;
        }
        if (activeGroup[7])
        {
            return group8;
        }
        if (activeGroup[8])
        {
            return group9;
        }
        if (activeGroup[9])
        {
            return group0;
        }
        return SelectedUnitsShallowCopy();
    }


    private void SelectGroup()
    {

        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeGroup[lastSelected] = false;
            activeGroup[0] = true;
            this.lastSelected = 0;
            unitController.ClearSelected();
            unitController.selectedUnits = UnitArrayToList(GetSelectedGroup());
            unitController.SelectControlGroup();

        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            activeGroup[lastSelected] = false;
            activeGroup[1] = true;
   
            this.lastSelected = 1;
            unitController.ClearSelected();
            unitController.selectedUnits = UnitArrayToList(GetSelectedGroup());
            unitController.SelectControlGroup();

        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeGroup[lastSelected] = false;
            activeGroup[2] = true;
            this.lastSelected = 2;
            unitController.ClearSelected();
            unitController.selectedUnits = UnitArrayToList(GetSelectedGroup());
            unitController.SelectControlGroup();

        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha4))
        {
            activeGroup[lastSelected] = false;
            activeGroup[3] = true;
            this.lastSelected = 3;
            unitController.ClearSelected();
            unitController.selectedUnits = UnitArrayToList(GetSelectedGroup());
            unitController.SelectControlGroup();

        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha5))
        {
            activeGroup[lastSelected] = false;
            activeGroup[4] = true;
            this.lastSelected = 4;
            unitController.ClearSelected();
            unitController.selectedUnits = UnitArrayToList(GetSelectedGroup());
            unitController.SelectControlGroup();

        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha6))
        {
            activeGroup[lastSelected] = false;
            activeGroup[5] = true;
            this.lastSelected = 5;
            unitController.ClearSelected();
            unitController.selectedUnits = UnitArrayToList(GetSelectedGroup());
            unitController.SelectControlGroup();

        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha7))
        {
            activeGroup[lastSelected] = false;
            activeGroup[6] = true;
            this.lastSelected = 6;
            unitController.ClearSelected();
            unitController.selectedUnits = UnitArrayToList(GetSelectedGroup());
            unitController.SelectControlGroup();

        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha8))
        {
            activeGroup[lastSelected] = false;
            activeGroup[7] = true;
            this.lastSelected = 7;
            unitController.ClearSelected();
            unitController.selectedUnits = UnitArrayToList(GetSelectedGroup());
            unitController.SelectControlGroup();

        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha9))
        {
            activeGroup[lastSelected] = false;
            activeGroup[8] = true;
            this.lastSelected = 8;
            unitController.ClearSelected();
            unitController.selectedUnits = UnitArrayToList(GetSelectedGroup());
            unitController.SelectControlGroup();

        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha0))
        {
            activeGroup[lastSelected] = false;
            activeGroup[9] = true;
            this.lastSelected = 9;
            unitController.ClearSelected();
            unitController.selectedUnits = UnitArrayToList(GetSelectedGroup());
            unitController.SelectControlGroup();

        }

    }


    private List<Unit> UnitArrayToList(Unit[] arr)
    {
        List<Unit> list = new List<Unit>();

        foreach (Unit u in arr)
        {
            list.Add(u);
        }
        return list;
    }


    private Unit[] CopySelectedIntoGroup(Unit[] selected, int length)
    {
        Unit[] toReturn = new Unit[MAX_UNITS_SELECTED];

        for (int i = 0; i < length; ++i)
        {
            toReturn[i] = selected[i];
        }

        return toReturn;
    }


}
