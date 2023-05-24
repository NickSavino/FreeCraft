using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureManager : MonoBehaviour
{

    // global constant colors
    public static readonly Color DEFAULT_TEMPLATE_COLOR = new Color(0, 256, 256, 0.25f);
    public static readonly Color RALLY_POINT_COLOR = new Color(256, 0, 0, 0.5f);

    //TODO: MAKE TAGS STATIC GLOBALS

    // List of currently selected structures
    List<GameObject> selectedStructures;

    // specific structure selection flags
    bool barracksSelected;
    bool factorySelected;
    bool stableSelected;
    bool airstripSelected;


    // flag to indicate that player is choosing a location for a structure
    bool templateActive;

    // texture to draw when choosing where to place structure
    Texture2D template;

    // dimensions of texture for use in GUI.DrawTexture()
    Rect templateRect;
    Rect worldLocationRect;

    // Start is called before the first frame update
    void Start()
    {
        //  globalRenderer = new SpriteRenderer();
        this.selectedStructures = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectBarracks();
        UnselectStructure();
        BuildStructure();
        SelectStructureClick();
        //  BuildBarracks();
    }


    private void OnGUI()
    {
        DrawPlaceholder();
    }


    /**
     * Function for selecting barracks specifically
     * .
     * Allows the barracks sprite to be drawn to screen when
     * placing a barracks.
     * 
     */
    void SelectBarracks()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            // indicate that a barracks is selected
            this.barracksSelected = true;

            // set template texture to barracks sprite
            this.template = Resources.Load<Texture2D>("sprite_barracks");

            // indicate that the user is currently choosing where to place a structure
            this.templateActive = true;
        }
    }


    /**
     * Places a GameObject for the desired structure into the scene.
     * 
     * This is done when a player places a structure
     * 
     */
    void BuildStructure()
    {   
        // declare the GameObject
        GameObject baseObject;

        // If player is placing a structure and the user left-clicks
        if (this.templateActive && Input.GetKeyDown(KeyCode.Mouse0)) {

            // declare the structure's sprite
            Sprite sprite;

            // if barracks is selected, configure sprite to barracks texture
            if (this.barracksSelected)
            {
                baseObject = new GameObject();
                sprite = Resources.Load<Sprite>("sprite_barracks");
                baseObject.AddComponent<StructureBarracks>();
            }
            
            // catch-all else condition, may not be necessary
            else
            {
                sprite = null;
                baseObject = null;
            }

            // Add and configure the SpriteRenderer of the GameObject
            SpriteRenderer renderer = baseObject.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.color = DEFAULT_TEMPLATE_COLOR;
            renderer.enabled = true;

            // Add the rally point's sprite renderer
            renderer.sprite = sprite;
            renderer.color = DEFAULT_TEMPLATE_COLOR;
            renderer.enabled = true;

            // Add and configure the Rigidbody2D of the GameObject
            Rigidbody2D rigidBody = baseObject.AddComponent<Rigidbody2D>();
            rigidBody.isKinematic = true;

            // Add a BoxCollider2D to the GameObject
            baseObject.AddComponent<BoxCollider2D>();

            // TODO: 10 IS A MAGIC NUMBER BELOW, AS OF RN IT IS THE Z-OFFSET OF THE CAMERA, ADDRESS THIS

            // scale the GameObject's position to the world scale position
            baseObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(templateRect.center.x, Camera.main.pixelHeight - templateRect.center.y, 0));
            baseObject.transform.position += new Vector3(0, 0, 10);

            // set tag
            baseObject.tag = "Structure";

            // set structure selected flags
            this.barracksSelected = false;
            this.templateActive = false;
            this.template = null;
        }

    }


    void BuildFactory()
    {

    }




    void BuildStable()
    {

    }






    /**
     * Draws the texture of the structure when choosing where to place it
     * 
     */
    void DrawPlaceholder()
    {
        // if the user is placing a structure
        if (this.templateActive)
        {   
            // get the mouse position, texture will follow this
            Vector3 mousePos = Input.mousePosition;

            // configure the placement position of the texture
            this.templateRect = new Rect(mousePos.x - template.width / 2, (Camera.main.pixelHeight - mousePos.y) - template.height / 2, template.width, template.height);
            
            // draw with the desired color
            // TODO: THIS SHOULD BE SET TO THE USER'S COLOR ONCE WE CREATE A COLOR LOOK-UP TABLE        
            GUI.color = DEFAULT_TEMPLATE_COLOR;

            // then draw the texture and reset the GUI color
            GUI.DrawTexture(this.templateRect, this.template);
            GUI.color = Color.white;
        }
    }


    /**
     * Allows the use to select a structure by left-clicking on it
     * 
     */
    void SelectStructureClick()
    {
        // clear current list of selected items
        this.selectedStructures.Clear();
        foreach (GameObject structure in GameObject.FindGameObjectsWithTag("Structure"))
        {
            if (structure.GetComponent<StructureBarracks>() != null)
            {
                StructureBarracks s = structure.GetComponent<StructureBarracks>();
                if (s.getMouseIsOver() && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    this.selectedStructures.Add(structure);
                    s.setIsSelected(true);
                }
            }
        }
        DebugPrintSelected();

    }


    void UnselectStructure()
    {
        this.selectedStructures.Clear();
        Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //this.globalRenderer.enabled = false;
            this.barracksSelected = false;
            this.templateActive = false;
            this.template = null;
            
        }

        foreach (GameObject structure in GameObject.FindGameObjectsWithTag("Structure"))
        {
            if (structure.GetComponent<StructureBarracks>() != null)
            {
                StructureBarracks s = structure.GetComponent<StructureBarracks>();
                if (!s.getMouseIsOver() && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    this.selectedStructures.Add(structure);
                    s.setIsSelected(false);
                }
            }
        }
        DebugPrintSelected();



    }

    void DebugPrintSelected()
    {
        foreach (GameObject structure in this.selectedStructures)
        {
            Debug.Log(structure.name);
        }
    }
}


