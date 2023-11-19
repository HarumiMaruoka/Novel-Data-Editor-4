// 日本語対応
using Cysharp.Threading.Tasks;
using Glib.NovelGameEditor;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TextPrinter : Glib.NovelGameEditor.NovelAnimation
{
    [SerializeField]
    private Text _view;

    [SerializeField]
    private string _text;

    public override async UniTask PlayAnimationAsync(NovelAnimationData animationData, CancellationToken token)
    {
        _view.text = _text + " and press space";
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        await UniTask.Yield();
    }
}