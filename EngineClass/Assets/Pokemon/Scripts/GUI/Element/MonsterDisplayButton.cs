using UnityEngine;
using UnityEngine.UI;


public class MonsterDisplayButton : MonoBehaviour
{
    private MonsterStateView m_monsterView;
    private Button m_button;

    public Button Button => m_button;

    private void Awake()
    {
        m_button = GetComponent<Button>();
        m_monsterView = GetComponent<MonsterStateView>();
    }

    public void Bind(MonsterDisplayModel pModel)
    {
        m_monsterView.Bind(pModel);
    }
}