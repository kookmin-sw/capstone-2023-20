using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject ItemView;
    public InventoryitemController inventoryitemController;
    private string content;
    private string header;

    private void Start()
    {
        header = inventoryitemController.getItemName();
        content = inventoryitemController.getItemDetails();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ItemView.activeSelf == false)
        {
            ItemTooltipManager.Show(content,header);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemTooltipManager.Hide();
    }
}
