using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using U22Game.Handlers;

namespace U22Game.Controllers
{
    public class EndingTextManager : MonoBehaviour
    {
        [SerializeField] private TextboxHandler textboxHandler;
        [SerializeField] private GameObject endingBlackImage;  // EndingBlackImage
        [SerializeField] private TextMeshProUGUI endingTextCenter;  // EndingTextCenter
        [SerializeField] private Canvas textboxCanvas;  // Textbox Canvas

        // 新しいフィールドの追加
        [SerializeField] private GameObject textboxFlame;  // TextboxFlame
        [SerializeField] private GameObject textfieldMain;  // TextfieldMain
        [SerializeField] private GameObject textSkip;  // TextSkip
        [SerializeField] private GameObject textfieldPlayerName;  // TextfieldPlayerName
        
        [SerializeField] private float displayDuration = 3f;

        private void Start()
        {
            if (textboxHandler == null)
            {
                Debug.LogError("EndingTextManager: TextboxHandler not assigned.");
            }
            if (endingBlackImage == null)
            {
                Debug.LogError("EndingTextManager: EndingBlackImage not assigned.");
            }
            if (endingTextCenter == null)
            {
                Debug.LogError("EndingTextManager: EndingTextCenter not assigned.");
            }
            if (textboxCanvas == null)
            {
                Debug.LogError("EndingTextManager: Textbox Canvas not assigned.");
            }

            HideEndingTextCenter();
            HideEndingBlackImage();
            HideTextbox();  // テキストボックスを非表示にする
            HideTextboxFlame();  // TextboxFlame を非表示にする
            HideTextfieldMain();  // TextfieldMain を非表示にする
            HideTextSkip();  // TextSkip を非表示にする
            HideTextfieldPlayerName();  // TextfieldPlayerName を非表示にする
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Alpha1 key pressed - Showing Betrayal Ending");
                StartCoroutine(ShowBetrayalEnding());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("Alpha2 key pressed - Showing Bankruptcy Ending");
                StartCoroutine(ShowBankruptcyEnding());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("Alpha3 key pressed - Showing Employment Ending");
                StartCoroutine(ShowEmploymentEnding());
            }
        }

        private void ShowEndingBlackImage()
        {
            endingBlackImage.SetActive(true);
            Debug.Log("EndingBlackImage set to active.");
        }

        private void HideEndingBlackImage()
        {
            endingBlackImage.SetActive(false);
            Debug.Log("EndingBlackImage set to inactive.");
        }

        private void ShowEndingTextCenter()
        {
            endingTextCenter.gameObject.SetActive(true);
            Debug.Log("EndingTextCenter set to active.");
        }

        private void HideEndingTextCenter()
        {
            endingTextCenter.gameObject.SetActive(false);
            Debug.Log("EndingTextCenter set to inactive.");
        }

        private void ShowTextbox()
        {
            textboxHandler.ShowTextbox();  // TextboxHandler のメソッドを使用
            Debug.Log("Textbox displayed.");
        }

        private void HideTextbox()
        {
            textboxHandler.HideTextbox();  // TextboxHandler のメソッドを使用
            Debug.Log("Textbox hidden.");
        }

        private void ShowTextboxFlame()
        {
            textboxFlame.SetActive(true);
            Debug.Log("TextboxFlame set to active.");
        }

        private void HideTextboxFlame()
        {
            textboxFlame.SetActive(false);
            Debug.Log("TextboxFlame set to inactive.");
        }

        private void ShowTextfieldMain()
        {
            textfieldMain.SetActive(true);
            Debug.Log("TextfieldMain set to active.");
        }

        private void HideTextfieldMain()
        {
            textfieldMain.SetActive(false);
            Debug.Log("TextfieldMain set to inactive.");
        }

        private void ShowTextSkip()
        {
            textSkip.SetActive(true);
            Debug.Log("TextSkip set to active.");
        }

        private void HideTextSkip()
        {
            textSkip.SetActive(false);
            Debug.Log("TextSkip set to inactive.");
        }

