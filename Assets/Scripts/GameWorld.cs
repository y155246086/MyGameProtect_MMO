using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameWorld {

    /// <summary>
    /// 根据entity id索引的实体
    /// </summary>
    static private Dictionary<uint, SpriteBase> spriteList = new Dictionary<uint, SpriteBase>();
    public static Player player;
    static public Dictionary<uint, SpriteBase> SpriteList
    {
        get { return spriteList; }
    }
    static public void OnEnterWorld(SpriteBase entity)
    {
       
    }

    // 处理 entity 离开场景事件
    static private void OnLeaveWorld(uint eid)
    {
        
    }
}
