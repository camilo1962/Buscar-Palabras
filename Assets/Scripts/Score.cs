using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    int score = 0;
    public float fadeSpeed;
    public float fadeInSpeed;
    public float scoreGhostSpeed;
    Coroutine highlightTextRoutine = null;
    Coroutine scoreGhostFadeInRoutine = null;
    public Game gameScript;
    public GameObject scoreGhostObject;
    public GameObject finalScoreObject;
    public GameObject clockObject;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI finalScoreText;

    public TextMeshProUGUI scoreVal0;
    public TextMeshProUGUI scoreVal1;
    public TextMeshProUGUI scoreVal2;
    public TextMeshProUGUI scoreVal3;
    public TextMeshProUGUI scoreVal4;

    void Start()
    {
        scoreGhostObject.SetActive(false);
        scoreText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        scoreVal0 = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        scoreVal1 = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        scoreVal2 = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        scoreVal3 = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        scoreVal4 = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        finalScoreText = finalScoreObject.GetComponent<TMPro.TextMeshProUGUI>();
        UpdateScoreText();
        TextMeshProUGUI scoreGhostText = scoreGhostObject.GetComponent<TMPro.TextMeshProUGUI>();
        Color newColor = scoreGhostText.color;
        newColor.a = 0f;
        scoreGhostText.color = newColor;
        ActualizarRecors();



    }

    public void ActualizarRecors()
    {       

        int posicionComparada = 1;
        bool seguirBuscando = true;
        while (posicionComparada <= 5 && seguirBuscando)
        {
            if(PlayerPrefs.GetInt("Best" + posicionComparada, -1) < score)
            {
                PlayerPrefs.SetInt("Best" + posicionComparada, score);
                seguirBuscando = false;

            }
            posicionComparada++;
        }

        //scoreVal0.text = PlayerPrefs.GetInt("Best", 0).ToString();
        //scoreVal1.text = PlayerPrefs.GetInt("score2").ToString();
        //scoreVal2.text = PlayerPrefs.GetInt("score3").ToString();
        //scoreVal3.text = PlayerPrefs.GetInt("score4").ToString();
        //scoreVal4.text = PlayerPrefs.GetInt("score5").ToString();
    }

    public void MostrarRecords()
    {
        scoreVal0.text = PlayerPrefs.GetInt("Best" , 1).ToString();
        scoreVal1.text = PlayerPrefs.GetInt("Best" + 2).ToString();
        scoreVal2.text = PlayerPrefs.GetInt("Best" + 3).ToString();
        scoreVal3.text = PlayerPrefs.GetInt("Best" + 4).ToString();
        scoreVal4.text = PlayerPrefs.GetInt("Best" + 5).ToString();



    }

    public void BorraRecord()
    {
        PlayerPrefs.DeleteKey("Best");
        PlayerPrefs.DeleteKey("score1");
        PlayerPrefs.DeleteKey("score2");
        PlayerPrefs.DeleteKey("score3");
        PlayerPrefs.DeleteKey("score4");
    }

    public void CorrectWord(string word)
    {
        if (highlightTextRoutine != null)
        {
            StopCoroutine(highlightTextRoutine);
        }
        highlightTextRoutine = StartCoroutine(HighlightText(Color.white));
        int pointsGained = (int)Mathf.Pow(2, word.Length - 3) * 4;
        score += pointsGained;
        UpdateScoreText();
        scoreGhostObject.GetComponent<TMPro.TextMeshProUGUI>().text = "+" + pointsGained.ToString();
        if (scoreGhostFadeInRoutine != null)
        {
            StopCoroutine(scoreGhostFadeInRoutine);
        }
        scoreGhostFadeInRoutine = StartCoroutine(ScoreGhostFadeIn());
        Debug.Log("Puntos: " + score.ToString() + ", Palabras: " + word);
    }

    public void IncorrectWord()
    {
        if (highlightTextRoutine != null)
        {
            StopCoroutine(highlightTextRoutine);
        }
        highlightTextRoutine = StartCoroutine(HighlightText(gameScript.backdropColor));
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        string text = score.ToString();
        scoreText.text = text.Length == 1 ? "0" + text : text;
        finalScoreText.text = text;
    }

    IEnumerator HighlightText(Color highlightColor)
    {
        clockObject.GetComponent<Timer>().InitHighlightText(fadeSpeed);
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            scoreText.color = Color32.Lerp(scoreText.color, highlightColor, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            scoreText.color = Color32.Lerp(scoreText.color, Color.black, fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator ScoreGhostFadeIn()
    {
        scoreGhostObject.SetActive(true);
        TextMeshProUGUI scoreGhostText = scoreGhostObject.GetComponent<TMPro.TextMeshProUGUI>();
        Color newColor = scoreGhostText.color;
        newColor.a = 0f;
        scoreGhostText.color = newColor;
        RectTransform scoreGhostPos = scoreGhostObject.GetComponent<RectTransform>();
        Vector3 pos = scoreGhostPos.anchoredPosition;
        pos.y = 180;
        scoreGhostPos.anchoredPosition = pos;
        pos.y = 195;
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            newColor = scoreGhostText.color;
            newColor.a = Mathf.Lerp(newColor.a, 1f, fadeInSpeed * Time.deltaTime);
            scoreGhostText.color = newColor;
            scoreGhostPos.anchoredPosition = Vector3.Lerp(
                scoreGhostPos.anchoredPosition, pos, scoreGhostSpeed * Time.deltaTime
            );
            yield return null;
        }
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            newColor = scoreGhostText.color;
            newColor.a = Mathf.Lerp(newColor.a, 0f, fadeSpeed * Time.deltaTime);
            scoreGhostText.color = newColor;
            yield return null;
        }
        newColor.a = 0f;
        scoreGhostText.color = newColor;
        scoreGhostObject.SetActive(false);
    }

    public void Update()
    {

       
       
    }
   

    public void TimeFinished()
    {


        if (score > PlayerPrefs.GetInt("Best", 0))
        {
            PlayerPrefs.SetInt("Best", score);
        }
        PlayerPrefs.SetInt("Latest_Score", score);
        PlayerPrefs.SetInt("Upload_Pending", 1);
        clockObject.GetComponent<Timer>().NewBest();
        this.ActualizarRecors();
#if UNITY_ANDROID
        // do android lb stuff here
#elif UNITY_IPHONE
        // do iphone lb stuff here
#endif
    }

}