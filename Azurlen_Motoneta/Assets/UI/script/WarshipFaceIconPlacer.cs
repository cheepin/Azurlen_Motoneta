using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace ui
{
	public class WarshipFaceIconPlacer : MonoBehaviour
	{
		[SerializeField] int columnLimit;
		[SerializeField] Vector2 firstOffset;
		[SerializeField] RectTransform canvasSize;
		[SerializeField] GameObject iconHolder;
		[SerializeField] float Xinterval;
		readonly string textResourceLabelPath = "WarshipLabel";
		readonly string iconFolderPath = "FaceIcon/";

		IEnumerable<data.WarshipDataScriptable> dataHolder;
		Vector2 elemInterval;
		GameObject textLabelRes;

		private void Awake()
		{
			iconHolder = GameObject.Find("IconHolder");
		}

		private void Start()
		{
			StartCoroutine(util.CoroutineHelper.WaitForEndOfFrame(()=>Clicked("Destroyer")));
			
		}

		public void Clicked(string warshipType)
		{
			SoundManager.Click();

			//---インデックスのリセット・及び子ファイルの消去
			index = 0;
			foreach(Transform child in iconHolder.transform)
			{
				Destroy(child.gameObject);
			}

			var displayPanel = GameObject.Find("DetailPanel(Clone)");
			Destroy(displayPanel);

			textLabelRes = Resources.Load(textResourceLabelPath) as GameObject;
			dataHolder = data.WarshipJsonDeserializer.GetWarshipData(warshipType);
			//---行の数　＝ データリストの数/縦の要素数
			int dataCount = dataHolder.Count();
			int rowNumber = dataCount / columnLimit;


			int myHeight = (int)(canvasSize.sizeDelta.y + GetComponent<RectTransform>().sizeDelta.y);
			int interval = myHeight / columnLimit;
			//---インターバルを解像度に合わせてスケーリング
			elemInterval.x = Vector2.Scale(canvasSize.sizeDelta, new Vector2(Xinterval, 0)).x;
			elemInterval.y = interval;

			util.FillElementUI.FillInverseColumns(Vector2.Scale(canvasSize.sizeDelta, firstOffset), elemInterval, columnLimit, rowNumber + 1, Placer);

			//---UpperPanelのテキスト変更
			SetUpperText(warshipType);

		}

		static public void SetUpperText(string warshipType)
		{
			switch(warshipType)
			{
				case "Destroyer":
					warshipType = "駆逐艦";
					break;
				case "LightCruiser":
					warshipType = "軽巡洋艦";
					break;
				case "HeavyCruiser":
					warshipType = "重巡洋艦";
					break;
				case "BattleShip":
					warshipType = "戦艦";
					break;
				case "BattleCruiser":
					warshipType = "巡洋戦艦";
					break;
				case "AircraftCarrier":
					warshipType = "空母";
					break;
				case "LightAircraftCarrier":
					warshipType = "軽空母";
					break;
				case "GunBoat":
					warshipType = "砲艦";
					break;
				default:
					break;
			}


			ui.UpperPanelText.SetText(warshipType);

		}

		int index = 0;
		void Placer(Vector2 pos)
		{
			if(index < dataHolder.Count())
			{
				//---インスタンス化
				GameObject newLabel = Instantiate(textLabelRes, iconHolder.transform);
				//---サイズ補正
				RectTransform rectTransform = newLabel.GetComponent<RectTransform>();
				rectTransform.anchorMin = Vector2.zero;
				rectTransform.anchorMax = Vector2.zero;
				rectTransform.anchoredPosition = pos;

				//---アイコン設置
				var iconRes = Resources.Load<Texture2D>(iconFolderPath + dataHolder.ElementAt(index).Name);
				newLabel.GetComponentInChildren<Image>().sprite = Sprite.Create(iconRes, new Rect(0, 0, 128, 128), Vector2.zero);
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