using UnityEngine;
using UnityEngine.UI;


public class TextButton : MonoBehaviour
{
    private Button m_button;
    private Text m_text;

    public Button Button => m_button;
    public Text Text => m_text;

    private void Awake()
    {
        m_button = GetComponent<Button>();
        m_text = GetComponentInChildren<Text>();
    }
}
