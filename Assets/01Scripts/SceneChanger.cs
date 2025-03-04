using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance;
    [Header("player Scene")]
    public SceneField playerScene;
    [Header("First Stage")]
    public SceneField firstStage;
    
    [HideInInspector] public SceneLoadTrigger CurrentSceneTrigger;
    public event UnityAction OnCurrentSceneChange;

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
        InitGameScene();
    }

    public void InitGameScene()
    {
        AsyncOperation playerSceneOperation = SceneManager.LoadSceneAsync(playerScene);
        playerSceneOperation.allowSceneActivation = true;
        AsyncOperation firstStageOperation = SceneManager.LoadSceneAsync(firstStage, LoadSceneMode.Additive);
        firstStageOperation.allowSceneActivation = true;
    }
    
    /// <summary>
    /// Check if disposableTrap is used 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool CheckTrigger(Vector3 key)
    {
        if (Manager.Game.DisaposableTrapDic.ContainsKey(key) == false)
        {
            Manager.Game.DisaposableTrapDic.Add(key, true);
        }
        return Manager.Game.DisaposableTrapDic[key];
    }
    /// <summary>
    /// Save if disposableTrap is used.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SaveTrigger(Vector3 key, bool value)
    {
        if (Manager.Game.DisaposableTrapDic.ContainsKey(key))
        {
            Manager.Game.DisaposableTrapDic[key] = value;
        }
    }

    public void SetCurrentSceneTrigger(SceneLoadTrigger trigger)
    {
        CurrentSceneTrigger = trigger;
        OnCurrentSceneChange?.Invoke();
    }

}
