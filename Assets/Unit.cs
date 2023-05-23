using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour, UnitMethods
{

    public UnitFields fields = new UnitFields();
    protected GameObject selected_unit;




    private void Awake()
    {
        selected_unit = transform.Find("Selected").gameObject;
        SetSelectedVisible(false);
    }

    public void SetSelectedVisible(bool visible)
    {
        selected_unit.SetActive(visible);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"called on {this.name} because of collision with {collision.collider.name} ");
    }

    // Start is called before the first frame update
    void Start()
    {
        fields.setPosition(transform.position);
        fields.setTargetPosition(transform.position);
        fields.setMovementSpeed(10);
    }

    // Update is called once per frame
    void Update() {
      

        if (transform.position != fields.getTargetPosition())
        {
                moveUnit(fields.getTargetPosition());
        }
    }

    void takeDamage() {
        //handles unit taking health
    }

    void onDeath() {
        //called when a units health reaches zero
    }


    public void moveUnit(Vector3 target_position) {
        //handles unit movement
        //move unit towards target position according to movement speed
        transform.position = Vector3.MoveTowards(transform.position, fields.getTargetPosition(), fields.getMovementSpeed() * Time.deltaTime);
    }

    void attackUnit() {
        //handles unit performing attacks
    }
}
