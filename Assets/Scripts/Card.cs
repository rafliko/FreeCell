using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
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
        if(Game.selection==null)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
            Game.selection = gameObject;
        }
        else
        {
            Game.selection.GetComponent<SpriteRenderer>().color = Color.white;
            if(transform.childCount == 0 && Game.selection != gameObject)
            {
                Game.selection.transform.parent = transform;
                Game.selection.transform.position = gameObject.transform.position + Vector3.down * 0.5f + Vector3.back;
            }
            Game.selection = null;
        }
    }
}
