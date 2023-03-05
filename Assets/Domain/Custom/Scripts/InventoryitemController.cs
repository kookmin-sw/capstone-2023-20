using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class InventoryitemController : MonoBehaviour
{
    Items item;

    public Button RemoveButton;
    public GameObject ItemView;
    public GameObject ItemTooltip;
    public GameObject SelectedItem;
    public GameObject TempView;
    private string ItemName = "";
    private string ItemDetails = "";
    private GameObject ItemModel;
    private GameObject itemPrefab;
    
    // ������(�κ��丮)�� ��Ȱ��ȭ���� ��� ItemView�� ItemTooltip ��� ��Ȱ��ȭ
    private void Update()
    {
        if (gameObject.activeSelf == false)
        {
            ItemView.SetActive(false);
            ItemTooltip.SetActive(false);
        }
    }

    public string getItemName()
    {
        ItemName = gameObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text;
        return ItemName;
    }

    public string getItemDetails()
    {
        ItemDetails = gameObject.transform.Find("ItemDetails").GetComponent<TextMeshProUGUI>().text;
        return ItemDetails;
    }

    // ������ ��� �� ItemView�� View�� ������ ����.
    public void RemoveItem()
    {
        ItemName = gameObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text;

        if (ItemName == "Phone" || ItemName=="HP" || ItemName == "W.C Key" || ItemName == "Flashlight")
        {
            InventoryManager.Instance.removeItem(item);
            Destroy(gameObject);
        }
        else
        {
            if (ItemName == "Phone") {
                ItemModel = GameObject.Find("phoneView");
                itemPrefab = Instantiate(ItemModel, new Vector3(1500, 1500, 1500), Quaternion.Euler(new Vector3(0,0,180)));
            }
            else if (ItemName == "HP")
            {
                ItemModel = GameObject.Find("syringeView");
                itemPrefab = Instantiate(ItemModel, new Vector3(1500, 1500, 1500), Quaternion.Euler(new Vector3(90,20,0)));
            }
            
            SelectedItem = itemPrefab;
            ItemView.SetActive(true);
           
        }
    }

    // SelectedItem�� Nullȭ ����.
    public void CloseItemview()
    {
        GameObject DestroyItemview = SelectedItem;
        SelectedItem = TempView;
        Destroy(DestroyItemview);
    }
}


