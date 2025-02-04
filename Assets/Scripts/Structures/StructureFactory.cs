using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureFactory : Structure
{


    public override void Update()
    {
        base.Update();
        SpawnArtillery();
    }

    // Refer to Structure Barracks if confused on this method
    public void SpawnArtillery()
    {
        if (this.isSelected && Input.GetKeyDown(KeyCode.A))
        {
            GameObject artillery = Instantiate(Resources.Load("Prefabs/Artillery")) as GameObject;
            Debug.Log(artillery);
            artillery.transform.position = new Vector3(transform.position.x, transform.position.y - transform.localScale.y, 0);
            UnitArtillery script = artillery.GetComponent<UnitArtillery>();
            script.SetDestination(new Vector3(this.rallyPoint.transform.position.x, this.rallyPoint.transform.position.y, 0));
         //   Instantiate(artillery);


            this.queuedUnit = artillery;
            artillery.SetActive(false);

            this.spawnStartTime = Time.time;
            this.unitSpawnTime = script.fields.spawnTime;
        }
    }

}
