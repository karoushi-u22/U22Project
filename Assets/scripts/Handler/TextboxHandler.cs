using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

namespace U22Game.Handlers
{
    public class TextboxHandler : MonoBehaviour
    {
        public static event UnityAction<TextboxHandler> StartTextboxEvent;
        public static event UnityAction TextboxClickEvent;
        public static event UnityAction CompleteSetTextEvent;
        
        private bool waitFlag = false;
        private Canvas textbox;
        private Coroutine setTextCoroutine;
        private Coroutine waitSkipCoroutine;
        
        [SerializeField] private TextMeshProUGUI textfieldMain;
        [SerializeField] private TextMeshProUGUI textfieldPlayerName;
        [SerializeField] private float delayStart = 0.5f;
        [SerializeField] private float delayDuration = 0.1f;

        [SerializeField] private Canvas endingBackgroundCanvas;
        [SerializeField] private Image endingBackgroundImage;
        [SerializeField] private TextMeshProUGUI endingCentralText;
        [SerializeField] private Image blackBackgroundImage; // 追加

        private void Start()
        {
            if (textbox == null && !TryGetComponent<Canvas>(out textbox))
            {
                Debug.LogError("Canvas component not found.");
            }

            if (textfieldMain == null)
            {
                Debug.LogError("TextMeshProUGUI component for textfieldMain not assigned.");
            }

            if (textfieldPlayerName == null)
            {
                Debug.LogError("TextMeshProUGUI component for textfieldPlayerName not assigned.");
            }

            if (endingBackgroundCanvas == null)
            {
                Debug.LogError("Ending background canvas not assigned.");
            }
            else
            {
                endingBackgroundCanvas.enabled = false;
            }

            if (endingCentralText == null)
            {
                Debug.LogError("Ending central text not assigned.");
            }
            else
            {
                endingCentralText.enabled = false;
            }

            if (endingBackgroundImage == null)
            {
                Debug.LogError("Ending background image not assigned.");
            }
            else
            {
                endingBackgroundImage.gameObject.SetActive(false);
            }

            if (blackBackgroundImage == null)
            {
                Debug.LogError("Black background image not assigned.");
            }
            else
            {
                blackBackgroundImage.gameObject.SetActive(false);
            }

            HideTextbox();
            StartTextboxEvent?.Invoke(this);
        }

        private void Update()
        {
            if (textbox.enabled)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
                {
                    int length = textfieldMain.text.Length;

                    if (setTextCoroutine == null)
                    {
                        TextboxClickEvent?.Invoke();

                        if (waitSkipCoroutine != null)
                        {
                            StopCoroutine(waitSkipCoroutine);
                            waitSkipCoroutine = null;
                        }
                    }
                    else if (!waitFlag && textfieldMain.maxVisibleCharacters < length)
                    {
                        StopCoroutine(setTextCoroutine);
                        setTextCoroutine = null;
                        textfieldMain.maxVisibleCharacters = length;
                        CompleteSetTextEvent?.Invoke();
                    }
                }
            }
        }

        private IEnumerator SetTextCoroutine(TextMeshProUGUI textMeshPro, string newText, int delaySkip)
        {
            textMeshPro.maxVisibleCharacters = 0;

            if (textfieldMain != null)
            {
                textfieldMain.text = newText;
            }

            waitSkipCoroutine = StartCoroutine(WaitSkipCoroutine(delaySkip));
            yield return new WaitForSeconds(delayStart);

            int length = textMeshPro.text.Length;
            for (int i = 0; i < length; i++)
            {
                textMeshPro.maxVisibleCharacters = i + 1;

                if (textMeshPro.text[i] == '\n')
                {
                    yield return new WaitForSeconds(delayDuration * 10);
                }
                else
                {
                    yield return new WaitForSeconds(delayDuration);
                }
            }

            textMeshPro.maxVisibleCharacters = length;
            setTextCoroutine = null;
            CompleteSetTextEvent?.Invoke();
        }

        private IEnumerator WaitSkipCoroutine(int delaySkip)
        {
            waitFlag = true;
            Debug.Log($"Wait Time: {Math.Max(delayStart, delaySkip)}");
            yield return new WaitForSeconds(Math.Max(delayStart, delaySkip));
            Debug.Log("Skip Available");
            waitFlag = false;
        }

        public void ShowTextbox()
        {
            if (textbox != null)
            {
                textbox.enabled = true;
                Debug.Log("TextboxHandler: Textbox shown.");
            }
        }

        public void HideTextbox()
        {
            if (textbox != null)
            {
                textbox.enabled = false;
                Debug.Log("TextboxHandler: Textbox hidden.");
            }
        }

        public void SetTextMain(string newText, int delaySkip)
        {
            if (setTextCoroutine != null)
            {
                StopCoroutine(setTextCoroutine);
            }

            setTextCoroutine = StartCoroutine(SetTextCoroutine(textfieldMain, newText, delaySkip));
            Debug.Log($"TextboxHandler: Setting main text: {newText}");
        }

        public void SetTextPlayerName(string newText)
        {
            if (textfieldPlayerName != null)
            {
                textfieldPlayerName.text = newText;
                Debug.Log($"TextboxHandler: Setting player name text: {newText}");
            }
        }

        public void ShowCentralText(string newText)
        {
            if (endingBackgroundCanvas != null && endingCentralText != null)
            {
                endingBackgroundCanvas.enabled = true;
                endingCentralText.text = newText;
                endingCentralText.enabled = true;
                Debug.Log($"TextboxHandler: Showing central text: {newText}");
            }
        }

        public void HideCentralText()
        {
            if (endingBackgroundCanvas != null && endingCentralText != null)
            {
                endingBackgroundCanvas.enabled = false;
                endingCentralText.enabled = false;
                Debug.Log("TextboxHandler: Hiding central text.");
            }
        }

        public void ShowEndingBackgroundImage()
        {
            if (endingBackgroundImage != null)
            {
                endingBackgroundImage.gameObject.SetActive(true);
                Debug.Log("EndingBackgroundImage: Showing image.");
            }
        }

        public void HideEndingBackgroundImage()
        {
            if (endingBackgroundImage != null)
            {
                endingBackgroundImage.gameObject.SetActive(false);
                Debug.Log("EndingBackgroundImage: Hiding image.");
            }
        }

        public void ShowBlackBackground()
        {
            if (blackBackgroundImage != null)
            {
                blackBackgroundImage.gameObject.SetActive(true);
                Debug.Log("BlackBackgroundImage: Showing image.");
            }
        }

        public void HideBlackBackground()
        {
            if (blackBackgroundImage != null)
            {
                blackBackgroundImage.gameObject.SetActive(false);
                Debug.Log("BlackBackgroundImage: Hiding image.");
            }
        }

        public IEnumerator DisplayTextByLine(string text)
        {
            string[] lines = text.Split('\n');
            foreach (var line in lines)
            {
                SetTextMain(line, 0); // 適切な遅延時間を設定する
                yield return new WaitUntil(() => !IsDisplayingText);
            }
        }

        public bool IsDisplayingText
        {
            get
            {
                return setTextCoroutine != null;
            }
        }
    }
}
