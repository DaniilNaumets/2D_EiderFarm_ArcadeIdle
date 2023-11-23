using Building.Constructions;
using UnityEngine;

namespace Building
{
    public class BuildMenu : MonoBehaviour
    {
        public Construction buildingPrefab;
        
        private Construction _construction;
        private SpriteRenderer _triggerSprite;
        private Vector3 _buildPos;
        private Quaternion _buildRot;
        
        public void SetPosition(SpriteRenderer triggerSprite, Vector3 buildPos, Quaternion buildRot)
        {
            _triggerSprite = triggerSprite;
            _buildPos = buildPos;
            _buildRot = buildRot;
        }

        public void Build()
        {
            if (_construction != null) return;
            
            if(_triggerSprite != null)
            _triggerSprite.enabled = false;
            Build(buildingPrefab);
            _construction.SetSprite(_construction.Upgrade());
        }

        public void Demolition()
        {
            if (_construction == null) return;

            _triggerSprite.enabled = true;
            Destroy(_construction.gameObject);
        }

        public void Upgrade()
        {
            if (_construction == null || !_construction.CanUpgrade()) return;

            _construction.SetSprite(_construction.Upgrade());
        }

        public Construction GetConstruction() => _construction;
        
        private void Build(Construction building)
        {
            if (_construction != null) Destroy(_construction);

            _construction = Instantiate(building, _buildPos, _buildRot);
        }
    }
}