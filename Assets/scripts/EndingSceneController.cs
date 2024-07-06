using System.Collections;
using UnityEngine;

namespace U22Game.Handlers
{
    public class EndingSceneController : MonoBehaviour
    {
        [SerializeField]
        private TextboxHandler textboxHandler;

        private void Start()
        {
            if (textboxHandler == null)
            {
                textboxHandler = FindObjectOfType<TextboxHandler>();
                if (textboxHandler == null)
                {
                    Debug.LogError("EndingSceneController: TextboxHandler not found in the scene.");
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("EndingSceneController: Alpha1 key pressed.");
                StartCoroutine(ShowBetrayalEnding());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("EndingSceneController: Alpha2 key pressed.");
                StartCoroutine(ShowBankruptcyEnding());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("EndingSceneController: Alpha3 key pressed.");
                StartCoroutine(ShowEmploymentEnding());
            }
        }

        public IEnumerator ShowBetrayalEnding()
        {
            textboxHandler.ShowEndingBackgroundImage();
            textboxHandler.ShowBlackBackground();

            string[] betrayalTextLines = {
                "その後、新聞の一面にはEHcopeの情報漏洩についての記事が掲載されていた。",
                "情報漏洩により会社の信用はガタ落ちし、ニュースキャスターが情報漏洩について説明していた。",
                "自分がいた会社が今わの際というのに、どこか他人事のように感じてしまう…。",
                "ひとたび家から出ると、周りの人々は好きな音楽を聴きながら歩き、会社に着くとOLたちは芸能人のエンタメ、上司は野球の話題で熱中している。",
                "岡田さんのおかげで、それなりの地位と給料はもらえているが…",
                "僕はこれでよかったんだろうか。"
            };

            foreach (var line in betrayalTextLines)
            {
                Debug.Log($"EndingSceneController: Showing betrayal line: {line}");
                textboxHandler.ShowCentralText(line);
                yield return new WaitForSeconds(3); // 3秒間表示
            }

            textboxHandler.HideCentralText();
            textboxHandler.HideEndingBackgroundImage();
            
            textboxHandler.ShowTextbox();
            textboxHandler.SetTextPlayerName("???");
            yield return StartCoroutine(textboxHandler.DisplayTextByLine("社員くん"));
            yield return new WaitUntil(() => !textboxHandler.IsDisplayingText);

            textboxHandler.SetTextPlayerName("社員");
            yield return StartCoroutine(textboxHandler.DisplayTextByLine("はいなんでしょう"));
            yield return new WaitUntil(() => !textboxHandler.IsDisplayingText);

            textboxHandler.SetTextPlayerName("???");
            yield return StartCoroutine(textboxHandler.DisplayTextByLine("君は自分がもっと評価されるべきだと感じないか？"));
            yield return new WaitUntil(() => !textboxHandler.IsDisplayingText);

            textboxHandler.HideTextbox();
          
            textboxHandler.HideBlackBackground();
        }

        public IEnumerator ShowBankruptcyEnding()
        {
            textboxHandler.ShowEndingBackgroundImage();
            

            string[] bankruptcyTextLines = {
                "その後、新聞の一面にはEHcopeのセキュリティ問題についての記事が掲載されており、会社がほぼ倒産に追い込まれていることが報じられ",
                "朝のニュースでは、ニュースキャスターと専門家が身近なセキュリティについて説明していた。",
                "クビになった自分は気分転換がてらハンバーガーでも食べに行こうかと町へ繰り出し、",
                "注文を済ませて席に座り、ふと辺りを見回すと、会社員が重要そうな書類を広げていたり、PCの電源を付けたまま追加注文をしようと席に立つ者もいた。",
                "昨日までの自分ならPADで即座に報告をしていただろうけど",
                "今となっては関係ない話だ…",
                "セキュリティって意外と身近なもんだったんだなと今更ながら痛感する。",
                "<会社倒産ＥＮＤ>"
            };

            foreach (var line in bankruptcyTextLines)
            {
                Debug.Log($"EndingSceneController: Showing bankruptcy line: {line}");
                textboxHandler.ShowCentralText(line);
                yield return new WaitForSeconds(3); // 3秒間表示
            }

            textboxHandler.HideCentralText();
            textboxHandler.HideEndingBackgroundImage();
        }

        public IEnumerator ShowEmploymentEnding()
        {
            textboxHandler.ShowEndingBackgroundImage();
            textboxHandler.ShowBlackBackground();

        
            textboxHandler.ShowTextbox();
            textboxHandler.SetTextPlayerName("古川正雄");
            yield return StartCoroutine(textboxHandler.DisplayTextByLine("来てくれたか"));
            yield return new WaitUntil(() => !textboxHandler.IsDisplayingText);

            textboxHandler.SetTextPlayerName("古川正雄");
            yield return StartCoroutine(textboxHandler.DisplayTextByLine("今日は君について話がある"));
            yield return new WaitUntil(() => !textboxHandler.IsDisplayingText);

            textboxHandler.SetTextPlayerName("古川正雄");
            yield return StartCoroutine(textboxHandler.DisplayTextByLine("今までの君の報告を評価した結果"));
            yield return new WaitUntil(() => !textboxHandler.IsDisplayingText);

            textboxHandler.SetTextPlayerName("古川正雄");
            yield return StartCoroutine(textboxHandler.DisplayTextByLine("おめでとう今日から君はEHcopeの一員だ"));
            yield return new WaitUntil(() => !textboxHandler.IsDisplayingText);

            string employmentText = 
                                "GAMECLEAR";

            foreach (var line in employmentText.Split('\n'))
            {
                Debug.Log($"EndingSceneController: Showing employment line: {line}");
                textboxHandler.ShowCentralText(line);
                yield return new WaitForSeconds(3); // 3秒間表示
            }

            textboxHandler.HideCentralText();
            textboxHandler.HideEndingBackgroundImage();
            textboxHandler.HideBlackBackground();
        }
    }
}
