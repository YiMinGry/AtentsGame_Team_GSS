using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollingText : MonoBehaviour
{
    public Queue<string> textQue = new Queue<string>();

    public void AddText(string _msg) 
    {
        textQue.Enqueue(_msg);
    }

    IEnumerator RollingStart() 
    {
        while (true) 
        { 
        
        }
    
    }
}
