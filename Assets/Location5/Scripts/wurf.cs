using System.Collections;
using UnityEngine;

public class wurf : MonoBehaviour
{
    ObjectPooler objectPooler;//für die bullets

    public GameObject Schaf; //das schaf wird an und aus gemacht 
    Vector3 neuePosition;
    Vector3 altePosition;

    public GameObject wurfObject; //sowas wie eine bullet

    private GameObject punkteText;

    private bool geworfen;

    public bool amLeben = true;
    public bool versteckt = true;

    float versteckZeit;
    float hervorkommZeit = 2f;

    private int interval = 7;

    ScoreManager sm;

    
    void Start()
    {
        sm = ScoreManager.Instance;

        objectPooler = ObjectPooler.Instance;

        punkteText = GameObject.FindGameObjectWithTag("TxtPunkte");

        neuePosition = Schaf.transform.position + new Vector3(0, -1, 0); //errechnet die neue position aus der position des schafes und einem 3d vector, der das schaf -10 auf der y achse verschiebt
        altePosition = Schaf.transform.position;

        versteckt = false;

        versteckZeit = Random.Range(5.0f, 10.0f);

        Verstecken();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % interval == 0)
        {
            //ermittelt, wie lange sich versteckt werden soll
            versteckZeit = Random.Range(4.0f, 7.0f);
        }

        if(!geworfen)
        {
            Schiessen();
        }
    }

    private void Verstecken()
    {
        if(!versteckt)
        {
            //das objekt soll für den spieler unzugänglich sein zum abschießen
            versteckt = true;

            geworfen = false;

            Schaf.transform.position = neuePosition;

            Invoke(nameof(Hervorkommen), versteckZeit);
        }
    }
    private void Hervorkommen()
    {
        if(amLeben)
        {
            versteckt = false;

            Schaf.transform.position = altePosition;

            //gameObject.GetComponent<MeshRenderer>().material = normalMaterial;
        }
        Invoke(nameof(Verstecken), hervorkommZeit);
    }

    void Schiessen()
    {
        if(amLeben && !versteckt)
        {
            geworfen = true;

            objectPooler.SpawnFromPool("Bullet", transform.position, Quaternion.identity);
        }
    }

    public void stirb()
    {
        if(!versteckt && amLeben)
        {
            Verstecken();

            StartCoroutine(Sterben());

            LeanTween.scale(punkteText, new Vector3(1.08f, 1.08f, 1.08f), 0.6f).setEase(LeanTweenType.easeOutBounce);

            if(sm != null)
            {
                sm.AddPoint(23);
            }

            LeanTween.scale(punkteText, new Vector3(1f, 1f, 1f), 0.3f).setDelay(0.3f).setEase(LeanTweenType.easeOutBack);

        }
    }

    IEnumerator Sterben()
    {
        amLeben = false;
        yield return new WaitForSeconds(20f);
        amLeben = true;
    }


    //Code von: Alexander Zotov auf Youtube "How to make an enemy to aim and to fire bullet towards player position"
}
