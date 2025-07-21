using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MonsterStateView : MonoBehaviour
{
    [SerializeField] private Image m_healthFillImage;
    [SerializeField] private Text m_monsterHealthText;
    [SerializeField] private Text m_monsterNameText;

    public void Bind(MonsterDisplayModel pModel)
    {
        //m_healthFillImage.fillAmount 
        //    = (float)pModel.Health / (float)pModel.MaxHealth;
        m_healthFillImage.DOFillAmount((float)pModel.Health / (float)pModel.MaxHealth, 0.1f);
        m_monsterNameText.text = pModel.Name;
        m_monsterHealthText.text
            = $"{pModel.Health}/ {pModel.MaxHealth}";
    }
}