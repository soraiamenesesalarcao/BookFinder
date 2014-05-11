using System;

public class PlayerScore : Object {

    public string Name;
    public int Score;

    public PlayerScore(string n, int s) {
        Name = n;
        Score = s;
    }

   public override string ToString() {
        return Name + " [" + Score + "]";
   }
}
