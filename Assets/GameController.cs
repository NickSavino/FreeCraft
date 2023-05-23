using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
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

    public static readonly float UNIT_ACCEPTABLE_DISTANCE = 1.0f;

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




    private Vector3 startPosition;
    private List<Unit> selected_units;

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
        MoveSelectedUnits();
        KeepMovingSelectedUnits();
        SpawnCavalry();
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
        bool yCheck = mouseSelectionViewScale.min.y >= unitWorldScaleY && mouseSelectionViewScale.max.y <= unitWorldScaleY;

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
                DebugPrintSelectedUnits();
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

    public void MoveSelectedUnits()
    {

        if (Input.GetMouseButtonDown(1))
        {
            foreach (GameObject unit in this.selectedUnits)
            {
                UnitInfantry infantry = unit.GetComponent<UnitInfantry>();
                if (infantry != null)
                {
                    infantry.SetDestination();
                }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selection_area_transform.gameObject.SetActive(true);
            this.startPosition = getMousePos();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mouse_pos = getMousePos();

            //calculates lower left 
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(this.startPosition.x, mouse_pos.x),
                Mathf.Min(this.startPosition.y, mouse_pos.x)
            );

            //calculates upper right
            Vector3 upperRight = new Vector3(
                Mathf.Max(this.startPosition.x, mouse_pos.x),
                Mathf.Max(this.startPosition.y, mouse_pos.y)
            );


            //transorms the selection box
            selection_area_transform.position = lowerLeft;
            selection_area_transform.localScale = upperRight - lowerLeft;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //LMB Released

            //create a an array of all objects inside selected area
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(this.startPosition, getMousePos());


            foreach (Unit unit in selected_units)
            {
                //awakes unit for selection
                unit.SetSelectedVisible(false);
            }

            selected_units.Clear();

            selection_area_transform.gameObject.SetActive(false);


            //select all units within selection area
            foreach(Collider2D collider2D in collider2DArray)
            {

                Unit unit = collider2D.GetComponent<Unit>();
                if (unit != null)
                {
                    selected_units.Add(unit);
                    unit.SetSelectedVisible(true);
                }
            }
            Debug.Log(selected_units.Count);
        }

        if (Input.GetMouseButtonDown(1))
        {
            //gives move command to every selected unit
            foreach (Unit unit in selected_units)
            {
                unit.fields.setTargetPosition(getMousePos());
            }
        }
    }

    public void KeepMovingSelectedUnits()
    {
        foreach (GameObject unit in this.selectedUnits)
        {
            UnitInfantry infantry = unit.GetComponent<UnitInfantry>();
            if (infantry != null)
            {
                infantry.moveUnit();
            }
        }
    }


    public void SpawnCavalry()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newCavalry = new GameObject();
            SpriteRenderer renderer = newCavalry.AddComponent<SpriteRenderer>();
            newCavalry.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Rigidbody2D ridigBody = newCavalry.AddComponent<Rigidbody2D>();
            BoxCollider2D collider = newCavalry.AddComponent<BoxCollider2D>();

            collider.size = new Vector2(0.1f, 0.1f);


            renderer.sprite = Resources.Load<Sprite>("sprite_cavalry");
            renderer.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            renderer.transform.position = new Vector3(renderer.transform.position.x, renderer.transform.position.y, 0);
            newCavalry.AddComponent<UnitInfantry>();
            newCavalry.tag = "Unit";

            
        }



    Vector3 getMousePos() {
        //private helper function returns mouse position
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
