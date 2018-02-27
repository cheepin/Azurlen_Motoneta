using System;
using UnityEngine;

namespace data
{
	[Serializable]
	public class WarshipDataScriptable : ScriptableObject
	{
	
		public string Name;
		public string Type;
		public string Camp;
		public string Rarity;
		[TextArea(1,20)]
		public string History;
		[TextArea(10,30)]
		public string Description;
		[TextArea(1,20)]
		public string Tips;
	}
}