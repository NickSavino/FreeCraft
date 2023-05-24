using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

    private Vector3 startPosition;

    private List<Unit> selectedUnits;
    // Start is called before the first frame update
    void Start()
    {
        selectedUnits = new List<Unit>();
        Debug.Log(Input.mousePresent);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //LMB Pressed
            startPosition = getMousePos();
            Debug.Log(startPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {

            Debug.Log(startPosition + " " + getMousePos());
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(this.startPosition, getMousePos());


            //deselect all units
            foreach (Unit unit in selectedUnits)
            {
                unit.SetSelectedVisible(false);
            }
            // clear selected units
            this.selectedUnits.Clear();

            // add new selected units
            Debug.Log("######");
            foreach (Collider2D collider2D in collider2DArray)
            {
               
                Unit unit = collider2D.GetComponent<Unit>();
                if (unit != null)
                {
                    unit.SetSelectedVisible(true);
                    selectedUnits.Add(unit);
                }
            }
            Debug.Log(selectedUnits.Count);
        }

        if (Input.GetMouseButtonDown(1))
        {
            foreach (Unit unit in selectedUnits)
            {
                unit.moveUnit(getMousePos());
            }
        }
    }

    private Vector3 getMousePos()
    {
        //private helper function returns mouse position
        
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
