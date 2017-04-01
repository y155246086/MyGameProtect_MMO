using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BattleFramework.Data;

public class DataCenter : SingleTon<DataCenter>
{

    //static DataCenter instance;

    
    private bool isInit = false;
    public List<PlayerData> playerList;
    public GameBaseData baseData;
    public void Init()
    {
        if (isInit == true)
        {
            return;
        }
        Debug.Log("DataCenter:初始化数据开始");
        playerList = PlayerData.LoadDatas();
        baseData = GameBaseData.LoadDatas()[0];
        isInit = true;
        
        Debug.Log("DataCenter:初始化数据结束");
    }
}

