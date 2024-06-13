using UnityEngine;
using U22Game.Handlers;

namespace U22Game.Events
{
    public class ShowDateTextEvent : MonoBehaviour
    {
        public TextInitHandler textInitHandler; // TextMeshProController の参照
        private SaveDataHandler saveData; // SaveData インスタンスを保持する変数

        private void Start()
        {
            saveData = JsonIoHandler.LoadFromJson();

            textInitHandler.SetText(saveData.CurrentDate.ToString() + "日目");
        }
    }
}
