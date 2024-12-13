using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class huhnWurf : MonoBehaviour
{

    private Touch touch;

    private Vector3 flugrichtung;


    //huhn vom Pool spawnen
    public string spawnObject;
    ObjectPooler objectPooler;

    private int Munition;
    public GameObject BtnReload;


    //animation

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;

        BtnReload.SetActive(false);
        Munition = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosFar = new Vector3(touch.position.x, touch.position.y, Camera.main.farClipPlane);
                Vector3 touchPosNear = new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane);
                Vector3 mousePosF = Camera.main.ScreenToWorldPoint(touchPosFar);
                Vector3 mousePosN = Camera.main.ScreenToWorldPoint(touchPosNear);
                flugrichtung = mousePosF;

                Debug.DrawRay(mousePosN, mousePosF - mousePosN, Color.red);//draws a gizmo for the ray

                RaycastHit hitInfo;

                // Check if the Munition variable is greater than 0
                if (Munition > 0)
                {
                    // Spawn an object using the object pooling system
                    objectPooler.SpawnFromPool(spawnObject, transform.position, Quaternion.identity);

                    // Decrement the Munition variable by 1
                    Munition--;

                    // Perform the raycast
                    if (Physics.Raycast(mousePosN, mousePosF - mousePosN, out hitInfo))
                    {
                        // Get the wurf and wurf2 components attached to the hit object
                        var rig = hitInfo.collider.GetComponent<wurf>();
                        var rig2 = hitInfo.collider.GetComponent<wurf2>();

                        // Call the stirb() function on the components if they are not null
                        if (rig != null)
                        {
                            rig.stirb();
                        }
                        else if (rig2 != null)
                        {
                            rig2.stirb();
                        }
                    }
                }
                else
                {
                    // Activate the BtnReload game object
                    BtnReload.SetActive(true);

                    // Use the LeanTween library to animate the scale of the BtnReload object
                    LeanTween.scale(BtnReload, new Vector3(1.06f, 1.06f, 1.06f), 0.5f).setEase(LeanTweenType.easeOutBack);
                    LeanTween.scale(BtnReload, new Vector3(1f, 1f, 1f), 0.7f).setDelay(0.3f).setEase(LeanTweenType.easeOutBounce);
                }
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Debug.Log("Ok Bye");
            }
        }
    }

    public void Reload() //is connected to a Button
    {
        Munition = 10;
        BtnReload.SetActive(false);
    }


    public Vector3 getflug(Vector3 pflugrichtung)
    {
        pflugrichtung = flugrichtung;
        return pflugrichtung;
    }

    //code from Holistic3D "Understanding Screen and World Coordinates for Raycasting"
}
