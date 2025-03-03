using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private SceneField[] sceneToLoad;
    private SceneField mandatoryScene;
    BoxCollider boxCollider;
    bool unloadableScene = true;
    List<Scene> unloadedScenes = new List<Scene>(2);


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
