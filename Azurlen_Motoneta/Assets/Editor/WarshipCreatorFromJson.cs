using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using data;

public class WarshipCreatorFromJson : ObjCreatorFromJson<WarshipDataScriptable,WarshipData>{


	[MenuItem ("Extension/ObjCreatorFromJSon")]
	static void Init()
	{
		GetWindow(typeof (WarshipCreatorFromJson),false,"ObjCreatorFromJson");
	}

	int descriptionLimit = 15999;

	protected override WarshipDataScriptable AssignScriptableObjectFromData(WarshipDataScriptable scriptableClassData, WarshipData rawClassData)
	{
		scriptableClassData.Name = rawClassData.Name;
		scriptableClassData.Camp = rawClassData.Camp;
		scriptableClassData.Type = rawClassData.Type;
		scriptableClassData.History = rawClassData.History;
		scriptableClassData.Description = rawClassData.Description;
		if(scriptableClassData.Description.Length>descriptionLimit)
		{
			Debug.Log($"{scriptableClassData.Name}の説明文が15999を越えました");
			throw new FileLoadException();
		}
		return scriptableClassData;
	}

}
