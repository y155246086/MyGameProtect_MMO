using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageText : MonoBehaviour {
    public static MessageText Instance;
	// Use this for initialization
	void Start () {
        Instance = this;
	}
	public void SetText(string text)
    {
        this.GetComponent<Text>().text = text;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
