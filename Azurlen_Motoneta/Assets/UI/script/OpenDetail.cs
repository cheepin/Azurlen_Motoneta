using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using data;
namespace ui
{
	public class OpenDetail : MonoBehaviour
	{
		readonly string path = "DetailPanel";
		public WarshipDataScriptable WarshipData{get;set; }

		public void Clicked()
		{
			SoundManager.Click();
			ui.UpperPanelText.SetText(WarshipData.Name);

			var detailPanelRes =  Resources.Load(path) as GameObject;
			var detailPanel = Instantiate(detailPanelRes,transform.parent.parent);
			detailPanel.GetComponent<DetailPanel>().WarshipData = WarshipData;

			#if UNITY_ANDROID
			ads.AdsRequest.Show(()=>
			{ 
				detailPanel.SetActive(true);
			});
			#endif
			#if UNITY_EDITOR
				detailPanel.SetActive(true);
			#endif

		}
		
	}

}