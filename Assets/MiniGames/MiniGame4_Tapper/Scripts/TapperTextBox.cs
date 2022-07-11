using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapperTextBox : MonoBehaviour
{
    [SerializeField]
    private GameObject TextBox;

    [SerializeField]
    private Text dialogText;

    private Transform taget;

    readonly Vector3 addPos = new Vector3(0, 0.3f, 0);

    public void Update()
    {
        if (taget != null)
        {
            transform.position = taget.position + addPos;
        }
    }

    public IEnumerator SetDialog(Transform _taget, string _msg, float _time = 0.5f)
    {
        taget = _taget;

        TextBox.SetActive(true);
        dialogText.text = _msg;
        yield return Utill.WaitForSeconds(_time);
        TextBox.SetActive(false);

        taget = null;
    }
}
