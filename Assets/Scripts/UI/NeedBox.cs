using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class NeedBox : MonoBehaviour
	{
		private Need _need;
	
		public GameObject name;
		public GameObject value;

		private void Update()
		{
			SetValue();
		}

		public void SetNeed(Need need)
		{
			_need = need;
			SetNeedBox();
		}
	
		private void SetNeedBox()
		{
			SetName();
			SetValue();
		}

		private void SetName()
		{
			this.name.GetComponent<Text>().text = _need.Name;
		}

		private void SetValue()
		{
			this.value.GetComponent<Text>().text = _need.Value.ToString(CultureInfo.InvariantCulture);
		}
	}
}
