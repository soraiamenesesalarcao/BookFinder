using UnityEngine;
using System.Collections;

public class Book : Collectible {

    void OnTriggerEnter(Collider c) {

        if (c.gameObject.tag.Equals("Molly")) {

            if (gameObject.tag.Equals("BookOrange")) {
                if (Definitions.Debug) Debug.Log("Book Orange");
                GameState.CurrentPlayer.Items.book_orange++;
                awardScore();

                if (Definitions.Debug) Debug.Log(GameState.CurrentPlayer.Items.book_orange);
            }

            if (gameObject.tag.Equals("BookYellow")) {
                if (Definitions.Debug) Debug.Log("Book Yellow");
                GameState.CurrentPlayer.Items.book_yellow++;
                awardScore();

                if (Definitions.Debug) Debug.Log(GameState.CurrentPlayer.Items.book_yellow);
            }

            if (gameObject.tag.Equals("BookBordeaux")) {
                if (Definitions.Debug) Debug.Log("Book Bordeaux");
                GameState.CurrentPlayer.Items.book_bordeaux++;
                awardScore();

                if (Definitions.Debug) Debug.Log(GameState.CurrentPlayer.Items.book_bordeaux);
            }

            if (gameObject.tag.Equals("BookBlue")) {
                if (Definitions.Debug) Debug.Log("Book Blue");
                GameState.CurrentPlayer.Items.book_blue++;
                awardScore();

                if (Definitions.Debug) Debug.Log(GameState.CurrentPlayer.Items.book_blue);
            }
            GameObject.Destroy(gameObject);
        }
        
    }
}
