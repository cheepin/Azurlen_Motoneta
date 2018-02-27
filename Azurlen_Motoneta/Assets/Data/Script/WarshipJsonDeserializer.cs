using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Globalization;
using data;
namespace data
{
	public class WarshipJsonDeserializer : util.Singleton<WarshipJsonDeserializer>
	{
		public IEnumerable<WarshipData> DestroyerDataList{get;private set;}
		[SerializeField] List<WarshipDataScriptable> WarshipDataList;


		void Start()
		{
			DontDestroyOnLoad(gameObject);
		}

		static  public IEnumerable<WarshipDataScriptable> GetWarshipData(string searchKeyword)
		{
			IEnumerable<WarshipDataScriptable> DestroyerDataList = null;

			if(searchKeyword=="Destroyer"||searchKeyword=="LightCruiser"||searchKeyword=="HeavyCruiser"||searchKeyword=="BattleShip"||searchKeyword=="BattleCruiser"||searchKeyword=="AircraftCarrier"||searchKeyword=="LightAircraftCarrier"||searchKeyword=="GunBoat"||searchKeyword=="RepairShip")
				 DestroyerDataList = Instance.WarshipDataList.Where(X=>X.Type == searchKeyword)
				.OrderBy(X=>X.Name,StringComparer.CurrentCultureIgnoreCase);
			else if(searchKeyword=="ユニオン"||searchKeyword=="ロイヤル"||searchKeyword=="鉄血"||searchKeyword=="北方連合"||searchKeyword=="重桜"||searchKeyword=="東煌")	
				DestroyerDataList = Instance.WarshipDataList.Where(X=>X.Camp == searchKeyword)
				.OrderBy(X=>X.Name,StringComparer.CurrentCultureIgnoreCase);
			else if(searchKeyword=="N"||searchKeyword=="R"||searchKeyword=="SR"||searchKeyword=="SSR")	
				DestroyerDataList = Instance.WarshipDataList.Where(X=>X.Rarity == searchKeyword)
				.OrderBy(X=>X.Name,StringComparer.CurrentCultureIgnoreCase);

			return DestroyerDataList;
		}

		static public WarshipDataScriptable GetScriptableWarshipData(string warshipName)
		{
			return Instance.WarshipDataList.Where(X=>X.Name == warshipName).First();
		}

	} 



}
