using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CH = util.CoroutineHelper;
namespace ui
{
	public class TextObjectExpander : MonoBehaviour
	{	
		[SerializeField]
		RectTransform detailFrame;
		[SerializeField]
		RectTransform pictureFrame;
		[SerializeField]
		RectTransform pictureFrameLabel;
		[SerializeField]
		RectTransform tipsFrame;
		[SerializeField]
		int SpaceHistoryAndDetail=10;

		[SerializeField]
		int SpaceTextAndPictureFrameLabel=10;
		[SerializeField]
		int SpacePictureFrameLabelAndPicture=10;


		Text historyText;
		Text detailText;
		Text tipsText;
		RectTransform historyLabel;
		readonly float labelHeight = -64;

		void Start()
		{
			historyText = GetComponentsInChildren<Text>().Where(X=>X.name == "HistoryText").First();
			detailText =  GetComponentsInChildren<Text>().Where(X=>X.name == "DetailText").First();
			tipsText = GetComponentsInChildren<Text>().Where(X=>X.name == "TipsText").First();
			historyLabel = GetComponentsInChildren<RectTransform>().Where(X=>X.name == "艦歴ラベル").First();

			AllClose();

			historyLabel.gameObject.SetActive(true);
			historyText.gameObject.SetActive(true);

			historyLabel.anchoredPosition = new Vector2(0,labelHeight);

			OpenElement("詳細");


			//---フレーム終了後、テキストオブジェクトとピクチャフレームの長さでコンテンツを拡充する
			StartCoroutine(CH.WaitForEndOfFrame(()=>
			{
				////---ピクチャーフレームラベルの設置
				//pictureFrameLabel.anchoredPosition = new Vector2(0, -offset  - detailText.preferredHeight-SpaceTextAndPictureFrameLabel );

				////---ピクチャーフレームの設置　(オフセット-テキストの高さ-任意の設定)
				//pictureFrame.anchoredPosition = new Vector2(0,pictureFrameLabel.anchoredPosition.y-SpacePictureFrameLabelAndPicture );

				////---自分のサイズをテキストとピクチャーフレームの高さに応じて拡張
				
			}));
			
		}
		
		/// <summary>
		/// 説明テキストの隙間
		/// </summary>
		readonly int textMarzin = 10;

		/// <summary>
		/// 項目を開く
		/// </summary>
		/// <param name="detailType">開く詳細の種類</param>
		public void OpenElement(string detailType)
		{
			AllClose();
			float rectSizeY=0;
			switch(detailType)
			{
				case "艦歴":
					historyLabel.gameObject.SetActive(true);
					historyText.gameObject.SetActive(true);
					historyLabel.anchoredPosition = new Vector2(0,labelHeight);
					rectSizeY = Mathf.Abs(historyText.preferredHeight + historyLabel.sizeDelta.y+SpaceTextAndPictureFrameLabel);
					break;

				//---詳細ラベルの設置
				case "詳細":
					detailText.gameObject.SetActive(true);
					detailFrame.gameObject.SetActive(true);

					detailFrame.anchoredPosition = new Vector2(0,labelHeight);
					detailText.GetComponent<RectTransform>().anchoredPosition  = new Vector2(textMarzin,detailFrame.anchoredPosition.y-SpaceTextAndPictureFrameLabel);

					int marzin = 50;
					rectSizeY = Mathf.Abs(detailText.preferredHeight + detailFrame.sizeDelta.y+SpaceTextAndPictureFrameLabel+marzin);	
					
					break;
				
				//---写真ラベルの設置
				case "写真":
					pictureFrame.gameObject.SetActive(true);
					pictureFrameLabel.gameObject.SetActive(true);

					pictureFrameLabel.anchoredPosition = new Vector2(0,labelHeight);
					pictureFrame.anchoredPosition = new Vector2(0,pictureFrameLabel.anchoredPosition.y-SpacePictureFrameLabelAndPicture);
					rectSizeY = Mathf.Abs(pictureFrameLabel.sizeDelta.y +SpaceTextAndPictureFrameLabel);	
					
					break;

				//---Tipsの設置
				case "Tips":
					tipsText.gameObject.SetActive(true);
					tipsFrame.gameObject.SetActive(true);

					tipsFrame.anchoredPosition = new Vector2(0,labelHeight);
					tipsText.GetComponent<RectTransform>().anchoredPosition  = new Vector2(textMarzin,tipsFrame.anchoredPosition.y-SpaceTextAndPictureFrameLabel);

					rectSizeY = Mathf.Abs(tipsText.preferredHeight+ tipsFrame.sizeDelta.y +SpaceTextAndPictureFrameLabel);	
					
					break;
				default:
					break;
			}
			var rect = GetComponent<RectTransform>();
			rect.sizeDelta = new Vector2(rect.sizeDelta.x,rectSizeY);
			
		}

		/// <summary>
		/// 全部のオブジェクトを非表示にする
		/// </summary>
		void AllClose()
		{
			pictureFrame.gameObject.SetActive(false);
			pictureFrameLabel.gameObject.SetActive(false);
			detailText.gameObject.SetActive(false);
			detailFrame.gameObject.SetActive(false);
			historyLabel.gameObject.SetActive(false);
			historyText.gameObject.SetActive(false);
			tipsFrame.gameObject.SetActive(false);
			tipsText.gameObject.SetActive(false);

		}


	}

}