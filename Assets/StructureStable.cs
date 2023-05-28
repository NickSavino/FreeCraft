using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureStable : Structure
{
    public override void Update()
    {
        base.Update();
        SpawnCavalry();
    }

    public void SpawnCavalry()
    {
        if (this.isSelected && Input.GetKeyDown(KeyCode.C))
        {
            GameObject cavalry = Resources.Load("Cavalry") as GameObject;
            cavalry.transform.position = new Vector3(transform.position.x + (2 * transform.localScale.x), transform.position.y + (2 * transform.localScale.x), 0);
            UnitCavalry script = cavalry.GetComponent<UnitCavalry>();
            script.SetDestination(new Vector3(this.rallyPoint.transform.position.x, this.rallyPoint.transform.position.y, 0));
            Instantiate(cavalry);



        }
    }
}
