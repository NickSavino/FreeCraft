using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UnitWorker : Unit
{
    // mining cooldown of unit. Math needs to be tweaked
    public float miningCooldown;
    public int miningYield;
    public float lastGatherActionTime;
    public int inventorySize;

    //how many items the worker unit currently has
    public int currentInv;


    //deposit that the worker is currently mining
    public StructureHeadquarters headquarters;
    public ResourceBase targetDeposit;



 
    public override void moveUnit()
    {

        //Checks to see if object is a resource, calling harvest funciton if true
        Collider2D collider = Physics2D.OverlapPoint(fields.target_position);

        if (collider != null)
        {
            targetDeposit = collider.GetComponent<ResourceBase>();
          //  if component exists, call harvest. checks if inventory is maxed out 
            if (targetDeposit != null)
            {
                if (currentInv >= inventorySize)
                {
                    currentInv = inventorySize;
                    fields.target_position = headquarters.transform.position;
                    DepositResource();
                }
                Harvest();
            }
            else
            {
                HaltUnit();
            }
        }

       // check to see if the unit is within range to destination and its not colliding with anything
        bool closeEnough = (fields.target_position - transform.position).magnitude <= GameController.UNIT_ACCEPTABLE_DISTANCE && this.collisions != 0;

        //if the unit is not at its target position and its not close enough, keep moving
        if (transform.position != fields.target_position && !closeEnough)
        {
            transform.position = Vector3.MoveTowards(transform.position, fields.target_position, fields.movement_speed * Time.deltaTime);
        }

    }

    public void Harvest()
    {

        if (Time.time <= lastGatherActionTime + miningCooldown)
        {
            return;
        }
        if (currentInv >= inventorySize)
        {
            return;
        }
        targetDeposit.MineResource(miningYield, this);
        lastGatherActionTime = Time.time;
    }

    public void DepositResource()
    {
        // resource at the headquarters
        //needs work
        headquarters.ReceiveResources(currentInv);
        currentInv = 0;

    }



    //Find Nearests friendly headquarters

    public void GetNearestHeadquarters()
    {
        //get list of all headquarters
        StructureHeadquarters[] hqList = GameObject.FindObjectsOfType<StructureHeadquarters>();
        float prevDistance = 9999;

        //iterate through and compare the distance of the current hq with the previous one, set the closest as this workers target headquarters
        foreach (var hq in hqList)
        {
            if (hq != null)
            {
                float distance = Vector3.Distance(this.gameObject.transform.position, hq.transform.position);
                Debug.Log(distance);
                Debug.Log(prevDistance);
                if (distance < prevDistance)
                {
                    this.headquarters = hq;
                    prevDistance = distance;
                }
            }
        }
    }
}
