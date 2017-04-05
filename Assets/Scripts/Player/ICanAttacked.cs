using UnityEngine;
using System.Collections;

public interface ICanAttacked {
    /// <summary>
    /// 设置攻击产生的伤害
    /// </summary>
    /// <param name="value"></param>
	void SetHurt(int value);
}
