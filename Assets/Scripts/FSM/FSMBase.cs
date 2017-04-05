using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMBase : MonoBehaviour {

    protected Transform playerTransfrom;
    protected GameObject[] pointList;
    protected Vector3 destPos;
    protected virtual void Initialize()
    {

    }
    protected virtual void FSMUpdate()
    {

    }
    protected virtual void FSMFixedUpdate()
    {

    }
    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
    }
    void FixedUpdate()
    {
        FSMFixedUpdate();
    }
}
