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

    // Refer to Structure Barracks if confused on this method
    public void SpawnCavalry()
    {
        if (this.isSelected && Input.GetKeyDown(KeyCode.C))
        {
            GameObject cavalry = Instantiate(Resources.Load("Cavalry")) as GameObject;
            cavalry.transform.position = new Vector3(transform.position.x, transform.position.y - transform.localScale.y, 0);
            UnitCavalry script = cavalry.GetComponent<UnitCavalry>();
            script.SetDestination(new Vector3(this.rallyPoint.transform.position.x, this.rallyPoint.transform.position.y, 0));
          //  Instantiate(cavalry);

            this.queuedUnit = cavalry;
            cavalry.SetActive(false);

            this.spawnStartTime = Time.time;
            this.unitSpawnTime = script.fields.spawnTime;

        }
    }
}
