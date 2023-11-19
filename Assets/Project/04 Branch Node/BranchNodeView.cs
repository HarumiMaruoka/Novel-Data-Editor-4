// 日本語対応
using System;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;

namespace Glib.NovelGameEditor
{
    public class BranchNodeView : NodeView, IInputNodeView
    {
        public BranchNodeView(NodeGraphView graphView, BranchNode node) : base(node, "Assets/Project/04 Branch Node/BranchNodeView.uxml")
        {
            _node = node;
            title = "Branch Node";

            _elementsContainer = this.Q("elements");

            var addButton = this.Q<Button>("add-button");
            addButton.clicked += OnClickedAddElementButton;

            foreach (var elem in node.Elements)
            {
                var view = CreateElementView(elem);

                view.OnNodeSelected += ElementSelected;
                graphView.OnInitialized += () => graphView.CreateEdge(elem);
            }
        }

        private List<BranchElementView> _branchElementViews = new List<BranchElementView>();

        public event Action<NodeView, Node> OnElementSelected;

        private BranchNode _node;
        private VisualElement _elementsContainer;

        public Port InputPort => _input;

        public IInputNode InputConnectable => _node;

        private void OnClickedAddElementButton()
        {
            var elem = _node.CreateElement();
            CreateElementView(elem);
            base.OnSelected();
        }

        private void ElementSelected(Node node)
        {
            OnElementSelected?.Invoke(this, node);
        }

        public BranchElementView CreateElementView(BranchElement element)
        {
            var elemView = new BranchElementView(element);
            _branchElementViews.Add(elemView);
            _elementsContainer.Add(elemView);
            elemView.OnClickedRemoveButton += DeleteElementView;
            return elemView;
        }

        public void DeleteElementView(BranchElementView elementView)
        {
            _node.DeleteElement(elementView.Node);
            _elementsContainer.Remove(elementView);
        }

        public void DeleteElements()
        {
            foreach (var elemView in _branchElementViews)
            {
                DeleteElementView(elemView);
            }
        }
    }
}