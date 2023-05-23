using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureManager : MonoBehaviour
{

    //TODO: MAKE TAGS STATIC GLOBALS
    List<GameObject> selectedStructures;
    public static readonly Color DEFAULT_TEMPLATE_COLOR = new Color(0, 256, 256, 0.25f);
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
        this.selectedStructures = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectBarracks();
        UnselectStructure();
        BuildStructure();
        SelectStructure();
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
        GameObject baseObject = new GameObject();
        if (this.templateActive && Input.GetKeyDown(KeyCode.Mouse0)) {


            Sprite sprite;
            if (this.barracksSelected)
            {
                sprite = Resources.Load<Sprite>("sprite_barracks");
                baseObject.AddComponent<StructureBarracks>();
            }
            else
            {
                sprite = null;
            }

            SpriteRenderer renderer = baseObject.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.color = DEFAULT_TEMPLATE_COLOR;
            renderer.enabled = true;
            Rigidbody2D rigidBody =  baseObject.AddComponent<Rigidbody2D>();
            rigidBody.isKinematic = true;
            baseObject.AddComponent<BoxCollider2D>();
            
            // 10 IS A MAGIC NUMBER HERE, AS OF RN IT IS THE Z-OFFSET OF THE CAMERA
            baseObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(templateRect.center.x, Camera.main.pixelHeight - templateRect.center.y, 0));
            baseObject.transform.position += new Vector3(0, 0, 10);
 
            baseObject.tag = "Structure";
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



    void SelectStructure()
    {
        this.selectedStructures.Clear();
        Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        foreach (GameObject structure in GameObject.FindGameObjectsWithTag("Structure"))
        {
            if (structure.GetComponent<StructureBarracks>() != null)
            {
                StructureBarracks s = structure.GetComponent<StructureBarracks>();
                if (s.getMouseIsOver() && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    this.selectedStructures.Add(structure);
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


