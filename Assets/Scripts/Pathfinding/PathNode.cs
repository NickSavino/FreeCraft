using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathNode 
{

    //Node keeps track of its position within the grid
    private int x;
    private int y;


    public PathNode(int x, int y)
    {
        this.x = x;
        this.y = y;
    }


    //Passes the position of the node within the grid and returns using the C# out method
    public (int x, int y) GetPosition()
    {
        return (this.x,  this.y);
    }
}
