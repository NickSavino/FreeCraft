using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour, UnitMethods
{

    [SerializeField] public UnitFields fields;
    protected GameObject selected_unit;
    protected int collisions;

    public Teams team;
    public Player player;
    public UnitType unitType;

    private void Awake()
    {
        selected_unit = transform.Find("Selected").gameObject;
        SetSelectedVisible(false);
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
        fields.position = transform.position;
        fields.target_position = transform.position;

    }

    // Update is called once per frame
    void Update() {
      

        if (transform.position != fields.target_position)
        {
                moveUnit();
        }

        fields.position = transform.position;
    }

    void takeDamage() {
        //handles unit taking health
    }

    void onDeath() {
        //called when a units health reaches zero
    }

    public virtual void HaltUnit()
    {
        Debug.Log("Halting Unit");
        fields.target_position = fields.position;
        transform.position = fields.position;
    }


    public virtual void moveUnit()
    {

        // check to see if the unit is within range to destination and its not colliding with anything
        bool closeEnough = (fields.target_position - transform.position).magnitude <= GameController.UNIT_ACCEPTABLE_DISTANCE && this.collisions != 0;

        // if the unit is not at its target position and its not close enough, keep moving
        if (transform.position != fields.target_position && !closeEnough)
        {
            transform.position = Vector3.MoveTowards(transform.position, fields.target_position, fields.movement_speed * Time.deltaTime);
        }
    }

    public virtual void moveUnit(Vector3 target_position)
    {
        //handles unit movement
        //move unit towards target position according to movement speed
        fields.target_position = target_position;
        transform.position = Vector3.MoveTowards(transform.position, fields.target_position, fields.movement_speed * Time.deltaTime);
    }

    void attackUnit() {
        //handles unit performing attacks
    }


    // set the target position for a unit
    //  this may be good to have public so that the GameController class can move units?
    public void SetDestination()
    {
        fields.target_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


}



