using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState {
    OpeningUI = 0,
    StartUI,
    StartChoiceUI,
    MainPlayingUI,
    GameEndUI,
    RankingUI,
    SettingUI,
    LoadingUI
}

public enum SceneState
{
    StartScene = 0,
    MainScene
}

public enum ChoiceState
{
    RunFriend = 0,
    NextRunFriend,
    BuffFriend
}

public enum ItemState
{
    ScoreItem = 0,
    HealItem,
    CoinItem,
    BonusItem,
    MagnetItem,
    BigItem,
    StageUpItem,
    GameEndItem
}
