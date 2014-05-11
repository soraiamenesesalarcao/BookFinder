using UnityEngine;
using System.Collections;

public class HUD  {

    public static bool IsInventoryVisible = false;

    public void Draw() {

        // experiencia
        Rect R = new Rect(0, 0, Screen.width, Screen.height);
        GUI.Box(R, "" + GameState.HasWon);
        // end of experiencia

        DrawInfo();
        DrawMap();
        if (IsInventoryVisible)
            DrawInventory();        
    }

    void DrawInfo() {
    }

    void DrawInventory() {
    }

    void DrawMap() {
    }
}
