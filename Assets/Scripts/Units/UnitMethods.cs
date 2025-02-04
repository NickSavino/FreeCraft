using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UnitMethods
{

    void takeDamage() {
        //handles unit taking health
    }

    void onDeath() {
        //called when a units health reaches zero
    }

    void moveUnit() {
        //handles unit movement
    }

    void moveUnit(Vector3 pos)
    {
        //handles unit movement
    }

    void attackUnit() {
        //handles unit performing attacks
    }

}
