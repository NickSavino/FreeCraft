using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfantry : MonoBehaviour
{

    private UnitFields fields = new UnitFields();
    private Rigidbody2D rigidBody;
    private new BoxCollider2D collider;

    private int collisions;

    /**
     * These functions keep track of the number of active entities that a unit is colliding with
     * 
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ++collisions;
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        --collisions;
    }

    // Start is called before the first frame update
    void Start()
    {
        fields.setPosition(transform.position);
        fields.setTargetPosition(transform.position);
        fields.setMovementSpeed(10);
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.collider = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {


    }




    void takeDamage() {
        //handles unit taking health
    }

    void onDeath() {
        //called when a units health reaches zero
    }

    public void moveUnit() {

        // check to see if the unit is within range to destination and its not colliding with anything
        bool closeEnough = (fields.getTargetPosition() - transform.position).magnitude <= GameController.UNIT_ACCEPTABLE_DISTANCE && this.collisions != 0;
       
        // if the unit is not at its target position and its not close enough, keep moving
        if (transform.position != fields.getTargetPosition() && !closeEnough)
        {
            transform.position = Vector3.MoveTowards(transform.position, fields.getTargetPosition(), fields.getMovementSpeed() * Time.deltaTime);
        }

        

    }

    // set the target position for a unit
    //  this may be good to have public so that the GameController class can move units?
    public void SetDestination()
    {
        fields.setTargetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    void attackUnit() {
        //handles unit performing attacks
        
    }
}
