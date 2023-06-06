using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    int prevChildCount;

    // Start is called before the first frame update
    void Start()
    {
        prevChildCount = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(prevChildCount!=transform.childCount && tag!="goal")
        {
            if(transform.childCount==0)
            {
                Game.fcCount++;
            }
            else if(transform.childCount==1)
            {
                Game.fcCount--;
            }
            prevChildCount = transform.childCount;
        }
    }

    void OnMouseDown()
    {
        if (Game.selectedCard != null)
        {
            //Move selected card to this cell

            Game.colorChildrenRecursive(Game.selectedCard.transform, Color.white);
            if ( (tag == "freecell" && Game.selectedCard.transform.childCount == 0) ||
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
