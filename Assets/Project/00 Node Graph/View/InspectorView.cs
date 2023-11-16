// 日本語対応
using UnityEditor;
using UnityEngine.UIElements;

namespace Glib.NovelGameEditor
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

        private Editor _editor;

        public void ShowNodeInfomation(Node node)
        {
            Clear();
            UnityEngine.Object.DestroyImmediate(_editor);
            _editor = Editor.CreateEditor(node);

            IMGUIContainer container = new IMGUIContainer(() =>
            {
                _editor.OnInspectorGUI();
            });
            Add(container);
        }
    }
}