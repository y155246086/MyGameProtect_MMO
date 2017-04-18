//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1022
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections.Generic;
using BattleFramework.Data;
using UnityEngine.UI;
public class GameCommonUtils
{
	public GameCommonUtils ()
	{
	}
	private static GameCommonUtils instance;

	public static GameCommonUtils Instance{
		get{
			if(instance == null){
				instance = new GameCommonUtils();
			}
			return instance;
		}
	}
	public string FormatNumber(int source)
	{
		string target = source > 999999 ? ((int)(source / 10000)) + "万" : source.ToString();
		return target;
	}

	
    public String LastTimeFormat(int second)
    {
        int day = second / (24 * 3600);
        int hour = (second % (24 * 3600)) / 3600;
        int minute = (second % 3600) / 60;
        int leftSecond = second % 60;
        string temp = "";
        if(day > 0 ){
            temp = day + "天" + hour + "时";
        }else if(hour > 0){
            temp = hour + "时" + minute + "分";
        }else if (minute > 0)
        {
            temp = minute + "分" + leftSecond + "秒";
        }
        else if (leftSecond > 0)
        {
            temp  = leftSecond + "秒";
        }
        return temp;
    }
    public String TimeFormat_HH_MM_SS(int second,string format = "hh:mm:ss")
    {
        int day = second / (24 * 3600);
        int hour = (second % (24 * 3600)) / 3600;
        int minute = (second % 3600) / 60;
        int leftSecond = second % 60;

         if(hour > 9)
         {
             format = format.Replace("hh", hour.ToString());
         }
         else if(hour > 0)
         {
             format = format.Replace("hh", "0" + hour.ToString());
         }
         else
         {
             format = format.Replace("hh", "00");
         }

         if (minute > 9)
         {
             format = format.Replace("mm", minute.ToString());
         }
         else if (minute > 0)
         {
             format = format.Replace("mm", "0" + minute.ToString());
         }
         else
         {
             format = format.Replace("mm", "00");
         }

         if (leftSecond > 9)
         {
             format = format.Replace("ss", leftSecond.ToString());
         }
         else if (leftSecond > 0)
         {
             format = format.Replace("ss", "0" + leftSecond.ToString());
         }
         else
         {
             format = format.Replace("ss", "00");
         }
         return format;
    }
   

    static public void Destroy(UnityEngine.Object obj)
    {
        if (obj)
        {
            if (obj is Transform)
            {
                Transform t = (obj as Transform);
                GameObject go = t.gameObject;

                if (Application.isPlaying)
                {
                    t.SetParent(null);
                    UnityEngine.Object.Destroy(go);
                }
                else UnityEngine.Object.DestroyImmediate(go);
            }
            else if (obj is GameObject)
            {
                GameObject go = obj as GameObject;
                Transform t = go.transform;

                if (Application.isPlaying)
                {
                    t.parent = null;
                    UnityEngine.Object.Destroy(go);
                }
                else UnityEngine.Object.DestroyImmediate(go);
            }
            else if (Application.isPlaying) UnityEngine.Object.Destroy(obj);
            else UnityEngine.Object.DestroyImmediate(obj);
        }
    }

    public Color Convet16StringToColor(string str)
    {
        int intValue = Convert.ToInt32(str, 16);
        byte b = (byte)(intValue % 256);
        byte g = (byte)((intValue / 256) % 256);
        byte r = (byte)(intValue / (256 * 256));
        return new Color32(r,g,b,255);
    }
    
    /// <summary>
    /// 获取粒子特效播放时间
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public float GetParticleSystemPlayTime(Transform t)
    {
        ParticleSystem[] particleSystems = t.GetComponentsInChildren<ParticleSystem>();
        float maxDuration = 0;
        foreach (ParticleSystem ps in particleSystems)
        {
            if (ps.enableEmission)
            {
                if (ps.loop)
                {
                    return -1f;
                }
                float dunration = 0f;
                if (ps.emissionRate <= 0)
                {
                    dunration = ps.startDelay + ps.startLifetime;
                }
                else
                {
                    dunration = ps.startDelay + Mathf.Max(ps.duration, ps.startLifetime);
                }
                if (dunration > maxDuration)
                {
                    maxDuration = dunration;
                }
            }
        }
        return maxDuration;
    }


