using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour, UnitMethods
{

    //import unit stats
    [SerializeField] public UnitFields fields;
    protected GameObject selected_unit;
    protected int collisions;

    //enums describing discrete unit values
    public Teams team;
    public Player player;
    public UnitType unitType;


    //callback invoked when user takes damage
    public UnityEvent onHealthChange;

    //current targetUnit 
    public Unit targetUnit;


    //used to keep track of when unit last attacked
    public float currentAttackTime;
    public float prevAttackTime;

    private void Awake()
    {
        selected_unit = transform.Find("Selected").gameObject;
        SetSelectedVisible(false);

        fields.position = transform.position;
        //  fields.target_position = transform.position;

    }



    public void SetSelectedVisible(bool visible)
    {
        selected_unit.SetActive(visible);
    }

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
        //fields.position = transform.position;
        //fields.target_position = transform.position;
    }

    // Update is called once per frame
    void Update() {


        //if unit has a target unit and the more time has passed then the attack delay, then attack unit
        //if (targetUnit != null)
        //{
        //    currentAttackTime = Time.time;
        //    //currentAttackTime - prevAttackTime = Time elapsed since last attack
        //    if (currentAttackTime - prevAttackTime > fields.attackDelay)
        //    {
        //    prevAttackTime = Time.time;
        //    }
        //}

        attackUnit();

        onDeath();

        moveUnit();


        fields.position = transform.position;
    }

    public virtual void takeDamage(int damage) {
        //handles unit taking health
        fields.health -= damage;
        onHealthChange.Invoke();
    }

    public virtual void onDeath() {
        if (fields.health <= 0)
        {
            //called when a units health reaches zero
            Destroy(this.gameObject);
        }

    }

    public virtual void HaltUnit()
    {
        fields.target_position = fields.position;
        transform.position = fields.position;
    }


    public virtual void moveUnit()
    {
        if (transform.position != fields.target_position)
        {
            // check to see if the unit is within range to destination and its not colliding with anything
            bool closeEnough = (fields.target_position - transform.position).magnitude <= GameController.UNIT_ACCEPTABLE_DISTANCE && this.collisions != 0;

            // if the unit is not at its target position and its not close enough, keep moving
            if (transform.position != fields.target_position && !closeEnough)
            {
                transform.position = Vector3.MoveTowards(transform.position, fields.target_position, fields.movement_speed * Time.deltaTime);
            }
        }
       }

    public virtual void moveUnit(Vector3 target_position)
    {

        //handles unit movement
        //move unit towards target position according to movement speed
        fields.target_position = target_position;
        transform.position = Vector3.MoveTowards(transform.position, fields.target_position, fields.movement_speed * Time.deltaTime);
    }


    /**
     * Handles unit attacking, uses the class member 'targetUnit' to determine where to deal damage
     */
    public virtual void attackUnit() {

        if (targetUnit != null)
        {
            currentAttackTime = Time.time;
            //currentAttackTime - prevAttackTime = Time elapsed since last attack
            if (currentAttackTime - prevAttackTime > fields.attackDelay)
            {


                float distance = Vector3.Distance(transform.position, targetUnit.transform.position);
                if (distance > fields.attackRange)
                {
                    return;
                }

                //if targetUnit exists and they are not on the same team, cause unit to take damage
                if (this.team != targetUnit.team)
                {
                    targetUnit.takeDamage(fields.attack);
                }

                prevAttackTime = Time.time;
            }
        }

    }


    // set the target position for a unit
    //  this may be good to have public so that the GameController class can move units?
    public void SetDestination()
    {
        transform.position = Vector3.MoveTowards(transform.position, fields.target_position, fields.movement_speed * Time.deltaTime);
    }

    public void SetDestination(Vector3 spawnPoint)
    {
        fields.target_position = spawnPoint;
        transform.position = Vector3.MoveTowards(transform.position, fields.target_position, fields.movement_speed * Time.deltaTime);
    }










}



