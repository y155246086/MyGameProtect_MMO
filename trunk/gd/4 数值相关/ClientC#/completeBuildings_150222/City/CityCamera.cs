using UnityEngine;
using System.Collections;

public class CityCamera : MonoBehaviour {

	public Camera cityCamera;

	public bool cameraMovable = true;
	public bool isCameraMoving = false;

	public float moveSpeed = 10;

	public Vector3[] movableArea;
	public Vector2[] movableArea2D;
	Vector3 movablePlaneNormal;

	void Awake()
	{
		cityCamera = Camera.main;
		if (movableArea.Length >= 3) {
			movablePlaneNormal = Vector3.Cross((movableArea[1]-movableArea[0]).normalized,(movableArea[2]-movableArea[0]).normalized);
		}
		movableArea2D = new Vector2[movableArea.Length];
		for(int i=0;i < movableArea.Length;i ++)
		{
			movableArea2D[i] = new Vector2(movableArea[i].x,movableArea[i].z);
		}
	}

	Vector2 preMousePos;
	Vector2 curMousePos;

	Vector3 startCameraPos;
	public Vector3 caculateCameraPos;
	Vector3 targetCameraPos;


	Vector3 pointAtMovablePlane;

	// Update is called once per frame
	void Update () {

#region move by keybouard
		float y = Input.GetAxis("Vertical");
		float x = Input.GetAxis("Horizontal");
		cityCamera.transform.position += new Vector3(x,y,0) * moveSpeed;
#endregion

#region move by mouse or touch
		if(Input.GetMouseButtonDown(1))
		{
			preMousePos = Input.mousePosition;
			startCameraPos = cityCamera.transform.position;
			targetCameraPos = cityCamera.transform.position;
		}
		if(Input.GetMouseButton(1))
		{
			curMousePos = Input.mousePosition;
			Vector2 deltaPos = curMousePos - preMousePos;

			targetCameraPos +=  new Vector3(deltaPos.x,0,deltaPos.y ) * moveSpeed;
			preMousePos = curMousePos; 
			caculateCameraPos = Vector3.Slerp(cityCamera.transform.position,targetCameraPos,0.8f);
			cityCamera.transform.position = caculateCameraPos;
//			pointAtMovablePlane = CommonUtility.GetIntersectWithLineAndPlane(caculateCameraPos,cityCamera.transform.forward,movablePlaneNormal,movableArea[0]);
//
//			if(CommonUtility.PointInPolygon(new Vector2(pointAtMovablePlane.x,pointAtMovablePlane.z) ,movableArea2D))
//			{
//				cityCamera.transform.position = caculateCameraPos;
//			}
		}
		else
		{
			pointAtMovablePlane = CommonUtility.GetIntersectWithLineAndPlane(cityCamera.transform.position,cityCamera.transform.forward,movablePlaneNormal,movableArea[0]);
			float x0 = 0;
			if(pointAtMovablePlane.x > 30 )
			{
				x0 = 30 - pointAtMovablePlane.x;
			}
			else if(pointAtMovablePlane.x < -30)
			{
				x0 = -30 - pointAtMovablePlane.x;
			}
			float y0 = 0;
			if(pointAtMovablePlane.z > 30 )
			{
				y0 = 30 - pointAtMovablePlane.z;
			}
			else if(pointAtMovablePlane.z < -70)
			{
				y0 = - 70 -pointAtMovablePlane.z;
			}

			cityCamera.transform.position = Vector3.Slerp(cityCamera.transform.position,cityCamera.transform.position+new Vector3(x0,0,y0),0.3f);
			cityCamera.transform.position = new Vector3(cityCamera.transform.position.x,70,cityCamera.transform.position.z);
		}




#endregion
	}

	void OnDrawGizmos()
	{
		if(movableArea!=null && movableArea.Length>=3)
		{
			for(int i = 1,j = 0;i < movableArea.Length;i ++,j ++)
			{
				Gizmos.DrawLine(movableArea[i],movableArea[j]);
				if(i == movableArea.Length - 1)
				{
					Gizmos.DrawLine(movableArea[i],movableArea[0]);
				}
			}
			Gizmos.DrawSphere(pointAtMovablePlane,0.5f);
		}
	}


}
