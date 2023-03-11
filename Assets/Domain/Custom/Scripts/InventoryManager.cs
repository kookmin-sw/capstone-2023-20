using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//김원진 - 인벤토리 습득/제거
public class InventoryManager : MonoBehaviour
{
   public static InventoryManager Instance;
   public List<Items> Items = new List<Items>();

    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject SelectedItem;
    public InventoryitemController[] InventoryItemControllers;
    public GameObject ItemView;

   private void Awake()
   {
        Instance = this;
   }

   public void addItem(Items items)
    {
        Items.Add(items);
        ListItems();
    }

    public void removeItem(Items items)
    {
        Items.Remove(items);
        
    }
    
    public void viewItem()
    {
        SelectedItem.transform.parent = ItemView.transform;
        SelectedItem.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void ListItems()
    {
        foreach(Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach(var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            

            var ItemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var ItemDetails = obj.transform.Find("ItemDetails").GetComponent<TextMeshProUGUI>();
            

            ItemName.text = item.ItemName;
            ItemIcon.sprite = item.ItemIcon;
            ItemDetails.text = item.ItemDetails;
            obj.GetComponent<InventoryitemController>().ItemModel = item.ItemModels;

        }
    }
}
