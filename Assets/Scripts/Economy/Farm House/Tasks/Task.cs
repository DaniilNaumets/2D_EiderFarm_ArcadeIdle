using System;
using General;
using UnityEngine;
using EventHandler = General.EventHandler;

namespace Economy.Farm_House
{
    public abstract class Task : ScriptableObject
    {
        [SerializeField] private TaskStage _stage;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _reward;
        
        public Sprite GetIcon() => _icon;
        public string GetName() => _name;
        public string GetDescription() => _description;
        public int GetReward() => _reward;

        public TaskStage GetStage() => _stage;
        
        public void SetStage(TaskStage stage)
        {
            _stage = stage;
            EventHandler.OnTaskStageChanged?.Invoke(this, _stage);
        }

        public void GiveReward(Inventory inventory)
        {
            if (inventory.TryGetBunch(GlobalConstants.Money, out ItemBunch wallet))
                wallet.AddItems(_reward);
        }
    }

    public enum TaskStage
    {
        NotStarted,
        Progressing,
        Completed,
        Passed
    }
}