using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public interface IShowerable
{
	IEnumerator Shower(Responsible responsible);
}
