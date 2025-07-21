using UnityEngine;

public class PotionItem : MonoBehaviour, IItem_After
{
    public void OnConsume(GameObject pUser)
    {
        pUser.GetComponent<Entity>().AddHealth(10);
    }
}