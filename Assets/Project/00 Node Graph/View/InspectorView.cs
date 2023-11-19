// 日本語対応
using UnityEditor;
using UnityEngine.UIElements;

namespace Glib.NovelGameEditor
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

        private Editor _editor;

        public void ShowNodeInfomation(NodeView nodeView, Node node)
        {
            Clear();
            UnityEngine.Object.DestroyImmediate(_editor);
            if (!node) return;
            _editor = Editor.CreateEditor(node);

            IMGUIContainer container = new IMGUIContainer(() =>
            {
                _editor.OnInspectorGUI();
                nodeView.ApplyNodeName(node);
            });
            Add(container);

        }
    }
}