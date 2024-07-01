using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U22Game.Handlers
{
    public class EventLoader : MonoBehaviour
    {
        [SerializeField] private string jsonFile = "events.json";
        private List<Events.Event> eventList;
        private int currentDate;
        private bool isFinishEvent = true;

        void Awake()
        {
            EventTextLoader.FinishEvent += OnFinishEvent;
        }

        void OnDestroy()
        {
            EventTextLoader.FinishEvent -= OnFinishEvent;
        }

        void Start()
        {
            if (jsonFile != null)
            {
                eventList = Events.Event.FromJson(jsonFile);
                currentDate = JsonIoHandler.LoadFromJson().CurrentDate;

                StartCoroutine(ExecuteEvents(eventList));
            }
            else
            {
                Debug.LogError("JSON file not assigned.");
            }
        }

        void OnFinishEvent()
        {
            isFinishEvent = true;
        }

        private IEnumerator ExecuteEvents(List<Events.Event> eventList)
        {
            foreach (var eventItem in eventList)
                {
                    Debug.Log($"EventFile: {eventItem.eventFile}\nExecuteDate: {eventItem.executeDate}\nExecuteTiming: {eventItem.executeTiming}");

                    if (eventItem.executeDate == currentDate && eventItem.executeTiming.Equals("afterLoad"))
                    {
                        yield return new WaitUntil(() => isFinishEvent);

                        EventTextLoader.LoadEvent(eventItem.eventFile);

                        isFinishEvent = false;
                    }
                }
        }
    }
}
