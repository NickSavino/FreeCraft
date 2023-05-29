using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Base structure class
 * 
 * All structures will extend this class
 * 
 */

public class Structure : MonoBehaviour
{   
    // if the mouse overlaps with transform.position
    protected bool mouseIsOver;

    // if the structure is selected
    protected bool isSelected;
    
    // Each structure will have a rally point
    protected GameObject rallyPoint;
    protected SpriteRenderer rallyPointSprite;


    // TODO: Should maybe go in awake, so it is allocated on GameObject.Instantiate
    // This requires converting structure spawns to using prefabs
    [SerializeField] protected UnitFields fields;



    // Start is called before the first frame update
    public void Start()
    {
        this.InstantiateRallyPoint();
        this.SetDefaultRallyPointPosition();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        SetRallyPointPositionOnClick();
    }

    protected void OnMouseEnter()
    {
        this.mouseIsOver = true;
    }

    protected void OnMouseExit()
    {
        this.mouseIsOver = false;
    }

    private void OnGUI()
    {
        DrawRallyPoint();
    }

    public bool getMouseIsOver()
    {
        return this.mouseIsOver;
    }

    public void setIsSelected(bool val)
    {
        this.isSelected = val;
    }

    public bool getIsSelected()
    {
        return this.isSelected;
    }




    /**
     * Draws the rally point in the game world
     * 
     */
    public void DrawRallyPoint()
    {
        // if is selected, render rally sprite, otherwise dont
        if (this.isSelected)
        {
            this.rallyPointSprite.enabled = true;
        }
        else
        {
            this.rallyPointSprite.enabled = false;
        }
    }



    /**
     * 
     * Essentially a constructor for the rally point
     * 
     * 
     */
    private void InstantiateRallyPoint()
    {
        this.rallyPoint = new GameObject();
        this.rallyPointSprite = this.rallyPoint.AddComponent<SpriteRenderer>();
        this.rallyPointSprite.sprite = Resources.Load<Sprite>("sprite_rally_point");
        this.rallyPointSprite.color = StructureManager.RALLY_POINT_COLOR;
        SetDefaultRallyPointPosition();

    }


    /**
     * Configures a coordinate in world-scale to screen-scale
     * for drawing the rally point
     * 
     * TODO: This could probably be a useful implementation for general
     * world-scale to camera-scale conversions
     * 
     */

    public void SetRallyPointPositionOnClick()
    {
        if (this.isSelected && Input.GetKeyDown(KeyCode.Mouse1))
        {

            // convert psoition to screen-scale
            Vector3 mousePos = Input.mousePosition;

            // invert y-coordinate after conversion
            this.rallyPoint.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z));
        }
    }



    /**
     * Sets the default rally point, in front of structure
     * 
     */
    public void SetDefaultRallyPointPosition()
    {
        // se the position to just below structure
        this.rallyPoint.transform.position = new Vector3(transform.position.x, transform.position.y - transform.localScale.y, 0);
    }


    /**
     *  Returns the rally point's position in screen scale
     */
    public Vector3 GetRallyPointScreenScale()
    {
        return Camera.main.WorldToScreenPoint(new Vector3(rallyPoint.transform.position.x, rallyPoint.transform.position.y, 0));

    }



    /**
 *  Returns the rally point's position in world Scale
 */
    public Vector3 GetRallyPointWorldScale()
    {
        return new Vector3(rallyPoint.transform.position.x, rallyPoint.transform.position.y, 0);
    }








}