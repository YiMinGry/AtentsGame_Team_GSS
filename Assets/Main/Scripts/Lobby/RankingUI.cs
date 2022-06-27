using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class testRank
{
    public string name;
    public int score;

    public testRank(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
public class RankingUI : MonoBehaviour
{
    Text[] rankSheetText = new Text[10];
    List<testRank> data = new List<testRank>();
    private void Start()
    {

        data.Add(new testRank("nicsda", 10302312));
        data.Add(new testRank("afsdf", 102312));
        data.Add(new testRank("gsdagsd", 1522));
        data.Add(new testRank("cnbvnr", 1425));
        data.Add(new testRank("dasc", 1022));
        data.Add(new testRank("casc", 1002));
        data.Add(new testRank("mizoq", 120));
        data.Add(new testRank("vsao88", 18));
        data.Add(new testRank("vszix", 10));
        data.Add(new testRank("zomqoa1", 1));
        

        for (int i=0;i<10;i++)
        {
            rankSheetText[i] = transform.GetChild(i).GetComponent<Text>();
            rankSheetText[i].text = $"{data[i].score,10}       {data[i].name}";
        }
        
        //for(int i=0;i<10;i++)
        //{
        //    rankSheetText[i] = rankSheet.transform.GetChild(i).GetComponent<Text>();
        //}
        //rankSheetText[0].text = $"asdffsdfsdsfdsfd";
        
    }
    
}
