using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static GameObject selectedCard;
    public static int n, m;

    public GameObject cardObj;
    public Button btReset;

    // Start is called before the first frame update
    void Start()
    {
        selectedCard = null;
        n = 4;
        m = 0;
        btReset.onClick.AddListener(Reload);
        Deal();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("N:"+n+" M:"+m);
    }
    
    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Deal()
    {
        int x, y;
        string tmp = "";
        string[] tab1 = new string[52];
        GameObject[] tab2 = new GameObject[52];
        for(int i=0; i<52; i++)
        {
            x = UnityEngine.Random.Range(1, 14);
            y = UnityEngine.Random.Range(1, 5);
            switch (y)
            {
                case 1:
                    tmp = "club_";
                    break;
                case 2:
                    tmp = "diamond_";
                    break;
                case 3:
                    tmp = "spade_";
                    break;
                case 4:
                    tmp = "heart_";
                    break;
            }
            switch (x)
            {
                case 11:
                    tmp += "jack";
                    break;   
                case 12:     
                    tmp += "queen";
                    break;   
                case 13:     
                    tmp += "king";
                    break;
                default:
                    tmp += x.ToString();
                    break;
            }
            if (Array.IndexOf(tab1, tmp) == -1)
            {
                tab1[i] = tmp;
                //Debug.Log("Anglo/" + tab1[i] + ".png");
                var c = Instantiate(cardObj, new Vector3((i%8)*1.5f-5.25f, -(i / 8)*0.5f+3.75f, -(i / 8)-1), Quaternion.identity);
                tab2[i] = c;
                if(i>7)
                {
                    c.transform.SetParent(tab2[i - 8].transform);
                }
                else
                {
                    c.transform.SetParent(GameObject.Find("t"+Convert.ToString(i+1)).transform);
                }
                Sprite sp = Resources.Load<Sprite>("Anglo/" + tab1[i]);
                c.GetComponent<SpriteRenderer>().sprite = sp;
                c.GetComponent<Card>().value = x;
                c.GetComponent<Card>().suit = y;
            }
            else i--;
        }
    }

    public static int countChildrenRecursive(Transform t)
    {
        if(t.childCount == 0) return 0;
        else return countChildrenRecursive(t.GetChild(0).transform) + 1;
    }

    public static bool checkChildrenRecursive(Transform t)
    {
        int v = t.GetComponent<Card>().value;
        int s = t.GetComponent<Card>().suit;
        if (t.childCount == 0)
        {
            return true;
        }
        else
        {
            int cv = t.GetChild(0).GetComponent<Card>().value;
            int cs = t.GetChild(0).GetComponent<Card>().suit;
            if (cv == v - 1 && cs % 2 != s % 2)
            {
                return checkChildrenRecursive(t.GetChild(0).transform);
            }
            else
            {
                return false;
            }
        }
    }

    public static void colorChildrenRecursive(Transform t, Color c)
    {
        t.GetComponent<SpriteRenderer>().color = c;
        if (t.childCount > 0) colorChildrenRecursive(t.GetChild(0).transform, c);
    }
}