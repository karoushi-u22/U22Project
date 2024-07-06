using UnityEngine;
using U22Game.Handlers;

public class CallScenarioHandler1 : MonoBehaviour
{
    private EndingSceneController endingSceneController;

    private void Start()
    {
        endingSceneController = FindObjectOfType<EndingSceneController>();
        if (endingSceneController == null)
        {
            Debug.LogError("CallScenarioHandler1: EndingSceneController not found in the scene.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("CallScenarioHandler1: Alpha1 key pressed.");
            StartCoroutine(endingSceneController.ShowBetrayalEnding());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("CallScenarioHandler1: Alpha2 key pressed.");
            StartCoroutine(endingSceneController.ShowBankruptcyEnding());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("CallScenarioHandler1: Alpha3 key pressed.");
            StartCoroutine(endingSceneController.ShowEmploymentEnding());
        }
    }
}
