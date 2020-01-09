using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public interface IPickable
{
	IEnumerator Pick(Responsible responsible);
}
