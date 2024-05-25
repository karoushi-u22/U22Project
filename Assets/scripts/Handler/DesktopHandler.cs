using System.Collections.Generic;
using U22Game.Utilities;

namespace U22Game.Handlers
{
    public class DesktopHandler
    {
        /// <summary>
        /// 各種アイテムの有無
        /// </summary>
        public bool ExistsUsb { get; set; }
        public bool ExistsStickyNote { get; set; }
        public bool WasInstalledSoftware { get; set; }


        /// <summary>
        /// 不正なアイテムの有無
        /// </summary>
        public bool IsBadUsb { get; set; } = false;
        public bool IsBadStickyNote { get; set; } = false;
        public bool IsBadSoftware { get; set; } = false;


        /// <summary>
        /// 脆弱性の有無
        /// </summary>
        public bool IsVulnerable => IsBadUsb || IsBadStickyNote || IsBadSoftware;


        // チェックボックスの状態とテキストを保存する項目
        public Dictionary<string, bool> CheckboxStates { get; set; } = new();


        /// <summary>
        /// 各種アイテムが存在するかどうかをランダムに生成
        /// </summary>
        public DesktopHandler()
        {
            ExistsUsb = RandomUtil.GetRandomBool();
            ExistsStickyNote = RandomUtil.GetRandomBool();
            WasInstalledSoftware = RandomUtil.GetRandomBool();
        }


        /// <summary>
        /// 不正なアイテムをランダムに生成(すべてfalseになる場合もあります。)
        /// </summary>
        public void GenerateBadItems()
        {
            if (ExistsUsb)
            {
                IsBadUsb = RandomUtil.GetRandomBool();
            }
            IsBadStickyNote = ExistsStickyNote;
            if (WasInstalledSoftware)
            {
                IsBadSoftware = RandomUtil.GetRandomBool();
            }
        }


        // チェックボックスの状態を設定するメソッド
        public void SetCheckboxState(string checkboxName, bool isChecked)
        {
            if (CheckboxStates.ContainsKey(checkboxName))
            {
                CheckboxStates[checkboxName] = isChecked;
            }
            else
            {
                CheckboxStates.Add(checkboxName, isChecked);
            }
        }

        // チェックボックスの状態を取得するメソッド
        public bool GetCheckboxState(string checkboxName)
        {
            if (CheckboxStates.ContainsKey(checkboxName))
            {
                return CheckboxStates[checkboxName];
            }
            else
            {
                // チェックボックスの状態が保存されていない場合、デフォルト値として false を返す
                return false;
            }
        }

    }
}
