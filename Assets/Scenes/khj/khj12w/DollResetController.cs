using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollResetController : MonoBehaviour
{
    public Transform[] objectsToReset;

    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;

    private void Start()
    {
        // 초기 위치와 회전 값을 저장합니다.
        int objectCount = objectsToReset.Length;
        initialPositions = new Vector3[objectCount];
        initialRotations = new Quaternion[objectCount];

        for (int i = 0; i < objectCount; i++)
        {
            initialPositions[i] = objectsToReset[i].position;
            initialRotations[i] = objectsToReset[i].rotation;
        }
    }

    private void OnMouseDown()
    {
        // 마우스로 클릭했을 때 호출되는 함수입니다.
        ResetObjectsToInitialPosition();
    }

    public void ResetObjectsToInitialPosition()
    {
        Debug.Log("click");
        // 오브젝트들의 위치와 회전 값을 초기 값으로 되돌립니다.
        for (int i = 0; i < objectsToReset.Length; i++)
        {
            objectsToReset[i].position = initialPositions[i];
            objectsToReset[i].rotation = initialRotations[i];
        }
    }
}
