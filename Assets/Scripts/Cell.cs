using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
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
        if (Game.selectedCard != null)
        {
            //Move selected card to this cell

            Game.selectedCard.GetComponent<SpriteRenderer>().color = Color.white;
            if( (tag == "freecell" && Game.selectedCard.transform.childCount == 0) ||
                (tag == "goal" && Game.selectedCard.transform.childCount == 0 && Game.selectedCard.GetComponent<Card>().value == 1) ||
                (tag == "tableau"))
            {
                Game.selectedCard.tag = tag;
                Game.selectedCard.transform.parent = transform;
                Game.selectedCard.transform.position = gameObject.transform.position + Vector3.back;
            }
            Game.selectedCard = null;
        }
    }
}
