using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
public class TestDotween : MonoBehaviour {

    public Transform target;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().rectTransform.anchoredPosition3D = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position); 
	}
}