        private void ShowTextfieldPlayerName()
        {
            textfieldPlayerName.SetActive(true);
            Debug.Log("TextfieldPlayerName set to active.");
        }

        private void HideTextfieldPlayerName()
        {
            textfieldPlayerName.SetActive(false);
            Debug.Log("TextfieldPlayerName set to inactive.");
        }
        private void LogUIPosition()
        {
            Debug.Log($"EndingBlackImage position: {endingBlackImage.transform.position}");
            Debug.Log($"EndingTextCenter position: {endingTextCenter.transform.position}");
        }

        public IEnumerator ShowBetrayalEnding()
        {
            ShowTextbox();  // Textbox UI を再表示する
            ShowEndingBlackImage(); // EndingBlackImage を表示する
            ShowEndingTextCenter(); // EndingTextCenter を表示する
            
            HideTextboxFlame();
            HideTextfieldMain();
            HideTextSkip();
            HideTextfieldPlayerName();

            LogUIPosition(); // UI 要素の位置をログに出力

            endingTextCenter.text = "その後、新聞の一面にはEHcopeの情報漏洩についての記事が掲載されていた。";
            Debug.Log("Betrayal Ending - Displaying Text: その後、新聞の一面にはEHcopeの情報漏洩についての記事が掲載されていた。");
            yield return new WaitForSeconds(displayDuration * 1.5f);

            endingTextCenter.text = "情報漏洩により会社の信用はガタ落ちし、ニュースキャスターが情報漏洩について説明していた。";
            Debug.Log("Betrayal Ending - Displaying Text: 情報漏洩により会社の信用はガタ落ちし、ニュースキャスターが情報漏洩について説明していた。");
            yield return new WaitForSeconds(displayDuration * 1.5f);

            endingTextCenter.text = "自分がいた会社が今わの際というのに、どこか他人事のように感じてしまう…。";
            Debug.Log("Betrayal Ending - Displaying Text: 自分がいた会社が今わの際というのに、どこか他人事のように感じてしまう…。");
            yield return new WaitForSeconds(displayDuration * 1.5f);

            endingTextCenter.text = "ひとたび家から出ると、周りの人々は好きな音楽を聴きながら歩き、会社に着くとOLたちは芸能人のエンタメ、上司は野球の話題で熱中している。";
            Debug.Log("Betrayal Ending - Displaying Text: ひとたび家から出ると、周りの人々は好きな音楽を聴きながら歩き、会社に着くとOLたちは芸能人のエンタメ、上司は野球の話題で熱中している。");
            yield return new WaitForSeconds(displayDuration * 1.5f);

            endingTextCenter.text = "岡田さんのおかげで、それなりの地位と給料はもらえているが…";
            Debug.Log("Betrayal Ending - Displaying Text: 岡田さんのおかげで、それなりの地位と給料はもらえているが…");
            yield return new WaitForSeconds(displayDuration * 1.5f);

            endingTextCenter.text = "僕はこれでよかったんだろうか。";
            Debug.Log("Betrayal Ending - Displaying Text: 僕はこれでよかったんだろうか。");
            yield return new WaitForSeconds(displayDuration * 1.5f);
            
            HideEndingTextCenter();  // EndingTextCenter を非表示にする
            ShowTextboxFlame();
            ShowTextfieldMain();
            ShowTextSkip();
            ShowTextfieldPlayerName();

            // TextboxHandler でテキストを設定する
            textboxHandler.SetTextPlayerName("???");
            textboxHandler.SetTextMain("社員くん", 2);
            yield return new WaitForSeconds(displayDuration * 1.5f);

            textboxHandler.SetTextPlayerName("社員");
            textboxHandler.SetTextMain("はいなんでしょう。", 2);
            yield return new WaitForSeconds(displayDuration * 1.5f);

            textboxHandler.SetTextPlayerName("???");
            textboxHandler.SetTextMain("君は自分がもっと評価されるべきだと感じないか？", 2);
            yield return new WaitForSeconds(displayDuration * 1.5f);

            HideTextboxFlame();
            HideTextfieldMain();
            HideTextSkip();
            HideTextfieldPlayerName();

            ShowEndingTextCenter(); // EndingTextCenter を表示する
            endingTextCenter.text = " 裏切りＥＮＤ ";
            yield return new WaitForSeconds(displayDuration);

            
        }

