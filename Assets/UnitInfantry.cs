using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfantry : MonoBehaviour, Unit
{

    private UnitFields fields = new UnitFields();



    // Start is called before the first frame update
    void Start() {
        
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

    void moveUnit() {
        //handles unit movement
    }

    void attackUnit() {
        //handles unit performing attacks
    }
}
