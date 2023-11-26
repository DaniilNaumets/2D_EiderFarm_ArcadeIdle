using System;
using UnityEngine;

namespace Economy.Farm_House
{
    public class CreateBuildingTaskCell : MenuCell
    {
        private CreateBuildingTask _task;

        public void SetCell(CreateBuildingTask task)
        {
            _task = task;
            RefreshButton();
        }

        public void ClickTaskBtn() => _task.CheckProgressing();

        private void SetButton(Sprite icon, string description, int requireCount, int currentCount)
        {
            SetButton(icon, description);
            _counter.text = $"{currentCount}/{requireCount}";
        }

        private void RefreshButton()
        {
            if (_task == null) return;

            SetButton(
                    _task.GetIcon(),
                    _task.GetDescription());
                    //_task.GetRequireCount(),
                    //_task.GetCurrentCount());
        }

    }
}