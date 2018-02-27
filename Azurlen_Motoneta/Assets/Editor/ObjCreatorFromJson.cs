using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using data;

/// <summary>
/// JsonファイルからScriptableObjectを生成し、保存する
/// TとT2は全く同様の名前のプロパティを持つ必要がある
/// </summary>
/// <typeparam name="T">ScriptableObjectクラス</typeparam>
/// <typeparam name="T2">素のクラス</typeparam>
public class ObjCreatorFromJson<T,T2> : EditorWindow  where T:ScriptableObject{

	string filePath = "";
	private void OnGUI()
	{
		EditorGUILayout.LabelField("Folder");
		folderName = EditorGUILayout.TextArea(folderName);
		EditorGUILayout.LabelField("Path");
		filePath = EditorGUILayout.TextArea(filePath,GUILayout.Height(180));
		if(GUILayout.Button("Create"))
		{
			var fileList = filePath.Split('\n').Select(X=>X.Replace("\r","")).Select(X=>X.Replace("\"",""));
			var scriptableObjects = 
			fileList.Select(file=>
			{
				T2 rawClassData = ExtructJsonFromFile(file);
				T warshipDataScriptable =CreateInstance<T>();
				return new 
				{
					FileName = file.Substring(file.LastIndexOf('\\')+1).Replace(".json",""),
				    SctiptableObject = AssignScriptableObjectFromData(warshipDataScriptable,rawClassData)
				};
			});

			foreach(var item in scriptableObjects)
			{
				utilEditor.ScriptableObjectCreator.CreateAsset(item.SctiptableObject, item.FileName, folderName);
			}
		}
	}

	/// <summary>
	/// 出力するフォルダの名前
	/// </summary>
	protected  string folderName;


	/// <summary>
	/// scriptableClassDataにrawClassDataをアサインする操作を実装
	/// </summary>
	/// <param name="scriptableClassData">データをアサインする側</param>
	/// <param name="rawClassData">参照側</param>
	/// <returns>アサインしたscriptableClassDataを返す</returns>
	protected virtual T AssignScriptableObjectFromData(T scriptableClassData,T2 rawClassData)
	{
		throw new NotImplementedException();
	}

	T2 ExtructJsonFromFile(string _filePath)
	{
		Debug.Log(_filePath);
		string jsonData = File.ReadAllText(_filePath);
		var data = JsonUtility.FromJson<T2>(jsonData);
		return data;
	}

}


