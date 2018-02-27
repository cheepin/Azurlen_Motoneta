using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PictureFrame : MonoBehaviour {

	[SerializeField] 
	SpriteAtlas pictureAtlas=null;
	public string ShipName{get;set; }

	void Start () {
		pictureAtlas.GetSprite("");
	}
	
	void Update () {
		
	}
}
