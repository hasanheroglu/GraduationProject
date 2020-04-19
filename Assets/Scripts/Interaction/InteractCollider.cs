using Interactable.Base;
using UnityEngine;

namespace Interaction
{
	public class InteractCollider : MonoBehaviour
	{

		private Responsible _responsible;

		private void Awake()
		{
			_responsible = GetComponentInParent<Responsible>();
		}

		private void OnTriggerEnter(Collider other)
		{
			var interactable = Util.GetInteractableFromCollider(other);
			if (interactable == null || _responsible == null || _responsible.Target == null) return;
			if (!ReferenceEquals(_responsible.Target, interactable.gameObject)) return;
			
			_responsible.TargetInRange = true;
		}
	
		private void OnTriggerStay(Collider other)
		{
			var interactable = Util.GetInteractableFromCollider(other);
			if (interactable == null || _responsible == null || _responsible.Target == null) return;
			if (!ReferenceEquals(_responsible.Target, interactable.gameObject)) return;
			
			_responsible.TargetInRange = true;
		}
	}
}
