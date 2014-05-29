using UnityEngine;
using System.Collections;

public class CollisionRoom : MonoBehaviour {

    private System.Random rndColor = new System.Random();
    private System.Random rndIntensity = new System.Random();

    int timeElapsed = 0;

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
        turnOnLight("AmbientLight");
    }
    
    void turnOnTorchs(string room) {
        GameObject[] gos1 = GameObject.FindGameObjectsWithTag("Light");
        GameObject[] gos2 = GameObject.FindGameObjectsWithTag("Flame");

        foreach (GameObject go in gos1)
            if (go.name.Contains(room) == true) go.light.enabled = true;

        foreach (GameObject go2 in gos2)
            if (go2.name.Contains(room) == true) go2.particleSystem.Play();

        //Debug.Log("Entrei" + room + "Acendi Cenas");
    }
    void turnOffTorchs(string room) {
        GameObject[] gos1 = GameObject.FindGameObjectsWithTag("Light");
        GameObject[] gos2 = GameObject.FindGameObjectsWithTag("Flame");

        foreach (GameObject go in gos1)
            if (go.name.Contains(room) == true && !go.name.Contains("Book")) go.light.enabled = false;

        foreach (GameObject go2 in gos2)
            if (go2.name.Contains(room) == true) go2.particleSystem.Stop();

       // Debug.Log("Sai" + room + "Apaguei Cenas");
    }

    void turnOnLight(string obj) { 
        GameObject[] gos1 = GameObject.FindGameObjectsWithTag("Light");
        foreach (GameObject go in gos1)
            if (go.name.Contains(obj) == true) go.light.enabled = true;
    }
    void turnOffLight(string obj) {
        GameObject[] gos1 = GameObject.FindGameObjectsWithTag("Light");
        foreach (GameObject go in gos1)
            if (go.name.Contains(obj) == true && !go.name.Contains("Book")) go.light.enabled = false;
    }

    // passar isto para a luz ambiente
    void changeLightColor(string obj) {
        GameObject go = GameObject.Find(obj);
        Color color = Color.black;
        int rndColorNum = rndColor.Next(7);
        int rndIntensityNum = rndIntensity.Next(20);

        switch (rndColorNum) {
            case 0: color = Color.yellow; break;
            case 1: color = Color.blue; break;
            case 2: color = Color.cyan; break;
            case 3: color = Color.gray; break;
            case 4: color = Color.green; break;
            case 5: color = Color.magenta; break;
            case 6: color = Color.white; break;
            default: color = Color.black; break;
        }

        go.light.color = color;
        go.light.intensity = rndIntensityNum / 20.0f;
    }

    void fire(string obj) {
        GameObject go = GameObject.Find(obj);
        GameObject bullet = Instantiate(go, go.transform.position, Quaternion.identity) as GameObject;
        bullet.transform.localScale = new Vector3(1, 1, 1);
        CollisionBullet cb = bullet.GetComponent("CollisionBullet") as CollisionBullet;
        cb.shoot = true;
    }

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Player")) {
            if (this.name.Equals("Room1")) {
                turnOnTorchs("Room1");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Room2")) {
                turnOnTorchs("Room2");
                changeLightColor("AmbientLight");

                ExplosionPS explosion = GameObject.Find("Explosion").GetComponent("ExplosionPS") as ExplosionPS;
                explosion.InitSP();
            }
            if (this.name.Equals("Room3")) {
                turnOnTorchs("Room3");
                changeLightColor("AmbientLight");
            }

            if (this.name.Equals("Hall1")) {
                turnOnTorchs("Hall1");
                turnOnLight("LightSkeleton2");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall2")) {
                turnOnTorchs("Hall2");
                changeLightColor("AmbientLight");
                GameObject[] waters = GameObject.FindGameObjectsWithTag("Water");

                foreach (GameObject g in waters) {
                    Debug.Log(g.name + ": " + g.transform.position);
                    WaterPS water = g.GetComponent("WaterPS") as WaterPS;
                    water.InitSP();
                }
            }
            if (this.name.Equals("Hall3")) {
                turnOnTorchs("Hall3");
                turnOnLight("LightTurret2");
            }
            if (this.name.Equals("Hall4")) {
                turnOnTorchs("Hall4");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall5")) {
                turnOnTorchs("Hall5");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall6")) {
                turnOnTorchs("Hall6");
                turnOnLight("LightTurret1");
            }
            if (this.name.Equals("Hall7")) {
                turnOnTorchs("Hall7");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall8")) {
                turnOnTorchs("Hall8");
                turnOnLight("LightSkeleton1");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall9")) {
                turnOnTorchs("Hall9");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall10")) {
                turnOnTorchs("Hall10");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall11")) {
                turnOnTorchs("Hall11");
                turnOnLight("LightTurret3");
            }
            if (this.name.Equals("Hall12")) {
                turnOnTorchs("Hall12");
                turnOnLight("LightSkeleton3");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall13")) {
                turnOnTorchs("Hall13");
                changeLightColor("AmbientLight");
                //GameState.Instance.EndGame(false);
            }
            if (this.name.Equals("Hall14")) {
                turnOnTorchs("Hall14");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall15")) {
                turnOnTorchs("Hall15");
                turnOnLight("LightTurret4");
            }
            if (this.name.Equals("Hall16")) {
                turnOnTorchs("Hall16");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall17")) {
                turnOnTorchs("Hall17");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall18")) {
                turnOnTorchs("Hall18");
                turnOnLight("LightSkeleton4");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall19")) {
                turnOnTorchs("Hall19");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall20")) {
                turnOnTorchs("Hall20");
                changeLightColor("AmbientLight");
                GameObject[] waters = GameObject.FindGameObjectsWithTag("Water");

                foreach (GameObject g in waters) {
                    Debug.Log(g.name + ": " + g.transform.position);
                    WaterPS water = g.GetComponent("WaterPS") as WaterPS;
                    water.InitSP();
                }
            }
            if (this.name.Equals("Hall21")) {
                turnOnTorchs("Hall21");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall22")) {
                turnOnTorchs("Hall22");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall23")) {
                turnOnTorchs("Hall23");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall24")) {
                turnOnTorchs("Hall24");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall25")) {
                turnOnTorchs("Hall25");
                changeLightColor("AmbientLight");
            }
            if (this.name.Equals("Hall26")) {
                turnOnTorchs("Hall26");
                changeLightColor("AmbientLight");
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
                turnOffLight("LightSkeleton1");
                turnOffTorchs("Hall8");
            }
            if (this.name.Equals("Hall9")) {
                turnOffTorchs("Hall9");
            }
            if (this.name.Equals("Hall10")) {
                turnOffTorchs("Hall10");
            }
            if (this.name.Equals("Hall11")) {
                turnOffTorchs("Hall11");
                turnOffLight("LightTurret3");
            }
            if (this.name.Equals("Hall12")) {
                turnOffLight("LightSkeleton3");
                turnOffTorchs("Hall12");
            }
            if (this.name.Equals("Hall13")) {
                turnOffTorchs("Hall13");
                GameState.Instance.EndGame(false);
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
    void OnTriggerStay(Collider col) {
        if (col.CompareTag("Player")) {
            if (this.name.Equals("Hall3")) {
                if (timeElapsed == 0 || timeElapsed >= 75) {
                    fire("BulletTurret2");
                    timeElapsed = 0;
                }
            }
            if (this.name.Equals("Hall6")) {
                if (timeElapsed == 0 || timeElapsed >= 75) {
                    fire("BulletTurret1");
                    timeElapsed = 0;
                }
            }
            if (this.name.Equals("Hall11")) {
                if (timeElapsed == 0 || timeElapsed >= 75) {
                    fire("BulletTurret3");
                    timeElapsed = 0;
                }
            }
            if (this.name.Equals("Hall15")) {
                if (timeElapsed == 0 || timeElapsed >= 75) {
                    fire("BulletTurret4");
                    timeElapsed = 0;
                }
            }

            timeElapsed++;        }
    }
}
