using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    // Collection of current selected units
    List<GameObject> selectedUnits;

    Rect mouseSelection;
    Vector3 mouseClickPosition;
    Vector3 mouseDragPosition;
    GameObject selectionBox;
    bool mouseHeld = false;
    RectTransform trans;
    



    // Start is called before the first frame update
    void Start()
    {
        this.selectedUnits = new List<GameObject>();
        this.mouseSelection = new Rect();
        this.mouseClickPosition = new Vector3(0, 0, 0);
        this.mouseDragPosition = new Vector3(0, 0, 0);
        trans = gameObject.AddComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        selectUnits();
    }

    private void OnGUI()
    {
        
    }

    public void selectUnits()
    {
        // check to see if the mouse button was clicked this frame
        if (Input.GetMouseButtonDown(0))
        {
            // if it was, set the top-left corner of the box to this position
            this.mouseClickPosition = Input.mousePosition;
            this.mouseClickPosition.z = 10;
            this.mouseSelection.min = Camera.main.ScreenToWorldPoint(this.mouseClickPosition);

        }

        // check to see if the mouse button was held at the beginning of this frame
        if (Input.GetMouseButton(0))
        {
            // if it was, update
            this.mouseDragPosition = Input.mousePosition;
            this.mouseDragPosition.z = 10;
            this.mouseSelection.max = Camera.main.ScreenToWorldPoint(this.mouseDragPosition);


            mouseHeld = true;
        }

        if (!Input.GetMouseButton(0) && mouseHeld)
        {
            mouseHeld = false;
            this.selectedUnits.Clear();
            Vector3 corner1 = this.mouseSelection.min;
            Vector3 corner2 = new Vector3(this.mouseSelection.max.x, this.mouseSelection.min.y, 10);
            Vector3 corner3 = this.mouseSelection.max;
            Vector3 corner0 = new Vector3(this.mouseSelection.min.x, this.mouseSelection.max.y, 10);


            Vector3[] corners = new Vector3[] { corner0, corner1, corner2, corner3 };

            //trans.GetWorldCorners(corners);

            foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
            {   // if within x range
                bool xCheck = Camera.main.ScreenToWorldPoint(unit.transform.position).x >= Camera.main.ScreenToWorldPoint(corners[0]).x && Camera.main.ScreenToWorldPoint(unit.transform.position).x <= Camera.main.ScreenToWorldPoint(corners[2]).x;
                bool yCheck = Camera.main.ScreenToWorldPoint(unit.transform.position).y >= Camera.main.ScreenToWorldPoint(corners[0]).y && Camera.main.ScreenToWorldPoint(unit.transform.position).y <= Camera.main.ScreenToWorldPoint(corners[1]).y;

                if (xCheck && yCheck)
                {
                    this.selectedUnits.Add(unit);
                    Debug.Log("Here");
                }
            }




            foreach (GameObject unit in this.selectedUnits)
            {
                Debug.Log(unit.name);
 
            }

        }
    }

    /**
     * 
     * Function to move current selected unit to target location
     * 
     */
    public void setSelectedTargetLocation()
    {
    
        Vector3 mousePosition = Input.mousePosition;
        Vector2 targetPosition = new Vector2(mousePosition.x, mousePosition.y);



       

    }
}