        public IEnumerator ShowBankruptcyEnding()
        {
            ShowTextbox();  // Textbox UI を再表示する
            ShowEndingBlackImage(); // EndingBlackImage を表示する
            ShowEndingTextCenter(); // EndingTextCenter を表示する

            HideTextboxFlame();
            HideTextfieldMain();
            HideTextSkip();
            HideTextfieldPlayerName();

            LogUIPosition(); // UI 要素の位置をログに出力

        
            endingTextCenter.text = "その後、新聞の一面にはEHcopeのセキュリティ問題についての記事が掲載されており、会社がほぼ倒産に追い込まれていることが報じられ";
            yield return new WaitForSeconds(displayDuration);

            endingTextCenter.text = "朝のニュースでは、ニュースキャスターと専門家が身近なセキュリティについて説明していた。";
            yield return new WaitForSeconds(displayDuration);

            endingTextCenter.text = "クビになった自分は気分転換がてらハンバーガーでも食べに行こうかと町へ繰り出し";
            yield return new WaitForSeconds(displayDuration);

            endingTextCenter.text = "注文を済ませて席に座り、ふと辺りを見回すと、会社員が重要そうな書類を広げていたり、PCの電源を付けたまま追加注文をしようと席に立つ者もいた。";
            yield return new WaitForSeconds(displayDuration);

            endingTextCenter.text = "昨日までの自分ならPADで即座に報告をしていただろうけど";
            yield return new WaitForSeconds(displayDuration);

            endingTextCenter.text = "今となっては関係ない話だ…";
            yield return new WaitForSeconds(displayDuration);

            endingTextCenter.text = "セキュリティって意外と身近なもんだったんだなと今更ながら痛感する。";
            yield return new WaitForSeconds(displayDuration);

            endingTextCenter.text = " 会社倒産ＥＮＤ ";
            yield return new WaitForSeconds(displayDuration);
        }

        public IEnumerator ShowEmploymentEnding()
        {
            ShowEndingBlackImage(); // EndingBlackImage を表示する
            HideEndingTextCenter(); // EndingTextCenter を非表示する

            ShowTextboxFlame();
            ShowTextfieldMain();
            ShowTextSkip();
            ShowTextfieldPlayerName();

            LogUIPosition(); // UI 要素の位置をログに出力


            textboxHandler.SetTextPlayerName("古川正雄");  // プレイヤー名を適切に設定
            textboxHandler.SetTextMain("来てくれたか", 2);
            yield return new WaitForSeconds(displayDuration * 1.5f);

            textboxHandler.SetTextPlayerName("古川正雄");  // プレイヤー名を適切に設定
            textboxHandler.SetTextMain("今日は君について話がある", 2);
            yield return new WaitForSeconds(displayDuration * 1.5f);

            textboxHandler.SetTextPlayerName("古川正雄");  // プレイヤー名を適切に設定
            textboxHandler.SetTextMain("今までの君の報告を評価した結果", 2);
            yield return new WaitForSeconds(displayDuration * 1.5f);

            textboxHandler.SetTextPlayerName("古川正雄");  // プレイヤー名を適切に設定
            textboxHandler.SetTextMain("おめでとう", 2);
            yield return new WaitForSeconds(displayDuration * 1.5f);

            textboxHandler.SetTextPlayerName("古川正雄");  // プレイヤー名を適切に設定
            textboxHandler.SetTextMain("今日から君はEHcopeの社員だ", 2);
            yield return new WaitForSeconds(displayDuration * 1.5f);
    
            HideTextboxFlame();
            HideTextfieldMain();
            HideTextSkip();
            HideTextfieldPlayerName();

            ShowEndingTextCenter(); // EndingTextCenter を表示する
            endingTextCenter.text = " 就職ＥＮＤ ";
            yield return new WaitForSeconds(displayDuration);


        }
    }
}
