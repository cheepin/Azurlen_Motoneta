using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CH = util.CoroutineHelper;

namespace ui
{
	public class SelectDetailButton : MonoBehaviour
	{

		TextObjectExpander detailPanel;
		static Vector2 previousAreaSize = Vector2.zero;
		private void Start()
		{
			GetComponentInChildren<Text>().color = new Color(0.5f,0.5f,0.5f,0.5f);
		}

		public void OpenDetail(string detailType)
		{
			//--previousAreaSizeにデータが入ってたらexpandしたサイズを元に戻す
			if(previousAreaSize != Vector2.zero)
			{
				var DescriptionArea = GameObject.Find("DescriptionArea").GetComponent<RectTransform>();
				DescriptionArea.sizeDelta = previousAreaSize;
				previousAreaSize = Vector2.zero;
			}
			
			detailPanel = GameObject.Find("DetailPanel(Clone)").GetComponentInChildren<TextObjectExpander>();
			detailPanel.OpenElement(detailType);

			
		}

		public void ExpandDeatail()
		{
			var label = GameObject.FindGameObjectWithTag("Label").GetComponent<RectTransform>();
			var previousPos =  label.position;

			var DescriptionArea = GameObject.Find("DescriptionArea").GetComponent<RectTransform>();

			var canvas =  GameObject.Find("Canvas").GetComponent<RectTransform>();

			//--previousAreaSizeにデータが入ってたらexpandしたサイズを元に戻す
			if(previousAreaSize == Vector2.zero)
			{
				previousAreaSize = DescriptionArea.sizeDelta;
				DescriptionArea.sizeDelta = new Vector2(canvas.sizeDelta.x,DescriptionArea.sizeDelta.y);
				label.position = previousPos;

			}
			else
			{
				DescriptionArea.sizeDelta = previousAreaSize;
				previousAreaSize = Vector2.zero;
				label.position = previousPos;

			}
	
		}
		

		public void SetVisible()
		{
			GetComponentInChildren<Text>().color = new Color(0.88f,0.88f,0.0f,1.0f);
		}

		public void SetInvisible()
		{
			GetComponentInChildren<Text>().color = new Color(0.5f,0.5f,0.5f,0.5f);
		}
	}

}