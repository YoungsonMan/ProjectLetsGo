using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    // 상태
    public enum NodeState
    {
        RUNNING, SUCCESS, FAILURE
    }
    
    public class Node
    {
        protected NodeState state;
        
        public Node parent;
        protected List<Node> children = new List<Node>();
        
        private Dictionary<string, object> _dataContext = new Dictionary<string,object>();

        public Node()
        {
            parent = null;
        }
        

        public Node(List<Node> children)
        { 
            foreach (Node child in children) //순회하면서 노드 연결
            {
                attach(child);
            }
        }

        // create edge between node and new child
        private void attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }
        
        // Evaluate: failure => 반복 키 찾기 (다음노드)
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        // setting the data
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }
        /// <summary>
        ///  key 찾기 tree root까지 찾을때까지 recursive & 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetData(string key)
        {
            object value = null;
            if(_dataContext.TryGetValue(key, out value)) return value;

            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null) return value;
                node = node.parent;
            }
            return null;
        }
        /// <summary>
        ///   반복하며 key 찾지만 root에 도달하면 요청을 무시
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }
            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared) return true;
                node = node.parent;
            }
            return false;
        }

    }
}
