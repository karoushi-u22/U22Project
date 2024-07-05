using UnityEngine;

namespace U22Game.Handlers
{
    public class CharacterEventHandler : MonoBehaviour
    {
        public string jsonFile;

        private bool isColliding = false;
        private bool isCharaEvent = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && isColliding && !isCharaEvent)
            {
                isCharaEvent = true;

                Debug.Log("F");

                EventTextLoader.LoadEvent(jsonFile);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // 接触した際にフラグを立てる
            isColliding = true;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            // 接触が終了したらフラグを下げる
            isColliding = false;
        }
    }
}
