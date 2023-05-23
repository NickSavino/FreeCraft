using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public static readonly Color DEFAULT_TEMPLATE_COLOR = new Color(256, 256, 256, 0.25f);
   // bool structureSelected;
   // SpriteRenderer globalRenderer;
    bool barracksSelected;
    bool templateActive;
    Texture2D template;
    Rect templateRect;
    Rect worldLocationRect;

    // Start is called before the first frame update
    void Start()
    {
      //  globalRenderer = new SpriteRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        SelectBarracks();
        UnselectStructure();
        BuildStructure();
      //  BuildBarracks();
    }

    private void OnGUI()
    {
        DrawPlaceholder();
    }



    void SelectBarracks()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {


           // GameObject baseObject = new GameObject();
           // SpriteRenderer renderer = baseObject.AddComponent<SpriteRenderer>();
           // renderer.sprite = Resources.Load<Sprite>("sprite_barracks");
          //  globalRenderer.sprite = renderer.sprite;
         //   this.globalRenderer.enabled = true;
            this.barracksSelected = true;
            this.template = Resources.Load<Texture2D>("sprite_barracks");
            this.templateActive = true;
            Debug.Log("Hello, World!");
        }
    }

    void BuildStructure()
    {

        if (this.templateActive && Input.GetKeyDown(KeyCode.Mouse0)) {


            Sprite sprite;
            if (this.barracksSelected)
            {
                sprite = Resources.Load<Sprite>("sprite_barracks");
            }
            else
            {
                sprite = null;
            }


            GameObject baseObject = new GameObject();
            SpriteRenderer renderer = baseObject.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.enabled = true;
            baseObject.AddComponent<Rigidbody2D>();
            baseObject.AddComponent<BoxCollider2D>();
            baseObject.transform.position = new Vector3(templateRect.center.x, templateRect.center.y, 0);
            this.barracksSelected = false;
            this.templateActive = false;
            this.template = null;
            Debug.Log(renderer.isVisible);
        }

    }


    void BuildFactory()
    {

    }


    void BuildStable()
    {

    }



   

    void UnselectStructure()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //this.globalRenderer.enabled = false;
            this.barracksSelected = false;
            this.templateActive = false;
            this.template = null;
        }
    }

    void DrawPlaceholder()
    {
        if (this.templateActive)
        {
            Vector3 mousePos = Input.mousePosition;

             this.templateRect = new Rect(mousePos.x - template.width / 2, (Camera.main.pixelHeight - mousePos.y) - template.height / 2, template.width, template.height);
           // this.templateRect = new Rect(mousePos.x, mousePos.y, template.width, template.height);
            GUI.color = DEFAULT_TEMPLATE_COLOR;
            GUI.DrawTexture(this.templateRect, this.template);
            GUI.color = Color.white;
        }
    }

}
