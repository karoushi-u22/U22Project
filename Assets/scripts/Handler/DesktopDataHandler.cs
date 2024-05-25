using System.Linq;
using System.Collections.Generic;

namespace U22Game.Handlers
{
    public class DesktopDataHandler
    {
        public Dictionary<string, DesktopHandler> DesktopDatas { get; set; } = new();

        // デスクトップデータを追加するメソッド
        public void AddDesktopData(string desktopName)
        {
            // デスクトップ名をキーとして新しい DesktopData を作成
            if (!DesktopDatas.ContainsKey(desktopName))
            {
                DesktopDatas[desktopName] = new DesktopHandler();
            }
        }

        // 指定したデスクトップ名のデスクトップデータを削除するメソッド
        public void RemoveDesktopData(string desktopName)
        {
            if (DesktopDatas.ContainsKey(desktopName))
            {
                DesktopDatas.Remove(desktopName);
            }
        }

        // 指定したデスクトップ名に対応するデスクトップデータを取得するメソッド
        public DesktopHandler GetDesktopData(string desktopName)
        {
            if (DesktopDatas.ContainsKey(desktopName))
            {
                return DesktopDatas[desktopName];
            }
            return null;
        }

        // すべてのデスクトップデータをクリアするメソッド
        public void ClearDesktopData()
        {
            DesktopDatas.Clear();
        }

        // チェックボックスの数を返すメソッド
        public int GetCheckboxCount(string desktopName)
        {
            // 指定したデスクトップ名のデスクトップデータを取得
            DesktopHandler desktopData = GetDesktopData(desktopName);

            // デスクトップデータが存在しない場合は0を返す
            if (desktopData == null)
            {
                return 0;
            }

            // チェックボックスの数を返す
            return desktopData.CheckboxStates.Count;
        }

        // チェックボックスの状態が一致した数を返すメソッド
        public int GetMatchingCheckboxStateCount(string desktopName)
        {
            // 指定したデスクトップ名のデスクトップデータを取得
            DesktopHandler desktopData = GetDesktopData(desktopName);

            // デスクトップデータが存在しない場合は0を返す
            if (desktopData == null)
            {
                return 0;
            }

            // チェックボックスの状態が保存された配列を取得
            bool[] checkboxStates = desktopData.CheckboxStates.Values.ToArray();

            // チェックボックスの状態と不正なアイテムの状態を比較し、一致している数をカウントする
            int matchingCount = 0;
            if (checkboxStates.Length > 0)
            {
                if (checkboxStates[0] == desktopData.IsBadUsb)
                {
                    matchingCount++;
                }
            }
            if (checkboxStates.Length > 1)
            {
                if (checkboxStates[1] == desktopData.IsBadStickyNote)
                {
                    matchingCount++;
                }
            }
            if (checkboxStates.Length > 2)
            {
                if (checkboxStates[2] == desktopData.IsBadSoftware)
                {
                    matchingCount++;
                }
            }

            return matchingCount;
        }
    }
}
