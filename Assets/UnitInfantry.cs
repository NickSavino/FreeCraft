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

    // TODO: Magic number 20 below related to camera transform.position.z = 10

    // Start is called before the first frame update
    void Start()
    {
        // fields.setPosition(transform.position);
        fields.setPosition(Camera.main.ScreenToWorldPoint(transform.position));
        fields.setPosition(new Vector3(fields.getPosition().x, fields.getPosition().y, 20));
        fields.setTargetPosition(Camera.main.ScreenToWorldPoint(transform.position));
        fields.setTargetPosition(new Vector3(transform.position.x, transform.position.y, 20));
        fields.setMovementSpeed(10);
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.collider = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
 
        moveUnit();
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
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, 20), fields.getTargetPosition(), fields.getMovementSpeed() * Time.deltaTime);
        }

        

    }

    // TODO: Magic number 20 below related to camera transform.position.z = 10

    // set the target position for a unit
    //  this may be good to have public so that the GameController class can move units?
    public void SetDestination()
    {
        Vector3 mousePos = Input.mousePosition;

        fields.setTargetPosition(new Vector3(mousePos.x, mousePos.y, 20));
    }

    // assume this vector is in world-scale
    public void SetDestination(Vector3 point)
    {
        fields.setTargetPosition(point);
    }



    void attackUnit() {
        //handles unit performing attacks
        
    }
}
