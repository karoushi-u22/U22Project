using UnityEngine;
using U22Game.Handlers;

public class DataManager : MonoBehaviour
{
    public SaveData saveData; // SaveData インスタンスを保持する変数

    void Start()
    {
        // 初期化された SaveData インスタンスを生成
        saveData = new SaveData();

        // Day1 のデータを生成する
        GenerateDayData(1, 6); // 1日目に6台のPCを生成

        // Day2 のデータを生成する
        GenerateDayData(2, 6); // 2日目に6台のPCを生成
    }

    // 指定された日付に指定された数のデスクトップデータを生成するメソッド
    void GenerateDayData(int date, int desktopCount)
    {
        for (int i = 1; i <= desktopCount; i++)
        {
            string desktopName = "PC" + i; // PC の名前を生成
            saveData.AddDesktopData(date, desktopName); // 生成した PC を SaveData に追加
            DesktopData desktopData = saveData.GetDesktopData(date, desktopName); // 追加した PC の DesktopData インスタンスを取得
            desktopData.GenerateBadItems(); // 不正なアイテムをランダムに生成
        }
    }
}
