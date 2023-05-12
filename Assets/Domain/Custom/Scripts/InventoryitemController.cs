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
    public Texture TaichiTexture;
    public Texture LatifaTexture;
    private string ItemName = "";
    private string ItemDetails = "";
    public GameObject ItemModel;
    private GameObject itemPrefab;
    
    
    private void Start()
    {
        if (InventoryManager.Instance.gameObject.name == "InventoryManagerLatifa")
        {
            ItemView.gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.transform.Find("RawImage").gameObject.GetComponent<RawImage>().texture = LatifaTexture;
        }
        else
        {
            ItemView.gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.transform.Find("RawImage").gameObject.GetComponent<RawImage>().texture = TaichiTexture;
        }
    }

    // 아이템(인벤토리)가 비활성화됐을 경우 ItemView와 ItemTooltip 모두 비활성화
    private void Update()
    {
        if (gameObject.activeSelf == false)
        {
            ItemView.SetActive(false);
            ItemTooltip.SetActive(false); 
        }
    }

    public GameObject getItemModel()
    {
        return ItemModel;
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


    // 아이템 사용 및 ItemView시 View용 아이템 복사.
    public void RemoveItem()
    {
        ItemName = gameObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text;

        if (ItemName == "Phone" || ItemName=="HP" )
        {
            InventoryManager.Instance.removeItem(ItemName);
            Destroy(gameObject);
        }
        else
        {
            if (ItemName == "Phone") {
                ItemModel = GameObject.Find("phoneView");
                itemPrefab = Instantiate(ItemModel, new Vector3(1500, 1500, 1500), Quaternion.Euler(new Vector3(0,0,180)));
            }
            else if (ItemName == "Attend")
            {
                Debug.Log("ItemModel: " + ItemModel);
                itemPrefab = Instantiate(ItemModel, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
                Debug.Log("MOVED! : "+itemPrefab.gameObject.name+ itemPrefab.gameObject.transform.localPosition);
                Debug.Log(itemPrefab);
            }
            

            Debug.Log("ItemModel: " + ItemModel);
            SelectedItem = itemPrefab;
            Debug.Log("SelectedItem:"+InventoryManager.Instance.SelectedItem);
            Debug.Log("itemPrefab:"+itemPrefab);
            InventoryManager.Instance.SelectedItem = SelectedItem;
            InventoryManager.Instance.viewItem();
            ItemView.SetActive(true);
           
        }
    }

    // SelectedItem의 Null화 방지.
    public void CloseItemview()
    {
        GameObject DestroyItemview = SelectedItem;
        SelectedItem = TempView;
        Destroy(DestroyItemview);
    }
}


