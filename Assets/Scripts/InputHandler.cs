using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Person))]
public class InputHandler : MonoBehaviour
{
    [SerializeField] private Person _person;
    [SerializeField] private Joystick _joystick;

    [Header("���������������� �����.")]
    [SerializeField, Range(0, 1)] private float _sensitivity;

    private void Start()
    {
        _person = GetComponent<Person>();
    }

    private void Update()
    {
        Vector3 direction = new Vector3(_joystick.Horizontal, _joystick.Vertical, 0);

        if (direction.magnitude > _sensitivity)
            _person.GetMovement().Move(direction, _person.GetSpeed());
    }
}
