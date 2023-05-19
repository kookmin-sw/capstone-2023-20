using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldTarget : MonoBehaviour
{
    public bool isPickedUp = false; // 아이템이 들어져 있는지 여부를 나타내는 변수

    public void PickUp()
    {
        isPickedUp = true;
    }

    public void Drop()
    {
        isPickedUp = false;
    }

    public bool CanBePickedUp()
    {
        return !isPickedUp;
    }

    private void Update()
    {
        // 예시: 모든 아이템들이 제자리에 위치하면 파괴됨
        if (CheckIfInPlace())
        {
            Die();
        }
    }

    private bool CheckIfInPlace()
    {
        // 아이템의 제자리 위치 여부를 확인하는 로직을 작성
        // 예시: 아이템의 위치나 조건을 체크하여 제자리에 위치하는지 판단
        // 각 아이템에 맞는 체크 로직을 작성해야 합니다.
        return false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
