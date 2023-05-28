using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureBarracks : Structure
{

    public override void Update()
    {
        base.Update();
        SpawnInfrantry();
    }

    public void SpawnInfrantry()
    {
        if (this.isSelected && Input.GetKeyDown(KeyCode.M))
        {
            GameObject infantry = Resources.Load("Infantry") as GameObject;
            infantry.transform.position = new Vector3(transform.position.x + (2 * transform.localScale.x), transform.position.y + (2 * transform.localScale.x), 0);
            UnitInfantry script = infantry.GetComponent<UnitInfantry>();
            script.SetDestination(new Vector3(this.rallyPoint.transform.position.x, this.rallyPoint.transform.position.y, 0));
            Instantiate(infantry);



        }
    }


    







}
