using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BloodText : MonoBehaviour {

    public static BloodText Instance;
	// Use this for initialization
	void Start () {
        Instance = this;
        this.GetComponent<Text>().text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
