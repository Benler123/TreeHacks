using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaptionTester : MonoBehaviour
{
    [SerializeField] private CaptionManager captionManager;
    [SerializeField] private float wordDelay = 0.5f;
    
    private string[] testWords = new string[] {
        "Welcome", "to test multiple words", "the", "virtual", "reality", "meeting", "where", 
        "we", "will", "discuss", "important", "topics", "and", "share", 
        "ideas", "about", "our", "project", "and", "future", "plans", "for", "the", "company", "and", "our", "team", "members", "and", "how", "we", "can", "work", "together", "to", "achieve", "our", "goals", "and", "make", "a", "difference", "in", "the", "world"
    };
    
    private int currentWord = 0;
    
    private void Start()
    {
        print("STARTING IN CAPTIONTEST");
        StartCoroutine(SendWords());
    }
    
    private IEnumerator SendWords()
    {
        while (true)
        {
            captionManager.AddNewWords(testWords[currentWord]);
            print("adding word: " + testWords[currentWord]);
            currentWord = (currentWord + 1) % testWords.Length;
            yield return new WaitForSeconds(wordDelay);
        }
    }
}