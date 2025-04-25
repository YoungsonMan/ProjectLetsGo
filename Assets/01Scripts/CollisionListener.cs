using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CollisionListener : MonoBehaviour
{
   private CollisionHandler collisionHandler = new CollisionHandler();

   void Start()
   {
      collisionHandler.OnTriggerExitEvent += OnTriggerExitHandler;
      collisionHandler.OnTriggerEnterEvent += OnTriggerEnterHandler;
   }

 //  private void OnDisable()
 //  {
 //     collisionHandler.OnTriggerExitEvent -= OnTriggerExit;
 //  }

   private void OnTriggerExit(Collider collision)
   {
      collisionHandler.NotifyTriggerExit(collision); // collisionExit 일어나면 알림
   }
   private void OnTriggerEnter(Collider collision)
   {
      collisionHandler.NotifyTriggerEnter(collision);
   }
   
   public static void OnTriggerExitHandler(Collider collision)
   {
      // CollisionExit Logic
      Debug.Log("Trigger Exit 이벤트핸들러 테스트");
      if (collision.gameObject.tag == "Player")
      {
         Debug.Log("asdsadasddsddsa");
      }
   }

   public void OnTriggerEnterHandler(Collider collision)
   {
      Debug.Log("COLLISION ENTER ALERT!!");
   }

   
   
   
}
