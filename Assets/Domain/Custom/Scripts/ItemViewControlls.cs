using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemViewControlls : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private InventoryitemController InventoryControlls;
    private GameObject SelectedItem;

    // ������ ������
    private void Start()
    {
        SelectedItem = InventoryControlls.SelectedItem;
    }

    // ���������� ItemView ����, ������ View ���� ���� ����
    public void OnDrag(PointerEventData eventData)
    {
        SelectedItem = InventoryControlls.SelectedItem;
        SelectedItem.transform.eulerAngles += new Vector3(-eventData.delta.y, -eventData.delta.x);
    }

    // ItemView �ݴ� �Լ�
    public void CloseView()
    {
        gameObject.SetActive(false);
    }
}
