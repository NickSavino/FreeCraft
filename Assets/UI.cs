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

    



    // Start is called before the first frame update
    void Start()
    {
        this.selectedUnits = new List<GameObject>();
        this.mouseSelection = new Rect();
        this.mouseClickPosition = new Vector3(0, 0, 0);
        this.mouseDragPosition = new Vector3(0, 0, 0);
     
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
            this.mouseSelection.min = this.mouseDragPosition;
            
        }

        // check to see if the mouse button was held at the beginning of this frame
        if (Input.GetMouseButton(0))
        {
            // if it was, update
            this.mouseDragPosition = Input.mousePosition;
            this.mouseDragPosition.z = 10;
            this.mouseSelection.max = this.mouseDragPosition;

            Debug.Log(this.mouseClickPosition);
            Debug.Log(this.mouseDragPosition);

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
