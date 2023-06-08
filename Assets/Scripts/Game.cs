using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Game : MonoBehaviour
{
    public static GameObject selectedCard;
    public static int n, m, goals;
    public static bool finish;

    public GameObject cardObj;
    public GameObject cardRigidbodyObj;
    public Button btReset;
    public Button btRules;
    public Button btAuto;
    public Text txtWin;

    // Start is called before the first frame update
    void Start()
    {
        selectedCard = null;
        n = 4;
        m = 0;
        goals = 0;
        finish = false;
        btReset.onClick.AddListener(Reload);
        btRules.onClick.AddListener(Rules);
        btAuto.onClick.AddListener(Finish);
        Deal();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(goals);

        if (goals == 4 && !finish)
        {
            Finish();
        }

        if(m == 4 && !finish) 
        {
            btAuto.GetComponent<Button>().interactable = true;
        }
        else
        {
            btAuto.GetComponent<Button>().interactable = false;
        }
    }
    
    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Rules()
    {
        Application.OpenURL("https://en.wikipedia.org/wiki/FreeCell");
    }

    void Finish()
    {
        Sprite sp;

        txtWin.enabled = true;
        for (int i=0; i<8; i++)
        {
            var t = GameObject.Find("t" + Convert.ToString(i + 1));
            var fc = GameObject.Find("fc" + Convert.ToString(i + 1));
            var g = GameObject.Find("g" + Convert.ToString(i + 1));
            if (t.transform.childCount > 0) Destroy(t.transform.GetChild(0).gameObject);
            if (fc!=null && fc.transform.childCount > 0) Destroy(fc.transform.GetChild(0).gameObject);
            if (g != null && g.transform.childCount > 0) Destroy(g.transform.GetChild(0).gameObject);
        }
        for(int i=0; i<20; i++)
        {
            var c = Instantiate(cardRigidbodyObj, new Vector3(0f, 4f, -i-1), Quaternion.identity);
            if(i%2==0) sp = Resources.Load<Sprite>("Anglo/spade_king");
            else sp = Resources.Load<Sprite>("Anglo/heart_king");
            c.GetComponent<SpriteRenderer>().sprite = sp;
        }

        finish = true;
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