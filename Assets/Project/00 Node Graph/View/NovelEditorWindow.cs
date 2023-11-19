using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Glib.NovelGameEditor
{
    public class NovelEditorWindow : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset m_VisualTreeAsset = default;

        private InspectorView _inspectorView = null;
        private NodeGraphView _nodeGraphView = null;

        [MenuItem("Window/UI Toolkit/NovelEditorWindow")]
        public static void OpenWindow()
        {
            NovelEditorWindow wnd = GetWindow<NovelEditorWindow>();
            wnd.titleContent = new GUIContent("NovelEditorWindow");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;

            m_VisualTreeAsset.CloneTree(root);

            _nodeGraphView = root.Q<NodeGraphView>();
            _inspectorView = root.Q<InspectorView>();

            _nodeGraphView.OnNodeSelectedEvent += OnNodeSelected;
        }

        private void OnSelectionChange()
        {
            var selectGameObject = Selection.activeObject as GameObject;

            if (selectGameObject && selectGameObject.TryGetComponent(out NovelNodeGraph nodeGraph))
            {
                _nodeGraphView.PopulateView(nodeGraph);
                return;
            }

            _inspectorView.ShowNodeInfomation(null, null);
            _nodeGraphView.PopulateView(null);
        }

        public void OnNodeSelected(NodeView nodeView, Node node)
        {
            _inspectorView.ShowNodeInfomation(nodeView, node);
        }
    }
}