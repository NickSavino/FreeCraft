using System;
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

    public static readonly float UNIT_ACCEPTABLE_DISTANCE = 3f;

    // Collection of current selected units


    // world and screen scales for box
    Rect mouseSelectionWorldScale;
    Rect mouseSelectionViewScale;


    // start corner and end corner for selection box
    Vector3 mouseClickPosition;
    Vector3 mouseDragPosition;

    // placeholder texture to allow use of GUI.DrawTexture() for box
    Texture2D boxTexture;

    // Start is called before the first frame update
    void Start()
    {
        //initialize fields
        this.mouseSelectionWorldScale = new Rect(0, 0, 0, 0);
        this.mouseSelectionViewScale = new Rect(0, 0, 0, 0);




        this.boxTexture = (Texture2D)Resources.Load("boxTexture");
        Cursor.SetCursor(Resources.Load<Texture2D>("cursor_regular"), new Vector2(0, 0), CursorMode.Auto);
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

            this.mouseDragPosition = Input.mousePosition;
            // record this position as top left corner of box in both scales
            this.mouseClickPosition = Input.mousePosition;
            this.mouseSelectionViewScale.min = this.mouseDragPosition;
            this.mouseSelectionWorldScale.min = Camera.main.ViewportToWorldPoint(this.mouseClickPosition);
        }

        // if user is holding down the mouse
        if (Input.GetMouseButton(0))
        {
            // set the bottom-right corner to the mouse position in both scales
            this.mouseDragPosition = Input.mousePosition;

            this.mouseSelectionViewScale.max = Input.mousePosition;
            this.mouseSelectionWorldScale.max = Camera.main.ViewportToWorldPoint(this.mouseDragPosition);
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


}