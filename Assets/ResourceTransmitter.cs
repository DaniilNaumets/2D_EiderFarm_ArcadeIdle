using System.Collections;
using Building.Constructions;
using Economy;
using General;
using UnityEngine;

[RequireComponent(typeof(Construction))]
[RequireComponent(typeof(BuildStorage))]
public class ResourceTransmitter : MonoBehaviour
{
    public delegate IEnumerator CoroutineDelegate(Item typeFrom, Inventory inv, int fluff);
    public event CoroutineDelegate TransmitteEvent;

    [SerializeField, Header("������� ���� ���������� �� ������")] private int _fluffCount;
    [SerializeField] private Item _typeToPlayer;
    [SerializeField] private Inventory _characterInventory;

    private Construction _construction;
    private BuildStorage _storage;

    private Machine _machine;


    private void Awake()
    {
        _construction = GetComponent<Construction>();
        _storage = GetComponent<BuildStorage>();
        _machine = GetComponent<Machine>();
    }

    public void CheckBag()
    {
        if (_characterInventory == null) return;

        Transmitte();

        if (_fluffCount != 0 && TransmitteEvent != null)
            StartCoroutine(TransmitteEvent?.Invoke(_typeToPlayer, _characterInventory, _fluffCount));
    }

    private void Transmitte()
    {
        int count = _storage.GetFluff();

        _characterInventory.AddItems(_typeToPlayer, count);
        _storage.ResetFluff();

        EventHandler.OnItemTransmitted?.Invoke(_construction.typeConstruction, _typeToPlayer, count);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InputHandler>())
        {
            _characterInventory = collision.gameObject.GetComponent<Inventory>();
            CheckBag();
            _machine.Animation(true, _construction.GetCurrentGrade());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_characterInventory == collision.GetComponent<Inventory>() &&
            collision.gameObject.GetComponent<InputHandler>())
        {
            _characterInventory = null;
            _machine.Animation(false, _construction.GetCurrentGrade());
        }
    }
}