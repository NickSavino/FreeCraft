using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureBarracks : MonoBehaviour
{
    public bool mouseIsOver;
    // Start is called before the first frame update
    void Start()
    {
        
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


    public bool getMouseIsOver()
    {
        return this.mouseIsOver;
    }
}
