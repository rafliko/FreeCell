using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int value, suit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if(Game.selectedCard==null)
        {
            //Select card
            int max = (int)Math.Pow(2, Game.m) * (Game.n + 1);

            if(Game.checkChildrenRecursive(transform) && Game.countChildrenRecursive(transform)+1 <= max)
            {
                //Debug.Log(Game.fcCount + ":" + Game.countChildrenRecursive(transform));
                Game.colorChildrenRecursive(transform, Color.yellow);
                Game.selectedCard = gameObject;
            }
        }
        else
        {
            //Move selected card under this card
            var scScript = Game.selectedCard.GetComponent<Card>();

            Game.colorChildrenRecursive(Game.selectedCard.transform, Color.white);
            if (tag == "tableau" && transform.childCount == 0 && transform.position.x != Game.selectedCard.transform.position.x &&
               scScript.value == value - 1 && scScript.suit % 2 != suit % 2)
            {
                Game.selectedCard.tag = tag;
                Game.selectedCard.transform.parent = transform;
                Game.selectedCard.transform.position = gameObject.transform.position + Vector3.down * 0.5f + Vector3.back;
            }
            else if(tag == "goal" && scScript.value == value + 1 && scScript.suit == suit)
            {
                Game.selectedCard.tag = tag;
                Game.selectedCard.transform.parent = transform;
                Game.selectedCard.transform.position = gameObject.transform.position + Vector3.back;
            }
            //Debug.Log("Child check: "+Game.checkChildrenRecursive(Game.selectedCard.transform));
            Game.selectedCard = null;
        }
    }
}
