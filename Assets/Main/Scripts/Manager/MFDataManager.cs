using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MFDataManager : MonoSingleton<MFDataManager>
{
    public MiniFriendData[] mfarr;

    /*초기화 부분*/
    //외부 저장소에서 미니친구 정보 로드
    //전체 오브젝트 소유 여부 세팅
    //잔체 오브젝트 착용 여부 세팅

    /*기능 부분*/
    //id로 오브젝트 찾기
    //id로 특정 미니친구 정보 불러오기
    //id로 소유여부 변경해주는 기능(뽑기해서 얻을때)//서버연동 필요
    //착용중인 미니친구 갯수 리턴 3마리 기준
    //id로 소유중인 미니친구 착용상태로 변경해주는 기능//서버 연동 필요
    //id로 착용중인 미니친구 해제하는 기능//서버 연동 필요

}
