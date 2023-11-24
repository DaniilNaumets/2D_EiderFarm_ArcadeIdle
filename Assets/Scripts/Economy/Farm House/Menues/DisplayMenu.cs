using UnityEngine;

namespace Economy.Farm_House
{
    public abstract class DisplayMenu : MonoBehaviour
    {
        [SerializeField] protected Transform _content;

        protected Inventory _playerInventory;
        protected bool _isHouseMenu = true; // ������������ ���� ��������� ��������� ������ ��� ����������� ������.
        
        public void SwitchDisplay()
        {
            _isHouseMenu = !_isHouseMenu;

            Draw();
        }

        public void DisplayActive(bool value)
        {
            _isHouseMenu = value;

            Draw();
        }

        public abstract void Draw();

        protected void ClearContent()
        {
            var length = _content.childCount;

            for (int i = 0; i < length; i++)
                Destroy(_content.GetChild(i).gameObject);
        }
    }
}