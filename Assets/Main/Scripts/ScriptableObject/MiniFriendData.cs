using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MiniFriend_Type
{
    Doll = 0,
    Monster,
    Animal,
    Robot
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Main/MiniFriendData", order = 1)]
public class MiniFriendData : ScriptableObject
{
    [Header("메인룸")]
    public int id = 0;
    public Sprite sprite;
    public MiniFriend_Type type;
    public string friendName = "미니친구1";
    public GameObject prefab;
    public bool isHave = false;

    [Header("미니게임1")]
    public string buff_MiniGame1 = "";

    [Header("미니게임2")]
    public string buff_MiniGame2 = "";

    [Header("미니게임3")]
    public string buff_MiniGame3 = "";

    [Header("미니게임4")]
    public string buff_MiniGame4 = "";

    [Header("미니게임5")]
    public string buff_MiniGame5 = "";
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Main/MiniFriend_DataList", order = 2)]
public class MiniFriend_DataList : ScriptableObject
{
    [Header("메인룸")]
    public MiniFriend_Type type;

    public int[] id;
    public Sprite[] sprite;
    public string[] friendName;
    public GameObject[] prefab;

    public int Length { get => id.Length; }

    [Header("미니게임1")]
    public string[] buff_MiniGame1;

    [Header("미니게임2")]
    public string[] buff_MiniGame2;

    [Header("미니게임3")]
    public string[] buff_MiniGame3;

    [Header("미니게임4")]
    public string[] buff_MiniGame4;

    [Header("미니게임5")]
    public string[] buff_MiniGame5;
}