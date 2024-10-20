using Cysharp.Threading.Tasks;
using HarmonyLib;
using Il2CppSystem.Collections.Generic;
using Scripts.Common.Scroller;
using Scripts.Common.Selector;
using Scripts.OutGame.Common;
using Scripts.OutGame.SongSelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static MusicDataInterface;
using static Scripts.OutGame.SongSelect.UiSongCenterButton;
using static Scripts.OutGame.Training.TrainingResultCrownSpriteTable;

namespace CrownsOnSongSelect.Plugins
{
    internal class EnableAllIconsForTesting
    {
        const bool SetIconsActive = true;


        [HarmonyPatch(typeof(UiSongScroller))]
        [HarmonyPatch(nameof(UiSongScroller.ScrollDown))]
        [HarmonyPatch(new Type[] { typeof(bool) })]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPostfix]
        public static void UiSongScroller_ScrollDown_Postfix(UiSongScroller __instance, bool isRepeatFast)
        {
            //Plugin.Log.LogInfo("UiSongScroller_ScrollDown_Postfix");

            EnableAllIcons(__instance.ActiveList);

            //Plugin.Log.LogInfo("");
        }

        [HarmonyPatch(typeof(UiSongScroller))]
        [HarmonyPatch(nameof(UiSongScroller.ScrollUp))]
        [HarmonyPatch(new Type[] { typeof(bool) })]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPostfix]
        public static void UiSongScroller_ScrollUp_Postfix(UiSongScroller __instance, bool isRepeatFast)
        {
            //Plugin.Log.LogInfo("UiSongScroller_ScrollUp_Postfix");

            EnableAllIcons(__instance.ActiveList);

            //Plugin.Log.LogInfo("");
        }

        public static void EnableAllIcons(Il2CppSystem.Collections.Generic.List<UiSongButton> songButtons)
        {
            if (SetIconsActive)
            {
                for (int i = 0; i < songButtons.Count; i++)
                {
                    if (songButtons[i] != null)
                    {
                        for (int j = 0; j < songButtons[i].icons.Length; j++)
                        {
                            if (songButtons[i].icons[j] != null)
                            {
                                songButtons[i].icons[j].active = true;
                            }
                        }
                    }
                }
            }
        }



        [HarmonyPatch(typeof(UiSongScroller))]
        [HarmonyPatch(nameof(UiSongScroller.Setup))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPostfix]
        public static void UiSongScroller_Setup_Postfix(UiSongScroller __instance)
        {
            if (SetIconsActive)
            {
                Plugin.Log.LogInfo("UiSongScroller_Setup_Postfix");

                for (int i = 0; i < __instance.centerButton.icons.datas.Count; i++)
                {
                    var data = __instance.centerButton.icons.datas[i];
                    data.Image.gameObject.SetActive(true);
                }

                Plugin.Log.LogInfo("");
            }
        }

        //static System.Collections.Generic.Dictionary<DataConst.CrownType, Sprite> CrownSprite = new System.Collections.Generic.Dictionary<DataConst.CrownType, Sprite>();

        [HarmonyPatch(typeof(UiSongCenterButton))]
        [HarmonyPatch(nameof(UiSongCenterButton.ExpandAsync))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPostfix]
        public static void UiSongCenterButton_ExpandAsync_Postfix(UiSongCenterButton __instance, UniTask __result)
        {
            Plugin.Log.LogInfo("UiSongCenterButton_ExpandAsync_Postfix");

            // This leads to a softlock
            //__result.GetAwaiter().OnCompleted(new Action(Test));

            for (int i = 0; i < __instance.icons.datas.Count; i++)
            {
                __instance.icons.datas[i].Image.gameObject.SetActive(true);
            }

            Plugin.Log.LogInfo("");
        }

        //[HarmonyPatch(typeof(UiSongCenterButtonSpriteChanger))]
        //[HarmonyPatch(nameof(UiSongCenterButtonSpriteChanger.UpdateIcon))]
        //[HarmonyPatch(MethodType.Normal)]
        //[HarmonyPrefix]
        //public static bool UiSongCenterButtonSpriteChanger_UpdateIcon_Postfix(UiSongCenterButtonSpriteChanger __instance)
        //{
        //    Plugin.Log.LogInfo("UiSongCenterButtonSpriteChanger_UpdateIcon_Postfix");

        //    // This leads to a softlock
        //    //__result.GetAwaiter().OnCompleted(new Action(Test));

        //    //for (int i = 0; i < __instance.datas.Count; i++)
        //    //{
        //    //    if (spriteSelect != null)
        //    //    {
        //    //        __instance.datas[i].Normal = spriteSelect.GetSprite("icon_crown_x");
        //    //        __instance.datas[i].Small = spriteSelect.GetSprite("icon_crown_x");
        //    //    }
        //    //}

        //    Plugin.Log.LogInfo("");
        //    return false;
        //}

        //[HarmonyPatch(typeof(UiSongCenterButtonSpriteChanger))]
        //[HarmonyPatch(nameof(UiSongCenterButtonSpriteChanger.ChangeToNormal))]
        //[HarmonyPatch(MethodType.Normal)]
        //[HarmonyPostfix]
        //public static void UiSongCenterButtonSpriteChanger_ChangeToNormal_Postfix(UiSongCenterButtonSpriteChanger __instance)
        //{
        //    Plugin.Log.LogInfo("UiSongCenterButtonSpriteChanger_ChangeToNormal_Postfix");

        //    Plugin.Log.LogInfo("");
        //}

        //[HarmonyPatch(typeof(UiSongCenterButtonSpriteChanger))]
        //[HarmonyPatch(nameof(UiSongCenterButtonSpriteChanger.ChangeToSmall))]
        //[HarmonyPatch(MethodType.Normal)]
        //[HarmonyPostfix]
        //public static void UiSongCenterButtonSpriteChanger_ChangeToSmall_Postfix(UiSongCenterButtonSpriteChanger __instance)
        //{
        //    Plugin.Log.LogInfo("UiSongCenterButtonSpriteChanger_ChangeToSmall_Postfix");

        //    // This leads to a softlock
        //    //__result.GetAwaiter().OnCompleted(new Action(Test));

        //    //for (int i = 0; i < __instance.datas.Count; i++)
        //    //{
        //    //    if (spriteSelect != null)
        //    //    {
        //    //        __instance.datas[i].Normal = spriteSelect.GetSprite("icon_crown_x");
        //    //        __instance.datas[i].Small = spriteSelect.GetSprite("icon_crown_x");
        //    //    }
        //    //}

        //    Plugin.Log.LogInfo("");
        //}
    }
}
