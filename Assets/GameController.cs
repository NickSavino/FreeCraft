using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * Class for managing UI interactions
 * 
 * TODO: Fix bug that requries the use to double-click before dragging to draw nice boxes
 */
public class GameController : MonoBehaviour
{

    // selection box color and alpha
    public static readonly Color SELECTION_COLOR = new Color(3, 252, 7, 0.25f);

    // Collection of current selected units
    List<GameObject> selectedUnits;


    // world and screen scales for box
    Rect mouseSelectionWorldScale;
    Rect mouseSelectionViewScale;


    // start corner and end corner for selection box
    Vector3 mouseClickPosition;
    Vector3 mouseDragPosition;

    // placeholder texture to allow use of GUI.DrawTexture() for box
    Texture2D boxTexture;

    // boolean indicatin if mouse was previously held, useful for selection unit on release
    bool mouseHeld;




    // Start is called before the first frame update
    void Start()
    {
        //initialize fields
        this.selectedUnits = new List<GameObject>();

        this.mouseSelectionWorldScale = new Rect(0, 0, 0, 0);
        this.mouseSelectionViewScale = new Rect(0, 0, 0, 0);



        Vector3 mouseClickPosition = new Vector3(0, 0, 0);
        Vector3 mouseDragPosition = new Vector3(0, 0, 0);
        this.boxTexture = (Texture2D)Resources.Load("boxTexture");
    }
    




    // Update is called once per frame
    void Update()
    {
        UpdateSelectionBox();
    }

    private void OnGUI()
    {
        DrawScreenSelectionBox();
    }


    /**
     * Function for updating the world and screen scale selection boxes
     * in response to user mouse clicks
     * 
     * 
     */
    private void UpdateSelectionBox()
    {
        // if the user just clicked left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // record this position as top left corner of box in both scales
            this.mouseClickPosition = Input.mousePosition;
            this.mouseSelectionViewScale.min = this.mouseDragPosition;
            this.mouseSelectionWorldScale.min = Camera.main.ViewportToWorldPoint(this.mouseClickPosition);
        }

        // if user is holding down the mouse
        if (Input.GetMouseButton(0))
        {
            //flag it
            this.mouseHeld = true;

            // set the bottom-right corner to the mouse position in both scales
            this.mouseDragPosition = Input.mousePosition;

            this.mouseSelectionViewScale.max = mouseDragPosition;
            this.mouseSelectionWorldScale.max = Camera.main.ViewportToWorldPoint(this.mouseDragPosition);

        }

        // if the user released left click
        if (!Input.GetMouseButton(0) && this.mouseHeld)
        {
            // flag this
            this.mouseHeld = false;

            // clear selected units
            this.selectedUnits.Clear();
            // add new selected units
            SelectUnits();

        }
    }


    /**
     * 
     * Draws the box to the screen
     */
    private void DrawScreenSelectionBox()
    {
        if (Input.GetMouseButton(0))
        {
            // rectangle representing dimensions, need to flip y values for some reason
            Rect drawRect = new Rect(mouseSelectionViewScale.min.x, Screen.height - mouseSelectionViewScale.min.y, mouseSelectionViewScale.width, -mouseSelectionViewScale.height);
            
            // set the GUI color to selection box, draw, and reset color
            GUI.color = SELECTION_COLOR;
            GUI.DrawTexture(drawRect, this.boxTexture);
            GUI.color = Color.white;
        }
    }

    /**
     * Collision detection of seelcted units left to right
     * 
     */
    private bool CheckBoxOverlapLeftToRight(GameObject unit)
    {
        // get world to camera conversion for units
        float unitWorldScaleX = Camera.main.WorldToScreenPoint(unit.transform.position).x;
        float unitWorldScaleY = Camera.main.WorldToScreenPoint(unit.transform.position).y;

        // if x in bounds
        bool xCheck = mouseSelectionViewScale.min.x <= unitWorldScaleX && mouseSelectionViewScale.max.x >= unitWorldScaleX;

        // if y in bounds
        // note again, y needs to be inverted here
        bool yCheck = mouseSelectionViewScale.min.y >= unitWorldScaleY  && mouseSelectionViewScale.max.y <= unitWorldScaleY;
      
        // return x and y in bounds
        return xCheck && yCheck;
    }

    /**
     * Same as LeftToRight version
     */
    private bool CheckBoxOverlapRightToLeft(GameObject unit)
    {
        float unitWorldScaleX = Camera.main.WorldToScreenPoint(unit.transform.position).x;
        float unitWorldScaleY = Camera.main.WorldToScreenPoint(unit.transform.position).y;

        bool xCheck = mouseSelectionViewScale.min.x >= unitWorldScaleX && mouseSelectionViewScale.max.x <= unitWorldScaleX;
        bool yCheck = mouseSelectionViewScale.min.y >= unitWorldScaleY && mouseSelectionViewScale.max.y <= unitWorldScaleY;


        Debug.Log(mouseSelectionViewScale.min.x);
        Debug.Log(mouseSelectionViewScale.min.y);
        Debug.Log(mouseSelectionViewScale.max.x);
        Debug.Log(mouseSelectionViewScale.max.y);
        Debug.Log(unitWorldScaleX);
        Debug.Log(unitWorldScaleY);
        return xCheck && yCheck;
    }


    /**
     * Function for adding units to list of selected units
     * 
     */
    private void SelectUnits()
    {
        // get all units
        // TODO: could probably be optimized, don't need to check every unit in the game
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            // if the box overlaps, select it
            if (CheckBoxOverlapLeftToRight(unit) || CheckBoxOverlapRightToLeft(unit))
            {
                this.selectedUnits.Add(unit);
            }
        }
       
    }

    // for debugging
    private void DebugPrintSelectedUnits()
    {
        foreach (GameObject unit in this.selectedUnits)
        {
            Debug.Log(unit.name);
        }
    }





}
