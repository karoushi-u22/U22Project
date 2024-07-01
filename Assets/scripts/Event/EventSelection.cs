using System;
using System.Collections.Generic;

namespace U22Game.Events
{
    [Serializable]
    public class EventSelection
    {
        public List<PlayerSelection> playerSelections { get; set; } = new();

        public class PlayerSelection
        {
            public string title { get; set; }
            public Body body { get; set; } = new();

            public class Body
            {
                public List<Selection> selections { get; set; } = new();

                public class Selection
                {
                    public string title { get; set; }           // 選択肢のタイトル
                    public bool playerSelected { get; set; }  // プレイヤーがこの選択肢を選んだか
                }
            }
        }
    }
}
