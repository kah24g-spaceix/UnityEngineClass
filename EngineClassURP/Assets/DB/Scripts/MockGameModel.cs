using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MockGameModel : MonoBehaviour, IGameModel
{
    private Dictionary<Int32, CharacterState> m_originalCharacterStates;

    private Dictionary<Int32, CharacterState> m_states;
    private Dictionary<Int32, ItemData> m_datas;

    private void Awake()
    {
        LoadData();
    }

    private void LoadData()
    {
        m_originalCharacterStates = new Dictionary<int, CharacterState>()
        {
            {10001,new CharacterState(10001,"characterA",5,100) },
            {10002, new CharacterState(10002,"characterB",5,100) }
        };
        m_datas = new Dictionary<int, ItemData>()
        {
            { 20001,new ItemData(20001,"Red Powder",0.1f,10,5) },
            { 20002,new ItemData(20002,"Blue Powder",0.1f,10,5) },
        };

        m_states = new Dictionary<int, CharacterState>();
        foreach (var originalState in m_originalCharacterStates.Values)
        {
            m_states.Add(originalState.ID, originalState);
        }
    }

    public bool DoEnchant(int pCharacterID, int pItemID)
    {
        if (!m_states.TryGetValue(pCharacterID, out var state))
            throw new Exception($"Character State with {pCharacterID} is not found");

        if (!m_datas.TryGetValue(pItemID, out var data))
            throw new Exception($"Item Data with {pItemID} is not found");

        if (UnityEngine.Random.Range(0f, 1f) <= data.Chance)
        {
            Int32 attack = state.Attack + data.Amount;
            CharacterState newState = new CharacterState(state.ID, state.Name, attack, state.Health);
            m_states[pCharacterID] = newState;
            return true;
        }
        else
        {
            Int32 health = state.Health - data.Damage;
            if(health <= 0)
                m_states.Remove(pCharacterID);
            else
            {
                CharacterState newState = new CharacterState(state.ID, state.Name, state.Attack, health);
                m_states[pCharacterID] = newState;
            }
            return false;
        }
    }

    public CharacterState[] GetCharacterStates()
    {
        return m_states.Values.ToArray();
    }

    public ItemData[] GetItemDatas()
    {
        return m_datas.Values.ToArray();
    }

    public Boolean LoadGame()
    {
        if (!PlayerPrefs.HasKey("Save"))
            return false;

        m_states.Clear();
        String value = PlayerPrefs.GetString("Save");
        List<CharacterSaveData> saveDatas = JsonConvert.DeserializeObject<List<CharacterSaveData>>(value);
        foreach (var saveData in saveDatas)
        {
            if(m_originalCharacterStates.TryGetValue(saveData.ID,out var originalState))
            {
                Int32 id = saveData.ID;
                String name = originalState.Name;
                Int32 attack = saveData.Attack;
                Int32 health = saveData.Health;

                m_states.Add(id, new CharacterState(id, name, attack, health));
            }
        }
        return true;
    }

    public void SaveGame()
    {
        List<CharacterSaveData> saveDatas = new List<CharacterSaveData>();

        foreach (var state in m_states.Values)
        {
            CharacterSaveData saveData = new CharacterSaveData(state);
            saveDatas.Add(saveData);
        }

        String json = JsonConvert.SerializeObject(saveDatas);
        Debug.Log(json);
        PlayerPrefs.SetString("Save", json);
    }
}