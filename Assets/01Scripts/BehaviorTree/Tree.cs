using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null; // recursively contains the entire tree

        // start에서 셋업하고
        protected void Start()
        {
            _root = SetupTree();
        }

        // update에서 돌면서 evaluate
        private void Update()
        {
            if (_root == null) _root.Evaluate();
        }

        protected abstract Node SetupTree();
    }
    
}
