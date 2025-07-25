using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Korb : MonoBehaviour
{
    public float distToGround = 0.5f;
    Rigidbody rb, rb2, rb3, rb4;
    public GameObject sheep, sheep2, sheep3, sheep4;

    //für die speech bubbles
    public int aktuelleSprechblase = 0;
    public GameObject[] bubbles;
    private bool einmal = false;

    // as long as no wall was hit we have a combo
    bool hasCombo;
    public TMP_Text comboText;
    public TMP_Text txtHighestCombo;
    public Image comboImage;
    RectTransform imageTransform;
    float originalY;
    int comboCount;
    int highestCombo;

    int gewinn = 73;

    ScoreManager sm;

    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();

        highestCombo = PlayerPrefs.GetInt("HighestCombo", 0);
        txtHighestCombo.text = highestCombo.ToString() + "X COMBO";

        if(PlayerPrefs.GetInt("AbilityNumber") == 3)
        {
            gewinn = 91;
        }

        comboImage.gameObject.SetActive(false);
        imageTransform = comboImage.GetComponent<RectTransform>();
        originalY = imageTransform.anchoredPosition.y;

        //alle sprechblasen verstecken
        foreach (GameObject bubble in bubbles)
        {
            bubble.SetActive(false);
        }

        //für die schafe
        rb = sheep.GetComponent<Rigidbody>();
        rb2 = sheep2.GetComponent<Rigidbody>();
        rb3 = sheep3.GetComponent<Rigidbody>();
        rb4 = sheep4.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(sm != null)
        {
            sm.AddPoint(gewinn);
            Combo();
        }

        Jump();
        if (einmal == false)
        {
            StartCoroutine(bubbleAktivieren());
        }
    }

    void Combo()
    {
        if (hasCombo)
        {
            comboCount++;
            ComboImageTrigger();
            comboImage.gameObject.SetActive(true);

            //check for the combo is higher than the higfhest for it to be the new highscore
            if(comboCount > highestCombo) {
                highestCombo = comboCount;
                txtHighestCombo.text = highestCombo.ToString() + "X COMBO";
            }
        }
        hasCombo = true;
    }

    void ComboImageTrigger()
    {
        comboText.text = comboCount.ToString() + "X\nCOMBO";

        LeanTween.cancel(gameObject);

        // Startposition zurücksetzen
        imageTransform.anchoredPosition = new Vector2(imageTransform.anchoredPosition.x, originalY); // originalY sollte die Start-Y-Position des Bildes sein

        // Startposition beibehalten, Opacity auf 1 setzen
        comboImage.color = new Color(comboImage.color.r, comboImage.color.g, comboImage.color.b, 1);

        // Zielposition festlegen (nach oben aus dem Bild)
        float targetY = imageTransform.anchoredPosition.y + 200; // Passe den Wert an je nach Bedarf

        // Bewegung nach oben und Opacity reduzieren
        LeanTween.moveY(imageTransform, targetY, 2f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.alpha(comboImage.rectTransform, 0f, 2f).setEase(LeanTweenType.easeInOutQuad);
    }

    /// <summary>
    /// Adss the combo to the points and sets it back to 0
    /// </summary>
    public void ResetCombo()
    {
        hasCombo = false;
        sm.AddPoint(23 *  comboCount);
        comboCount = 0;
    }

    public void Jump()
    {
        Vector3 jumpVelocity = new Vector3(0, 2f, 0);
        rb.velocity = rb.velocity + jumpVelocity;
        rb2.velocity = rb.velocity + jumpVelocity;
        rb3.velocity = rb.velocity + jumpVelocity;
        rb4.velocity = rb.velocity + jumpVelocity;
    }

    IEnumerator bubbleAktivieren()
    {
        einmal = true;
        aktuelleSprechblase = Random.Range(0, bubbles.Length +1);
        bubbles[aktuelleSprechblase].SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        bubbles[aktuelleSprechblase].SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        einmal = false;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("HighestCombo", highestCombo);
    }
}
