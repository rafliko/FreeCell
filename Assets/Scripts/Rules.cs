using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour
{
    public GameObject cardObj;

    // Start is called before the first frame update
    void Start()
    {
        Deal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Deal()
    {
        int x, y;
        string tmp = "";
        string[] tab = new string[52];
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
                    tmp = "heart_";
                    break;
                case 4:
                    tmp = "spade_";
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
            if (Array.IndexOf(tab, tmp) == -1)
            {
                tab[i] = tmp;
                Debug.Log("Anglo/" + tab[i] + ".png");
                var c = Instantiate(cardObj, new Vector3((i%8)*1.5f-5.25f, -(i / 8)*0.5f+3.75f, -(i / 8)-1), Quaternion.identity);
                Sprite sp = Resources.Load<Sprite>("Anglo/" + tab[i]);
                c.GetComponent<SpriteRenderer>().sprite = sp;
            }
            else i--;
        }
    }
}