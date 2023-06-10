using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

    private Vector3 startPosition;


    GameObject hud;
    HUDController hudC;

    public List<Unit> selectedUnits;

    // Start is called before the first frame update
    void Start()
    {
        selectedUnits = new List<Unit>();
        Debug.Log(Input.mousePresent);

        hud = GameObject.FindGameObjectWithTag("HUD");
        if ( hud != null )
        {
            hudC = hud.GetComponent<HUDController>();
        }

    }

    // Update is called once per frame
    void Update()
    {

        HaltListener();


        SelectUnits();

        MoveOrAttack(); 
        
    }

    private void HaltListener()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (Unit unit in selectedUnits)
            {
                unit.fields.target_position = unit.transform.position;
            }
        }
    }

    private Vector3 getMousePos()
    {
        //private helper function returns mouse position

        var mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        
        return Camera.main.ScreenToWorldPoint(mousePos);
    }


    private void SelectUnits()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //LMB Pressed
            startPosition = getMousePos();
        }

        if (Input.GetMouseButtonUp(0))
        {

            //creates an array of all collider 
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(this.startPosition, getMousePos());


            //deselect all units
            foreach (Unit unit in selectedUnits)
            {
                unit.SetSelectedVisible(false);
            }
            // clear selected units
            this.selectedUnits.Clear();

            // add new selected units
            foreach (Collider2D collider2D in collider2DArray)
            {

                Unit unit = collider2D.GetComponent<Unit>();
                if (unit != null)
                {
                    unit.SetSelectedVisible(true);
                    selectedUnits.Add(unit);
                }
            }
        }

    }


    private void MoveOrAttack()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Unit target = null;
            //Creates a collider at rightclick point, fetches unit componenet
            Collider2D collider = Physics2D.OverlapPoint(getMousePos());
            if (collider != null)
            {
                target = collider.GetComponent<Unit>();
            }

            foreach (Unit unit in selectedUnits)
            {
                unit.moveUnit(getMousePos());

                //if target is in fact a unit, attack
                if (target != null)
                {
                    unit.targetUnit = target;
                }
            }


        }
    }

    public void SelectControlGroup(List<Unit> group)
    {
        ClearSelected();
        // HUDController sets this class's selected unit field before this function is called
        
        // add new selected units
        foreach (Unit unit in group)
        {
            unit.SetSelectedVisible(true);
            selectedUnits.Add(unit);
        }
    }

    public void ClearSelected()
    {
        //deselect all units
        foreach (Unit unit in selectedUnits)
        {
            unit.SetSelectedVisible(false);
        }
        // clear selected units
        this.selectedUnits.Clear();
    }


}
