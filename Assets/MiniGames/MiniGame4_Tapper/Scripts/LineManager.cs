using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class LineManager : MonoBehaviour
{
    public TapperGameManager tapperGameManager;

    [SerializeField]
    Transform[] lines;

    [SerializeField]
    Transform[] ordererInitPos;

    List<GameObject>[] lineOderList = new List<GameObject>[4];

    IEnumerator TutorialWaveStartCor;
    IEnumerator InfiniteWaveStartCor;

    public void StartTutorial()
    {
        if (TutorialWaveStartCor != null) 
        {
            StopCoroutine(TutorialWaveStartCor);
            TutorialWaveStartCor = null;
        }

        TutorialWaveStartCor = TutorialWaveStart();

        tapperGameManager.ResetCounts();

        StartCoroutine(TutorialWaveStartCor);
    }
    public void StartWave()
    {
        if (InfiniteWaveStartCor != null)
        {
            StopCoroutine(InfiniteWaveStartCor);
            InfiniteWaveStartCor = null;
        }

        InfiniteWaveStartCor = InfiniteWaveStart();

        tapperGameManager.ResetCounts();

        StartCoroutine(InfiniteWaveStartCor);
    }

    IEnumerator TutorialWaveStart()
    {
        for (int i = 0; i < lineOderList.Length; i++)
        {
            if (lineOderList[i] != null)
            {
                for (int j = 0; j < lineOderList[i].Count; j++)
                {
                    Destroy(lineOderList[i][j]);
                }
            }

            lineOderList[i] = new List<GameObject>();
        }



        yield return Utill.WaitForSeconds(3f);

        UnitSkinManager _unit = Instantiate(Resources.Load<GameObject>("Cha/Units/Unit_0"), ordererInitPos[0]).GetComponent<UnitSkinManager>();
        _unit.RandomSet();
        lineOderList[0].Add(_unit.gameObject);
        _unit.GetComponent<OderMove>().tapperGameManager = tapperGameManager;
        _unit.GetComponent<SortingGroup>().sortingOrder = lineOderList[0].Count + 30;

        yield return Utill.WaitForSeconds(1f);

        UnitSkinManager _unit1 = Instantiate(Resources.Load<GameObject>("Cha/Units/Unit_0"), ordererInitPos[1]).GetComponent<UnitSkinManager>();
        _unit1.RandomSet();
        lineOderList[1].Add(_unit1.gameObject);
        _unit1.GetComponent<OderMove>().tapperGameManager = tapperGameManager;
        _unit1.GetComponent<SortingGroup>().sortingOrder = lineOderList[1].Count + 30;

        yield return Utill.WaitForSeconds(1f);

        UnitSkinManager _unit2 = Instantiate(Resources.Load<GameObject>("Cha/Units/Unit_0"), ordererInitPos[2]).GetComponent<UnitSkinManager>();
        _unit2.RandomSet();
        lineOderList[2].Add(_unit2.gameObject);
        _unit2.GetComponent<OderMove>().tapperGameManager = tapperGameManager;
        _unit2.GetComponent<SortingGroup>().sortingOrder = lineOderList[2].Count + 30;

        yield return Utill.WaitForSeconds(1f);

        UnitSkinManager _unit3 = Instantiate(Resources.Load<GameObject>("Cha/Units/Unit_0"), ordererInitPos[3]).GetComponent<UnitSkinManager>();
        _unit3.RandomSet();
        lineOderList[3].Add(_unit3.gameObject);
        _unit3.GetComponent<OderMove>().tapperGameManager = tapperGameManager;
        _unit3.GetComponent<SortingGroup>().sortingOrder = lineOderList[3].Count + 30;


        tapperGameManager.tapperUIManager.SetTutorialPopupCloser(true);

        yield return new WaitUntil(() => tapperGameManager.tapperUIManager.IsTutorial() == false);

        tapperGameManager.playerMove.enabled = false;



        for (int i = 0; i < lineOderList.Length; i++)
        {
            if (lineOderList[i] != null)
            {
                for (int j = 0; j < lineOderList[i].Count; j++)
                {
                    Destroy(lineOderList[i][j]);
                }
            }

            lineOderList[i] = new List<GameObject>();
        }

        tapperGameManager.ResetCounts();
        tapperGameManager.tapperUIManager.SetTitle(true);
    }

    IEnumerator InfiniteWaveStart()
    {
        for (int i = 0; i < lineOderList.Length; i++)
        {
            if (lineOderList[i] != null)
            {
                for (int j = 0; j < lineOderList[i].Count; j++)
                {
                    Destroy(lineOderList[i][j]);
                }
            }

            lineOderList[i] = new List<GameObject>();
        }

        while (true)
        {
            yield return Utill.WaitForSeconds(2f);

            int _line = Random.Range(0, 4);

            UnitSkinManager _unit = Instantiate(Resources.Load<GameObject>("Cha/Units/Unit_0"), ordererInitPos[_line]).GetComponent<UnitSkinManager>();
            _unit.RandomSet();
            lineOderList[_line].Add(_unit.gameObject);
            _unit.GetComponent<OderMove>().tapperGameManager = tapperGameManager;
            _unit.GetComponent<SortingGroup>().sortingOrder = lineOderList[_line].Count + 30;

        }
    }
}
