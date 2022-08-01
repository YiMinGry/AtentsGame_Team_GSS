using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapperTextBoxManager : MonoBehaviour
{
    [SerializeField]
    Transform textBoxInitPos;
    [SerializeField]
    Transform textBoxObj;
    public List<TapperTextBox> TapperTextBoxs = new List<TapperTextBox>();

    const int errIndex = -1;

    private void Awake()
    {
        if (TapperTextBoxs.Count <= 0)
        {
            TapperTextBoxs.Clear();

            for (int i = 0; i < 10; i++)
            {
                Instantiate(textBoxObj, textBoxInitPos);

                TapperTextBoxs.Add(Instantiate(textBoxObj, textBoxInitPos).GetComponent<TapperTextBox>());
            }
        }
    }

    TapperTextBox GetTextBox()
    {
        int _idx = errIndex;

        try
        {
            for (int i = 0; i < TapperTextBoxs.Count; i++)
            {
                if (TapperTextBoxs[i].gameObject.activeSelf == true)
                {
                    continue;
                }

                _idx = i;

                break;
            }

            return TapperTextBoxs[_idx];
        }
        catch
        {
            TapperTextBox _tt = Instantiate(textBoxObj, textBoxInitPos).GetComponent<TapperTextBox>();

            TapperTextBoxs.Add(Instantiate(textBoxObj, textBoxInitPos).GetComponent<TapperTextBox>());

            return _tt;
        }
    }


    public void SetDiaLog(Transform _taget, string _msg)
    {
        StartCoroutine(GetTextBox().SetDialog(_taget, _msg, 1f));
    }

}
