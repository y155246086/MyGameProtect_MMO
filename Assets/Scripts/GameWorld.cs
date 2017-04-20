using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BattleFramework.Data;

public class GameWorld {

    /// <summary>
    /// 根据entity id索引的实体
    /// </summary>
    static private Dictionary<uint, EntityParent> spriteList = new Dictionary<uint, EntityParent>();
    public static EntityMyself player;
    public static GameSceneData sceneData;
    static public Dictionary<uint, EntityParent> SpriteList
    {
        get { return spriteList; }
    }
    static public void OnEnterWorld(EntityParent entity)
    {
        if (!spriteList.ContainsValue(entity))
        {
            spriteList.Add(entity.ID, entity);
        }
    }

    // 处理 entity 离开场景事件
    static private void OnLeaveWorld(uint eid)
    {
        if (spriteList.ContainsKey(eid))
        {
            spriteList.Remove(eid);
        }
    }
    /// <summary>
    /// 获取一个精灵
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static EntityParent GetEntity(uint id)
    {
        if(spriteList.ContainsKey(id))
        {
            return spriteList[id];
        }
        return null;
    }
    /// <summary>
    /// 添加一个新的精灵
    /// </summary>
    /// <param name="type"></param>
    public static void AddNewEntity(SpriteType type)
    {
        EntityParent entity = null;
        EntityServerInfo info = new EntityServerInfo();
        switch (type)
        {
            case SpriteType.NONE:

                break;
            case SpriteType.Myself:
                if(player == null)
                {
                    entity = new EntityMyself();
                    info.id = 1;
                    info.dataId = 1;
                    info.position = Vector3.zero;
                    info.x = 100;
                    info.y = 100;
                }
                else
                {
                    info.position = Vector3.zero;
                    info.x = 100;
                    info.y = 100;
                    player.SetEntityServerInfo(info);
                    player.UpdatePosition();
                    return;
                }
                
                break;
            case SpriteType.Player:
                entity = new EntityPlayer();
                info.id = 2;
                info.dataId = 1;
                info.position = Vector3.zero;
                info.x = 100;
                info.y = 100;
                break;
            case SpriteType.Monster:
                entity = new EntityMonster();
                info.id = 3;
                info.dataId = 1;
                info.position = Vector3.zero;
                info.x = 100;
                info.y = 100;
                break;
            default:
                break;
        }
        if (entity == null)
            return;
        entity.SetEntityServerInfo(info);
        entity.CreateModel();
        entity.EnterWorld();
        OnEnterWorld(entity);
    }
    static private void RemoveEntity(uint eid)
    {
        if (!spriteList.ContainsKey(eid))
        {
            return;
        }

        EntityParent entity = spriteList[eid];
        if (entity == null)
            return;
        entity.LeaveWorld();
        OnLeaveWorld(eid);
    }
    internal static void Reset()
    {
        List<uint> list = new List<uint>();
        foreach (var item in spriteList)
        {
            if((item.Value is EntityMyself) == false)
            {
                list.Add(item.Key);
            }
        }
        foreach (var item in list)
        {
            RemoveEntity(item);
        }

    }
}
