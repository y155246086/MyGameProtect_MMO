using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PropertyType
{
    HP,
    MAX_HP,
    Attack_Dis,//攻击距离
    Chase_Dis,//追逐距离
    Arrive_Dis,
    Patrol_Radius
}
public class PropertyManager {
    //存放属性
    private Dictionary<PropertyType, int> map;
    public PropertyManager()
    {
        map = new Dictionary<PropertyType, int>();
        AddProperty(PropertyType.HP, 10000);
        AddProperty(PropertyType.MAX_HP, 10000);
        AddProperty(PropertyType.Attack_Dis, 5);
        AddProperty(PropertyType.Chase_Dis, 10);
        AddProperty(PropertyType.Arrive_Dis, 1);
        AddProperty(PropertyType.Patrol_Radius, 8);
    }
    /// <summary>
    /// 获取属性值
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetPropertyValue(PropertyType type)
    {
        if(map.ContainsKey(type))
        {
            return map[type];
        }
        Debuger.LogError("没有此属性 + " + type);
        return 0;
    }
    public void AddProperty(PropertyType type,int propertyValue)
    {
        if (map.ContainsKey(type))
        {
            map[type] = propertyValue;
        }
        else
        {
            map.Add(type, propertyValue);
        }
        
    }
    /// <summary>
    /// 改变属性
    /// </summary>
    /// <param name="propertyType"></param>
    /// <param name="value"></param>
    public void ChangeProperty(PropertyType propertyType, int value)
    {
        if (map.ContainsKey(propertyType))
        {
            map[propertyType] += value;
        }
    }

    public void Clear()
    {

    }
}
