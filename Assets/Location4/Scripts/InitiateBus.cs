using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateBus : MonoBehaviour
{
    //public GameObject Bus, Bus2, Bus3;

    ObjectPooler objectPooler; //das selbe wie gameobject bus nur im pool
    public string FirstBus;
    public string SecondBus;
    public string ThirdBus;

    public float respawnTime = 1.0f;
    public float deleteTime = 10.0f;
    private bool gameIsRunning = true;

    private int busNummer; //event nummer

    //um die zeit zu beschleunigen
    [Range(0.1f, 3)]
    public float modifiedScale;
    [SerializeField] private float increaseScale;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;

        if(PlayerPrefs.GetInt("AbilityNumber", 0) == 2)
        {
            increaseScale= 0.004f;
            Debug.Log("AbilityCross Ja");
        }
        else
        {
            increaseScale = 0.006f;
            Debug.Log("AbilityCross Nein");
        }
        

        //das spiel läuft verlangsamt
        modifiedScale = 0.7f;
        spawnBus();
        StartCoroutine(busSpawnen());
    }

    // Update is called once per frame
    void Update()
    {
        //die zeit wird beschleunigt solange sie sich unter dreifacher geschwindigkeit befindet
        if (modifiedScale < 3f)
        {
            modifiedScale += increaseScale * Time.deltaTime;
        }
        Time.timeScale = modifiedScale;
    }

    private void spawnBus()
    {
        //bus nummer entscheidet, welches der szenarios gewählt wird
        // 4 busse spawnen hintereinander in der reihenfolge die ein event erstellt 
        // bus1 muss immer in der mitte spawnen, damit keine unüberwindbaren lücken entstehen
        //bus 4 ist kürzer als alle anderen busse, weshalb er weiter vorne spawnen muss
        //nach bus 4 muss immer irgendwann bus 3 kommen (dieser bus hat am ende ein geschlossenes dach)
        busNummer = Random.Range(1, 9); //events die eine varietät für die busse erstellen

        /*switch(busNummer)
        {
            case 1:
                GameObject a = Instantiate(Bus) as GameObject;
                a.transform.position = new Vector3(0, -2, 25.3f);
                GameObject b = Instantiate(Bus) as GameObject;
                b.transform.position = new Vector3(-6, -2, 40);
                GameObject c = Instantiate(Bus) as GameObject;
                c.transform.position = new Vector3(0, -2, 54.3f);
                GameObject d = Instantiate(Bus2) as GameObject;
                d.transform.position = new Vector3(6, -2, 68.8f);
                GameObject e = Instantiate(Bus2) as GameObject;
                e.transform.position = new Vector3(0, -2, 83.1f);
                GameObject f = Instantiate(Bus2) as GameObject;
                f.transform.position = new Vector3(-6, -2, 97.4f);
                GameObject g = Instantiate(Bus) as GameObject;
                g.transform.position = new Vector3(0, -2, 111.7f);
                GameObject h = Instantiate(Bus2) as GameObject;
                h.transform.position = new Vector3(6, -2, 126f);
                Destroy(a, deleteTime - 20);
                Destroy(b, deleteTime - 17);
                Destroy(c, deleteTime - 14);
                Destroy(d, deleteTime - 10);
                Destroy(e, deleteTime - 6);
                Destroy(f, deleteTime - 3);
                Destroy(g, deleteTime - 1);
                Destroy(h, deleteTime);
                break;

            case 2:
                GameObject a1 = Instantiate(Bus) as GameObject;
                a1.transform.position = new Vector3(0, -2, 25.3f);
                GameObject b1 = Instantiate(Bus2) as GameObject;
                b1.transform.position = new Vector3(-6, -2, 40);
                GameObject c1 = Instantiate(Bus) as GameObject;
                c1.transform.position = new Vector3(0, -2, 54.3f);
                GameObject d1 = Instantiate(Bus3) as GameObject;
                d1.transform.position = new Vector3(-6, -2, 68.8f);
                GameObject f1 = Instantiate(Bus2) as GameObject;
                f1.transform.position = new Vector3(0, -2, 97.4f);
                GameObject g1 = Instantiate(Bus3) as GameObject;
                g1.transform.position = new Vector3(6, -2, 111.7f);
                Destroy(a1, deleteTime - 20);
                Destroy(b1, deleteTime - 17);
                Destroy(c1, deleteTime - 14);
                Destroy(d1, deleteTime - 8);
                Destroy(f1, deleteTime - 3);
                Destroy(g1, deleteTime - 1);
                break;

                case 3:
                GameObject a2 = Instantiate(Bus2) as GameObject;
                a2.transform.position = new Vector3(0, -2, 25.3f);
                GameObject b2 = Instantiate(Bus2) as GameObject;
                b2.transform.position = new Vector3(6, -2, 40);
                GameObject k2 = Instantiate(Bus) as GameObject;
                k2.transform.position = new Vector3(-6, -2, 40);
                GameObject c2 = Instantiate(Bus3) as GameObject;
                c2.transform.position = new Vector3(0, -2, 54.3f);
                GameObject e2 = Instantiate(Bus) as GameObject;
                e2.transform.position = new Vector3(6, -2, 83.1f);
                GameObject f2 = Instantiate(Bus2) as GameObject;
                f2.transform.position = new Vector3(0, -2, 97.4f);
                GameObject g2 = Instantiate(Bus3) as GameObject;
                g2.transform.position = new Vector3(-6, -2, 111.7f);
                Destroy(a2, deleteTime - 20);
                Destroy(b2, deleteTime - 17);
                Destroy(k2, deleteTime - 17);
                Destroy(c2, deleteTime - 10);
                Destroy(e2, deleteTime - 6);
                Destroy(f2, deleteTime - 3);
                Destroy(g2, deleteTime);
                break;

            case 4:
                GameObject a3 = Instantiate(Bus3) as GameObject;
                a3.transform.position = new Vector3(0, -2, 25.3f);
                GameObject c3 = Instantiate(Bus) as GameObject;
                c3.transform.position = new Vector3(-6, -2, 54.3f);
                GameObject d3 = Instantiate(Bus) as GameObject;
                d3.transform.position = new Vector3(0, -2, 68.8f);
                GameObject e3 = Instantiate(Bus2) as GameObject;
                e3.transform.position = new Vector3(-6, -2, 83.1f);
                GameObject k3 = Instantiate(Bus2) as GameObject;
                k3.transform.position = new Vector3(6, -2, 83.1f);
                GameObject f3 = Instantiate(Bus3) as GameObject;
                f3.transform.position = new Vector3(0, -2, 97.4f);
                GameObject h3 = Instantiate(Bus) as GameObject;
                h3.transform.position = new Vector3(6, -2, 126f);
                Destroy(a3, deleteTime - 18);
                Destroy(c3, deleteTime - 14);
                Destroy(d3, deleteTime - 10);
                Destroy(e3, deleteTime - 6);
                Destroy(k3, deleteTime - 6);
                Destroy(f3, deleteTime);
                Destroy(h3, deleteTime);
                break;

            case 5:
                GameObject a4 = Instantiate(Bus2) as GameObject;
                a4.transform.position = new Vector3(0, -2, 25.3f);
                GameObject b4 = Instantiate(Bus2) as GameObject;
                b4.transform.position = new Vector3(6, -2, 40);
                GameObject c4 = Instantiate(Bus) as GameObject;
                c4.transform.position = new Vector3(0, -2, 54.3f);
                GameObject d4 = Instantiate(Bus3) as GameObject;
                d4.transform.position = new Vector3(-6, -2, 68.8f);
                GameObject k4 = Instantiate(Bus3) as GameObject;
                k4.transform.position = new Vector3(6, -2, 68.8f);
                GameObject f4 = Instantiate(Bus2) as GameObject;
                f4.transform.position = new Vector3(0, -2, 97.4f);
                GameObject h4 = Instantiate(Bus3) as GameObject;
                h4.transform.position = new Vector3(6, -2, 111.7f);
                Destroy(a4, deleteTime - 20);
                Destroy(b4, deleteTime - 17);
                Destroy(c4, deleteTime - 14);
                Destroy(d4, deleteTime - 8);
                Destroy(k4, deleteTime - 8);
                Destroy(f4, deleteTime - 3);
                Destroy(h4, deleteTime);
                break;

            case 6:
                GameObject a5 = Instantiate(Bus) as GameObject;
                a5.transform.position = new Vector3(0, -2, 25.3f);
                GameObject b5 = Instantiate(Bus2) as GameObject;
                b5.transform.position = new Vector3(6, -2, 40);
                GameObject c5 = Instantiate(Bus) as GameObject;
                c5.transform.position = new Vector3(0, -2, 54.3f);
                GameObject d5 = Instantiate(Bus) as GameObject;
                d5.transform.position = new Vector3(6, -2, 68.8f);
                GameObject e5 = Instantiate(Bus2) as GameObject;
                e5.transform.position = new Vector3(0, -2, 83.1f);
                GameObject f5 = Instantiate(Bus) as GameObject;
                f5.transform.position = new Vector3(6, -2, 97.4f);
                GameObject k5 = Instantiate(Bus2) as GameObject;
                k5.transform.position = new Vector3(-6, -2, 97.4f);
                GameObject g5 = Instantiate(Bus2) as GameObject;
                g5.transform.position = new Vector3(0, -2, 111.7f);
                GameObject h5 = Instantiate(Bus) as GameObject;
                h5.transform.position = new Vector3(-6, -2, 126f);
                Destroy(a5, deleteTime - 20);
                Destroy(b5, deleteTime - 17);
                Destroy(c5, deleteTime - 14);
                Destroy(d5, deleteTime - 10);
                Destroy(e5, deleteTime - 6);
                Destroy(f5, deleteTime - 3);
                Destroy(k5, deleteTime - 3);
                Destroy(g5, deleteTime - 1);
                Destroy(h5, deleteTime);
                break;

            case 7:
                GameObject rudolf = Instantiate(Bus2) as GameObject;
                rudolf.transform.position = new Vector3(0, -2, 25.3f);
                GameObject b6 = Instantiate(Bus3) as GameObject;
                b6.transform.position = new Vector3(6, -2, 40);
                GameObject d6 = Instantiate(Bus) as GameObject;
                d6.transform.position = new Vector3(0, -2, 68.8f);
                GameObject e6 = Instantiate(Bus) as GameObject;
                e6.transform.position = new Vector3(6, -2, 83.1f);
                GameObject k6 = Instantiate(Bus) as GameObject;
                k6.transform.position = new Vector3(-6, -2, 83.1f);
                GameObject f6 = Instantiate(Bus2) as GameObject;
                f6.transform.position = new Vector3(0, -2, 97.4f);
                GameObject g6 = Instantiate(Bus3) as GameObject;
                g6.transform.position = new Vector3(6, -2, 111.7f);
                GameObject l6 = Instantiate(Bus3) as GameObject;
                l6.transform.position = new Vector3(-6, -2, 111.7f);
                Destroy(rudolf, deleteTime - 20);
                Destroy(b6, deleteTime - 15);
                Destroy(d6, deleteTime - 10);
                Destroy(e6, deleteTime - 6);
                Destroy(k6, deleteTime - 6);
                Destroy(f6, deleteTime - 3);
                Destroy(g6, deleteTime);
                break;

            case 8:
                GameObject a7 = Instantiate(Bus) as GameObject;
                a7.transform.position = new Vector3(0, -2, 25.3f);
                GameObject b7 = Instantiate(Bus) as GameObject;
                b7.transform.position = new Vector3(-6, -2, 40);
                GameObject c7 = Instantiate(Bus3) as GameObject;
                c7.transform.position = new Vector3(0, -2, 54.3f);
                GameObject e7 = Instantiate(Bus2) as GameObject;
                e7.transform.position = new Vector3(-6, -2, 83.1f);
                GameObject k7 = Instantiate(Bus2) as GameObject;
                k7.transform.position = new Vector3(6, -2, 83.1f);
                GameObject f7 = Instantiate(Bus) as GameObject;
                f7.transform.position = new Vector3(0, -2, 97.4f);
                GameObject g7 = Instantiate(Bus3) as GameObject;
                g7.transform.position = new Vector3(6, -2, 111.7f);
                Destroy(a7, deleteTime - 20);
                Destroy(b7, deleteTime - 17);
                Destroy(c7, deleteTime - 12);
                Destroy(e7, deleteTime - 6);
                Destroy(k7, deleteTime - 6);
                Destroy(f7, deleteTime - 3);
                Destroy(g7, deleteTime);
                break;

        }
        */

        switch (busNummer)
        {
            case 1:
                objectPooler.SpawnFromPool(FirstBus, new Vector3(0, -2, 25.3f), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(-6, -2, 40), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(0, -2, 54.3f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(6, -2, 68.8f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(0, -2, 83.1f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(-6, -2, 97.4f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(0, -2, 111.7f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(6, -2, 126f), Quaternion.identity);

                break;

            case 2:
                objectPooler.SpawnFromPool(FirstBus, new Vector3(0, -2, 25.3f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(-6, -2, 40), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(0, -2, 54.3f), Quaternion.identity);
                objectPooler.SpawnFromPool(ThirdBus, new Vector3(-6, -2, 68.8f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(0, -2, 97.4f), Quaternion.identity);
                objectPooler.SpawnFromPool(ThirdBus, new Vector3(6, -2, 111.7f), Quaternion.identity);

                break;

            case 3:
                objectPooler.SpawnFromPool(SecondBus, new Vector3(0, -2, 25.3f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(6, -2, 40), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(-6, -2, 40), Quaternion.identity);
                objectPooler.SpawnFromPool(ThirdBus, new Vector3(0, -2, 54.3f), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(6, -2, 83.1f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(0, -2, 97.4f), Quaternion.identity);
                objectPooler.SpawnFromPool(ThirdBus, new Vector3(-6, -2, 111.7f), Quaternion.identity);

                break;

            case 4:
                objectPooler.SpawnFromPool(ThirdBus, new Vector3(0, -2, 25.3f), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(-6, -2, 54.3f), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(0, -2, 68.8f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(-6, -2, 83.1f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(6, -2, 83.1f), Quaternion.identity);
                objectPooler.SpawnFromPool(ThirdBus, new Vector3(0, -2, 97.4f), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(6, -2, 126f), Quaternion.identity);

                break;

            case 5:
                objectPooler.SpawnFromPool(SecondBus, new Vector3(0, -2, 25.3f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(6, -2, 40), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(0, -2, 54.3f), Quaternion.identity);
                objectPooler.SpawnFromPool(ThirdBus, new Vector3(-6, -2, 68.8f), Quaternion.identity);
                objectPooler.SpawnFromPool(ThirdBus, new Vector3(6, -2, 68.8f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(0, -2, 97.4f), Quaternion.identity);
                objectPooler.SpawnFromPool(ThirdBus, new Vector3(6, -2, 111.7f), Quaternion.identity);

                break;

            case 6:
                objectPooler.SpawnFromPool(FirstBus, new Vector3(0, -2, 25.3f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(6, -2, 40), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(0, -2, 54.3f), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(-6, -2, 68.8f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(0, -2, 83.1f), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(6, -2, 97.4f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(-6, -2, 97.4f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(0, -2, 111.7f), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(-6, -2, 126f), Quaternion.identity);

                break;

            case 7:
                objectPooler.SpawnFromPool(SecondBus, new Vector3(0, -2, 25.3f), Quaternion.identity);
                objectPooler.SpawnFromPool(ThirdBus, new Vector3(6, -2, 40), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(0, -2, 68.8f), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(6, -2, 83.1f), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(-6, -2, 83.1f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(0, -2, 97.4f), Quaternion.identity);
                objectPooler.SpawnFromPool(ThirdBus, new Vector3(6, -2, 111.7f), Quaternion.identity);
                objectPooler.SpawnFromPool(ThirdBus, new Vector3(-6, -2, 111.7f), Quaternion.identity);

                break;

            case 8:
                objectPooler.SpawnFromPool(FirstBus, new Vector3(0, -2, 25.3f), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(-6, -2, 40), Quaternion.identity);
                objectPooler.SpawnFromPool(ThirdBus, new Vector3(0, -2, 54.3f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(-6, -2, 83.1f), Quaternion.identity);
                objectPooler.SpawnFromPool(SecondBus, new Vector3(6, -2, 83.1f), Quaternion.identity);
                objectPooler.SpawnFromPool(FirstBus, new Vector3(0, -2, 97.4f), Quaternion.identity);
                objectPooler.SpawnFromPool(ThirdBus, new Vector3(6, -2, 111.7f), Quaternion.identity);

                break;

        }
        Debug.Log(busNummer);
    }

    IEnumerator busSpawnen()
    {
        while (gameIsRunning == true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnBus();
        }
    }
}
