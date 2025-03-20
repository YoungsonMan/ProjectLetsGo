using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] PlayerView _playerView;

    [SerializeField] private int _hp;
    [SerializeField] private int _mp;
    
    // speed
    // power
    // defense
    
    
    
    public int HP { get => _hp; set {_hp = value; _playerView.SetHP(value);} }
    public int MP { get => _mp; set {_mp = value; _playerView.SetMP(value);} }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
