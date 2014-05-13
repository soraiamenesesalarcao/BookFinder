using UnityEngine;
using System.Collections;

public class CollisionRoom : MonoBehaviour {

    void Start() {

        string halls = "";
        string rooms = "";

        for (int i = 1; i < 27; i++) {
            halls = "Hall" + i;
            turnOffTorchs(halls);
        }
        for (int i = 2; i < 4; i++) {
            halls = "Room" + i;
            turnOffTorchs(rooms);
        }
    }
    

    void turnOnTorchs(string room) {
        GameObject[] gos1 = GameObject.FindGameObjectsWithTag("Light");
        GameObject[] gos2 = GameObject.FindGameObjectsWithTag("Flame");

        foreach (GameObject go in gos1)
            if (go.name.Contains(room) == true) go.light.enabled = true;

        foreach (GameObject go2 in gos2)
            if (go2.name.Contains(room) == true) go2.particleSystem.Play();

        Debug.Log("Entrei" + room + "Acendi Cenas");
    }
    void turnOffTorchs(string room) {
        GameObject[] gos1 = GameObject.FindGameObjectsWithTag("Light");
        GameObject[] gos2 = GameObject.FindGameObjectsWithTag("Flame");

        foreach (GameObject go in gos1)
            if (go.name.Contains(room) == true) go.light.enabled = false;

        foreach (GameObject go2 in gos2)
            if (go2.name.Contains(room) == true) go2.particleSystem.Stop();

        Debug.Log("Sai" + room + "Apaguei Cenas");
    }

    void turnOnLight(string obj) { 
        GameObject[] gos1 = GameObject.FindGameObjectsWithTag("Light");
        foreach (GameObject go in gos1)
            if (go.name.Contains(obj) == true) go.light.enabled = true;
    }
    void turnOffLight(string obj) {
        GameObject[] gos1 = GameObject.FindGameObjectsWithTag("Light");
        foreach (GameObject go in gos1)
            if (go.name.Contains(obj) == true) go.light.enabled = false;
    }

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Player")) {
            if (this.name.Equals("Room1")) {
                turnOnTorchs("Room1");
            }
            if (this.name.Equals("Room2")) {
                turnOnTorchs("Room2");
            }
            if (this.name.Equals("Room3")) {
                turnOnTorchs("Room3");
            }

