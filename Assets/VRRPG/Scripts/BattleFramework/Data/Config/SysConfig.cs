using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//这个类用来读取一些简单的外部系统配置，经常用于调试啊什么的
public class SysConfig  {

	public const string configResourcePath = "Configs/SysConfig/BaseConfig";
	public const string userConfigResourcePath = "Configs/SysConfig/UserConfig";

    public const string K_AUTH_SERVER_IP = "AUTH_SERVER_IP";
    public const string K_AUTH_SERVER_PORT = "AUTH_SERVER_PORT";
    public const string K_CHANNELID = "CHANNELID";
    public const string K_VERSION = "VERSION";
 
	Dictionary<string,string> mBaseProperties;
	Dictionary<string,string> mUserProperties;

    private SysConfig()
    {
		mBaseProperties = Load (configResourcePath);
		mUserProperties = Load (userConfigResourcePath);
	}

	public static Dictionary<string,string> LoadPropertyNames(){
		Dictionary<string,string> properties = new Dictionary<string, string> ();
		TextAsset textAsset = Resources.Load<TextAsset> (configResourcePath);
		string[] lines = textAsset.text.Split(new char[]{'\r','\n'});
		foreach(string str in lines)
		{
			if(str.IndexOf("=")!=-1)
			{
				string key = str.Split('=')[0];
				string keyValue = "";
				if(str.IndexOf("//")!=-1)
				{
					Debug.Log(str.Split(new char[]{'/','/'}).Length);
					keyValue = str.Split(new char[]{'/','/'})[2];
				}
				properties.Add(key,keyValue);
				Debug.Log("key=" + key);
			}
		}
		return properties;
	}


	Dictionary<string,string> Load(string path)
	{
		TextAsset textAsset = Resources.Load<TextAsset>(path);
		Dictionary<string,string> config = new Dictionary<string, string> ();
		string text = textAsset.text;
		string[] lines = textAsset.text.Split(new char[]{'\r','\n'});

		for(int i=0;i<lines.Length;i++)
		{
			lines[i] = lines[i].Replace(";","");
			if(lines[i].IndexOf("//")!=-1)
			{
				lines[i] = lines[i].Remove(lines[i].IndexOf("//"));
//				Debug.Log(lines[i]);
			}
			string[] keys = lines[i].Split('=');
			if(keys.Length>=2)
			{
				config.Add(keys[0].Trim(),keys[1].Trim());
			}
		}
		return config;
	}

	public bool HasProperties(string key){
		return userProperties.ContainsKey (key) || baseProperties.ContainsKey (key);
	}

	public Dictionary<string,string> baseProperties
	{
		get
		{			
			return mBaseProperties;
		}
	}

	public Dictionary<string,string> userProperties
	{
		get
		{
			return mUserProperties;
		}
	}

	bool TryGetBaseStringProperties(string key,ref string value){
		if(baseProperties.ContainsKey(key))
		{
			value = baseProperties[key];
			return true;
		}
		return false;
	}

	bool TryGetUserStringProperties(string key,ref string value){
		if(userProperties.ContainsKey(key))
		{
			value = userProperties[key];
			return true;
		}
		return false;
	}
	
	public string GetStringProperties(string key)
	{
		string value = "";
		if(!TryGetUserStringProperties(key,ref value))
		{
			TryGetBaseStringProperties(key,ref value);
		}
		return value;
	}

	public int GetIntProperties(string key)
	{
		int value = 0;
		string str = GetStringProperties (key);
		if (!int.TryParse (str, out value)) {
			if(baseProperties.ContainsKey(key))
			{
				str = baseProperties[key];
				int.TryParse (str, out value);
			}
		}
		return value;
	}

	public float GetFloatProperties(string key)
	{
		float value = 0;
		string str = GetStringProperties (key);
		if(!float.TryParse (str,out value))
		{
			if(baseProperties.ContainsKey(key))
			{
				str = baseProperties[key];
				float.TryParse(str,out value);
			}
		}
		return value;
	}

	public bool GetBoolProperties(string key)
	{
		bool value = false;
		string str = GetStringProperties (key);
		bool.TryParse (str,out value);
		return value;
	}

    //*****************************************************************//
    private static SysConfig _INSTANCE = new SysConfig();
    public static SysConfig GetInstance()
    {
        return _INSTANCE;
    }

}







