using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform selection_area_transform;

    private Vector3 startPosition;
    private List<Unit> selected_units;

    private void Awake()
    {
        selected_units = new List<Unit>();
        selection_area_transform.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selection_area_transform.gameObject.SetActive(true);
            this.startPosition = getMousePos();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mouse_pos = getMousePos();

            //calculates lower left 
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(this.startPosition.x, mouse_pos.x),
                Mathf.Min(this.startPosition.y, mouse_pos.x)
            );

            //calculates upper right
            Vector3 upperRight = new Vector3(
                Mathf.Max(this.startPosition.x, mouse_pos.x),
                Mathf.Max(this.startPosition.y, mouse_pos.y)
            );


            //transorms the selection box
            selection_area_transform.position = lowerLeft;
            selection_area_transform.localScale = upperRight - lowerLeft;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //LMB Released

            //create a an array of all objects inside selected area
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(this.startPosition, getMousePos());


            foreach (Unit unit in selected_units)
            {
                //awakes unit for selection
                unit.SetSelectedVisible(false);
            }

            selected_units.Clear();

            selection_area_transform.gameObject.SetActive(false);


            //select all units within selection area
            foreach(Collider2D collider2D in collider2DArray)
            {

                Unit unit = collider2D.GetComponent<Unit>();
                if (unit != null)
                {
                    selected_units.Add(unit);
                    unit.SetSelectedVisible(true);
                }
            }
            Debug.Log(selected_units.Count);
        }

        if (Input.GetMouseButtonDown(1))
        {
            //gives move command to every selected unit
            foreach (Unit unit in selected_units)
            {
                unit.fields.setTargetPosition(getMousePos());
            }
        }
    }

    Vector3 getMousePos() {
        //private helper function returns mouse position
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
