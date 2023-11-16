// 日本語対応
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public class AnimationNodeView : NodeView, IInputNodeView, IOutputNodeView
    {
        public AnimationNodeView(AnimationNode node) : base(node)
        {
            _node = node;
            title = "Animation Node";
        }

        private AnimationNode _node;

        public Port OutputPort => _output;
        public Port InputPort => _input;

        public IOutputNode OutputConnectable => _node;
        public IInputNode InputConnectable => _node;
    }
}