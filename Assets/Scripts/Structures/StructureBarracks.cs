using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureBarracks : Structure
{

    public override void Update()
    {
        base.Update();
        QueueInfrantry();
    }

    /**
     * Spawn infantry
     * 
     * 
     */
    protected void QueueInfrantry()
    {
        // if the barracks is selected and M is pressed
        if (this.isSelected && Input.GetKeyDown(KeyCode.M))
        {
          
            // load the prefab
            GameObject infantry = Instantiate(Resources.Load("Prefabs/Infantry")) as GameObject;
         //   Instantiate(infantry);
            // set the position to the default rally point initially (has to exit from structure)
            infantry.transform.position = new Vector3(transform.position.x, transform.position.y - transform.localScale.y, 0);

            // set the destination before calling Awake in UnitInfantry
            UnitInfantry script = infantry.GetComponent<UnitInfantry>();
            script.SetDestination(new Vector3(this.rallyPoint.transform.position.x, this.rallyPoint.transform.position.y, 0));

            this.queuedUnit = infantry;
            infantry.SetActive(false);

            this.spawnStartTime = Time.time;
            this.unitSpawnTime = script.fields.spawnTime;


            // Instantiate, including a call to Awake in unit infantry
            // Instantiate(infantry);

        }
    }

    
    


 








}
