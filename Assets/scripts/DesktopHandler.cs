
using System;
using U22Game.Utilities;

namespace U22Game.Handlers
{
    public class DesktopHandler
    {
        /// <summary>
        /// 各種アイテムの有無
        /// </summary>
        public bool existsUsb;
        public bool existsStickyNote;
        public bool wasInstalledSoftware;


        /// <summary>
        /// 不正なアイテムの有無
        /// </summary>
        public bool isBadUsb = false;
        public bool isBadStickyNote = false;
        public bool isBadSoftware = false;


        /// <summary>
        /// 脆弱性の有無
        /// </summary>
        public bool isVulnerable => isBadUsb || isBadStickyNote || isBadSoftware;


        /// <summary>
        /// 各種アイテムが存在するかどうかをランダムに生成
        /// </summary>
        public DesktopHandler()
        {
            existsUsb = RandomUtil.GetRandomBool();
            existsStickyNote = RandomUtil.GetRandomBool();
            wasInstalledSoftware = RandomUtil.GetRandomBool();
        }


        /// <summary>
        /// 不正なアイテムをランダムに生成(すべてfalseになる場合もあります。)
        /// </summary>
        public void GenerateBadItems()
        {
            if (existsUsb)
            {
                isBadUsb = RandomUtil.GetRandomBool();
            }
            if (existsStickyNote)
            {
                isBadStickyNote = RandomUtil.GetRandomBool();
            }
            if (wasInstalledSoftware)
            {
                isBadSoftware = RandomUtil.GetRandomBool();
            }
        }

    }
}