    /// <summary>
    /// 将阿拉伯数字转换成汉字
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public string SwitchNumToHanZi(int num)
    {
        if (num > 9)
        {
            return "";
        }
        string result = "";
        switch (num)
        {
            case 1:
                result = "一";
                break;
            case 2:
                result = "二";
                break;
            case 3:
                result = "三";
                break;
            case 4:
                result = "四";
                break;
            case 5:
                result = "五";
                break;
            case 6:
                result = "六";
                break;
            case 7:
                result = "七";
                break;
            case 8:
                result = "八";
                break;
            case 9:
                result = "九";
                break;
        }
        return result;
    }

    /// <summary>
    /// 将时间戳转化为TimeData;
    /// </summary>
    /// <param name="timeStamp"></param>
    /// <returns></returns>
    public DateTime StampToDataTime(long timeStamp)
    {
        DateTime startDataTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long time = timeStamp * 10000000;
        TimeSpan toStamp = new TimeSpan(time);
        return startDataTime.Add(toStamp);
    }


    /// <summary>
    ///  将TimeData转换为时间戳
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public long DataTimeToStamp(DateTime time)
    {
        DateTime startDataTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        return (long)((time - startDataTime).TotalSeconds);
    }
    /// <summary>
    /// 转向目标
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public void RotateToTarget(Transform source , Vector3 target)
    {
        Vector3 relative = source.InverseTransformPoint(target);
        float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
        source.Rotate(Vector3.up, angle);
    }

    public int GetLayer(string layerName)
    {
        return (1 << LayerMask.NameToLayer(layerName));
    }
    /// <summary>
    /// var cube = new GameObject();
    //  var filter = cube.AddComponent<MeshFilter>();
    //  cube.AddComponent<MeshRenderer>();
    /// </summary>
    /// <param name="center"></param>
    /// <param name="canvas"></param>
    /// <param name="filter"></param>
    /// <param name="raidus"></param>
    /// <param name="angle"></param>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    /// <param name="angleOffset"></param>
    /// <param name="posType"></param>
    public void DrawCircle(GameObject center,GameObject canvas, MeshFilter filter, float raidus, float angle, float offsetX = 0, float offsetY = 0, float angleOffset = 0, int posType = 0)
    {
        int ANGLE_STEP = 15;
        var mesh = new Mesh();
        int len = (int)Math.Floor(angle / ANGLE_STEP);
        len = len + 2;
        Vector3[] vs = new Vector3[len];
        float sin = (float)Math.Sin(angleOffset * Math.PI / 180);
        float cos = (float)Math.Cos(angleOffset * Math.PI / 180);
     
        Matrix4x4 m = center.transform.localToWorldMatrix;
        Matrix4x4 m1 = new Matrix4x4();
        m1.SetRow(0, new Vector4(0, 0, 0, offsetY)); //1
        m1.SetRow(1, new Vector4(0, 1, 0, 0));
        m1.SetRow(2, new Vector4(0, 0, 0, offsetX)); //-1
        m1.SetRow(3, new Vector4(0, 0, 0, 1));
        m = m * m1;
        Vector3 v0 = new Vector3(m.m03, m.m13, m.m23);
        //vs[0] = theOwner.Transform.position;
        vs[0] = v0;
        for (int i = 1; i < len; i++)
        {
            //canvas.transform.position = theOwner.Transform.position;
            canvas.transform.position = v0;
            canvas.transform.rotation = center.transform.rotation;
            canvas.transform.Rotate(new Vector3(0, -angle * 0.5f, 0));
            if (i != len - 1)
            {//非最后一个点
                canvas.transform.Rotate(new Vector3(0, ANGLE_STEP * i, 0));
                var v = canvas.transform.position + canvas.transform.forward * raidus;
                vs[i] = v;
            }
            else
            {//最后一个顶点
                //float r = angle - ANGLE_STEP * (i - 1);
                canvas.transform.Rotate(new Vector3(0, angle, 0));
                var v = canvas.transform.position + canvas.transform.forward * raidus;
                vs[i] = v;
            }
        }
        //三角形数
        int tc = len - 2;
        int[] triangles = new int[tc * 3];
        for (int j = 0; j < tc; j++)
        {
            triangles[j * 3] = 0;
            triangles[j * 3 + 1] = j + 1;
            if (j != 23)
            {
                triangles[j * 3 + 2] = j + 2;
            }
            else
            {
                triangles[j * 3 + 2] = 1;
            }
        }
        canvas.transform.position = Vector3.zero;
        canvas.transform.rotation = new Quaternion();
        mesh.vertices = vs;
        mesh.triangles = triangles;
        filter.mesh = mesh;
    }

