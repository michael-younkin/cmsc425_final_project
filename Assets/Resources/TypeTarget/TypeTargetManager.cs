using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TypeTargetManager : MonoBehaviour {

    HashSet<char> validChars = new HashSet<char>(new char[]
    {
        'a',
        'b',
        'c',
        'd',
        'e',
        'f',
        'g',
        'h',
        'i',
        'j',
        'k',
        'l',
        'm',
        'n',
        'o',
        'p',
        'q',
        'r',
        's',
        't',
        'u',
        'v',
        'w',
        'x',
        'y',
        'z',
        '0',
        '1',
        '2',
        '3',
        '4',
        '5',
        '6',
        '7',
        '8',
        '9',
        ' ',
    });

    List<string> words = new List<string>();
    HashSet<TypeTarget> targets = new HashSet<TypeTarget>();

    public void Awake()
    {
        var text = GameUtil.SafeLoad<TextAsset>("TypeTarget/WordData/words");
        var rawWords = text.text.Split('\n');
        foreach (var word in rawWords)
        {
            bool isValid = true;
            foreach (var c in word.ToCharArray())
            {
                if (!validChars.Contains(c))
                {
                    isValid = false;
                    break;
                }
            }
            if (!isValid)
            {
                continue;
            }
            words.Add(word);
        }
    }

    public void Start()
    {
        // Retrieve the type targets in the scene
        foreach (var target in FindObjectsOfType<TypeTarget>())
        {
            targets.Add(target);
        }
        if (targets.Count == 0)
        {
            Debug.LogError("No type targets found.");
        }

        // Assign 
        foreach (var target in targets)
        {
            AssignText(target);
        }
    }

    private string PickRandomWord()
    {
        return words[Random.Range(0, words.Count)];
    }

    private bool IsTextUnique(string text)
    {
        foreach (var target in targets)
        {
            if (target.Text == text)
            {
                return false;
            }
        }
        return true;
    }

    private void AssignText(TypeTarget target)
    {
        var previousText = target.Text;
        var newText = previousText;
        while (newText == previousText || !IsTextUnique(newText))
        {
            newText = PickRandomWord();
        }
        target.Text = newText;
    }
}
