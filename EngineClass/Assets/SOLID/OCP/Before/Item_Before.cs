using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Potion, // hp+
    Bomb, // hp-
    Score, // score +1

}
public class Item_Before : MonoBehaviour
{
    [SerializeField] private ItemType type;
    public ItemType Type => type;

    


}
