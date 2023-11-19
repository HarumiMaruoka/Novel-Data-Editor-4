// 日本語対応
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public class BranchNode : Node, IMultiParent
    {
        [SerializeField]
        private List<Node> _parents = new List<Node>();
        [SerializeField]
        private List<BranchElement> _elements = new List<BranchElement>();

        public Node Node => this;

        public IReadOnlyList<BranchElement> Elements => _elements;
        public List<Node> Parents => _parents;

        public void InputConnect(Node parent)
        {
            _parents.Add(parent);
        }

        public void InputDisconnect(Node parent)
        {
            _parents.Remove(parent);
        }

        public BranchElement CreateElement()
        {
            var instance = ScriptableObject.CreateInstance<BranchElement>();
            _elements.Add(instance);
            return instance;
        }

        public bool DeleteElement(BranchElement element)
        {
            return _elements.Remove(element);
        }

        private int _index = 0;

        public override void OnEnter()
        {
            Debug.Log("branch node enter");
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _index++;
                if (_index == _elements.Count) _index = 0;
                Debug.Log($"Index: {_index}");
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _index--;
                if (_index == -1) _index = _elements.Count - 1;
                Debug.Log($"Index: {_index}");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_elements[_index].Child != null)
                    _controller.MoveTo(_elements[_index].Child);
            }
        }

        public override void OnExit()
        {
            Debug.Log("branch node exit");
        }
    }
}