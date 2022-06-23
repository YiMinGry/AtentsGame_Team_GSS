using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class LineManager : MonoBehaviour
{
    [SerializeField]
    Transform[] lines;

    [SerializeField]
    Transform[] ordererInitPos;

    List<GameObject>[] lineOderList = new List<GameObject>[4];

    private void Start()
    {
        StartCoroutine(WaveStart());
    }

    IEnumerator WaveStart()
    {
        for (int i = 0; i < lineOderList.Length; i++)
        {
            lineOderList[i] = new List<GameObject>();
        }

        while (true)
        {
            int _line = Random.Range(0, 4);

            UnitSkinManager _unit = Instantiate(Resources.Load<GameObject>("Cha/Units/Unit_0"), ordererInitPos[_line]).GetComponent<UnitSkinManager>();
            _unit.RandomSet();
            lineOderList[_line].Add(_unit.gameObject);

            _unit.GetComponent<SortingGroup>().sortingOrder = lineOderList[_line].Count + 1;

            yield return Utill.WaitForSeconds(3f);
        }
    }
}
