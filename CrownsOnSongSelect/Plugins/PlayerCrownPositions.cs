using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CrownsOnSongSelect.Plugins
{
    public enum CrownId
    {
        P1Oni,
        P1Ura,
        P2Oni,
        P2Ura,
    }

    public enum CrownIdPosition
    {
        P1OniSelected,
        P1OniUnselected,
        P1UraSelected,
        P1UraUnselected,
        P2OniSelected,
        P2OniUnselected,
        P2UraSelected,
        P2UraUnselected,
    }

    public struct CrownPosition
    {
        public Vector2 Position = Vector2.zero;
        public Vector2 Scale = Vector2.one;

        public CrownPosition(Vector2 position, Vector2 scale)
        {
            Position = position;
            Scale = scale;
        }
    }

    public struct CrownData
    {
        //DataConst.CrownType crown, EnsoData.EnsoLevelType level
        public int PlayerNum;
        public DataConst.CrownType Crown;
        public EnsoData.EnsoLevelType Level;
        public CrownId CrownId { get { return GetCrownId(); } }

        public CrownData(int playerNum, DataConst.CrownType crown, EnsoData.EnsoLevelType level)
        {
            PlayerNum = playerNum;
            Crown = crown;
            Level = level;
        }

        CrownId GetCrownId()
        {
            if (Level == EnsoData.EnsoLevelType.Ura)
            {
                if (PlayerNum == 0)
                {
                    return CrownId.P1Ura;
                }
                else
                {
                    return CrownId.P2Ura;
                }
            }
            else
            {
                if (PlayerNum == 0)
                {
                    return CrownId.P1Oni;
                }
                else
                {
                    return CrownId.P2Oni;
                }
            }
        }
    }

    internal class PlayerCrownPositions
    {
        public static CrownPosition GetCrownPosition(int playerNo, bool isUra, bool isSelected)
        {
            if (playerNo == 0)
            {
                if (!isUra)
                {
                    if (isSelected)
                    {
                        return GetCrownPosition(CrownIdPosition.P1OniSelected);

                    }
                    else
                    {
                        return GetCrownPosition(CrownIdPosition.P1OniUnselected);

                    }
                }
                else
                {
                    if (isSelected)
                    {
                        return GetCrownPosition(CrownIdPosition.P1UraSelected);

                    }
                    else
                    {
                        return GetCrownPosition(CrownIdPosition.P1UraUnselected);
                    }
                }
            }
            else if (playerNo == 1)
            {
                if (!isUra)
                {
                    if (isSelected)
                    {
                        return GetCrownPosition(CrownIdPosition.P2OniSelected);

                    }
                    else
                    {
                        return GetCrownPosition(CrownIdPosition.P2OniUnselected);

                    }
                }
                else
                {
                    if (isSelected)
                    {
                        return GetCrownPosition(CrownIdPosition.P2UraSelected);

                    }
                    else
                    {
                        return GetCrownPosition(CrownIdPosition.P2UraUnselected);
                    }
                }
            }
            else
            {
                return new CrownPosition(
                            new Vector2(0, 0),
                            new Vector2(1f, 1f));
            }
        }

        public static CrownPosition GetCrownPosition(CrownIdPosition crown)
        {
            //switch (crown)
            //{
            //    case CrownIdPosition.P1OniSelected:
            //        crown = CrownIdPosition.P2OniSelected;
            //        break;
            //    case CrownIdPosition.P1OniUnselected:
            //        crown = CrownIdPosition.P2OniUnselected;
            //        break;
            //    case CrownIdPosition.P1UraSelected:
            //        crown = CrownIdPosition.P2UraSelected;
            //        break;
            //    case CrownIdPosition.P1UraUnselected:
            //        crown = CrownIdPosition.P2UraUnselected;
            //        break;
            //    case CrownIdPosition.P2OniSelected:
            //        crown = CrownIdPosition.P1OniSelected;
            //        break;
            //    case CrownIdPosition.P2OniUnselected:
            //        crown = CrownIdPosition.P1OniUnselected;
            //        break;
            //    case CrownIdPosition.P2UraSelected:
            //        crown = CrownIdPosition.P1UraSelected;
            //        break;
            //    case CrownIdPosition.P2UraUnselected:
            //        crown = CrownIdPosition.P1UraUnselected;
            //        break;
            //    default:
            //        break;
            //}

            switch (crown)
            {
                case CrownIdPosition.P1OniSelected:
                    return new CrownPosition(
                            //new Vector2(-445, 110),
                            new Vector2(-500, 62),
                            new Vector2(1f, 1f));
                case CrownIdPosition.P1OniUnselected:
                    return new CrownPosition(
                            new Vector2(-407, 20),
                            new Vector2(0.8f, 0.8f));
                case CrownIdPosition.P1UraSelected:
                    return new CrownPosition(
                            //new Vector2(-445, 55),
                            new Vector2(-500, 7),
                            new Vector2(1f, 1f));
                case CrownIdPosition.P1UraUnselected:
                    return new CrownPosition(
                            new Vector2(-407, -20),
                            new Vector2(0.8f, 0.8f));
                case CrownIdPosition.P2OniSelected:
                    return new CrownPosition(
                            new Vector2(445, 110),
                            new Vector2(1f, 1f));
                case CrownIdPosition.P2OniUnselected:
                    return new CrownPosition(
                            new Vector2(407, 20),
                            new Vector2(0.8f, 0.8f));
                case CrownIdPosition.P2UraSelected:
                    return new CrownPosition(
                            new Vector2(445, 55),
                            new Vector2(1f, 1f));
                case CrownIdPosition.P2UraUnselected:
                    return new CrownPosition(
                            new Vector2(407, -20),
                            new Vector2(0.8f, 0.8f));
                default:
                    return new CrownPosition(
                            new Vector2(0, 0),
                            new Vector2(1f, 1f));
            }
        }

        public static CrownPosition GetCrownPosition(CrownId id, bool isSelected)
        {
            switch (id)
            {
                case CrownId.P1Oni: return isSelected ? GetCrownPosition(CrownIdPosition.P1OniSelected) : GetCrownPosition(CrownIdPosition.P1OniUnselected);
                case CrownId.P1Ura: return isSelected ? GetCrownPosition(CrownIdPosition.P1UraSelected) : GetCrownPosition(CrownIdPosition.P1UraUnselected);
                case CrownId.P2Oni: return isSelected ? GetCrownPosition(CrownIdPosition.P2OniSelected) : GetCrownPosition(CrownIdPosition.P2OniUnselected);
                case CrownId.P2Ura: return isSelected ? GetCrownPosition(CrownIdPosition.P2UraSelected) : GetCrownPosition(CrownIdPosition.P2UraUnselected);
            }

            return new CrownPosition(new Vector2(0, 0),
                                     new Vector2(1f, 1f));
        }
    }
}
