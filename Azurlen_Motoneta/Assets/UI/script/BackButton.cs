using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour {

	public void Clicked()
	{
		SoundManager.Click();
		var displayPanel = GameObject.Find("DetailPanel(Clone)");
	    ui.WarshipFaceIconPlacer.SetUpperText(displayPanel.GetComponent<DetailPanel>().WarshipData.Type);

		Destroy(displayPanel);
	}
}
