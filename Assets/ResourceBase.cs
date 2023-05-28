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
    [SerializeField] public int remainingResources;


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

    public void MineResource(int amount, Unit gatheringUnit)
    {
        remainingResources -= amount;
        int resourcesMined = amount;


        if (onQuantityChange!= null)
        {
            onQuantityChange.Invoke();
        }
    }

    

    void OnDepletion()
    {
        if (remainingResources == 0) {
            Destroy(this.gameObject);
        }
    }
}