    /// <summary>
    /// 绘制矩形
    /// </summary>
    /// <param name="center"></param>
    public void DrawRect(GameObject center)
    {
        float offsetY = 0;
        float offsetX = 0;
        float h = 10f;
        float w = 10f;
        var cube = new GameObject();
        var filter = cube.AddComponent<MeshFilter>();
        cube.AddComponent<MeshRenderer>();
        //float sin = (float)Math.Sin(angleOffset * Math.PI / 180);
        //float cos = (float)Math.Cos(angleOffset * Math.PI / 180);
        Matrix4x4 m = center.transform.localToWorldMatrix;
        Matrix4x4 m1 = new Matrix4x4();
        m1.SetRow(0, new Vector4(0, 0, 0, offsetY)); //1
        m1.SetRow(1, new Vector4(0, 1, 0, 0));
        m1.SetRow(2, new Vector4(0, 0, 0, offsetX)); //-1
        m1.SetRow(3, new Vector4(0, 0, 0, 1));
        m = m * m1;
        Vector3 posi = new Vector3(m.m03, m.m13, m.m23);
        var mesh = new Mesh();
        //cube.transform.position = theOwner.Transform.position;
        cube.transform.position = posi;
        cube.transform.rotation = center.transform.rotation;
        cube.transform.Rotate(new Vector3(0, 90, 0));
        var v0 = cube.transform.position + cube.transform.forward * w * 0.5f;

        cube.transform.position = v0;
        cube.transform.rotation = center.transform.rotation;
        var v1 = cube.transform.position + cube.transform.forward * h;

        //cube.transform.position = theOwner.Transform.position;
        cube.transform.position = posi;
        cube.transform.rotation = center.transform.rotation;
        cube.transform.Rotate(new Vector3(0, -90, 0));
        var v2 = cube.transform.position + cube.transform.forward * w * 0.5f;

        cube.transform.position = v2;
        cube.transform.rotation = center.transform.rotation;
        var v3 = cube.transform.position + cube.transform.forward * h;

        cube.transform.position = Vector3.zero;
        cube.transform.rotation = new Quaternion();
        mesh.vertices = new Vector3[] { v0, v1, v2, v3 };
        mesh.triangles = new int[] { 2, 1, 0, 2, 3, 1 };
        filter.mesh = mesh;
    }
    #region 范围距离判断


