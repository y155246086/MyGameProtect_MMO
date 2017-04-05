using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BattleFramework.Data;

public class SingleTon<T> : MonoBehaviour
    where T : MonoBehaviour
{
	
	static T instance;
	
	public static T Instance()
	{
		if (instance == null) {
			instance = GameObject.FindObjectOfType<T>();
			if(instance == null)
			{
				GameObject go = new GameObject(typeof(T).FullName);
				instance = go.AddComponent<T>();
			}
			GameObject.DontDestroyOnLoad(instance.gameObject);
		}
		return instance;
	}

}
