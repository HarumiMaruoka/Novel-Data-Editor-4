using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        private Node _node;

        protected Port _input = null;
        protected Port _output = null;

        public Node Node => _node;

        public Port Input => _input;
        public Port Output => _output;

        public event Action<Node> OnNodeSelected;

        public NodeView(Node node)
        {
            Initialize(node);
        }

        public NodeView(Node node, string uxml) : base(uxml)
        {
            Initialize(node);
        }

        private void Initialize(Node node)
        {
            _node = node;
            this.viewDataKey = node.ViewData.GUID;

            style.left = node.ViewData.Position.x;
            style.top = node.ViewData.Position.y;

            Orientation orientation = Orientation.Horizontal;

            if (this is IInputNodeView)
            {
                Direction direction = Direction.Input;
                if (node is IMultiParent)
                {
                    _input = CreatePort(orientation, direction, Port.Capacity.Multi);
                }
                else if (node is ISingleParent)
                {
                    _input = CreatePort(orientation, direction, Port.Capacity.Single);
                }

                if (_input != null)
                {
                    inputContainer.Add(_input);
                }
            }
            if (this is IOutputNodeView)
            {
                Direction direction = Direction.Output;
                if (node is IMultiChild)
                {
                    _output = CreatePort(orientation, direction, Port.Capacity.Multi);
                }
                else if (node is ISingleChild)
                {
                    _output = CreatePort(orientation, direction, Port.Capacity.Single);
                }

                if (_output != null)
                {
                    outputContainer.Add(_output);
                }
            }
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            _node.ViewData.Position = new Vector2(newPos.x, newPos.y);
        }

        public override void OnSelected()
        {
            base.OnSelected();
            OnNodeSelected?.Invoke(_node);
        }

        protected Port CreatePort(Orientation orientation, Direction direction, Port.Capacity capacity)
        {
            var port = Port.Create<Edge>(orientation, direction, capacity, typeof(bool));
            port.portName = "";
            return port;
        }
    }
}