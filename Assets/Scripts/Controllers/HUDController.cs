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

    List<Unit> group1;
    List<Unit> group2;
    List<Unit> group3;
    List<Unit> group4;
    List<Unit> group5;
    List<Unit> group6;
    List<Unit> group7;
    List<Unit> group8;
    List<Unit> group9;
    List<Unit> group0;

    bool[] activeGroup;
    int lastSelected;

    public static readonly int MAX_UNITS_SELECTED = 16;
    public static readonly double DOUBLE_CLICK_DELAY = 0.5f;





    private class NumericNameSorter : IComparer<GameObject>
    {


        public int Compare(GameObject x, GameObject y)
        {
            int xName = Convert.ToInt32(x.name);
            int yName = Convert.ToInt32(y.name);

            if (xName == yName)
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
        // activeHealth = GameObject.FindGameObjectWithTag("ActiveHealth").GetComponent<Text>();
        unitController = GameObject.FindGameObjectWithTag("UnitController").GetComponent<UnitController>();

        lastSelected = 0;

        // one for each control group
        //    this.activeGroup = new bool[10];

        this.group1 = new List<Unit>();
        this.group2 = new List<Unit>();
        this.group3 = new List<Unit>();
        this.group4 = new List<Unit>();
        this.group5 = new List<Unit>();
        this.group3 = new List<Unit>();
        this.group6 = new List<Unit>();
        this.group7 = new List<Unit>();
        this.group8 = new List<Unit>();
        this.group9 = new List<Unit>();
        this.group0 = new List<Unit>();


        //   InitControlGroups();
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
        SelectControlGroup();

        AddToControlGroup();


        // Debug.Log(unitController);

    }


    private void UpdateActivePortrait()
    {
        // Unit[] selectedUnits = SelectedUnitsShallowCopy();
        List<Unit> selectedUnits = unitController.selectedUnits;
  



        ClearActivePortrait();

        if (selectedUnits.Count != 0)
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
        List<Unit> selectedUnits = unitController.selectedUnits;
        selectedUnits.Sort(new UnitNameSorter());

        ClearSmallPortraits();

        if (selectedUnits.Count != 0)
        {


            for (int i = 0;  i < MAX_UNITS_SELECTED && i < selectedUnits.Count; ++i)
            {
                smallPortraits[i].GetComponent<Image>().sprite = selectedUnits[i].GetComponent<SpriteRenderer>().sprite;
                smallPortraits[i].GetComponent<Image>().color = Color.white;
            }
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


    private void SetControlGroup()
    {
        if (Input.GetKey(KeyCode.CapsLock))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                group1 = ListShallowCopy(unitController.selectedUnits);
               
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                group2 = ListShallowCopy(unitController.selectedUnits);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                group3 = ListShallowCopy(unitController.selectedUnits);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                group4 = ListShallowCopy(unitController.selectedUnits);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                group5 = ListShallowCopy(unitController.selectedUnits);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                group6 = ListShallowCopy(unitController.selectedUnits);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                group7 = ListShallowCopy(unitController.selectedUnits);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                group8 = ListShallowCopy(unitController.selectedUnits);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                group9 = ListShallowCopy(unitController.selectedUnits);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                group0 = ListShallowCopy(unitController.selectedUnits);

            }

        }
    }

    private void SelectControlGroup()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                unitController.SelectControlGroup(ListShallowCopy(group1));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                unitController.SelectControlGroup(ListShallowCopy(group2));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                unitController.SelectControlGroup(ListShallowCopy(group3));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                unitController.SelectControlGroup(ListShallowCopy(group4));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                unitController.SelectControlGroup(ListShallowCopy(group5));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                unitController.SelectControlGroup(ListShallowCopy(group6));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                unitController.SelectControlGroup(ListShallowCopy(group7));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                unitController.SelectControlGroup(ListShallowCopy(group8));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                unitController.SelectControlGroup(ListShallowCopy(group9));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                unitController.SelectControlGroup(ListShallowCopy(group0));
            }
        }
    }


    private void AddToControlGroup()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                foreach (Unit u in unitController.selectedUnits)
                {
                    if (!(group1.Contains(u)))
                    {
                        group1.Add(u);
                    }
                }
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                foreach (Unit u in unitController.selectedUnits)
                {
                    if (!(group2.Contains(u)))
                    {
                        group2.Add(u);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                foreach (Unit u in unitController.selectedUnits)
                {
                    if (!(group3.Contains(u)))
                    {
                        group3.Add(u);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                foreach (Unit u in unitController.selectedUnits)
                {
                    if (!(group4.Contains(u)))
                    {
                        group4.Add(u);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                foreach (Unit u in unitController.selectedUnits)
                {
                    if (!(group5.Contains(u)))
                    {
                        group5.Add(u);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                foreach (Unit u in unitController.selectedUnits)
                {
                    if (!(group6.Contains(u)))
                    {
                        group6.Add(u);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                foreach (Unit u in unitController.selectedUnits)
                {
                    if (!(group7.Contains(u)))
                    {
                        group7.Add(u);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                foreach (Unit u in unitController.selectedUnits)
                {
                    if (!(group8.Contains(u)))
                    {
                        group8.Add(u);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                foreach (Unit u in unitController.selectedUnits)
                {
                    if (!(group9.Contains(u)))
                    {
                        group9.Add(u);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                foreach (Unit u in unitController.selectedUnits)
                {
                    if (!(group0.Contains(u)))
                    {
                        group0.Add(u);
                    }
                }
            }
        }
    }

    private List<Unit> ListShallowCopy(List<Unit> src)
    {
        List<Unit> toReturn = new List<Unit>();
        foreach (Unit u in src)
        {
            toReturn.Add(u);
        }
        return toReturn;
    }

}