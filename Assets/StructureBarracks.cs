using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureBarracks : MonoBehaviour
{
    private bool mouseIsOver;
    private bool isSelected;
    private Vector3 rallyPoint;



    // Start is called before the first frame update
    void Start()
    {
        this.SetDefaultRallyPoint();
    }

    // Update is called once per frame
    void Update()
    {
       SetRallyPointOnClick();
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
            GUI.color = StructureManager.RALLY_POINT_COLOR;
            Texture2D texture = Resources.Load<Texture2D>("sprite_rally_point");
            GUI.DrawTexture(new Rect(this.rallyPoint.x, this.rallyPoint.y, texture.width, texture.height), texture);
            GUI.color = Color.white;
        }
    }



    /**
     * Configures a coordinate in world-scale to screen-scale
     * for drawing the rally point
     * 
     * TODO: This could probably be a useful implementation for general
     * world-scale to camera-scale conversions
     * 
     */

    public void SetRallyPointOnClick()
    {
        if (this.isSelected && Input.GetKeyDown(KeyCode.Mouse1))
        {
            // convert psoition to screen-scale
            this.rallyPoint = Input.mousePosition;

            // invert y-coordinate after conversion
            this.rallyPoint = new Vector3(rallyPoint.x, Camera.main.pixelHeight - rallyPoint.y, rallyPoint.z);
        }
    }




    public void SetDefaultRallyPoint()
    {
        // convert psoition to screen-scale
        this.rallyPoint = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y, transform.position.z));

        // invert y-coordinate after conversion
        this.rallyPoint = new Vector3(rallyPoint.x, Camera.main.pixelHeight - rallyPoint.y, rallyPoint.z);
    }


    /**
     *  Returns the rally point's position in world scale
     */
    public Vector3 GetRallyPointWorldScale()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(rallyPoint.x, rallyPoint.y, rallyPoint.z));
    }



    /**
 *  Returns the rally point's position in screen Scale
 */
    public Vector3 GetRallyPointScreenScale()
    {
        return new Vector3(rallyPoint.x, rallyPoint.y, rallyPoint.z);
    }







}
