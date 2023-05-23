using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureBarracks : MonoBehaviour
{
    private bool mouseIsOver;
    private bool isSelected;
    private Vector3 spawnPoint;



    // Start is called before the first frame update
    void Start()
    {
        this.spawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        DrawSpawnPoint();
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


    public void DrawSpawnPoint()
    {
        if (this.isSelected)
        {
            GUI.color = StructureManager.SPAWN_POINT_COLOR;
            Texture2D texture = Resources.Load<Texture2D>("sprite_triangle");
            GUI.DrawTexture(new Rect(this.spawnPoint.x, this.spawnPoint.y, texture.width, texture.height), texture);
            GUI.color = Color.white;
        }
    }

    public void drawSpawnPoint()
    {

    }

}
