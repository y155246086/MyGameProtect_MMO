using BattleFramework.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {

    public static MonsterManager Instance;
    [System.NonSerialized]
    public List<MonsterAI> monsterList = new List<MonsterAI>();
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }
    /// <summary>
    /// 用过点参数创建怪物
    /// </summary>
    /// <param name="list"></param>
    public void CreateMonster(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {

            CreateMonster(MonsterData.dataList[Random.Range(0, MonsterData.dataList.Count)], list[i].transform.position, 1);
        }
    }
    public void RandomRefresh(Vector3 v)
    {
        CreateMonster(MonsterData.dataList[Random.Range(0, MonsterData.dataList.Count)], v, 1);
    }
    public void CreateMonster(int id, Vector3 v, int groupId = 1)
    {
        CreateMonster(MonsterData.GetByID(id), v, groupId);
    }
    public void CreateMonster(MonsterData data, Vector3 v, int groupId)
    {
        MonsterAI entity = GetNewMonster(data, v, groupId);
        monsterList.Add(entity);
    }
    /// <summary>
    /// 获取一只新怪
    /// </summary>
    /// <param name="data"></param>
    /// <param name="v"></param>
    /// <param name="groupId"></param>
    /// <returns></returns>
    public MonsterAI GetNewMonster(MonsterData data, Vector3 v, int groupId)
    {
        if (data == null)
        {
            Debug.LogError("怪的数据不存在");
            return null;
        }
        

        GameObject go = Res.ResourceManager.Instance.Instantiate<GameObject>(data.PrefabsPath);
        go.transform.localPosition = v;
        go.transform.localScale = new Vector3(data.scale,data.scale,data.scale);

        MonsterAI ai = go.AddComponent<MonsterAI>();
        ai.BornPoint = v;
        ai.SetData(data);
        
        //if (data.bornEffect.Length > 1)
        //{
        //    BaofengCommon.PlayEffect(data.bornEffect, v - new Vector3(0, 0.5f, 0), 4f);//出生特效
        //}
        if (data.bornSound.Length > 1)
        {
            Mogo.SoundManager.GameObjectPlaySound(data.bornSound, ai.gameObject, false, true);//出生音效
        }
        go.layer = GameCommon.ENEMY_LAYER;
        Vector3 cameraPosition = ai.transform.position;
        Vector3 cameraForward = new Vector3(0, -1, 0);

        Ray ray = new Ray(cameraPosition, cameraForward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, GameCommonUtils.Instance.GetLayer("Ground")))
        {
            float h = ai.GetComponent<CharacterController>().height - ai.GetComponent<CharacterController>().center.y;
            go.transform.localPosition = new Vector3(go.transform.localPosition.x, hit.point.y + h, go.transform.localPosition.z);
        }

        return ai;
    }
    public void Reset()
    {
        foreach (var item in monsterList)
        {
            item.gameObject.SetActive(false);
            //GameObject.Destroy(item.gameObject);
        }
        monsterList.Clear();
    }
}
