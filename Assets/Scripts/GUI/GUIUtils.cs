using UnityEngine;

public static class GUIUtils {

    public static Texture2D MakeTexture(int width, int height, Color col) {

        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i) {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    public static string SecondsToHMS(float sec) {
        string hms;

        float min = sec / 60.0f;
        float hou = min / 60.0f;

        int hours = (int)hou;
        int mins = (int)((hou - hours) * 60.0f);
        int secs = (int)((min - mins) * 60.0f);

        hms = hours + ":" + mins + ":" + secs;

        return hms;
    }

}
