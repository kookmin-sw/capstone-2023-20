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
        // �ʱ� ��ġ�� ȸ�� ���� �����մϴ�.
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
        // ���콺�� Ŭ������ �� ȣ��Ǵ� �Լ��Դϴ�.
        ResetObjectsToInitialPosition();
    }

    public void ResetObjectsToInitialPosition()
    {
        Debug.Log("click");
        // ������Ʈ���� ��ġ�� ȸ�� ���� �ʱ� ������ �ǵ����ϴ�.
        for (int i = 0; i < objectsToReset.Length; i++)
        {
            objectsToReset[i].position = initialPositions[i];
            objectsToReset[i].rotation = initialRotations[i];
        }
    }
}
