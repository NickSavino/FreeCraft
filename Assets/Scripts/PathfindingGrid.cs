using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class PathfindingGrid
{
    private int width;
    private int height;
    private float cellSize;

    private Vector3 origin;

    private int[,] gridArray;
    private TextMesh[,] debugTextArray;

    


    //Default constructor
    //Generates a 2d Array of Custom pathfinding Nodes based on width and height
    public PathfindingGrid(int width, int height, float cellSize, Vector3 origin = new Vector3())
    {

        //set member variables
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.origin = origin;


        //initialize array
        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];


        //creates a node for every cell
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[x, y] = CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 5, Color.white, TextAnchor.MiddleCenter);

                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x, y + 1), Color.blue, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.blue, 100f);
            }
        }

        //draws out grid
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.blue, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.blue, 100f);

    }

    //Converts coordinates to world position
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + origin;
    }


    //converts a world position to x and y values, offset y the origin
    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - origin).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - origin).y / cellSize);
    }


    //sets the value in a cell based off its coordinates
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x,y].ToString();
        }
    }


    //takes world position and converts to x and y values, then sets cell value
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }


    //gets a value within a cell on a grid
    //returns -1 if no value is found
    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        return -1;
    }


    //uses worldposition and converts to useable coordinates
    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    //creates text in world
    //handles default values
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 5, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000)
    {
        if (color == null) color = Color.white;
        return CreateWorldText(text, parent, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }


    //override
    public static TextMesh CreateWorldText(string text, Transform parent, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {

        //create textMesh object and sets values
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.fontSize = fontSize;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
}
