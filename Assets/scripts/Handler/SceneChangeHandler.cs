using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace U22Game.Handlers
{
    public class SceneChangeHandler : MonoBehaviour
    {
        // 移動するまでの待機時間（秒）
        [SerializeField] private float delay = 5f;
        // 移動先のシーン名
        [SerializeField] private string sceneName;

        void Start()
        {
            // 指定した時間後にシーンを移動するコルーチンを開始
            StartCoroutine(ChangeSceneAfterDelay());
        }

        IEnumerator ChangeSceneAfterDelay()
        {
            // 指定した時間待機
            yield return new WaitForSeconds(delay);
            // シーンの移動
            SceneManager.LoadScene(sceneName);
        }
    }
}
