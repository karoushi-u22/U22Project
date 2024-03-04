using System;
using System.Linq;
using System.Collections.Generic;

namespace U22Game.Handlers
{
    [Serializable]
    public class SaveData
    {
        public Dictionary<int, DayData> daysData = new();

        // 日付のデータにデスクトップデータを追加するメソッド
        public void AddDesktopData(int date, string desktopName)
        {
            // 日付のデータが存在しない場合は新しい DayData を作成
            if (!daysData.ContainsKey(date))
            {
                daysData[date] = new DayData();
            }

            // デスクトップデータを追加
            daysData[date].AddDesktopData(desktopName);
        }

        // 日付のデータからデスクトップデータを削除するメソッド
        public void RemoveDesktopData(int date, string desktopName)
        {
            if (daysData.ContainsKey(date))
            {
                daysData[date].RemoveDesktopData(desktopName);
            }
        }

        // 指定した日付とデスクトップ名に対応するデスクトップデータを取得するメソッド
        public DesktopData GetDesktopData(int date, string desktopName)
        {
            if (daysData.ContainsKey(date))
            {
                return daysData[date].GetDesktopData(desktopName);
            }
            return null;
        }

        // すべてのデスクトップデータをクリアするメソッド
        public void ClearDesktopData()
        {
            foreach (var dayData in daysData.Values)
            {
                dayData.ClearDesktopData();
            }
            daysData.Clear();
        }

        // 指定した日付のすべてのデスクトップデータでチェックボックスの状態が一致した数を返すメソッド
        public int GetMatchingCheckboxStateCount(int date)
        {
            // 指定した日付のデータを取得
            DayData dayData = GetDayData(date);

            // データが存在しない場合は0を返す
            if (dayData == null)
            {
                return 0;
            }

            // 一致した数をカウントする変数
            int matchingCount = 0;

            // すべてのデスクトップデータに対してチェックボックスの状態を調べる
            foreach (var desktopData in dayData.desktopsData.Values)
            {
                // チェックボックスの状態が保存された配列を取得
                bool[] checkboxStates = desktopData.checkboxStates.Values.ToArray();

                // チェックボックスの状態と不正なアイテムの状態を比較し、一致している数をカウントする
                if (checkboxStates.Length > 0 && checkboxStates[0] == desktopData.IsBadUsb())
                {
                    matchingCount++;
                }
                if (checkboxStates.Length > 1 && checkboxStates[1] == desktopData.IsBadStickyNote())
                {
                    matchingCount++;
                }
                if (checkboxStates.Length > 2 && checkboxStates[2] == desktopData.IsBadSoftware())
                {
                    matchingCount++;
                }
            }

            return matchingCount;
        }

        // 指定した日付に対応するデータを取得するメソッド
        public DayData GetDayData(int date)
        {
            // 指定された日付のデータを取得する
            if (daysData.ContainsKey(date))
            {
                return daysData[date];
            }
            else
            {
                return null; // データが存在しない場合は null を返す
            }
        }
    }

    [Serializable]
    public class DayData
    {
        public Dictionary<string, DesktopData> desktopsData = new();

        // デスクトップデータを追加するメソッド
        public void AddDesktopData(string desktopName)
        {
            // デスクトップ名をキーとして新しい DesktopData を作成
            if (!desktopsData.ContainsKey(desktopName))
            {
                desktopsData[desktopName] = new DesktopData();
            }
        }

        // 指定したデスクトップ名のデスクトップデータを削除するメソッド
        public void RemoveDesktopData(string desktopName)
        {
            if (desktopsData.ContainsKey(desktopName))
            {
                desktopsData.Remove(desktopName);
            }
        }

        // 指定したデスクトップ名に対応するデスクトップデータを取得するメソッド
        public DesktopData GetDesktopData(string desktopName)
        {
            if (desktopsData.ContainsKey(desktopName))
            {
                return desktopsData[desktopName];
            }
            return null;
        }

        // すべてのデスクトップデータをクリアするメソッド
        public void ClearDesktopData()
        {
            desktopsData.Clear();
        }

        // チェックボックスの状態が一致した数を返すメソッド
        public int GetMatchingCheckboxStateCount(string desktopName)
        {
            // 指定したデスクトップ名のデスクトップデータを取得
            DesktopData desktopData = GetDesktopData(desktopName);

            // デスクトップデータが存在しない場合は0を返す
            if (desktopData == null)
            {
                return 0;
            }

            // チェックボックスの状態が保存された配列を取得
            bool[] checkboxStates = desktopData.checkboxStates.Values.ToArray();

            // チェックボックスの状態と不正なアイテムの状態を比較し、一致している数をカウントする
            int matchingCount = 0;
            if (checkboxStates.Length > 0)
            {
                if (checkboxStates[0] == desktopData.IsBadUsb())
                {
                    matchingCount++;
                }
            }
            if (checkboxStates.Length > 1)
            {
                if (checkboxStates[1] == desktopData.IsBadStickyNote())
                {
                    matchingCount++;
                }
            }
            if (checkboxStates.Length > 2)
            {
                if (checkboxStates[2] == desktopData.IsBadSoftware())
                {
                    matchingCount++;
                }
            }

            return matchingCount;
        }
    }

    [Serializable]
    public class DesktopData
    {
        private DesktopHandler desktopHandler;

        // チェックボックスの状態とテキストを保存する項目
        public Dictionary<string, bool> checkboxStates = new();

        public DesktopData()
        {
            desktopHandler = new DesktopHandler();
        }

        // チェックボックスの状態を設定するメソッド
        public void SetCheckboxState(string checkboxName, bool isChecked)
        {
            if (checkboxStates.ContainsKey(checkboxName))
            {
                checkboxStates[checkboxName] = isChecked;
            }
            else
            {
                checkboxStates.Add(checkboxName, isChecked);
            }
        }

        // チェックボックスの状態を取得するメソッド
        public bool GetCheckboxState(string checkboxName)
        {
            if (checkboxStates.ContainsKey(checkboxName))
            {
                return checkboxStates[checkboxName];
            }
            else
            {
                // チェックボックスの状態が保存されていない場合、デフォルト値として false を返す
                return false;
            }
        }

        // 各種アイテムの有無を取得するメソッド
        public bool HasUsb()
        {
            return desktopHandler.existsUsb;
        }

        public bool HasStickyNote()
        {
            return desktopHandler.existsStickyNote;
        }

        public bool HasInstalledSoftware()
        {
            return desktopHandler.wasInstalledSoftware;
        }

        // 不正なアイテムの有無を取得するメソッド
        public bool IsBadUsb()
        {
            return desktopHandler.isBadUsb;
        }

        public bool IsBadStickyNote()
        {
            return desktopHandler.isBadStickyNote;
        }

        public bool IsBadSoftware()
        {
            return desktopHandler.isBadSoftware;
        }

        // 脆弱性の有無を取得するメソッド
        public bool IsVulnerable()
        {
            return desktopHandler.isVulnerable;
        }

        // 不正なアイテムをランダムに生成するメソッド
        public void GenerateBadItems()
        {
            desktopHandler.GenerateBadItems();
        }
    }
}
