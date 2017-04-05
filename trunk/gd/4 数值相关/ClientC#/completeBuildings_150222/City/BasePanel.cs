using UnityEngine;
using System.Collections;

namespace BattleFramework.City
{
	public class BasePanel : MonoBehaviour {

		protected CityController mCity;

		void Awake()
		{
			mCity = CityController.SingleTon ();
		}

		void Start()
		{
			Init ();
		}

		protected virtual void Init()
		{

		}

	}
}