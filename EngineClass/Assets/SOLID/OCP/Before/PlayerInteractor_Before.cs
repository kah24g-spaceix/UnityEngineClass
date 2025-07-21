using UnityEngine;

public class PlayerInterctor_Before : MonoBehaviour
{
    private Item_Before m_currentItem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnItemInteract(m_currentItem);
            m_currentItem = null;
        }
    }
    private void OnItemInteract(Item_Before item)
    {
        switch (item.Type)
        {
            case ItemType.Potion:
                GetComponent<Entity>().AddHealth(10);
                break;
            case ItemType.Bomb:
                GetComponent<Entity>().Kill();
                break;
            case ItemType.Score:
                FindObjectOfType<GameController>().AddScore(1);
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Item_Before>(out var item))
            m_currentItem = item;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Item_Before>(out var item))
        {
            if (m_currentItem == item)
                m_currentItem = null;
        }
    }
}