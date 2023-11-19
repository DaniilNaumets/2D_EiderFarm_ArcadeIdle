using Economy;
using Economy.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildStorage))]
public class Machine : MonoBehaviour
{
    private BuildStorage _storage;
    private Converter _converter;
    private ResourceTransmitter _transmitter;

    private bool _isWorked;

    [SerializeField, Header("����� ������������")]
    private int _delayProduction;

    private ItemType _typeFromPlayer;

    private void Start()
    {
        _storage = GetComponent<BuildStorage>();
        _converter = GetComponent<Converter>();
        _transmitter = GetComponent<ResourceTransmitter>();

        _transmitter.TransmitteEvent += Production;
    }

    private void Make(int _fluffCount)
    {
        _storage.AddFluff(_fluffCount);
        _transmitter.CheckBag();
    }

    private IEnumerator Production(ItemType _typeToPlayer, Inventory _characterInventory, int _fluffCount)
    {
        if (!_isWorked)
        {
            _isWorked = true;
            _typeFromPlayer = _converter.ConvertToFrom(_typeToPlayer);
            _characterInventory.RemoveItems(_typeFromPlayer, _fluffCount);
            yield return new WaitForSecondsRealtime(_delayProduction);
            _isWorked = false;
            Make(_fluffCount);
        }
    }
}
