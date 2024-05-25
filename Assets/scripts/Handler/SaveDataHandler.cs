using System;
using System.Linq;
using System.Collections.Generic;

namespace U22Game.Handlers
{
    [Serializable]
    public class SaveDataHandler
    {
        public int CheckboxCnt { get; set; }
        public int SuccessCnt { get; set; }
        public int MissReportCnt { get; set; }
        public int MissCnt { get; set; }

        public Dictionary<int, DesktopDataHandler> SaveDatas { get; set; } = new();

        // 日付のデータにデスクトップデータを追加するメソッド
        public void AddDesktopData(int date, string desktopName)
        {
            // 日付のデータが存在しない場合は新しい DayData を作成
            if (!SaveDatas.ContainsKey(date))
            {
                SaveDatas[date] = new DesktopDataHandler();
            }

            // デスクトップデータを追加
            SaveDatas[date].AddDesktopData(desktopName);
        }

        // 日付のデータからデスクトップデータを削除するメソッド
        public void RemoveDesktopData(int date, string desktopName)
        {
            if (SaveDatas.ContainsKey(date))
            {
                SaveDatas[date].RemoveDesktopData(desktopName);
            }
        }

        // 指定した日付とデスクトップ名に対応するデスクトップデータを取得するメソッド
        public DesktopHandler GetDesktopData(int date, string desktopName)
        {
            if (SaveDatas.ContainsKey(date))
            {
                return SaveDatas[date].GetDesktopData(desktopName);
            }
            return null;
        }

        // すべてのデスクトップデータをクリアするメソッド
        public void ClearDesktopData()
        {
            foreach (var dayData in SaveDatas.Values)
            {
                dayData.ClearDesktopData();
            }
            SaveDatas.Clear();
        }

        // 指定した日付のすべてのデスクトップデータでチェックボックスの数を返すメソッド
        public int GetCheckboxCount(int date)
        {
            // 指定した日付のデータを取得
            DesktopDataHandler dayData = GetDayData(date);

            // データが存在しない場合は0を返す
            if (dayData == null)
            {
                return 0;
            }

            // チェックボックスの総数をカウントする変数
            int checkboxCount = 0;

            // すべてのデスクトップデータに対してチェックボックスの数をカウントする
            foreach (var desktopData in dayData.DesktopDatas.Values)
            {
                checkboxCount += desktopData.CheckboxStates.Count;
            }

            return checkboxCount;
        }

        // 指定した日付のすべてのデスクトップデータでチェックボックスの状態が一致した数を返すメソッド
        public int GetMatchingCheckboxStateCount(int date)
        {
            // 指定した日付のデータを取得
            DesktopDataHandler dayData = GetDayData(date);

            // データが存在しない場合は0を返す
            if (dayData == null)
            {
                return 0;
            }

            // 一致した数をカウントする変数
            int matchingCount = 0;

            // すべてのデスクトップデータに対してチェックボックスの状態を調べる
            foreach (var desktopData in dayData.DesktopDatas.Values)
            {
                // チェックボックスの状態が保存された配列を取得
                bool[] checkboxStates = desktopData.CheckboxStates.Values.ToArray();

                // チェックボックスの状態と不正なアイテムの状態を比較し、一致している数をカウントする
                if (checkboxStates.Length > 0 && checkboxStates[0] == desktopData.IsBadUsb)
                {
                    matchingCount++;
                }
                if (checkboxStates.Length > 1 && checkboxStates[1] == desktopData.IsBadStickyNote)
                {
                    matchingCount++;
                }
                if (checkboxStates.Length > 2 && checkboxStates[2] == desktopData.IsBadSoftware)
                {
                    matchingCount++;
                }
            }

            return matchingCount;
        }

