using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;

namespace ui
{
	public class WarshipLabelPlacer : MonoBehaviour
	{
		[SerializeField] int columnLimit;
		[SerializeField] Vector2 firstOffset;
		[SerializeField] RectTransform canvasSize;
		[SerializeField] float Xinterval;
		readonly string textResourceLabelPath = "WarshipLabel";
		readonly string iconFolderPath = "FaceIcon/";
		
		IEnumerable<data.WarshipDataScriptable> dataHolder;
		Vector2 elemInterval;
		GameObject textLabelRes;

		private void Awake()
		{
		}

		public void Clicked(string warshipType)
		{
			//---インデックスのリセット・及び子ファイルの消去
			index = 0;
			foreach(Transform child in transform)
			{
				Destroy(child.gameObject);
			}

			textLabelRes = Resources.Load(textResourceLabelPath) as GameObject;
			dataHolder = data.WarshipJsonDeserializer.GetWarshipData(warshipType);
			//---行の数　＝ データリストの数/縦の要素数
			int dataCount = dataHolder.Count();
			int rowNumber = dataCount/columnLimit;


			int myHeight = (int)(canvasSize.sizeDelta.y + GetComponent<RectTransform>().sizeDelta.y);
			int interval =  myHeight/columnLimit;
			elemInterval.x = Vector2.Scale(canvasSize.sizeDelta,new Vector2(Xinterval,0)).x;
			elemInterval.y = interval;

			//Vector2 canvasFirstPos = new Vector2(canvasSize.position.x);
			//---フィル処理
			util.FillElementUI.FillColumns(Vector2.Scale(canvasSize.sizeDelta,firstOffset),elemInterval,columnLimit,rowNumber+1,Placer);
			

		}

		int index = 0;
		void Placer(Vector2 pos)
		{	
			if(index< dataHolder.Count())
			{
				//---インスタンス化
				GameObject newLabel = Instantiate(textLabelRes,transform);
				//---サイズ補正
				RectTransform rectTransform = newLabel.GetComponent<RectTransform>();
				rectTransform.anchorMin = Vector2.zero;
				rectTransform.anchorMax = Vector2.zero;
				rectTransform.anchoredPosition = pos;

				//---アイコン設置
				var iconRes = Resources.Load<Texture2D>(iconFolderPath + dataHolder.ElementAt(index).Name);
				newLabel.GetComponentInChildren<Image>().sprite = Sprite.Create(iconRes,new Rect(0,0,128,128),Vector2.zero);				
				//---艦名設置
				TextInput(newLabel.GetComponentInChildren<Text>());
				//---データを渡す
				newLabel.GetComponent<OpenDetail>().WarshipData = dataHolder.ElementAt(index);

				index++;
			}
		}

		void TextInput(Text textComp)
		{
			var shipData = dataHolder.ElementAt(index);
			textComp.text = shipData.Name;
		}


	}

}