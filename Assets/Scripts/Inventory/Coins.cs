using UnityEngine;
using System.Collections;

public class Coins : Inventory {

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag.Equals("Coins"))
        {
            if (Definitions.Debug) Debug.Log("Coins");
            coins++;

            if (Definitions.Debug) Debug.Log(coins);
        }

        GameObject.Destroy(gameObject);
    }

}