            if (this.name.Equals("Hall1")) {
                turnOnTorchs("Hall1");
                turnOnLight("LightSkeleton2");

            }
            if (this.name.Equals("Hall2")) {
                turnOnTorchs("Hall2");
            }
            if (this.name.Equals("Hall3")) {
                turnOnTorchs("Hall3");
                turnOnLight("LightTurret2");
            }
            if (this.name.Equals("Hall4")) {
                turnOnTorchs("Hall4");
                turnOnLight("LightSkeleton1");
            }
            if (this.name.Equals("Hall5")) {
                turnOnTorchs("Hall5");
            }
            if (this.name.Equals("Hall6")) {
                turnOnTorchs("Hall6");
                turnOnLight("LightTurret1");
            }
            if (this.name.Equals("Hall7")) {
                turnOnTorchs("Hall7");
            }
            if (this.name.Equals("Hall8")) {
                turnOnTorchs("Hall8");
            }
            if (this.name.Equals("Hall9")) {
                turnOnTorchs("Hall9");
                turnOnLight("LightSkeleton3");
            }
            if (this.name.Equals("Hall10")) {
                turnOnTorchs("Hall10");
            }
            if (this.name.Equals("Hall11")) {
                turnOnTorchs("Hall11");
                turnOnLight("LightTurret3");
            }
            if (this.name.Equals("Hall12")) {
                turnOnTorchs("Hall12");
            }
            if (this.name.Equals("Hall13")) {
                turnOnTorchs("Hall13");
            }
            if (this.name.Equals("Hall14")) {
                turnOnTorchs("Hall14");
            }
            if (this.name.Equals("Hall15")) {
                turnOnTorchs("Hall15");
                turnOnLight("LightTurret4");
            }
            if (this.name.Equals("Hall16")) {
                turnOnTorchs("Hall16");
            }
            if (this.name.Equals("Hall17")) {
                turnOnTorchs("Hall17");
            }
            if (this.name.Equals("Hall18")) {
                turnOnTorchs("Hall18");
                turnOnLight("LightSkeleton4");
            }
            if (this.name.Equals("Hall19")) {
                turnOnTorchs("Hall19");
            }
            if (this.name.Equals("Hall20")) {
                turnOnTorchs("Hall20");
            }
            if (this.name.Equals("Hall21")) {
                turnOnTorchs("Hall21");
            }
            if (this.name.Equals("Hall22")) {
                turnOnTorchs("Hall22");
            }
            if (this.name.Equals("Hall23")) {
                turnOnTorchs("Hall23");
            }
            if (this.name.Equals("Hall24")) {
                turnOnTorchs("Hall24");
            }
            if (this.name.Equals("Hall25")) {
                turnOnTorchs("Hall25");
            }
            if (this.name.Equals("Hall26")) {
                turnOnTorchs("Hall26");
            }
        }
    }
    void OnTriggerExit(Collider col) {
        if (col.CompareTag("Player")) {
            if (this.name.Equals("Room1")) {
                turnOffTorchs("Room1");
            }
            if (this.name.Equals("Room2")) {
                turnOffTorchs("Room2");
            }
            if (this.name.Equals("Room3")) {
                turnOffTorchs("Room3");
            }

            if (this.name.Equals("Hall1")) {
                turnOffTorchs("Hall1");
                turnOffLight("LightSkeleton2");
            }
            if (this.name.Equals("Hall2")) {
                turnOffTorchs("Hall2");
            }
            if (this.name.Equals("Hall3")) {
                turnOffTorchs("Hall3");
                turnOffLight("LightTurret2");
            }
            if (this.name.Equals("Hall4")) {
                turnOffTorchs("Hall4");
                turnOffLight("LightSkeleton1");
            }
            if (this.name.Equals("Hall5")) {
                turnOffTorchs("Hall5");
            }
            if (this.name.Equals("Hall6")) {
                turnOffTorchs("Hall6");
                turnOffLight("LightTurret1");
            }
            if (this.name.Equals("Hall7")) {
                turnOffTorchs("Hall7");
            }
            if (this.name.Equals("Hall8")) {
                turnOffTorchs("Hall8");
            }
            if (this.name.Equals("Hall9")) {
                turnOffTorchs("Hall9");
                turnOffLight("LightSkeleton3");
            }
            if (this.name.Equals("Hall10")) {
                turnOffTorchs("Hall10");
            }
            if (this.name.Equals("Hall11")) {
                turnOffTorchs("Hall11");
                turnOffLight("LightTurret3");
            }
            if (this.name.Equals("Hall12")) {
                turnOffTorchs("Hall12");
            }
            if (this.name.Equals("Hall13")) {
                turnOffTorchs("Hall13");
            }
            if (this.name.Equals("Hall14")) {
                turnOffTorchs("Hall14");
            }
            if (this.name.Equals("Hall15")) {
                turnOffTorchs("Hall15");
                turnOffLight("LightTurret4");
            }
            if (this.name.Equals("Hall16")) {
                turnOffTorchs("Hall16");
            }
            if (this.name.Equals("Hall17")) {
                turnOffTorchs("Hall17");
            }
            if (this.name.Equals("Hall18")) {
                turnOffTorchs("Hall18");
                turnOffLight("LightSkeleton4");
            }
            if (this.name.Equals("Hall19")) {
                turnOffTorchs("Hall19");
            }
            if (this.name.Equals("Hall20")) {
                turnOffTorchs("Hall20");
            }
            if (this.name.Equals("Hall21")) {
                turnOffTorchs("Hall21");
            }
            if (this.name.Equals("Hall22")) {
                turnOffTorchs("Hall22");
            }
            if (this.name.Equals("Hall23")) {
                turnOffTorchs("Hall23");
            }
            if (this.name.Equals("Hall24")) {
                turnOffTorchs("Hall24");
            }
            if (this.name.Equals("Hall25")) {
                turnOffTorchs("Hall25");
            }
            if (this.name.Equals("Hall26")) {
                turnOffTorchs("Hall26");
            }
        }
    }
}
