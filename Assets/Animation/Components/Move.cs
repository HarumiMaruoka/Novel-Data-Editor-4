// 日本語対応
using Cysharp.Threading.Tasks;
using Glib.NovelGameEditor;
using System.Threading;

public class Move : Glib.NovelGameEditor.NovelAnimation
{
    [UnityEngine.SerializeField]
    private float _speed = 1f;

    private float _timer = 0f;

    public override async UniTask PlayAnimationAsync(NovelAnimationData animationData, CancellationToken token)
    {
        _timer = 0f;
        while (_timer < 3f)
        {
            transform.Translate(new UnityEngine.Vector3(_speed * UnityEngine.Time.deltaTime * 4f, 0f, 0f));
            await UniTask.Yield();
            _timer += UnityEngine.Time.deltaTime;
        }
    }
}