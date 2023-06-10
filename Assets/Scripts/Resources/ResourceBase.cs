using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ResourceType
{
    Mineral, Crystal, Supply
}
public class ResourceBase : MonoBehaviour
{

    //Defines the type of resource that exists
    public ResourceType resourceType;
    
    //How many resources are left in the deposit
    [SerializeField] public double remainingResources;


    //UnityEvent that is invoked when remainingResources is changed
    public UnityEvent onQuantityChange;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnDepletion();
    }


    /*
     * Summary: using UnityEvent callbacks, this function handles how many resources to remove from the deposit,
     * adding that amount to the players inventory
     */
    public void MineResource(int miningYield, UnitWorker gatheringUnit)
    {
        
        remainingResources -= miningYield;
        int resourcesMined = miningYield;


        if (onQuantityChange!= null)
        {
            onQuantityChange.Invoke();
        }

        gatheringUnit.currentInv += resourcesMined;
    }

    

    void OnDepletion()
    {
        if (remainingResources == 0) {
            Destroy(this.gameObject);
        }
    }
}
