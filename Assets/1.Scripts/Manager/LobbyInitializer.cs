using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyInitializer : SceneInitializer//상속받아와서
{
    [SerializeField]
    private bool isDev = false;//메인씬 데브씬 나눠서 사용하기위한 플래그 
    [SerializeField]
    private LobbyUI lobbyUI;//필요할지 몰라서 일단 넣어만 두었습니다

    void Start()
    {
        MapLoad("Prefabs/Lobby/3DMap_2" + (isDev == true ? "_Dev" : ""));//리소스 폴더에서 맵 프리펩 로드

        AddMainCanvas("Achivement");//메인 캔버스에 팝업 추가 리소스폴더에 프리펩으로 존재해야함
        AddMainCanvas("Ranking");
        AddMainCanvas("chat");
        AddMainCanvas("Gacha");
        
                    AddMainCanvas("PhoneModeUI");

    }

    public override void MapLoad(string _path)
    {
        GameObject _map = Instantiate(Resources.Load<GameObject>(_path));
    }

    public override void AddMainCanvas(string _uiName)
    {
        if (mainCanvas == null)//메인 캔버스가 없으면 생성
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
            Debug.Log("해당 경로에 " + _uiName + "파일이 없습니다.");
        }

    }

    public override void AddObject(string _name)//일반 오브젝트도 생성하고 싶어질수도 있으니 일단 만들어만 두었습니다
    {
        GameObject _obj = Instantiate(Resources.Load<GameObject>("Prefabs/Lobby/" + _name));
        _obj.name = _name;
    }
}
