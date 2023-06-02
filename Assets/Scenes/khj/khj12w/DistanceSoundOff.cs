using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceSoundOff : MonoBehaviour
{
    private AudioSource audioSource;
    private Transform listener;
    public float maxDistance = 10f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        listener = Camera.main.transform; // �Ǵ� �ٸ� ����� �������� Transform�� ����Ͻʽÿ�
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, listener.position);

        if (distance > maxDistance)
        {
            audioSource.enabled = false;
        }
        else
        {
            audioSource.enabled = true;
        }
    }
}
