using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Person : MonoBehaviour
{
    [SerializeField] private Movement _movement;

    [Header("�������� �������� ���������.")]
    [SerializeField, Range(0, 10)] private float _speed;

    private void Start() => _movement = GetComponent<Movement>();

    public Movement GetMovement() => _movement;
    public float GetSpeed() => _speed;

}
