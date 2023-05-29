using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureAirstrip : Structure
{
    public override void Update()
    {
        base.Update();
        SpawnAir();
    }

    // Refer to Structure Barracks if confused on this method
    public void SpawnAir()
    {
        if (this.isSelected && Input.GetKeyDown(KeyCode.A))
        {
            GameObject air = Resources.Load("Air") as GameObject;
            air.transform.position = new Vector3(transform.position.x, transform.position.y - transform.localScale.y, 0);
            UnitAir script = air.GetComponent<UnitAir>();
            script.SetDestination(new Vector3(this.rallyPoint.transform.position.x, this.rallyPoint.transform.position.y, 0));
            Instantiate(air);



        }
    }
}
