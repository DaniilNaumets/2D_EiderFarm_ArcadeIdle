using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStorage : MonoBehaviour
{

    [Header("���������� ����.")]
    [SerializeField] private int _fluffCount;

    public void AddFluff()
    {
        _fluffCount++;
    }
}
