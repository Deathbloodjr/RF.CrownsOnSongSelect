using Scripts.OutGame.Common;
using Scripts.OutGame.SongSelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CrownsOnSongSelect.Plugins
{
    internal class SpriteInitialization
    {
        public static Dictionary<DataConst.CrownType, Sprite> CrownSprites = new Dictionary<DataConst.CrownType, Sprite>();
        public static Dictionary<EnsoData.EnsoLevelType, Sprite> DifficultySprites = new Dictionary<EnsoData.EnsoLevelType, Sprite>();

        public static void InitializeCrownSprites(SongSelectSprite songSelectSprite)
        {
            if (songSelectSprite == null)
            {
                return;
            }

            ClearNullsFromCrownDictionary();

            if (CrownSprites.ContainsKey(DataConst.CrownType.Off) &&
                CrownSprites.ContainsKey(DataConst.CrownType.None) &&
                CrownSprites.ContainsKey(DataConst.CrownType.Silver) &&
                CrownSprites.ContainsKey(DataConst.CrownType.Gold) &&
                CrownSprites.ContainsKey(DataConst.CrownType.Rainbow))
            {
                return;
            }

            // idk if I'm even going to use the Off crown
            if (!CrownSprites.ContainsKey(DataConst.CrownType.Off) || CrownSprites[DataConst.CrownType.Off] == null)
            {
                CrownSprites.Add(DataConst.CrownType.Off, null);
                Plugin.Log.LogInfo("Sprite added for crown: " + DataConst.CrownType.Off.ToString());
            }
            if (!CrownSprites.ContainsKey(DataConst.CrownType.None) || CrownSprites[DataConst.CrownType.None] == null)
            {
                CrownSprites.Add(DataConst.CrownType.None, songSelectSprite.GetSprite("icon_crown_x"));
                Plugin.Log.LogInfo("Sprite added for crown: " + DataConst.CrownType.None.ToString());
            }
            if (!CrownSprites.ContainsKey(DataConst.CrownType.Silver) || CrownSprites[DataConst.CrownType.Silver] == null)
            {
                CrownSprites.Add(DataConst.CrownType.Silver, songSelectSprite.GetSprite("icon_crown_silver"));
                Plugin.Log.LogInfo("Sprite added for crown: " + DataConst.CrownType.Silver.ToString());
            }
            if (!CrownSprites.ContainsKey(DataConst.CrownType.Gold) || CrownSprites[DataConst.CrownType.Gold] == null)
            {
                CrownSprites.Add(DataConst.CrownType.Gold, songSelectSprite.GetSprite("icon_crown_gold"));
                Plugin.Log.LogInfo("Sprite added for crown: " + DataConst.CrownType.Gold.ToString());
            }
            if (!CrownSprites.ContainsKey(DataConst.CrownType.Rainbow) || CrownSprites[DataConst.CrownType.Rainbow] == null)
            {
                CrownSprites.Add(DataConst.CrownType.Rainbow, songSelectSprite.GetSprite("icon_crown_rainbow"));
                Plugin.Log.LogInfo("Sprite added for crown: " + DataConst.CrownType.Rainbow.ToString());
            }
        }

        
        public static void InitializeDifficultySprites()
        {
            ClearNullsFromDiffDictionary();

            // Check if everything's already initialized
            if (DifficultySprites.ContainsKey(EnsoData.EnsoLevelType.Easy) &&
                DifficultySprites.ContainsKey(EnsoData.EnsoLevelType.Normal) &&
                DifficultySprites.ContainsKey(EnsoData.EnsoLevelType.Hard) &&
                DifficultySprites.ContainsKey(EnsoData.EnsoLevelType.Mania) &&
                DifficultySprites.ContainsKey(EnsoData.EnsoLevelType.Ura))
            {
                return;
            }

            Dictionary<string, EnsoData.EnsoLevelType> GameObjectNameToEnsoLevel = new Dictionary<string, EnsoData.EnsoLevelType>()
            {
                { "ScoreComponent", EnsoData.EnsoLevelType.Easy },
                { "ScoreComponent (1)", EnsoData.EnsoLevelType.Normal },
                { "ScoreComponent (2)", EnsoData.EnsoLevelType.Hard },
                { "ScoreComponent (3)", EnsoData.EnsoLevelType.Mania },
                { "ScoreComponent (4)", EnsoData.EnsoLevelType.Ura },
            };

            GameObject obj = GameObject.Find("ScoreBoards");
            var children = obj.GetComponentsInChildren<DifficultyScorePanel>();
            for (int i = 0; i < children.Length; i++)
            {
                if (!DifficultySprites.ContainsKey(GameObjectNameToEnsoLevel[children[i].name]))
                {
                    var iconCourse = children[i].transform.FindChild("IconCourse");
                    if (iconCourse != null)
                    {
                        Sprite sprite = iconCourse.GetComponent<Image>().sprite;
                        DifficultySprites.Add(GameObjectNameToEnsoLevel[children[i].name], sprite);
                        Plugin.Log.LogInfo("Sprite added for difficulty: " + GameObjectNameToEnsoLevel[children[i].name].ToString());
                    }
                }
            }
        }

        private static void ClearNullsFromCrownDictionary()
        {
            foreach (var item in CrownSprites)
            {
                if (item.Value == null)
                {
                    CrownSprites.Remove(item.Key);
                }
            }
        }

        private static void ClearNullsFromDiffDictionary()
        {
            foreach (var item in DifficultySprites)
            {
                if (item.Value == null)
                {
                    DifficultySprites.Remove(item.Key);
                }
            }
        }
    }
}
