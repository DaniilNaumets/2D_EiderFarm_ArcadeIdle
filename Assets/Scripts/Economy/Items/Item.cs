using System;
using UnityEngine;

namespace Economy
{
    [CreateAssetMenu(menuName = "Inventory/Item", fileName = "New Item", order = 0)]
    [Serializable] public class Item : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private int _price;

        public Sprite GetIcon() => _icon;
        public string GetName() => _name;
        public int GetPrice() => _price;
    }
}