using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 提供一些必须要挂载带物体上的方法
/// </summary>
public class GameObjectUtils : MonoBehaviour {

    private static GameObjectUtils _Instance;
    public static GameObjectUtils Instance
    {
        get
        {
            if(_Instance == null)
            {
                GameObject o = new GameObject("GameObjectUtils");
                DontDestroyOnLoad(o);
                _Instance = o.AddComponent<GameObjectUtils>();
            }
            return _Instance;
        }
    }
    public void CheckAttaceTrigger(string stateName, float triggerTime, Animator animator, System.Action actionTrigger, System.Action endTriggerAction = null)
    {
        if (triggerTime >= 1)
        {
            Debuger.LogError("时间有问题：：：：：：：：");
            return;
        }
        StartCoroutine(_CheckAttaceTrigger(stateName, triggerTime, animator, actionTrigger, endTriggerAction));
    }
    IEnumerator _CheckAttaceTrigger(string stateName, float triggerTime, Animator animator, System.Action actionTrigger,System.Action endTriggerAction = null)
    {
        int attackState = Animator.StringToHash(stateName);
        float t = 0;
        while (true)
        {
            if (animator == null)
            {
                Debuger.LogError("参数有问题：animator");
                break;
            }

            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (info.fullPathHash == attackState)
            {

                float du = triggerTime * info.length;
                Mogo.Util.FrameTimerHeap.AddTimer((uint)du, 0, () =>
                {
                    actionTrigger();
                });
                du = info.length * 1000;
                if(endTriggerAction!=null)
                {
                    Mogo.Util.FrameTimerHeap.AddTimer((uint)du, 0, () =>
                    {
                        endTriggerAction();
                    });
                }
                break;
            }
            t += Time.deltaTime;
            if (t > 60)
            {
                Debuger.LogError("超过60秒没有触发关键帧，跳出while循环");
                break;
            }
            yield return 0;
        }
    }
}
