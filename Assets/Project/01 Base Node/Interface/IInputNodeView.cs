// 日本語対応
using UnityEditor.Experimental.GraphView;

namespace Glib.NovelGameEditor
{
    public interface IInputNodeView
    {
        Port InputPort { get; }
        IInputNode InputConnectable { get; }
    }
}