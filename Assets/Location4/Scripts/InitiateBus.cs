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
