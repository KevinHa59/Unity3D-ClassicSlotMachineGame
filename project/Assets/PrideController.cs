using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrideController : MonoBehaviour
{
    List<triplePrideItem> triplePride = new List<triplePrideItem>();
    List<doublePrideItem> doublePride = new List<doublePrideItem>();

    private void Start()
    {
        addPrides();
    }

    void addPrides() {
        triplePride.Add(new triplePrideItem("7", 1500, 3000, 3000000));
        triplePride.Add(new triplePrideItem("diamond", 1000, 1500, 3000));
        triplePride.Add(new triplePrideItem("dollar", 700, 1000, 1500));
        triplePride.Add(new triplePrideItem("bar3", 500, 700, 1200));
        triplePride.Add(new triplePrideItem("bar2", 300, 500, 800));
        triplePride.Add(new triplePrideItem("other", 150, 200, 300));

        doublePride.Add(new doublePrideItem("7", 100, 180, 300));
        doublePride.Add(new doublePrideItem("diamond", 80, 150, 250));
        doublePride.Add(new doublePrideItem("dollar", 60, 130, 200));
        doublePride.Add(new doublePrideItem("bell", 40, 80, 160));
    }

    int pride = 0;
    int matchCount;
    public bool checkPride(string item1, string item2, string item3, int credit) {
        matchCount = 0;
        string matchItem = "";
        pride = 0;
        bool win = false;
        
        if (item1 == item2 && item1 == item3 && item1 != "") {
            matchCount = 3;
            win = true;
        }
        else if ((item1 == item2 || item1 == item3 || item2 == item3) && item1 != "" && item2 != "")
        {
            matchCount = 2;
            if (item1 == item2 || item2 == item3) {
                matchItem = item2;
            } else if (item1 == item3) {
                matchItem = item1;
            }

            
        }

        
        if (matchCount == 3) {
            pride = triplePride[triplePride.Count-1].getPride(credit);
            for (int i = 0; i < triplePride.Count; i++) {
                if (triplePride[i].getItem() == item1)
                {
                    
                    pride = triplePride[i].getPride(credit);
                    
                }
            }
        }else if (matchCount == 2)
        {
            for (int i = 0; i < doublePride.Count; i++)
            {
                if (doublePride[i].getItem() == matchItem)
                {
                    pride = doublePride[i].getPride(credit);
                    win = true;
                }
            }
        }
        
        return win;
    }

    public int getWinPride() {
        
        return pride;
    }
    public int getMatchCount() {
        return matchCount;
    }
}

class triplePrideItem {
    string item;
    int price1credit;
    int price2credit;
    int price3credit;
    public triplePrideItem(string item, int price1, int price2, int price3)
    {
        this.item = item;
        this.price1credit = price2;
        this.price2credit = price2;
        this.price3credit = price3;

    }
    public string getItem() {
        return item;
    }
    public int getPrice1() {
        return price1credit;
    }
    public int getPrice2()
    {
        return price2credit;
    }
    public int getPrice3()
    {
        return price3credit;
    }

    public int getPride(int credit) {
        if (credit == 1)
        {
            return price1credit;
        }
        else if (credit == 2)
        {
            return price2credit;
        }
        else {
            return price3credit;
        }
    }
}

class doublePrideItem {
    string item;
    int price1credit;
    int price2credit;
    int price3credit;

    public doublePrideItem(string item, int price1credit, int price2credit, int price3credit)
    {
        this.item = item;
        this.price1credit = price1credit;
        this.price2credit = price2credit;
        this.price3credit = price3credit;
    }

    public string getItem()
    {
        return item;
    }
    public int getPrice1()
    {
        return price1credit;
    }
    public int getPrice2()
    {
        return price2credit;
    }
    public int getPrice3()
    {
        return price3credit;
    }

    public int getPride(int credit)
    {
        if (credit == 1)
        {
            return price1credit;
        }
        else if (credit == 2)
        {
            return price2credit;
        }
        else
        {
            return price3credit;
        }
    }
}