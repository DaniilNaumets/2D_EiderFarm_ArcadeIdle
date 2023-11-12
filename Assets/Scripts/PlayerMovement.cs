using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;

    [Header("Скорость движения персонажа.")]
    [SerializeField, Range(0, 10)] private float _speed;
    [Header("Коэффициент скорости.")]
    [SerializeField] private float _coefSpeed;   
    [Header("Чувствительность стика.")]
    [SerializeField, Range(0, 1)] private float _sensitivity;

    private void Start()
    {
        throw new NotImplementedException();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(_joystick.Horizontal, _joystick.Vertical, 0);
        
        if(direction.magnitude > _sensitivity)
        {
            Vector3 movement = direction.normalized * _speed * _coefSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
    }
}