using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterBattleManager : BattleManager {

    public MonsterBattleManager(EntityParent _theOwner, SkillManager _skillManager)
        : base(_theOwner, _skillManager)
    {
    }
    override public void OnHit(int _actionID, uint _attackerID, uint woundId, List<int> harm)
    {
        if (woundId != theOwner.ID)
        {
            return;
        }
        int hitType = harm[0];
        int hitNum = harm[1];
        if (hitType != 1 && !theOwner.immuneShift)
        {
            base.OnHit(_actionID, _attackerID, woundId, harm);
        }
        if (GameWorld.showHitShader && (theOwner as EntityMonster).HitShader != null)
        {
            int _h = Mogo.Util.Utils.Choice<int>((theOwner as EntityMonster).HitShader);
            if (_h == 1)
            {//等于1闪白
                GameCommonUtils.Instance.GetHit(theOwner.gameObject, 0.2f, HitColorType.HCT_WHITE);
            }
            else if (_h == 2)
            {//等于2闪红
                GameCommonUtils.Instance.GetHit(theOwner.gameObject, 0.2f, HitColorType.HCT_RED);
            }
        }
        if (GameWorld.showFloatBlood)
        {
            FloatBlood(hitType, hitNum);
        }
    }
}
