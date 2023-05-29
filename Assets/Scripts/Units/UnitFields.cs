using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFields : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] public int health;
    public int attack;
    public int defense;
    public int uid; //Discussion on implentation required

    public float movement_speed;
    public float attack_range;

    public string unitName;

    public Vector3 position;
    public Vector3 target_position;

  
}
