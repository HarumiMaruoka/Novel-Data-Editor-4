// 日本語対応
using Cysharp.Threading.Tasks;
using Glib.NovelGameEditor;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TextPrinter2 : Glib.NovelGameEditor.NovelAnimation
{
    [SerializeField]
    private Text _view;
    [SerializeField]
    private string _text;
    [SerializeField]
    private float _interval = 0.1f;

    private float _timer = 0f;
    private int _index = -1;

    public override async UniTask PlayAnimationAsync(NovelAnimationData animationData, CancellationToken token)
    {
        while (_index < _text.Length)
        {
            if (_timer > _interval)
            {
                _timer -= _interval;
                _index++;
                ShowMessage(_index);
            }

            if (Input.GetKeyDown(KeyCode.Space)) break;

            await UniTask.Yield();
            _timer += Time.deltaTime;
        }

        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        await UniTask.Yield();
    }

    private StringBuilder _stringBuilder = new StringBuilder();

    private void ShowMessage(int showIndex)
    {
        _stringBuilder.Clear();
        for (int i = 0; i < showIndex && i < _text.Length; i++)
        {
            _stringBuilder.Append(_text[i]);
        }
        _view.text = _stringBuilder.ToString();
    }
}