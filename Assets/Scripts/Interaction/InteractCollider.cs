using Interactable.Base;
using UnityEngine;

namespace Interaction
{
	public class InteractCollider : MonoBehaviour
	{

		private Responsible _responsible;

		private void Start()
		{
			_responsible = GetComponentInParent<Responsible>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!ReferenceEquals(other.transform.parent.gameObject, _responsible.Target)
				&& !ReferenceEquals(other.gameObject, _responsible.Target))
			{
				return;
			}
		
			_responsible.TargetInRange = true;
		}
	
		private void OnTriggerStay(Collider other)
		{
			Debug.Log(other.gameObject.name);
			if (!ReferenceEquals(other.transform.parent.gameObject, _responsible.Target) 
			    && !ReferenceEquals(other.gameObject, _responsible.Target))
			{
				return;
			}
		
			_responsible.TargetInRange = true;
		}
	}
}
