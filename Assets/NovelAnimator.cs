// 日本語対応
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public abstract class NovelAnimator : MonoBehaviour
    {
        public abstract UniTask PlayAnimationAsync(NovelAnimationData animationData, CancellationToken token);
    }
}