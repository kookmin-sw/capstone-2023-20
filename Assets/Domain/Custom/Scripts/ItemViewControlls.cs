using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemViewControlls : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private InventoryitemController InventoryControlls;
    private GameObject SelectedItem;

    // 선택한 아이템
    private void Start()
    {
        SelectedItem = InventoryControlls.SelectedItem;
    }

    // 지속적으로 ItemView 갱신, 아이템 View 시점 조작 구현
    public void OnDrag(PointerEventData eventData)
    {
        SelectedItem = InventoryControlls.SelectedItem;
        SelectedItem.transform.eulerAngles += new Vector3(-eventData.delta.y, -eventData.delta.x);
    }

    // ItemView 닫는 함수
    public void CloseView()
    {
        gameObject.SetActive(false);
    }
}
