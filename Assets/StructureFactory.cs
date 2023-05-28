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

    public void SpawnArtillery()
    {
        if (this.isSelected && Input.GetKeyDown(KeyCode.A))
        {
            GameObject artillery = Resources.Load("Artillery") as GameObject;
            Debug.Log(artillery);
            artillery.transform.position = new Vector3(transform.position.x + (2 * transform.localScale.x), transform.position.y + (2 * transform.localScale.x), 0);
            UnitArtillery script = artillery.GetComponent<UnitArtillery>();
            script.SetDestination(new Vector3(this.rallyPoint.transform.position.x, this.rallyPoint.transform.position.y, 0));
            Instantiate(artillery);



        }
    }

}
