using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController Player;
    
    public bool IsCleared;
    // 클리어시
    public event UnityAction OnCleared;
    // 유다이
    public event UnityAction OnGameOver;
    
    public Dictionary<Vector3, bool> DisaposableTrapDic = new Dictionary<Vector3, bool>(32);
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
