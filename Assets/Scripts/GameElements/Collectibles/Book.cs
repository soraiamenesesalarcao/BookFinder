using UnityEngine;
using System.Collections;

public class Book : Collectible {
    

    void OnTriggerEnter(Collider c) {

        if (c.gameObject.tag.Equals("Player")) {

            playerInfo = c.GetComponent(typeof(CharacterInfo)) as CharacterInfo;

            if (gameObject.tag.Equals("BookOrange")) {
                if (Definitions.Debug) Debug.Log("Book Orange");
                playerInfo.Items.book_orange++;

                if (Definitions.Debug) Debug.Log(playerInfo.Items.book_orange);
            }

            if (gameObject.tag.Equals("BookYellow")) {
                if (Definitions.Debug) Debug.Log("Book Yellow");
                playerInfo.Items.book_yellow++;

                if (Definitions.Debug) Debug.Log(playerInfo.Items.book_yellow);
            }

            if (gameObject.tag.Equals("BookBordeaux")) {
                if (Definitions.Debug) Debug.Log("Book Bordeaux");
                playerInfo.Items.book_bordeaux++;

                if (Definitions.Debug) Debug.Log(playerInfo.Items.book_bordeaux);
            }

            if (gameObject.tag.Equals("BookBlue")) {
                if (Definitions.Debug) Debug.Log("Book Blue");
                playerInfo.Items.book_blue++;

                if (Definitions.Debug) Debug.Log(playerInfo.Items.book_blue);
            }
        }
        GameObject.Destroy(gameObject);
    }
}
