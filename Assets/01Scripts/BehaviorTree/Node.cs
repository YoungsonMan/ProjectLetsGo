using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    // 상태
    public enum NodeState   // execution states
    {
        RUNNING, SUCCESS, FAILURE 
    }
    
    public class Node
    {
        protected NodeState state;  // 노드 상태
        
        public Node parent;     // 부모노드
        protected List<Node> children = new List<Node>(); // 자식 노드
        
        private Dictionary<string, object> _dataContext = new Dictionary<string,object>();
        // shared data 를 보관하기 위해 dictionary 구성

        public Node()
        {
            parent = null; 
        }
        

        public Node(List<Node> children)
        { 
            foreach (Node child in children) //순회하면서 노드 연결 | 트리 구축
            {
                attach(child);
            }
        }

        // create edge between node and new child
        private void attach(Node node) //노드 이어주는 edge 
        {
            node.parent = this;
            children.Add(node);
        }
        
        // Evaluate: failure => 반복 키 찾기 (다음노드)
        public virtual NodeState Evaluate() => NodeState.FAILURE;   // virtual로 하고 각 노드를 구성할때 세부화.

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
