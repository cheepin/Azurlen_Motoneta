using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using util;
namespace ui
{
	public class UpperPanelText : Singleton<UpperPanelText>
	{
		Text myText;
		private void Start()
		{
			Instance.myText = GetComponent<Text>();
		}


		static public void SetText(string text)
		{
			Instance.myText.text = text;
		}
		

	}

}