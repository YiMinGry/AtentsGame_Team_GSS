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
    [Header("���η�")]
    public int id = 0;
    public Sprite sprite;
    public MiniFriend_Type type;
    public string friendName = "�̴�ģ��1";
    public GameObject prefab;
    public bool isHave = false;

    [Header("�̴ϰ���1")]
    public string buff_MiniGame1 = "";

    [Header("�̴ϰ���2")]
    public string buff_MiniGame2 = "";

    [Header("�̴ϰ���3")]
    public string buff_MiniGame3 = "";

    [Header("�̴ϰ���4")]
    public string buff_MiniGame4 = "";

    [Header("�̴ϰ���5")]
    public string buff_MiniGame5 = "";
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Main/MiniFriend_DataList", order = 2)]
public class MiniFriend_DataList : ScriptableObject
{
    [Header("���η�")]
    public MiniFriend_Type type;

    public int[] id;
    public Sprite[] sprite;
    public string[] friendName;
    public GameObject[] prefab;

    public int Length { get => id.Length; }

    [Header("�̴ϰ���1")]
    public string[] buff_MiniGame1;

    [Header("�̴ϰ���2")]
    public string[] buff_MiniGame2;

    [Header("�̴ϰ���3")]
    public string[] buff_MiniGame3;

    [Header("�̴ϰ���4")]
    public string[] buff_MiniGame4;

    [Header("�̴ϰ���5")]
    public string[] buff_MiniGame5;
}