using HarmonyLib;
using Scripts.OutGame.SongSelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static Scripts.OutGame.SongSelect.UiSongButton;

namespace CrownsOnSongSelect.Plugins
{
    class CrownsPatch
    {
        [HarmonyPatch(typeof(UiSongButton))]
        [HarmonyPatch(nameof(UiSongButton.ActivateCrownMania))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPrefix]
        public static bool UiSongButton_ActivateCrownMania_Prefix(UiSongButton __instance)
        {
            return false;
        }

        [HarmonyPatch(typeof(UiSongButton))]
        [HarmonyPatch(nameof(UiSongButton.ActivateCrownUra))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPrefix]
        public static bool UiSongButton_ActivateCrownUra_Prefix(UiSongButton __instance)
        {
            return false;
        }

        [HarmonyPatch(typeof(UiSongButton))]
        [HarmonyPatch(nameof(UiSongButton.CanStartUraSequence))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPrefix]
        public static bool UiSongButton_CanStartUraSequence_Prefix(UiSongButton __instance, ref bool __result)
        {
            __result = false;
            return false;
        }
        [HarmonyPatch(typeof(UiSongButton))]
        [HarmonyPatch(nameof(UiSongButton.StartUraSequenceAnimation))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPrefix]
        public static bool UiSongButton_StartUraSequenceAnimation_Prefix(UiSongButton __instance)
        {
            return false;
        }


        [HarmonyPatch(typeof(UiSongButton))]
        [HarmonyPatch(nameof(UiSongButton.SetCrown))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPrefix]
        public static void UiSongButton_SetCrown_Prefix(UiSongButton __instance)
        {
            SetCrownIcons(__instance);
        }

        public static void SetCrownIcons(UiSongButton __instance)
        {
            if (__instance.isRandom)
            {
                for (int i = 0; i < __instance.crowns.Length; i++)
                {
                    __instance.crowns[i].SetActiveSafe(false);
                }
                return;
            }

            Dictionary<CrownObjTypes, bool> enabledCrowns = new Dictionary<CrownObjTypes, bool>()
            {
                { CrownObjTypes.Easy, Plugin.Instance.ConfigEnableEasy.Value },
                { CrownObjTypes.Normal, Plugin.Instance.ConfigEnableNormal.Value },
                { CrownObjTypes.Hard, Plugin.Instance.ConfigEnableHard.Value },
                { CrownObjTypes.Mania, Plugin.Instance.ConfigEnableOni.Value },
                { CrownObjTypes.Ura, Plugin.Instance.ConfigEnableUra.Value },
            };

            var crownGroup = __instance.transform.FindChild("CrownGroup").gameObject;
            var layoutGroup = crownGroup.GetComponent<HorizontalLayoutGroup>();
            layoutGroup.enabled = false;

            Vector2 crownSize = new Vector2(28, 84);
            Vector2 curPosition = new Vector2(-467, 0);

            for (CrownObjTypes i = 0; i <= CrownObjTypes.Ura; i++)
            {
                var crown = __instance.crowns[(int)i];
                crown.SetActiveSafe(enabledCrowns[i]);
                if (enabledCrowns[i])
                {
                    crown.localPosition = curPosition;
                    crown.set_sizeDelta_Injected(ref crownSize);
                    curPosition.x += crownSize.x;
                }

                if (i == CrownObjTypes.Ura && !__instance.hasUra)
                {
                    crown.SetActiveSafe(false);
                }
            }

            __instance.KillSequence();
        }

    }

    [HarmonyPatch]
    public class UiSongButtonSetupPatch
    {
        static System.Reflection.MethodBase TargetMethod()
        {
            return typeof(UiSongButton).GetMethod(nameof(UiSongButton.Setup)).MakeGenericMethod(typeof(SongButton));
        }

        static void Postfix(UiSongButton __instance)
        {
            CrownsPatch.SetCrownIcons(__instance);
        }
    }
}
