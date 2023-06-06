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

            GetComponent<SpriteRenderer>().color = Color.yellow;
            Game.selectedCard = gameObject;
        }
        else
        {
            //Move selected card under this card
            var scRenderer = Game.selectedCard.GetComponent<SpriteRenderer>();
            var scScript = Game.selectedCard.GetComponent<Card>();

            scRenderer.color = Color.white;
            if(tag == "tableau" && transform.childCount == 0 && transform.position.x != Game.selectedCard.transform.position.x)
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
            Game.selectedCard = null;
        }
    }
}
