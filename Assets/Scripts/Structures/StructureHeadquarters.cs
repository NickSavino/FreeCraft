using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureHeadquarters : Structure
{
    public override void Update()
    {
        base.Update();
        SpawnHeadquarters();
    }

    /**
     * Spawn worker
     * 
     * 
     */
    public void SpawnHeadquarters()
    {
        // if the headquarters is selected and W is pressed
        if (this.isSelected && Input.GetKeyDown(KeyCode.W))
        {
            // load the prefab
            GameObject worker = Resources.Load("Worker") as GameObject;

            // set the position to the default rally point initially (has to exit from structure)
            worker.transform.position = new Vector3(transform.position.x, transform.position.y - transform.localScale.y, 0);

            // set the destination before calling Awake in UnitWorker
            UnitWorker script = worker.GetComponent<UnitWorker>();
            script.SetDestination(new Vector3(this.rallyPoint.transform.position.x, this.rallyPoint.transform.position.y, 0));

            // Instantiate, including a call to Awake in unit worker
            //Instantiate(worker);

            this.queuedUnit = worker;
            worker.SetActive(false);

            this.spawnStartTime = Time.time;
            this.unitSpawnTime = script.fields.spawnTime;

        }
    }
}