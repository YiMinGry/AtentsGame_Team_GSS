using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test11 : MonoBehaviour
{
    // Start is called before the first frame update
    private int a = 111;
    protected int b=222;
    public int c = 333;

    protected void Awake()
    {
        Debug.Log("프로텍티드");
        Debug.Log($"{a},{b},{c}");
    }
    public void Start()
    {
        Debug.Log("퍼블릭");
        Debug.Log($"{a},{b},{c}");
    }
    private void Update()
    {
        Debug.Log("프라이빗");
        Debug.Log($"{a},{b},{c}");
    }


}
