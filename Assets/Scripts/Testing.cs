using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Testing : MonoBehaviour
{

    private PathfindingGrid grid;



    public void Start()
    {
        grid = new PathfindingGrid(4, 2, 1f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(getMousePos(), 56);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(getMousePos()));
        }
    }

    private Vector3 getMousePos()
    {
        //private helper function returns mouse position

        var mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePos);
    }

}
