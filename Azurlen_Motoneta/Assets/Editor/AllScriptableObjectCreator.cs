using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Linq;
using System.IO;

namespace utilEditor
{
	[Serializable]
	public class AllScriptableObjectCreator : ScriptableObjectCreator {

		protected string baseTypeName;
		Action listener;
		UnityEngine.Object objSomething=null;
		ScriptableObject scriptableData;
		bool onceScaned = false;
		string fileName;
		string folderName;
		[MenuItem ("Extension/ScriptableObjectCreator")]
		static void  Init () 
		{
			GetWindow(typeof (AllScriptableObjectCreator),false,"ScriptableObjectCreator");
		}

		void OnGUI()
		{
			if(onceScaned)
				GUI.enabled = false;
			objSomething = EditorGUILayout.ObjectField(objSomething,typeof(UnityEngine.Object),false,GUILayout.Height(20));
			if(objSomething != null )
			{
				//---インプットされたデータを元にインスタンスを生成
				scriptableData = CreateInstance(objSomething.name);

				//---ファイルネームエリア
				listener += ()=>fileName = EditorGUILayout.TextField("FileName",fileName);

				//---ベースクラス名を取得（ソートに使う）
				baseTypeName = scriptableData.GetType().BaseType.ToString();

				//---ScriptableObjectをスキャンしてインプットフィールドを生成
				var infolist = ScanType(scriptableData,baseTypeName);
				listener = CreateInputField(infolist,scriptableData,listener);	

				//---生成されるフォルダを指定
				listener += ()=> folderName = EditorGUILayout.TextField("FolderName",folderName);

				//---このインプットフィールドを使用禁止へ
				objSomething = null;
				onceScaned = true;
			}
			GUILayout.Space(10);
			GUI.enabled = true;
			//---登録されたリスナーデリゲートを元にインプットフィールドを生成
			listener?.Invoke();
	
			if(!onceScaned)
				GUI.enabled = false;
			if(GUILayout.Button("Create"))
			{
				CreateAsset(scriptableData,fileName,folderName);

				//---項目をリセット
				onceScaned = false;
				listener = null;
			}
		}

		
		
	}
}

namespace utilEditor
{
	public class ScriptableObjectCreator : EditorWindow
	{
		/// <summary>
		///　生成したScriptableオブジェクトを指定したパスに保存する
		/// </summary>
		/// <typeparam name="T">保存したいScriptableObjectのクラス</typeparam>
		/// <param name="asset">保存したいScriptableObject</param>
		/// <param name="name">ファイル名</param>
		/// <param name="directory">保存するディレクトリ</param>
		public static void CreateAsset<T> (T asset,string name,string directory) where T : ScriptableObject
		{
 
			string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath ($"Assets/Data/{directory}/" + name + ".asset");
 
			AssetDatabase.CreateAsset (asset, assetPathAndName);
			AssetDatabase.SaveAssets ();
       		AssetDatabase.Refresh();
		}

		//---入力されたクラスをスキャンしてフィールド名を取得する
		static public List<FieldInfo> ScanType(ScriptableObject data,string baseTypeName)
		{
			//---タイプ名とフィールド名を取得
			Type t = data.GetType();
			var fieldList = t.GetFields();
			//---ベースクラスのフィールドからインプットフィールドが生成されるようにソート
			var orderedFieldList = fieldList.OrderByDescending(X=>X.DeclaringType.ToString()==baseTypeName).ToList();
			return orderedFieldList;
		}

		/// <summary>
		/// クラスのフィールドからインプットフィールドを生成
		/// </summary>
		/// <param name="orderedFieldList">フィールドのリスト</param>
		/// <param name="thisClass">自分のクラス</param>
		/// <param name="listener">EditorGuiLayoutメソッドを格納するためのリスナー</param>
		/// <returns></returns>
		static public Action CreateInputField(List<FieldInfo> orderedFieldList,ScriptableObject thisClass,Action listener)
		{
			foreach(var field in orderedFieldList)
			{
				switch(field.FieldType.Name)
				{
					case "String":
						listener += CreateTextFieldFromType(field,thisClass);
						break;
					case "Int32":
						listener += CreateInputFieldFromType(field,thisClass);
						break;
					case "Single":
						listener += CreateFloatFieldFromType(field,thisClass);
						break;
				}
			}
			return listener;
		}
		//---CreateInputFieldで使用される
		//---単体ではあまり使う気なし
		static public Action CreateInputFieldFromType(FieldInfo data,ScriptableObject thisClass)
		{
			return () => data.SetValue(thisClass,EditorGUILayout.IntField(data.Name,(int)data.GetValue(thisClass)) );
		}
		static public Action CreateTextFieldFromType(FieldInfo data,ScriptableObject thisClass)
		{
			return () => data.SetValue(thisClass,EditorGUILayout.TextField(data.Name,(string)data.GetValue(thisClass)) );
		}
		static public Action CreateFloatFieldFromType(FieldInfo data,ScriptableObject thisClass)
		{
			return () => data.SetValue(thisClass,EditorGUILayout.FloatField(data.Name,(float)data.GetValue(thisClass)) );
		}
	}


	static public class	Assets
	{
		//---指定したフォルダを元にアセットリストを取得する
		//---folderPath: (ex) "Resource/testFolder/"
		//---extension:(ex) ".psd"
		static public List<T> GetAssetsFromDirectory<T>(string folderPath,string extension) where T:UnityEngine.Object
		{
			var objList = Directory.GetFiles($"{Application.dataPath}/{folderPath}")
					.Where((X)=>X.Contains(extension))
					.Where((X)=>!X.Contains(".meta"))
					.Select((X)=>X.Substring(Application.dataPath.Length-6));
			List<T> pdbList = new List<T>();
			foreach(var item in objList)
			{
				pdbList.Add(AssetDatabase.LoadAssetAtPath<T>(item));
			} 
			return pdbList;

		}

	}

}
