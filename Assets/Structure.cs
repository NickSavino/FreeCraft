using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    protected bool mouseIsOver;
    protected bool isSelected;
    protected GameObject rallyPoint;
    protected SpriteRenderer rallyPointSprite;


    // TODO: Should maybe go in awake, so it is allocated on GameObject.Instantiate
    protected UnitFields fields = new UnitFields();


    //TODO: Magic numbers 20 below are related to camera height. Essentially,
    // the z-coord of a GameObject must be greater than the camera's z
    // for the sprite to render
    //
    // in this case, camera z = 10 


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





    public void DrawRallyPoint()
    {
        if (this.isSelected)
        {
            this.rallyPointSprite.enabled = true;
        }
        else
        {
            this.rallyPointSprite.enabled = false;
        }
    }


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




    public void SetDefaultRallyPointPosition()
    {
        // convert psoition to screen-scale
        this.rallyPoint.transform.position = new Vector3(rallyPoint.transform.position.x,
                                                rallyPoint.transform.position.y,
                                                  0);
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