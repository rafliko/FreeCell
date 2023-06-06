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
        if (Game.selection != null)
        {
            Game.selection.GetComponent<SpriteRenderer>().color = Color.white;
            if (transform.childCount == 0)
            {
                if(((tag=="freecell" || tag=="goal") && Game.selection.transform.childCount==0) || tag=="tableau")
                {
                    Game.selection.transform.parent = transform;
                    Game.selection.transform.position = gameObject.transform.position + Vector3.back;
                }
            }
            Game.selection = null;
        }
    }
}
