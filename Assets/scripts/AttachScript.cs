using UnityEngine;
using U22Game.Events;

namespace U22Game.Utilities{
    public class AttachScript : MonoBehaviour{
        [SerializeField] private string attachObjectName;
        [SerializeField] private int dayData;

        void Start()
        {
            AttachScriptToChildren(attachObjectName);
        }

        // 指定した名前で始まる子オブジェクトに別のスクリプトをアタッチする
        void AttachScriptToChildren(string objectName)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.name.StartsWith(objectName))
                {
                    // 子オブジェクトにスクリプトをアタッチ
                    child.gameObject.AddComponent<DesktopEvent>().dayData = dayData;
                }
            }
        }
    }
}
