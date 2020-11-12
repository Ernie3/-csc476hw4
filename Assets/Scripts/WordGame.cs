using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public enum GameMode
{
    preGame, // before game starts
    loading, // word list is loading and being parse
    makeLevel, // individual WordLevel is being created
    levelPrep, // level visuals are instantiated
    inLevel // level is in progress
}

public class WordGame : MonoBehaviour
{
    public static WordGame S;

    [Header("Set Dynamically")]
    public GameMode mode = GameMode.preGame;
    public WordLevel currLevel;

    void Awake()
    {
        S = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mode = GameMode.loading;
        WordList.INIT();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WordListParseComplete()
    {
        mode = GameMode.makeLevel;
        currLevel = MakeWordLevel();
    }

    public WordLevel MakeWordLevel(int levelNum = -1)
    {
        var level = new WordLevel();
        if(levelNum == -1)
        {
            level.longWordIndex = Random.Range(0, WordList.LONG_WORD_COUNT);
        }
        else
        {
            // TODO: this case
        }

        level.levelNum = levelNum;
        level.word = WordList.GET_LONG_WORD(level.longWordIndex);
        level.charDict = WordLevel.MakeCharDict(level.word);

        StartCoroutine(FindSubWordsCoroutine(level));

        return level;
    }

    public IEnumerator FindSubWordsCoroutine(WordLevel level)
    {
        level.subWords = new List<string>();
        string str;

        var words = WordList.GET_WORDS();

        for (var i = 0; i < WordList.WORD_COUNT; i++)
        {
            str = words[i];

            if (WordLevel.CheckWordInLevel(str, level))
                level.subWords.Add(str);

            if (i % WordList.NUM_TO_PARSE_BEFORE_YIELD == 0)
                yield return null;
        }

        level.subWords.Sort();
        level.subWords = SortWordsByLength(level.subWords).ToList();

        SubWordSearchComplete();
    }

    public static IEnumerable<string> SortWordsByLength(IEnumerable<string> ws)
    {
        return ws.OrderBy(s => s.Length);
    }

    public void SubWordSearchComplete()
    {
        mode = GameMode.levelPrep;
    }
}
