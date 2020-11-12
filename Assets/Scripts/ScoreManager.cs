using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager S;

    [Header("Set in Inspector")]
    public List<float> scoreFontSizes = new List<float> { 36, 64, 64, 1 };
    public Vector3 scoreMidPoint = new Vector3(1, 1, 0);
    public float scoreTravelTime = 3f;
    public float scoreComboDelay = 0.5f;

    private RectTransform rectTrans;

    void Awake()
    {
        S = this;
        rectTrans = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SCORE(Wyrd wyrd, int combo)
    {
        S.Score(wyrd, combo);
    }

    private void Score(Wyrd wyrd, int combo)
    {
        // create a List of Vector2 Bezier points for FloatingScore
        var pts = new List<Vector2>();

        var pt = wyrd.letters[0].transform.position;
        pt = Camera.main.WorldToViewportPoint(pt);
        pts.Add(pt); // make first Bezier point

        // add a second Bezier point
        pts.Add(scoreMidPoint);

        // make the Scoreboard the last Bezier point
        pts.Add(rectTrans.anchorMax);

        // Set the value of the Floating Score
        var value = wyrd.letters.Count * combo;
        var fs = Scoreboard.S.CreateFloatingScore(value, pts);

        fs.timeDuration = scoreTravelTime;
        fs.timeStart = Time.time + combo * scoreComboDelay;
        fs.fontSizes = scoreFontSizes;

        // Double the InOut Easing effect
        fs.easingCurve = Easing.InOut + Easing.InOut;

        // make the text of the FloatingScore something like "3 x 2"
        var txt = wyrd.letters.Count.ToString();
        if (combo > 1)
            txt += " x " + combo;

        fs.GetComponent<Text>().text = txt;
    }
}
