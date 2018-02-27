using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ui
{

	public class SortButton : MonoBehaviour
	{
		[SerializeField]
		GameObject WarshipTypeBox;
		[SerializeField]
		GameObject RarerityBox;
		[SerializeField]
		GameObject CampBox;
		
		public void Clicked(string keyword)
		{
			RarerityBox.SetActive(false);
			WarshipTypeBox.SetActive(false);
			CampBox.SetActive(false);


			switch(keyword)
			{
				case "艦種":
					WarshipTypeBox.SetActive(true);
					break;
				case "レアリティ":
					RarerityBox.SetActive(true);
					break;
				case "陣営":
					CampBox.SetActive(true);
					break;
				default:
					break;
			}

			transform.parent.gameObject.SetActive(false);
		}


	} 
}
