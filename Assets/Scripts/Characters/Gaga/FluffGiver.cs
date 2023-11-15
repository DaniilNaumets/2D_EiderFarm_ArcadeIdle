using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluffGiver : MonoBehaviour
{
    public event Action FluffGiveEvent;

    [SerializeField] private bool hasGivenFluff;

    [Header("���� ��������� ���� (� ���������).")]
    [SerializeField] private int _chance;

    private void Start() => hasGivenFluff = false;

    // ���� ��� ������� � �������������
    public void GiveFluff()
    {
        if (!hasGivenFluff)
        {
            hasGivenFluff = true;
            if (UnityEngine.Random.Range(0, 100) < _chance)
            {
                FluffGiveEvent.Invoke();
            }
        }
    }

}
