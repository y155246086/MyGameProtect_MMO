using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PropertyType
{
    HP,
    MAX_HP
}
public class PropertyManager {
    //存放属性
    private Dictionary<PropertyType, int> map;
    public PropertyManager()
    {
        map = new Dictionary<PropertyType, int>();
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
}
