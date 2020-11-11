using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordList : MonoBehaviour
{
    private static WordList S;

    [Header("Set in Inspector")]
    public TextAsset wordListText;
    public int numToParseBeforeYield = 10000;
    public int wordLengthMin = 3;
    public int wordLengthMax = 7;

    [Header("Set Dynamically")]
    public int currLine;
    public int totalLines;
    public int longWordCount;
    public int wordCount;

    private string[] lines;
    private List<string> longWords;
    private List<string> words;

    void Awake()
    {
        S = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        lines = wordListText.text.Split('\n');
        totalLines = lines.Length;
        StartCoroutine(ParseLines());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ParseLines()
    {
        string word;
        longWords = new List<string>();
        words = new List<string>();

        for (currLine = 0; currLine < totalLines; currLine++)
        {
            word = lines[currLine];

            if (word.Length == wordLengthMax)
                longWords.Add(word);

            if (word.Length >= wordLengthMin && word.Length <= wordLengthMax)
                words.Add(word);

            // determine whether the coroutine should yield
            if (currLine % numToParseBeforeYield == 0)
            {
                longWordCount = longWords.Count;
                wordCount = words.Count;

                yield return null;
            }
        }

        longWordCount = longWords.Count;
        wordCount = words.Count;
    }

    public static List<string> GET_WORDS()
    {
        return S.words;
    }

    public static string GET_WORD(int index)
    {
        return S.words[index];
    }

    public static List<string> GET_LONG_WORDS()
    {
        return S.longWords;
    }

    public static string GET_LONG_WORD(int index)
    {
        return S.longWords[index];
    }

    public static int WORD_COUNT => S.wordCount;
    public static int LONG_WORD_COUNT => S.longWordCount;
    public static int NUM_TO_PARSE_BEFORE_YIELD => S.numToParseBeforeYield;
    public static int WORD_LENGTH_MIN => S.wordLengthMin;
    public static int WORD_LENGTH_MAX => S.wordLengthMax;
}
