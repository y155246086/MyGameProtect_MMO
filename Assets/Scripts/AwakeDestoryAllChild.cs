using UnityEngine;
using System.Collections;

public class AwakeDestoryAllChild : MonoBehaviour {

	void Awake()
    {
        Transform[] tList = this.GetComponentsInChildren<Transform>();
        foreach (var item in tList)
        {
            GameObject.Destroy(item.gameObject);
        }
    }
}
