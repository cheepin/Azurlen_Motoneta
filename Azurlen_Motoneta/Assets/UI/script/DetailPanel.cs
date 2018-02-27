using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class DetailPanel : MonoBehaviour {

	[SerializeField] Text detail;
	[SerializeField] Text history;
	[SerializeField] Text tips;

	[SerializeField] Image pictureFrame;
	[SerializeField] Image bodyIllustration;
	[SerializeField] SpriteAtlas bodyIllustAtlas;
	[SerializeField] SpriteAtlas pictureFrameAtlas;

	Button upperHistoryButton;
	Button upperDetailButton;
	Button upperPictureButton;
	Button upperTipsButton;
	Button expandButton;


	public data.WarshipDataScriptable WarshipData {get;set; }
	void Start () 
	{
		if(WarshipData!=null)
		{
			//---説明文・イラスト・写真のセット
			detail.text = WarshipData.Description;
			history.text = WarshipData.History;
			tips.text = WarshipData.Tips;
			bodyIllustration.sprite = bodyIllustAtlas.GetSprite(WarshipData.Name);
			pictureFrame.sprite = pictureFrameAtlas.GetSprite(WarshipData.Name);

			upperHistoryButton = GameObject.Find("艦歴").GetComponent<Button>();
			upperDetailButton = GameObject.Find("詳細").GetComponent<Button>();
			upperPictureButton = GameObject.Find("写真").GetComponent<Button>();
			upperTipsButton = GameObject.Find("Tips").GetComponent<Button>();
			expandButton = GameObject.Find("Expand").GetComponent<Button>();

			if(history.text.Length>0)
			{
				upperHistoryButton.interactable = true;
				upperHistoryButton.GetComponent<ui.SelectDetailButton>().SetVisible();
			}
			if(detail.text.Length>0)
			{
				upperDetailButton.interactable = true;
				upperDetailButton.GetComponent<ui.SelectDetailButton>().SetVisible();
			}
			if(tips.text.Length>0)
			{
				upperTipsButton.interactable = true;
				upperTipsButton.GetComponent<ui.SelectDetailButton>().SetVisible();
			}

			upperPictureButton.interactable = true;
			upperPictureButton.GetComponent<ui.SelectDetailButton>().SetVisible();
			expandButton.interactable = true;
			expandButton.GetComponent<ui.SelectDetailButton>().SetVisible();


		}
	}

	private void OnDestroy()
	{
		upperHistoryButton.interactable = false;
		upperDetailButton.interactable = false;
		upperTipsButton.interactable = false;
		upperPictureButton.interactable = false;
		expandButton.interactable = false;
		upperHistoryButton.GetComponent<ui.SelectDetailButton>().SetInvisible();
		upperDetailButton.GetComponent<ui.SelectDetailButton>().SetInvisible();
		upperTipsButton.GetComponent<ui.SelectDetailButton>().SetInvisible();
		upperPictureButton.GetComponent<ui.SelectDetailButton>().SetInvisible();
		expandButton.GetComponent<ui.SelectDetailButton>().SetInvisible();


	}

}
