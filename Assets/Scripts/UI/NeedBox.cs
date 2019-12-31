using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class NeedBox : MonoBehaviour
	{
		private Need _need;
		private RawImage _status;
	
		public GameObject name;
		public GameObject status;

		private void Awake()
		{
			_status = status.GetComponent<RawImage>();
		}

		private void Update()
		{
			SetStatus();
		}

		public void SetNeed(Need need)
		{
			_need = need;
			SetNeedBox();
		}
	
		private void SetNeedBox()
		{
			SetName();
			SetStatus();
		}

		private void SetName()
		{
			this.name.GetComponent<Text>().text = _need.Name;
		}

		private void SetStatus()
		{
			var statusColor = ((_need.Value / 100) * 120) / 360;
			_status.color = Color.HSVToRGB(statusColor, 1, 1);
		}
	}
}
