using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameModel : MonoBehaviour, IGameModel
{
    private Dictionary<Int32, CharacterState> m_originalCharacterStates;

    private Dictionary<Int32, CharacterState> m_states;
    private Dictionary<Int32, ItemData> m_datas;

    private IGameSaver m_gameSaver = new JsonGameSaver();
    private IGameDataGateWay m_gameDataGateWay = new SQLiteGameDataGateWay();

    private void Awake()
    {
        LoadData();
    }

    private void LoadData()
    {
        m_originalCharacterStates = new Dictionary<int, CharacterState>();
        m_datas = new Dictionary<int, ItemData>();

        foreach (var characterState in m_gameDataGateWay.LoadCharacters())
        {
            m_originalCharacterStates.Add(characterState.ID, characterState);
        }
        foreach (var itemData in m_gameDataGateWay.LoadItems())
        {
            m_datas.Add(itemData.ID, itemData);
        }

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
            if (health <= 0)
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

        List<CharacterSaveData> saveDatas = m_gameSaver.Load();
        if (saveDatas == null)
            return false;

        foreach (var saveData in saveDatas)
        {
            if (m_originalCharacterStates.TryGetValue(saveData.ID, out var originalState))
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

        m_gameSaver.Save(saveDatas);
    }
}