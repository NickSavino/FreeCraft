using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/**
 * Class for allowing user to create, control, and manage structures
 * 
 * 
 * 
 */


public class StructureManager : MonoBehaviour
{
    

    // TODO: Structure spawning should use prefabs

    // global constant colors
    public static readonly Color DEFAULT_TEMPLATE_COLOR = new Color(0, 256, 256, 0.25f);
    public static readonly Color RALLY_POINT_COLOR = new Color(256, 0, 0, 0.5f);

    //TODO: MAKE TAGS STATIC GLOBALS

    // List of currently selected structures
    public List<GameObject> selectedStructures;

    // specific structure selection flags
    public bool headquartersSelected;
    public bool barracksSelected;
    public bool factorySelected;
    public bool stableSelected;
    public bool airstripSelected;


    Sprite barracksSprite;
    Sprite airstripSprite;
    Sprite headquartersSprite;
    Sprite stableSprite;
    Sprite factorySprite;


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
        InitSprites();
    }

    // Update is called once per frame
    void Update()
    {
        SelectAirstrip();
        SelectStable();
        SelectBarracks();
        SelectFactory();
        SelectHeadquarters();
        UnselectStructure();
        BuildStructure();
        SelectStructureClick();
        Debug.Log(selectedStructures.Count);
        //DrawPlaceholder();
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
        if (Input.GetKeyDown(KeyCode.B) && !this.templateActive)
        {
            // indicate that a barracks is selected
            this.barracksSelected = true;

            // set template texture to barracks sprite
            this.template = Resources.Load<Texture2D>("sprite_barracks");

            // indicate that the user is currently choosing where to place a structure
            this.templateActive = true;
        }
    }


    void SelectFactory()
    {
        if (Input.GetKeyDown(KeyCode.F) && !this.templateActive)
        {
            // indicate that a barracks is selected
            this.factorySelected = true;

            // set template texture to barracks sprite
            this.template = Resources.Load<Texture2D>("sprite_factory");

            // indicate that the user is currently choosing where to place a structure
            this.templateActive = true;
        }
    }


    void SelectStable()
    {
        if (Input.GetKeyDown(KeyCode.S) && !this.templateActive)
        {
            // indicate that a barracks is selected
            this.stableSelected = true;

            // set template texture to barracks sprite
            this.template = Resources.Load<Texture2D>("sprite_stable");

            // indicate that the user is currently choosing where to place a structure
            this.templateActive = true;
        }
    }


    void SelectAirstrip()
    {
        if (Input.GetKeyDown(KeyCode.E) && !this.templateActive)
        {
            // indicate that a barracks is selected
            this.airstripSelected = true;

            // set template texture to barracks sprite
            this.template = Resources.Load<Texture2D>("sprite_airstrip");

            // indicate that the user is currently choosing where to place a structure
            this.templateActive = true;
        }
    }

    void SelectHeadquarters()
    {
        if (Input.GetKeyDown(KeyCode.N) && !this.templateActive)
        {
            // indicate that a barracks is selected
            this.headquartersSelected = true;

            // set template texture to barracks sprite
            this.template = Resources.Load<Texture2D>("sprite_headquarters");

            // indicate that the user is currently choosing where to place a structure
            this.templateActive = true;
        }
    }

    /**
     * Places a GameObject for the desired structure into the scene.
     * 
     * This is done when a player places a structure
     * 
     * TODO: Could probably be optimized
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
            else if (this.factorySelected)
            {
                baseObject = new GameObject();
                sprite = Resources.Load<Sprite>("sprite_factory");
                baseObject.AddComponent<StructureFactory>();
            }

            else if (this.stableSelected)
            {
                baseObject = new GameObject();
                sprite = Resources.Load<Sprite>("sprite_stable");
                baseObject.AddComponent<StructureStable>();
            }
            else if (this.airstripSelected)
            {
                baseObject = new GameObject();
                sprite = Resources.Load<Sprite>("sprite_airstrip");
                baseObject.AddComponent<StructureAirstrip>();
            } else if (this.headquartersSelected)
            {
                baseObject = new GameObject();
                sprite = Resources.Load<Sprite>("sprite_headquarters");
                baseObject.AddComponent<StructureHeadquarters>();
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
            // baseObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(templateRect.center.x, Camera.main.pixelHeight - templateRect.center.y, 0));
            baseObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(templateRect.center.x, Camera.main.pixelHeight - templateRect.center.y, -Camera.main.transform.position.z));
            // set tag
            baseObject.tag = "Structure";

            // set structure selected flags
            this.barracksSelected = false;
            this.templateActive = false;
            this.template = null;
        }

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
     * 
     */
    void SelectStructureClick()
    {
        // clear current list of selected items

        // iterate through each structure
        foreach (GameObject structure in GameObject.FindGameObjectsWithTag("Structure"))
        {

                // set the structure to selected if user clicked on it
                Structure s = structure.GetComponent<Structure>();
                if (s.getMouseIsOver() && Input.GetKeyDown(KeyCode.Mouse0))
                {
                this.selectedStructures.Clear();
                this.selectedStructures.Add(structure);
                    s.setIsSelected(true);
                }
        }

    }



    /**
     * Unselect structure if ESC is clicked, or if user clicked anywhere else
     * 
     * 
     * 
     */

    void UnselectStructure()
    {
        // if escape is clicked
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // reset selection fields in this class
            this.barracksSelected = false;
            this.templateActive = false;
            this.template = null;
            this.selectedStructures.Clear();

            // iterate over each structure, find the type of structure, then set its selected to false
            foreach (GameObject structure in GameObject.FindGameObjectsWithTag("Structure"))
            {
                // unselect the structure using its script
                Structure s = structure.GetComponent<Structure>();
                s.setIsSelected(false);
            }
        }


        else if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    selectedStructures.Clear();
                }
                else
                {
                    foreach (GameObject structure in GameObject.FindGameObjectsWithTag("Structure"))
                    {
                        Structure s = structure.GetComponent<Structure>();

                        // if the user clicked anywhere not on the structure, unselect it
                        if (!s.getMouseIsOver() && Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            s.setIsSelected(false);
                            selectedStructures.Remove(structure);
                        }

                        //   this.selectedStructures.Clear();
                    }
                }
    }


    public Sprite GetActiveSprite()
    {
        if (barracksSelected)
        {
            return barracksSprite;
        }
        if (factorySelected)
        {
            return factorySprite;
        }
        if (airstripSelected)
        {
            return airstripSprite;
        }
        if (stableSelected)
        {
            return stableSprite;
        }
        if (headquartersSelected)
        {
            return headquartersSprite;
        }
        return null;

    }


    private void InitSprites()
    {
        barracksSprite = Resources.Load<Sprite>("sprite_barracks");
        factorySprite = Resources.Load<Sprite>("sprite_factory");
        airstripSprite = Resources.Load<Sprite>("sprite_airstrip");
        stableSprite = Resources.Load<Sprite>("sprite_stable");
        headquartersSprite = Resources.Load<Sprite>("sprite_headquarters");
    }

    //public void SelectControlGroup(List<Structure> group)
    //{
    //    ClearSelected();
    //    // HUDController sets this class's selected unit field before this function is called

    //    // add new selected units
    //    foreach (Structure struc in group)
    //    {
    //      //  struc.SetSelectedVisible(true);
    //        selectedStructures.Add(struc);
    //    }
    //}

    //public void ClearSelected()
    //{
    //    //deselect all units
    //    foreach (GameObject struc in selectedStructures)
    //    {
    //     //   unit.SetSelectedVisible(false);
    //    }
    //    // clear selected units
    //    this.selectedUnits.Clear();
    //}

}


