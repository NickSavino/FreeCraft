using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFields
{
    protected int health;
    protected int attack;
    protected int defense;
    protected int uid; //Discussion on implentation required

    protected float movement_speed;
    protected float attack_range;

    protected string name;

    protected Vector2 position;
    protected Vector2 target_position;


    //GETTERS//

    public int getHealth() { return this.health; }
    public int getAttack() { return this.attack; }
    public int getDefense() { return this.defense; }
    public int getUniqueID() { return this.uid; }


    public float getMovementSpeed() { return this.movement_speed; }
    public float getAttackRange() { return this.attack_range; }


    public string getUnitName() { return this.name; }
    
    public Vector3 getPosition() { return this.position; }
    public Vector3 getTargetPosition() { return this.target_position; }

    //SETTERS//

    public void setHealth(int health) {  this.health = health; }
    public void setAttack(int attack) {  this.attack = attack; }
    public void setDefense(int defense) {  this.defense = defense; }
    public void setUniqueID(int uid) {  this.uid = uid; }


    public void setMovementSpeed(float movement_speed) {  this.movement_speed = movement_speed; }
    public void setAttackRange(float attack_range) {  this.attack_range = attack_range; }


    public void setUnitName(string name) { this.name = name; }
    
    public void setPosition(Vector3 position) {  this.position = position; }
    public void setTargetPosition(Vector3 target_position) { this.target_position = target_position; }

}
