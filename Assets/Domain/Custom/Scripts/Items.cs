using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]

public class Items : ScriptableObject
{
    // Start is called before the first frame update
    public int id;
    public string ItemName;
    public int value;
    public Sprite ItemIcon;
    public string ItemDetails;
}
