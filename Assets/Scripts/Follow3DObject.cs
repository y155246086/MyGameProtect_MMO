using UnityEngine;
using System.Collections;

public class Follow3DObject : MonoBehaviour
{

    public Transform target;
    public Vector3 offset = new Vector3(0, 1, 0);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Camera uicamera = GameObject.Find("UICamera").GetComponent<Camera>();
            Vector3 v = Camera.main.WorldToViewportPoint(target.position + offset);
            v = GameObject.Find("UICamera").GetComponent<Camera>().ViewportToWorldPoint(v);
            //v = uicamera.WorldToScreenPoint(v);
            transform.position = v;
        }
    }
}
