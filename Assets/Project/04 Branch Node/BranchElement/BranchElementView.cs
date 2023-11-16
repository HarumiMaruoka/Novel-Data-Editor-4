// 日本語対応
using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Glib.NovelGameEditor
{
    public class BranchElementView : VisualElement, IOutputNodeView
    {
        private BranchElement _node;
        private Port _output = null;

        public event Action<Node> OnNodeSelected;

        public BranchElementView(BranchElement node)
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>
                ("Assets/Project/04 Branch Node/BranchElement/BranchElementView.uxml");
            visualTree.CloneTree(this);

            _node = node;

            this.viewDataKey = node.ViewData.GUID;
            Direction direction = Direction.Output;

            _output = CreatePort(direction, Port.Capacity.Single);
            var outputContainer = this.Q("output");
            outputContainer.Add(_output);


            var mainButton = this.Q<Button>("main");
            mainButton.clicked += Select;
            mainButton.text = "Branch Element";
            this.Q<Button>("remove").clicked += ClickedRemoveButton;
        }

        public BranchElement Node => _node;
        public Port OutputPort => _output;
        public IOutputNode OutputConnectable => _node;

        public event Action<BranchElementView> OnClickedRemoveButton;

        private Port CreatePort(Direction direction, Port.Capacity capacity)
        {
            var port = Port.Create<Edge>(Orientation.Horizontal, direction, capacity, typeof(bool));
            port.portName = "";
            return port;
        }

        private void Select()
        {
            OnNodeSelected?.Invoke(_node);
        }

        private void ClickedRemoveButton()
        {
            OnClickedRemoveButton?.Invoke(this);
        }
    }
}