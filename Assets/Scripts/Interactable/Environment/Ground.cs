using System;
using System.Collections;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using Manager;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Interactable.Environment
{
	public class Ground : Interactable.Base.Interactable, IWalkable
	{
		private static int _instanceCount;
		
		[SerializeField] public bool Occupied;
		
		private void Start()
		{
			SetGroupName("ground");
			instanceNo = _instanceCount;
			_instanceCount++;
			InUse = 999; //infinite use change this value!!!
			SetMethods();
		}

		private void Update()
		{
			if (Occupied)
			{
				gameObject.GetComponent<ShaderAdjuster>().SetColor(Color.red);
			}
		}

		[Activity(ActivityType.None)]
		[Interactable(typeof(Responsible))]
		public IEnumerator Walk(Responsible responsible)
		{ 
			yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
			responsible.FinishJob();
		}
	}
}