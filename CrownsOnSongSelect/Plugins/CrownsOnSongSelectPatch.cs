using HarmonyLib;
using Scripts.Common.Selector;
using Scripts.OutGame.Common;
using Scripts.OutGame.SongSelect;
using Scripts.UserData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static MusicDataInterface;

namespace CrownsOnSongSelect.Plugins
{
    internal class CrownsOnSongSelectPatch
    {
        static Dictionary<UiSongButton, ButtonCrownObject> UiSongButtonObjects = new Dictionary<UiSongButton, ButtonCrownObject>();
        static ButtonCrownObject UiSongCenterButtonObject;

        [HarmonyPatch(typeof(UiSongScroller))]
        [HarmonyPatch(nameof(UiSongScroller.Setup))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPostfix]
        public static void UiSongScroller_Setup_Postfix(UiSongScroller __instance)
        {
            ClearUiSongButtonDictionary();
            SpriteInitialization.InitializeCrownSprites(__instance.songSelectSprite);
            SpriteInitialization.InitializeDifficultySprites();
        }

        private static void ClearUiSongButtonDictionary()
        {
            foreach (var item in UiSongButtonObjects)
            {
                if (item.Key == null ||
                    item.Value == null)
                {
                    UiSongButtonObjects.Remove(item.Key);
                }
            }
        }

        [HarmonyPatch(typeof(UiSongCenterButton))]
        [HarmonyPatch(nameof(UiSongCenterButton.ShrinkAsync))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPrefix]
        public static bool UiSongCenterButton_ShrinkAsync_Prefix(UiSongCenterButton __instance)
        {
            if (UiSongCenterButtonObject != null)
            {
                UiSongCenterButtonObject.ShrinkCrowns();
            }
            return true;
        }

        [HarmonyPatch(typeof(UiSongCenterButton))]
        [HarmonyPatch(nameof(UiSongCenterButton.ExpandAsync))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPrefix]
        public static bool UiSongCenterButton_ExpandAsync_Prefix(UiSongCenterButton __instance)
        {
            if (UiSongCenterButtonObject != null)
            {
                UiSongCenterButtonObject.ExpandCrowns();
            }
            return true;
        }

        [HarmonyPatch(typeof(UiSongCenterButton))]
        [HarmonyPatch(nameof(UiSongCenterButton.Setup))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPostfix]
        public static void UiSongCenterButton_Setup_Postfix(UiSongCenterButton __instance, MusicDataInterface.MusicInfoAccesser item)
        {
            if (UiSongCenterButtonObject == null || !UiSongCenterButtonObject.IsInitialized())
            {
                UiSongCenterButtonObject = new ButtonCrownObject(__instance);
            }
            UiSongCenterButtonObject.ChangeCrowns(item);
        }

        [HarmonyPatch]
        public class UiSongButtonSetupPatch
        {
            static System.Reflection.MethodBase TargetMethod()
            {
                return typeof(UiSongButton).GetMethod(nameof(UiSongButton.Setup)).MakeGenericMethod(typeof(SongButton));
            }

            static void Postfix(UiSongButton __instance, SongButton model)
            {
                if (!UiSongButtonObjects.ContainsKey(__instance))
                {
                    UiSongButtonObjects.Add(__instance, new ButtonCrownObject(__instance));
                }
                UiSongButtonObjects[__instance].ChangeCrowns(model.Value);
            }
        }
    }
}
