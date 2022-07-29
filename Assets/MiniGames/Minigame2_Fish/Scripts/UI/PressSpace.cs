using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressSpace : MonoBehaviour
{
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(TextBlink());
    }

    IEnumerator TextBlink()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            image.enabled = false;
            yield return new WaitForSeconds(0.5f);
            image.enabled = true;
        }
    }
}
