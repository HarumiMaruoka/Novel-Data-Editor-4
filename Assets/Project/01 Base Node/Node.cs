﻿using UnityEngine;

namespace Glib.NovelGameEditor
{
    public class Node : ScriptableObject
    {
        [SerializeField]
        private NodeViewData _viewData;

        protected NovelGameController _controller;

        public NodeViewData ViewData => _viewData ??= new NodeViewData();

        public void Initialize(NovelGameController controller)
        {
            _controller = controller;
        }

        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }
}