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

    public void SpawnAir()
    {
        if (this.isSelected && Input.GetKeyDown(KeyCode.A))
        {
            GameObject air = Resources.Load("Air") as GameObject;
            air.transform.position = new Vector3(transform.position.x + (2 * transform.localScale.x), transform.position.y + (2 * transform.localScale.x), 0);
            UnitAir script = air.GetComponent<UnitAir>();
            script.SetDestination(new Vector3(this.rallyPoint.transform.position.x, this.rallyPoint.transform.position.y, 0));
            Instantiate(air);



        }
    }
}
