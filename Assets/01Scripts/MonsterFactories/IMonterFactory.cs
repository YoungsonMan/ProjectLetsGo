using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterFactories
{
    public interface IMonterFactory
    {
        IMonster GetMonster(MonsterSO monsterSO, int monID);
    }

    public interface IMonster
    {
        void Initialize(MonsterSO _monsterSO);

        void Attack();

        void ReceiveDamage();
    }
    
}

