using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSquare : MonoBehaviour
{

    Texture2D text;
    // Start is called before the first frame update
    void Start()
    {
        this.text = (Texture2D)Resources.Load("boxTexture");
    }


    private void OnGUI()
    {
       // GUI.Button(new Rect(500, 250, 100, 100), "Hello");
        GUI.DrawTexture(new Rect(500, 250, 100, 100), this.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
