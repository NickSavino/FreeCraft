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
    public float attackRange;
    //How long a unit must wait in between attacks, needs to be reworked and implemented as attack speed
    public float attackDelay;
    public float spawnTime;

    public string unitName;

    public Vector3 position;
    public Vector3 target_position;
}
