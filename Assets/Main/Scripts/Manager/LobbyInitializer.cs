using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyInitializer : SceneInitializer//��ӹ޾ƿͼ�
{
    [SerializeField]
    private bool isDev = false;//���ξ� ����� ������ ����ϱ����� �÷��� 
    [SerializeField]
    private LobbyUI lobbyUI;//�ʿ����� ���� �ϴ� �־ �ξ����ϴ�

    public string MapPrefab_Name = "3DMap";

    void Start()
    {
        AudioManager.Inst.PlayBGM("RoccoW - Free Singles 2013 - 03 Messeah");

        UserDataManager.instance.RefreshUserInfo();
        MapLoad("Prefabs/Lobby/" + MapPrefab_Name + (isDev == true ? "_Dev" : ""));//���ҽ� �������� �� ������ �ε�

        AddMainCanvas("Achivement");//���� ĵ������ �˾� �߰� ���ҽ������� ���������� �����ؾ���
        AddMainCanvas("Ranking");
        AddMainCanvas("GSS_Desktop");
        AddMainCanvas("Gacha");
    }

    public override void MapLoad(string _path)
    {
        GameObject _map = Instantiate(Resources.Load<GameObject>(_path));
    }

    public override void AddMainCanvas(string _uiName)
    {
        if (mainCanvas == null)//���� ĵ������ ������ ����
        {
            mainCanvas = Instantiate(Resources.Load<GameObject>("Prefabs/Lobby/MainCanvas"));
        }


        try
        {
            GameObject _obj = Instantiate(Resources.Load<GameObject>("Prefabs/Lobby/" + _uiName), mainCanvas.transform);
            _obj.name = _uiName;
        }
        catch
        {
            Debug.Log("�ش� ��ο� " + _uiName + "������ �����ϴ�.");
        }

    }

    public override void AddObject(string _name)//�Ϲ� ������Ʈ�� �����ϰ� �;������� ������ �ϴ� ���� �ξ����ϴ�
    {
        GameObject _obj = Instantiate(Resources.Load<GameObject>("Prefabs/Lobby/" + _name));
        _obj.name = _name;
    }
}
