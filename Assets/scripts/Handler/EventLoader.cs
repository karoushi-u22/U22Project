using System.Collections.Generic;
using UnityEngine;

namespace U22Game.Handlers
{
    public class EventLoader : MonoBehaviour
    {
        [SerializeField] private string jsonFile = "events.json";
        private List<Events.Event> eventList;
        private int currentDate;

        void Start()
        {
            if (jsonFile != null)
            {
                eventList = Events.Event.FromJson(jsonFile);
                currentDate = JsonIoHandler.LoadFromJson().CurrentDate;

                foreach (var eventItem in eventList)
                {
                    Debug.Log($"EventFile: {eventItem.eventFile}\nExecuteDate: {eventItem.executeDate}\nExecuteTiming: {eventItem.executeTiming}");

                    if (eventItem.executeDate == currentDate && eventItem.executeTiming.Equals("afterLoad"))
                    {
                        EventTextLoader.LoadEvent(eventItem.eventFile);
                    }
                }
            }
            else
            {
                Debug.LogError("JSON file not assigned.");
            }
        }
    }
}
