using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureBarracks : MonoBehaviour
{
    private bool mouseIsOver;
    private bool isSelected;
    private GameObject rallyPoint;
    private SpriteRenderer rallyPointSprite;

    //TODO: Magic numbers 20 below are related to camera height. Essentially,
    // the z-coord of a GameObject must be greater than the camera's z
    // for the sprite to render
    //
    // in this case, camera z = 10 


    // Start is called before the first frame update
    void Start()
    {
        this.InstantiateRallyPoint();
        this.SetDefaultRallyPointPosition();
    }

    // Update is called once per frame
    void Update()
    {
        SetRallyPointPositionOnClick();
        SpawnInfrantry();
    }

    private void OnMouseEnter()
    {
        this.mouseIsOver = true;
    }

    private void OnMouseExit()
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


    public void SpawnInfantry()
    {
        //if (this.isSelected && Input.GetKeyDown(KeyCode.I))
        //{
        //    GameObject baseObject = new GameObject();
        //    baseObject.AddComponent<Rigidbody2D>();
        //    baseObject.AddComponent<BoxCollider2D>();
        //    baseObject.tag = "Unit";

        //    SpriteRenderer renderer = baseObject.AddComponent<SpriteRenderer>();
        //    renderer.sprite = Resources.Load<Sprite>("sprite_square");
        //    renderer.color = new Color(256, 256, 256, 1f);

        //    baseObject.transform.position = this.spawnPoint;
        //    baseObject.AddComponent<UnitInfantry>();


        //}
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
            this.rallyPoint.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // invert y-coordinate after conversion
            this.rallyPoint.transform.position = new Vector3(rallyPoint.transform.position.x, 
                                           rallyPoint.transform.position.y, 
                                           20);
        }
    }




    public void SetDefaultRallyPointPosition()
    {
        // convert psoition to screen-scale
        this.rallyPoint.transform.position = new Vector3(transform.position.x, transform.position.y, 20);

        // invert y-coordinate after conversion
        this.rallyPoint.transform.position = new Vector3(rallyPoint.transform.position.x,
                                                        rallyPoint.transform.position.y - transform.localScale.y, 
                                                          20);
    }


    /**
     *  Returns the rally point's position in screen scale
     */
    public Vector3 GetRallyPointScreenScale()
    {
        return Camera.main.WorldToScreenPoint(new Vector3(rallyPoint.transform.position.x, rallyPoint.transform.position.y, 20));
    }



    /**
 *  Returns the rally point's position in world Scale
 */
    public Vector3 GetRallyPointWorldScale()
    {
        return new Vector3(rallyPoint.transform.position.x, rallyPoint.transform.position.y, 20);
    }

    
    public void SpawnInfrantry()
    {
        if (this.isSelected && Input.GetKeyDown(KeyCode.A))
        {
            GameObject infantry = new GameObject();
            SpriteRenderer sprite = infantry.AddComponent<SpriteRenderer>();
            sprite.sprite = Resources.Load<Sprite>("sprite_square");
            infantry.AddComponent<Rigidbody2D>();
            infantry.AddComponent<BoxCollider2D>();
            UnitInfantry script = infantry.AddComponent<UnitInfantry>();
            script.SetDestination(GetRallyPointWorldScale());
            infantry.tag = "Unit";
            infantry.transform.position = transform.position;
            infantry.transform.position = new Vector3(infantry.transform.position.x, infantry.transform.position.y, 20);
        }
    }







}
