using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public class NovelAnimationController
    {
        public async UniTask<bool> PlayAnimation(IEnumerable<NovelAnimator> animations, NovelAnimationData controlData, CancellationToken token)
        {
            List<UniTask> animationTasks = new List<UniTask>();

            // アニメーションを並列再生
            foreach (var anim in animations)
            {
                animationTasks.Add(anim.PlayAnimationAsync(controlData, token));
            }

            // 全てのアニメーションが正常に終了するか、キャンセルされるまで待機
            try
            {
                await UniTask.WhenAll(animationTasks);
                return true; // すべてのアニメーションが正常に終了した場合
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Canceled");
                return false; // キャンセルされた場合
            }
        }
    }
}