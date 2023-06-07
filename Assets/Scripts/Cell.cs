using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    int prevChildCount;

    // Start is called before the first frame update
    void Start()
    {
        if(tag=="freecell") prevChildCount = 0;
        else if(tag=="tableau") prevChildCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(prevChildCount!=transform.childCount && tag!="goal")
        {
            if(tag=="freecell")
            {
                if (transform.childCount == 0) Game.n++;
                else if (transform.childCount == 1) Game.n--;
            }
            else if(tag=="tableau")
            {
                if (transform.childCount == 0) Game.m++;
                else if (transform.childCount == 1) Game.m--;
            }
            prevChildCount = transform.childCount;
        }
    }

    void OnMouseDown()
    {
        if (Game.selectedCard != null)
        {
            //Move selected card to this cell
            int max = (int)Math.Pow(2, Game.m) * (Game.n + 1);
            var scScript = Game.selectedCard.GetComponent<Card>();

            Game.colorChildrenRecursive(Game.selectedCard.transform, Color.white);
            if ( (tag == "freecell" && Game.selectedCard.transform.childCount == 0) ||
                (tag == "goal" && Game.selectedCard.transform.childCount == 0 && Game.selectedCard.GetComponent<Card>().value == 1) ||
                (tag == "tableau") && Game.countChildrenRecursive(Game.selectedCard.transform)+1 <= max/2)
            {
                if (scScript.value == 13 && Game.selectedCard.tag == "goal") Game.goals--;
                Game.selectedCard.tag = tag;
                Game.selectedCard.transform.parent = transform;
                Game.selectedCard.transform.position = gameObject.transform.position + Vector3.back;
            }
            Game.selectedCard = null;
        }
    }
}
