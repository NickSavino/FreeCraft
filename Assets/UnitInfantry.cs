using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfantry : MonoBehaviour, Unit
{

    private UnitFields fields = new UnitFields();



    // Start is called before the first frame update
    void Start()
    {
        fields.setPosition(transform.position);
        fields.setTargetPosition(transform.position);
        fields.setMovementSpeed(10);
    }

    // Update is called once per frame
    void Update() {
        
        if (Input.GetMouseButtonDown(1)) {
            //If RMB pressed, set units target position to mouse position in game world
            fields.setTargetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log("Current Position: " +  transform.position);
            Debug.Log("Target Position:  " + fields.getTargetPosition());
        }

        if (transform.position != fields.getTargetPosition())
        {
            moveUnit();
        }
    }

    void takeDamage() {
        //handles unit taking health
    }

    void onDeath() {
        //called when a units health reaches zero
    }

    void moveUnit() {
        //handles unit movement
        Debug.Log("Move Unit Called");
        //move unit towards target position according to movement speed
        transform.position = Vector3.MoveTowards(transform.position, fields.getTargetPosition(), fields.getMovementSpeed() * Time.deltaTime);
    }

    void attackUnit() {
        //handles unit performing attacks
    }
}
