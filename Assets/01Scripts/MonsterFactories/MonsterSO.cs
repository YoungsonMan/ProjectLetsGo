using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSO", menuName = "ScriptableObjects/MonsterSO", order = 1)]
public class MonsterSO : ScriptableObject
{
    public GameObject monsterPrefab;
    public int monsterID;
    public string monsterName;
    public float monsterHealth;
    public float monsterDamage;
    public float monsterSpeed;
    
}
