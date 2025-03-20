using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private Collider _boxCollider;
    
    [SerializeField] private string _targetScene;
    [SerializeField] private string _triggeredScene;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _boxCollider.bounds.size);
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player")) //(collision.gameObject.tag == Manager.Game.Player.gameObject.tag)
        {
            // 트리거가 바라보는 방향으로 빠져나가면 코루틴 행시키기
            var direction = Vector3.Angle(transform.forward, collision.transform.position - transform.position);
            if (direction < 90f)
            {
                Debug.Log("맵로딩 코루틴실행");
                StartCoroutine(LoadTargetScene());
            }
            else
            {
                Debug.Log("Unload Coroutine");
                StartCoroutine(UnloadTargetScene());
            }
        }
    }

    // 불러올 씬 코루틴 함수
    private IEnumerator LoadTargetScene()
    {
        var targetScene = SceneManager.GetSceneByName(_targetScene);
        // 이미 로딩된 씬인지 확인
        if (!targetScene.isLoaded)
        {
            var operation = SceneManager.LoadSceneAsync(_targetScene, LoadSceneMode.Additive);

            while (!operation.isDone)
            {
                yield return null;
            }
        }
    }

    private IEnumerator UnloadTargetScene()
    {
        var targetScene = SceneManager.GetSceneByName(_targetScene);
        
        if (targetScene.isLoaded)
        {
            var currentScene = SceneManager.GetSceneByName(_triggeredScene);
            SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("Player"), currentScene);
            
            var operation = SceneManager.UnloadSceneAsync(targetScene);
            while (!operation.isDone)
            {
                yield return null;
            }
        }
    }

}




