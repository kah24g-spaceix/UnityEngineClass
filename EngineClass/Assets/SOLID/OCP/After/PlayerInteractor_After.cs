using UnityEngine;

public class PlayerInterctor_After : MonoBehaviour
{
    private IItem_After m_currentItem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            m_currentItem.OnConsume(gameObject);
            m_currentItem = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IItem_After>(out var item))
            m_currentItem = item;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IItem_After>(out var item))
        {
            if (m_currentItem == item)
                m_currentItem = null;
        }
    }
}