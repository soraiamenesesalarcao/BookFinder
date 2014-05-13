using UnityEngine;
using System.Collections;

public class Book : Inventory {

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag.Equals("BookOrange"))
        {
            if (Definitions.Debug) Debug.Log("Book Orange");
            book_orange++;

            if (Definitions.Debug) Debug.Log(book_orange);
        }

        if(c.gameObject.tag.Equals("BookYellow"))
        {
            if(Definitions.Debug) Debug.Log("Book Yellow");
            book_yellow++;

            if (Definitions.Debug) Debug.Log(book_yellow);
        }

        if (c.gameObject.tag.Equals("BookBordeaux"))
        {
            if (Definitions.Debug) Debug.Log("Book Bordeaux");
            book_bordeaux++;

            if (Definitions.Debug) Debug.Log(book_bordeaux);
        }

        if (c.gameObject.tag.Equals("BookBlue"))
        {
            if (Definitions.Debug) Debug.Log("Book Blue");
            book_blue++;

            if (Definitions.Debug) Debug.Log(book_blue);
        }
       
        GameObject.Destroy(gameObject);
    }
}