        // DayData クラス内にメソッドを追加
        public int GetUncheckedBadItemsCount(int date)
        {
            // 指定した日付のデスクトップデータを取得
            DesktopDataHandler dayData = GetDayData(date);

            // データが存在しない場合は0を返す
            if (dayData == null)
            {
                return 0;
            }

            // 不正なアイテムがあるかどうかを示すフラグ
            bool hasBadItems = false;

            // 不正なアイテムがあるかどうかを確認
            foreach (var desktopData in dayData.DesktopDatas.Values)
            {
                if (desktopData.IsBadUsb || desktopData.IsBadStickyNote || desktopData.IsBadSoftware)
                {
                    hasBadItems = true;
                    break;
                }
            }

            // 不正なアイテムがない場合は0を返す
            if (!hasBadItems)
            {
                return 0;
            }

            // 不正なアイテムがある場合、対応するチェックボックスにチェックされていない数をカウントする
            int uncheckedBadItemsCount = 0;

            foreach (var desktopData in dayData.DesktopDatas.Values)
            {
                // チェックボックスの状態が保存された配列を取得
                bool[] checkboxStates = desktopData.CheckboxStates.Values.ToArray();

                // チェックボックスの状態と不正なアイテムの状態を比較し、対応するチェックボックスにチェックされていない場合にカウントする
                if (checkboxStates.Length > 0 && !checkboxStates[0] && desktopData.IsBadUsb)
                {
                    uncheckedBadItemsCount++;
                }
                if (checkboxStates.Length > 1 && !checkboxStates[1] && desktopData.IsBadStickyNote)
                {
                    uncheckedBadItemsCount++;
                }
                if (checkboxStates.Length > 2 && !checkboxStates[2] && desktopData.IsBadSoftware)
                {
                    uncheckedBadItemsCount++;
                }
            }

            return uncheckedBadItemsCount;
        }

        // DayData クラス内にメソッドを追加
        public int GetMisscheckedItemsCount(int date)
        {
            // 指定した日付のデスクトップデータを取得
            DesktopDataHandler dayData = GetDayData(date);

            // データが存在しない場合は0を返す
            if (dayData == null)
            {
                return 0;
            }

            // 不正なアイテムがないかどうかを示すフラグ
            bool hasNoBadItems = true;

            // 不正なアイテムがあるかどうかを確認
            foreach (var desktopData in dayData.DesktopDatas.Values)
            {
                if (desktopData.IsBadUsb || desktopData.IsBadStickyNote || desktopData.IsBadSoftware)
                {
                    hasNoBadItems = false;
                    break;
                }
            }

            // 不正なアイテムがある場合は0を返す
            if (hasNoBadItems)
            {
                return 0;
            }

            // 不正なアイテムがない場合、対応するチェックボックスにチェックされている数をカウントする
            int misscheckedItemsCount = 0;

            foreach (var desktopData in dayData.DesktopDatas.Values)
            {
                // チェックボックスの状態が保存された配列を取得
                bool[] checkboxStates = desktopData.CheckboxStates.Values.ToArray();

                // チェックボックスの状態と不正なアイテムの状態を比較し、対応するチェックボックスにチェックされている場合にカウントする
                if (checkboxStates.Length > 0 && checkboxStates[0] && !desktopData.IsBadUsb)
                {
                    misscheckedItemsCount++;
                }
                if (checkboxStates.Length > 0 && checkboxStates[1] && !desktopData.IsBadStickyNote)
                {
                    misscheckedItemsCount++;
                }
                if (checkboxStates.Length > 0 && checkboxStates[2] && !desktopData.IsBadSoftware)
                {
                    misscheckedItemsCount++;
                }
            }

            return misscheckedItemsCount;
        }

        // 指定した日付に対応するデータを取得するメソッド
        public DesktopDataHandler GetDayData(int date)
        {
            // 指定された日付のデータを取得する
            if (SaveDatas.ContainsKey(date))
            {
                return SaveDatas[date];
            }
            else
            {
                return null; // データが存在しない場合は null を返す
            }
        }
    }
}