    static public bool CrossPointOld(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
    {
        //Vector3 rst = Vector3.zero;
        float d = (v2.z - v1.z) * (v4.x - v3.x) - (v4.z - v3.z) * (v2.x - v1.x);
        if (d == 0)
        {//平行线
            return false;
        }
        //x0   =   [(x2-x1)*(x4-x3)*(y3-y1)+(y2-y1)*(x4-x3)*x1-(y4-y3)*(x2-x1)*x3]/d
        //y0   =   [(y2-y1)*(y4-y3)*(x3-x1)+(x2-x1)*(y4-y3)*y1-(x4-x3)*(y2-y1)*y3]/(-d)
        float x0 = ((v2.x - v1.x) * (v4.x - v3.x) * (v3.z - v1.z) + (v2.z - v1.z) * (v4.x - v3.x) * v1.x - (v4.z - v3.z) * (v2.x - v1.x) * v3.x) / d;
        float y0 = ((v2.z - v1.z) * (v4.z - v3.z) * (v3.x - v1.x) + (v2.x - v1.x) * (v4.z - v3.z) * v1.z - (v4.x - v3.x) * (v2.z - v1.z) * v3.z) / -d;
        //(x0-x1)*(x0-x2) <=0
        //(x0-x3)*(x0-x4) <=0
        //(y0-y1)*(y0-y2) <=0
        //(y0-y3)*(y0-y4) <=0
        if (((x0 - v1.x) * (x0 - v2.x) <= 0) &&
            ((x0 - v3.x) * (x0 - v4.x) <= 0) &&
            ((y0 - v1.z) * (y0 - v2.z) <= 0) &&
            ((y0 - v3.z) * (y0 - v4.z) <= 0))
        {//相交，交点为x0,y0
            //rst = new Vector3(x0, 0, y0);
            return true;
        }

        return false;
    }

    static private float mulpti(Vector3 ps, Vector3 pe, Vector3 p)
    {
        float m;
        m = (pe.x - ps.x) * (p.z - ps.z) - (p.x - ps.x) * (pe.z - ps.z);
        return m;
    }


    static public bool CrossPoint(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
    {
        if (Math.Max(v1.x, v2.x) >= Math.Min(v3.x, v4.x) &&
            Math.Max(v3.x, v4.x) >= Math.Min(v1.x, v2.x) &&
            Math.Max(v1.z, v2.z) >= Math.Min(v3.z, v4.z) &&
            Math.Max(v3.z, v4.z) >= Math.Min(v1.z, v2.z) &&
            mulpti(v1, v2, v3) * mulpti(v1, v2, v4) <= 0 &&
            mulpti(v3, v4, v1) * mulpti(v3, v4, v2) <= 0)
            return true;
        else
            return false;
    }

    static public bool InRect(Vector3 p, Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
    {//检测 p点在v0, v1, v2, v3构成的四边形中
        bool rst = false;
        int cnt = 0; //交点计数
        Vector3 p1 = p + new Vector3(50, 0, 0);
        if (CrossPoint(p, p1, v0, v1))
        {
            cnt++;
        }
        if (CrossPoint(p, p1, v1, v2))
        {
            cnt++;
        }
        if (CrossPoint(p, p1, v2, v3))
        {
            cnt++;
        }
        if (CrossPoint(p, p1, v3, v0))
        {
            cnt++;
        }
        if (cnt == 1)
        {
            rst = true;
        }
        return rst;
    }

    static public List<List<uint>> GetEntitiesWorldRange(float x1, float y1, float x2, float y2)
    {//要求填的矩形和世界坐标轴平行
        List<List<uint>> list = new List<List<uint>>();
        List<uint> listDummy = new List<uint>();
        List<uint> listMonster = new List<uint>();
        List<uint> listPlayer = new List<uint>();
        List<uint> listMercenary = new List<uint>();
        list.Add(listDummy);
        list.Add(listMonster);
        list.Add(listPlayer);
        list.Add(listMercenary);
        if (x1 > x2 && y1 < y2)
        {
            float s = x1;
            x1 = x2;
            x2 = s;
            s = y1;
            y1 = y2;
            y2 = s;
        }
        //x1,y1为左上角,x2,y2为右下角
        foreach (var item in GameWorld.SpriteList)
        {
            if (item.Value.transform == null)
            {
                continue;
            }
            Vector3 p = item.Value.transform.position;
            if (p.x < x1 || p.z > y1 || p.x > x2 || p.z < y2)
            {
                continue;
            }
            if (item.Value is SpriteBase)
            {
                listMonster.Add(item.Key);
            }
        }
        return list;
    }

    static public List<List<uint>> GetEntitiesFrontLineNew(Transform t, float length, Vector3 direction, float width, float offsetX = 0, float offsetY = 0, float angleOffset = 0)
    {
        return GetEntitiesFrontLineNew(t.localToWorldMatrix, t.rotation, t.forward, t.position, length, direction, width, offsetX, offsetY, angleOffset);
    }

    /// <summary>
    /// 返回 角色前方，指定范围内的所有对象
    /// </summary>
    /// <param name="t"></param>
    /// <param name="distance"></param>
    /// <param name="layerMask"></param>
    /// <returns></returns>
    static public List<List<uint>> GetEntitiesFrontLine(Transform t, float distance, Vector3 direction, float radius = 0.5f, float offset = 0, float angleOffset = 0)
    {
        return GetEntitiesFrontLine(t.localToWorldMatrix, t.rotation, t.forward, t.position, distance, direction, radius, offset, angleOffset);
    }

    /// <summary>
    /// 返回角色周围指定半径范围内的所有对象。
    /// </summary>
    /// <param name="t"></param>
    /// <param name="radius"></param>
    /// <param name="layerMask"></param>
    /// <returns></returns>
    static public List<List<uint>> GetEntitiesInRange(Transform t, float radius, float offsetX = 0, float offsetY = 0, float angleOffset = 0)
    {
        return GetEntitiesInRange(t.localToWorldMatrix, t.rotation, t.forward, t.position, radius, offsetX, offsetY, angleOffset);
    }

    static public List<List<uint>> GetEntitiesInRange(Vector3 position, float radius, float offsetX = 0, float offsetY = 0, float angleOffset = 0)
    {
        List<List<uint>> list = new List<List<uint>>();
        List<uint> listDummy = new List<uint>();
        List<uint> listMonster = new List<uint>();
        List<uint> listPlayer = new List<uint>();
        List<uint> listMercenary = new List<uint>();
        //遍历entities
        foreach (KeyValuePair<uint, SpriteBase> pair in GameWorld.SpriteList)
        {
            SpriteBase entity = pair.Value;
            if (!entity.transform)
            {
                continue;
            }

            float entityRadius = 1f;
            if ((position - entity.transform.position).magnitude > radius + entityRadius) continue;

            if (pair.Value is MonsterAI)
            {
                listMonster.Add(pair.Key);
            }

        }
        list.Add(listDummy);
        list.Add(listMonster);
        list.Add(listPlayer);
        list.Add(listMercenary);

        return list;
    }

    /// <summary>
    /// 返回角色周围指定扇形范围内的所有对象。
    /// </summary>
    /// <param name="t"></param>
    /// <param name="radius"></param>
    /// <param name="angle"></param>
    /// <param name="layerMask"></param>
    /// <returns></returns>
    static public List<List<uint>> GetEntitiesInSector(Transform t, float radius, float angle = 180f, float offsetX = 0, float offsetY = 0, float angleOffset = 0)
    {
        return GetEntitiesInSector(t.localToWorldMatrix, t.rotation, t.forward, t.position, radius, angle, offsetX, offsetY, angleOffset);
    }

    static public List<List<uint>> GetEntitiesFrontLineNew(Matrix4x4 ltwM, Quaternion rotation, Vector3 forward, Vector3 position, float length, Vector3 direction, float width, float offsetX = 0, float offsetY = 0, float angleOffset = 0)
    {
        List<List<uint>> list = new List<List<uint>>();
        List<uint> listDummy = new List<uint>();
        List<uint> listMonster = new List<uint>();
        List<uint> listPlayer = new List<uint>();
        List<uint> listMercenary = new List<uint>();
        list.Add(listDummy);
        list.Add(listMonster);
        list.Add(listPlayer);
        list.Add(listMercenary);

        foreach (KeyValuePair<uint, SpriteBase> pair in GameWorld.SpriteList)
        {
            if (pair.Value.transform == null)
            {
                continue;
            }
            float r = 1f;//精灵半径
            Matrix4x4 m = ltwM;
            Matrix4x4 m1 = new Matrix4x4();
            m1.SetRow(0, new Vector4(0, 0, 0, (width + r) * 0.5f + offsetY)); //1
            m1.SetRow(1, new Vector4(0, 1, 0, 0));
            m1.SetRow(2, new Vector4(0, 0, 0, 0)); //-1
            m1.SetRow(3, new Vector4(0, 0, 0, 1));
            m = m * m1;
            Vector3 v0 = new Vector3(m.m03, m.m13, m.m23);

            m = ltwM;
            m1.SetRow(2, new Vector4(0, 0, 0, (length + r + offsetX)));
            m = m * m1;
            Vector3 v1 = new Vector3(m.m03, m.m13, m.m23);

            m = ltwM;
            m1.SetRow(0, new Vector4(0, 0, 0, -(width + r) * 0.5f + offsetY));
            m = m * m1;
            Vector3 v2 = new Vector3(m.m03, m.m13, m.m23);

            m = ltwM;
            m1.SetRow(2, new Vector4(0, 0, 0, (0 + offsetX)));
            m = m * m1;
            Vector3 v3 = new Vector3(m.m03, m.m13, m.m23);

            Vector3 p = pair.Value.transform.position;
            if (!InRect(p, v0, v1, v2, v3))
            {
                continue;
            }
            if (pair.Value is MonsterAI)
            {
                listMonster.Add(pair.Key);
            }
        }
        return list;
    }

    /// <summary>
    /// 返回 角色前方，指定范围内的所有对象
    /// </summary>
    /// <param name="t"></param>
    /// <param name="distance"></param>
    /// <param name="layerMask"></param>
    /// <returns></returns>
    static public List<List<uint>> GetEntitiesFrontLine(Matrix4x4 ltwM, Quaternion rotation, Vector3 forward, Vector3 position, float distance, Vector3 direction, float radius = 0.5f, float offset = 0, float angleOffset = 0)
    {
        List<List<uint>> list = new List<List<uint>>();
        List<uint> listDummy = new List<uint>();
        List<uint> listMonster = new List<uint>();
        List<uint> listPlayer = new List<uint>();

        RaycastHit[] hits = Physics.SphereCastAll(position, radius, direction, distance);

        foreach (RaycastHit hit in hits)
        {
            SpriteBase entity = hit.transform.GetComponent<SpriteBase>();
            if (entity is MonsterAI)
            {
                listMonster.Add(1);
            }
        }
        list.Add(listDummy);
        list.Add(listMonster);
        list.Add(listPlayer);

        return list;
    }

    /// <summary>
    /// 返回角色周围指定半径范围内的所有对象。
    /// </summary>
    /// <param name="t"></param>
    /// <param name="radius"></param>
    /// <param name="layerMask"></param>
    /// <returns></returns>
    static public List<List<uint>> GetEntitiesInRange(Matrix4x4 ltwM, Quaternion rotation, Vector3 forward, Vector3 position, float radius, float offsetX = 0, float offsetY = 0, float angleOffset = 0)
    {
        List<List<uint>> list = new List<List<uint>>();
        List<uint> listDummy = new List<uint>();
        List<uint> listMonster = new List<uint>();
        List<uint> listPlayer = new List<uint>();
        //float sin = (float)Math.Sin(angleOffset * Math.PI / 180);
        //float cos = (float)Math.Cos(angleOffset * Math.PI / 180);
        Matrix4x4 m = ltwM;
        Matrix4x4 m1 = new Matrix4x4();
        m1.SetRow(0, new Vector4(0, 0, offsetY, 0)); //1
        m1.SetRow(1, new Vector4(0, 1, 0, 0));
        m1.SetRow(2, new Vector4(0, 0, 0, offsetX)); //-1
        m1.SetRow(3, new Vector4(0, 0, 0, 1));
        m = m * m1;
        Vector3 posi = new Vector3(m.m03, m.m13, m.m23);
        //遍历entities
        foreach (KeyValuePair<uint, SpriteBase> pair in GameWorld.SpriteList)
        {
            SpriteBase entity = pair.Value;
            if (!entity.transform)
            {
                continue;
            }

            float entityRadius = 1f;
            //if ((t.position - entity.Transform.position).magnitude > radius + entityRadius) continue;
            if ((posi - entity.transform.position).magnitude > radius + entityRadius) continue;

            if (pair.Value is MonsterAI)
            {
                listMonster.Add(pair.Key);
            }

        }
        list.Add(listDummy);
        list.Add(listMonster);
        list.Add(listPlayer);

        return list;
    }

    /// <summary>
    /// 返回角色周围指定扇形范围内的所有对象。
    /// </summary>
    /// <param name="t"></param>
    /// <param name="radius"></param>
    /// <param name="angle"></param>
    /// <param name="layerMask"></param>
    /// <returns></returns>
    static public List<List<uint>> GetEntitiesInSector(Matrix4x4 ltwM, Quaternion rotation, Vector3 forward, Vector3 position, float radius, float angle = 180f, float offsetX = 0, float offsetY = 0, float angleOffset = 0)
    {
        List<List<uint>> list = new List<List<uint>>();
        List<uint> listDummy = new List<uint>();
        List<uint> listMonster = new List<uint>();
        List<uint> listPlayer = new List<uint>();
        List<uint> listMercenary = new List<uint>();

        Matrix4x4 m = ltwM;
        Matrix4x4 m1 = new Matrix4x4();
        m1.SetRow(0, new Vector4(0, 0, 0, offsetY)); //1
        m1.SetRow(1, new Vector4(0, 1, 0, 0));
        m1.SetRow(2, new Vector4(0, 0, 0, offsetX)); //-1
        m1.SetRow(3, new Vector4(0, 0, 0, 1));
        m = m * m1;
        Vector3 posi = new Vector3(m.m03, m.m13, m.m23);
        //遍历entities
        foreach (KeyValuePair<uint, SpriteBase> pair in GameWorld.SpriteList)
        {
            SpriteBase entity = pair.Value;


            float entityRadius = 1f;//精灵半径
            if ((posi - entity.transform.position).magnitude > radius + entityRadius) continue;

            //得到切线与（目标物体到人物线）的夹角a
            //float a = Mathf.Atan(entityRadius / (entity.Transform.position - t.position).magnitude);
            float a = Mathf.Asin(entityRadius / (entity.transform.position - posi).magnitude);

            //得到目标点与人物正前方的夹角b
            //float b = Vector3.Angle((entity.Transform.position - t.position), t.forward);
            float b = Vector3.Angle((entity.transform.position - posi), forward);

            //判断b - a 是否在 angle/2内
            if ((b - a) > angle / 2) continue;

           if (pair.Value is MonsterAI)
            {
                listMonster.Add(pair.Key);
            }
            

        }
        list.Add(listDummy);
        list.Add(listMonster);
        list.Add(listPlayer);
        list.Add(listMercenary);

        return list;
    }

    static public List<Transform> GetTransformsInSector(Transform t, Dictionary<Transform, float> transformDic, float radius, float angle = 180f)
    {
        List<Transform> resultList = new List<Transform>();


        //遍历entities
        foreach (KeyValuePair<Transform, float> pair in transformDic)
        {

            float entityRadius = pair.Value;
            if ((t.position - pair.Key.position).magnitude > radius + entityRadius) continue;

            //得到切线与（目标物体到人物线）的夹角a
            float a = Mathf.Asin(entityRadius / (pair.Key.position - t.position).magnitude);

            //得到目标点与人物正前方的夹角b
            float b = Vector3.Angle((pair.Key.position - t.position), t.forward);

            //判断b - a 是否在 angle/2内
            if ((b - a) > angle / 2) continue;

            resultList.Add(pair.Key);
        }
        return resultList;
    }

    static public List<Transform> GetTransformsInCircle(Transform t, Dictionary<Transform, float> transformDic, float radius)
    {
        List<Transform> resultList = new List<Transform>();

        //遍历entities
        foreach (KeyValuePair<Transform, float> pair in transformDic)
        {

            float entityRadius = pair.Value;
            if ((t.position - pair.Key.position).magnitude > radius + entityRadius) continue;

            //得到切线与（目标物体到人物线）的夹角a
            float a = Mathf.Asin(entityRadius / (pair.Key.position - t.position).magnitude);

            //得到目标点与人物正前方的夹角b
            float b = Vector3.Angle((pair.Key.position - t.position), t.forward);

            //判断b - a 是否在 angle/2内
            if ((b - a) > 180) continue;

            resultList.Add(pair.Key);
        }
        return resultList;
    }

    static public List<Transform> GetTransformsInRange(Vector3 position, Dictionary<Transform, float> transformDic, float radius)
    {
        List<Transform> list = new List<Transform>();

        //遍历entities
        foreach (KeyValuePair<Transform, float> pair in transformDic)
        {

            float entityRadius = pair.Value;
            if ((position - pair.Key.position).magnitude > radius + entityRadius) continue;

            list.Add(pair.Key);

        }
        return list;
    }

    static public List<Transform> GetTransformsFrontLineNew(Transform t, Dictionary<Transform, float> transformDic, float length, Vector3 direction, float width)
    {
        List<Transform> list = new List<Transform>();

        foreach (KeyValuePair<Transform, float> pair in transformDic)
        {
            float r = pair.Value;
            Matrix4x4 m = t.localToWorldMatrix;
            Matrix4x4 m1 = new Matrix4x4();
            m1.SetRow(0, new Vector4(0, 0, 0, (width + r) * 0.5f)); //1
            m1.SetRow(1, new Vector4(0, 1, 0, 0));
            m1.SetRow(2, new Vector4(0, 0, 0, 0)); //-1
            m1.SetRow(3, new Vector4(0, 0, 0, 1));
            m = m * m1;
            Vector3 v0 = new Vector3(m.m03, m.m13, m.m23);

            m = t.localToWorldMatrix;
            m1.SetRow(2, new Vector4(0, 0, 0, (length + r)));
            m = m * m1;
            Vector3 v1 = new Vector3(m.m03, m.m13, m.m23);

            m = t.localToWorldMatrix;
            m1.SetRow(0, new Vector4(0, 0, 0, -(width + r) * 0.5f));
            m = m * m1;
            Vector3 v2 = new Vector3(m.m03, m.m13, m.m23);

            m = t.localToWorldMatrix;
            m1.SetRow(2, new Vector4(0, 0, 0, 0));
            m = m * m1;
            Vector3 v3 = new Vector3(m.m03, m.m13, m.m23);

            Vector3 p = pair.Key.position;
            if (!InRect(p, v0, v1, v2, v3))
            {
                continue;
            }

            list.Add(pair.Key);
        }
        return list;
    }

   

    static public bool GetPointInTerrain(float x, float z, out Vector3 point)
    {
        RaycastHit hit;
        var flag = Physics.Linecast(new Vector3(x, 1000, z), new Vector3(x, -1000, z), out hit, (int)LayerMask.GetMask("Terrain"));
        if (flag)
        {
            point = new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z);
            return true;
        }
        else
        {
            point = new Vector3(x, 50, z);
            return false;
        }

    }

    /// <summary>
    /// 由近到远排序
    /// </summary>
    /// <param name="t"></param>
    /// <param name="gos"></param>
    /// <param name="count">返回数量</param>
    /// <returns></returns>
    static public void SortByDistance(Transform t, List<GameObject> gos)
    {
        gos.Sort(delegate(GameObject a, GameObject b)
        {
            Vector3 aPos = a.transform.position;
            Vector3 bPos = b.transform.position;
            if (Vector3.Distance(t.position, aPos) >= Vector3.Distance(t.position, bPos)) return 1;
            else return -1;
        });

    }

    static public void SortByDistance(Transform t, List<Transform> gos)
    {
        gos.Sort(delegate(Transform a, Transform b)
        {
            Vector3 aPos = a.position;
            Vector3 bPos = b.position;
            if (Vector3.Distance(t.position, aPos) >= Vector3.Distance(t.position, bPos)) return 1;
            else return -1;
        });

    }

    /// <summary>
    /// 支持深层孩子
    /// </summary>
    static public Transform GetChild(Transform transform, string boneName)
    {
        Transform child = transform.FindChild(boneName);
        if (child == null)
        {
            foreach (Transform c in transform)
            {
                child = GetChild(c, boneName);
                if (child != null) return child;
            }
        }
        return child;
    }

    /// <summary>
    /// 得到所有孩子，无层次限制
    /// </summary>
    static public List<Transform> GetAllChild(Transform transform)
    {
        List<Transform> children = new List<Transform>();
        children.AddRange(transform.GetComponentsInChildren<Transform>());
        foreach (Transform child in children)
        {
            children.AddRange(GetAllChild(child));
        }

        return children;
    }

    #endregion

    #region 获取设备参数

    /// <summary>
    /// 获取设备参数。
    /// </summary>
    /// <returns></returns>
    public static string GetDeviceInfo()
    {
        var props = typeof(SystemInfo).GetProperties();
        var needed = new Dictionary<string, string>();
        needed["deviceModel"] = "DM";
        needed["operatingSystem"] = "OS";
        needed["processorType"] = "PT";
        needed["processorCount"] = "PC";
        needed["graphicsDeviceName"] = "GDN";
        needed["systemMemorySize"] = "SMS";
        needed["graphicsMemorySize"] = "GMS";
        var sb = new System.Text.StringBuilder();

        foreach (var item in props)
        {
            if (needed.ContainsKey(item.Name))
            {
                var value = item.GetGetMethod().Invoke(null, null);
                sb.AppendFormat("{0}: {1}\n", needed[item.Name], value);
            }
        }

        return sb.ToString();
    }

    #endregion

    public static string GetRedString(string str)
    {
        return string.Concat("[FF0000]", str, "[-]");
    }

    static public Vector3 ConvertWorldPos(Camera fromCamera, Camera toCamera, Vector3 pos)
    {
        return toCamera.ScreenToWorldPoint(fromCamera.WorldToScreenPoint(pos));
    }

    public static List<KeyValuePair<int, TValue>> OrderByKey<TValue>(Dictionary<int, TValue> dic)
    {
        List<KeyValuePair<int, TValue>> myList = new List<KeyValuePair<int, TValue>>();
        foreach (var item in dic)
        {
            myList.Add(item);
        }

        myList.Sort((firstPair, nextPair) =>
        {
            return firstPair.Key == nextPair.Key ? 0 : (firstPair.Key < nextPair.Key ? -1 : 1);
        });
        return myList;
    }

    public static List<KeyValuePair<TKey, TValue>> OrderByValue<TKey, TValue>(Dictionary<TKey, TValue> dic, Func<TValue, int> comparer)
    {
        List<KeyValuePair<TKey, TValue>> myList = new List<KeyValuePair<TKey, TValue>>();
        foreach (var item in dic)
        {
            myList.Add(item);
        }

        myList.Sort((firstPair, nextPair) =>
        {
            var firstPairValue = comparer(firstPair.Value);
            var nextPairValue = comparer(nextPair.Value);
            return firstPairValue == nextPairValue ? 0 : (firstPairValue < nextPairValue ? -1 : 1);
        }
        );
        return myList;
    }
    /// <summary>
    /// 重置加载的资源的shader，解决加载资源shader丢失的问题
    /// </summary>
    /// <param name="obj"></param>
    public static void ResetShader(UnityEngine.Object obj)
    {

        List<Material> listMat = new List<Material>();
        listMat.Clear();
        if (obj is Material)
        {

            Material m = obj as Material;

            listMat.Add(m);

        }
        else if (obj is GameObject)
        {
            GameObject go = obj as GameObject;
            Renderer[] rends = go.GetComponentsInChildren<Renderer>();
            if (null != rends)
            {
                foreach (Renderer item in rends)
                {
                    Material[] materialsArr = item.sharedMaterials;
                    foreach (Material m in materialsArr)
                        listMat.Add(m);
                }
            }
        }
        for (int i = 0; i < listMat.Count; i++)
        {
            Material m = listMat[i];
            if (null == m)
                continue;
            var shaderName = m.shader.name;
            var newShader = Shader.Find(shaderName);
            if (newShader != null)
                m.shader = newShader;
        }
    }
}




