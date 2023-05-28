using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWorker : Unit
{
    public int miningSpeed;
    public int inventorySize;

    public ResourceBase targetDeposit;



    public override void moveUnit()
    {
        
        //Checks to see if object is a resource, calling harvest funciton if true
        Collider2D collider = Physics2D.OverlapPoint(fields.target_position);
        if (collider != null )
        {
            targetDeposit = collider.GetComponent<ResourceBase>();
            if (targetDeposit != null)
            {
                Harvest();
            }
        }
        
        // check to see if the unit is within range to destination and its not colliding with anything
        bool closeEnough = (fields.target_position - transform.position).magnitude <= GameController.UNIT_ACCEPTABLE_DISTANCE && this.collisions != 0;
        
        // if the unit is not at its target position and its not close enough, keep moving
        if (transform.position != fields.target_position && !closeEnough)
        {
            transform.position = Vector3.MoveTowards(transform.position, fields.target_position, fields.movement_speed * Time.deltaTime);
        }

        

    }
    public void Harvest()
    {
        targetDeposit.MineResource(miningSpeed, this);
    }

    public void DepositResource()
    {

    }
}
