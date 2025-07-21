using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class JsonGameSaver : IGameSaver
{
	public List<CharacterSaveData> Load()
	{
		if (!PlayerPrefs.HasKey("Save"))
			return null;
		string json = PlayerPrefs.GetString("Save");
		return JsonConvert.DeserializeObject<List<CharacterSaveData>>(json);
	}
	public void Save(List<CharacterSaveData> pSaveData)
	{
		string json = JsonConvert.SerializeObject(pSaveData);
		PlayerPrefs.SetString("Save", json);
	}
}