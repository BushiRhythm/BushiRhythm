/****************************************************************************
*
* CRI Middleware SDK
*
* Copyright (c) 2011-2012 CRI Middleware Co., Ltd.
*
* Library  : CRI Atom
* Module   : CRI Atom for Unity
* File     : CriAtomProjInfo_Unity.cs
* Tool Ver.          : CRI Atom Craft LE Ver.2.13.00
* Date Time          : 2015/06/28 20:27
* Project Name       : Kengou_ADX2
* Project Comment    : 
*
****************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class CriAtomAcfInfo
{
    static partial void GetCueInfoInternal()
    {
        acfInfo = new AcfInfo("ACF", 0, "", "Kengou_ADX2.acf","c78024af-5c2f-4556-bcbb-e8b2c98d5d23","DspBusSetting_0");
        acfInfo.aisacControlNameList.Add("Any");
        acfInfo.aisacControlNameList.Add("Distance");
        acfInfo.aisacControlNameList.Add("AisacControl02");
        acfInfo.aisacControlNameList.Add("AisacControl03");
        acfInfo.aisacControlNameList.Add("AisacControl04");
        acfInfo.aisacControlNameList.Add("AisacControl05");
        acfInfo.aisacControlNameList.Add("AisacControl06");
        acfInfo.aisacControlNameList.Add("AisacControl07");
        acfInfo.aisacControlNameList.Add("AisacControl08");
        acfInfo.aisacControlNameList.Add("AisacControl09");
        acfInfo.aisacControlNameList.Add("AisacControl10");
        acfInfo.aisacControlNameList.Add("AisacControl11");
        acfInfo.aisacControlNameList.Add("AisacControl12");
        acfInfo.aisacControlNameList.Add("AisacControl13");
        acfInfo.aisacControlNameList.Add("AisacControl14");
        acfInfo.aisacControlNameList.Add("AisacControl15");
        acfInfo.acbInfoList.Clear();
        AcbInfo newAcbInfo = null;
        newAcbInfo = new AcbInfo("CueSheet_0", 0, "", "CueSheet_0.acb", "CueSheet_0.awb","25ede1a5-64cb-4665-9198-51b6f187e3cb");
        acfInfo.acbInfoList.Add(newAcbInfo);
        newAcbInfo.cueInfoList.Add(0, new CueInfo("stage", 0, ""));
    }
}
