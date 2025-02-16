// using UnityEngine;
// using TMPro;
// using System.Collections;
// using System.Collections.Generic;

// public class CaptionManager : MonoBehaviour
// {
//     [Header("UI References")]
//     [SerializeField] private TMP_Text line1Text;
//     [SerializeField] private TMP_Text line2Text;
//     [SerializeField] private TMP_Text overflowTester; // Invisible text component for testing overflow

    
//     [Header("Configuration")]
//     [SerializeField] private float scrollDuration = 0.25f;
    
//     private Queue<string> pendingWords = new Queue<string>();
//     private bool isScrolling = false;

//     private void Start()
//     {
//         // Initialize UI components
//         InitializeUI();
        
//         // Set initial text states
//         line1Text.text = "";
//         line2Text.text = "";
//         overflowTester.text = "";
//     }

//     private void InitializeUI()
//     {
//         // Configure text components
//         line1Text.color = Color.white;
//         line2Text.color = Color.white;
//         line1Text.enableWordWrapping = false;
//         line2Text.enableWordWrapping = false;

//         // Configure overflow tester (make it invisible but identical in properties)
//         overflowTester.color = new Color(0, 0, 0, 0);
//         overflowTester.enableWordWrapping = false;
//         overflowTester.rectTransform.sizeDelta = line1Text.rectTransform.sizeDelta;

//         RectTransform rectTransform = overflowTester.GetComponent<RectTransform>();
//         print($"Width: {rectTransform.rect.width}, Height: {rectTransform.rect.height}");
//         print($"Font Size: {overflowTester.fontSize}");
//     }

//     public void AddNewWords(string newWords)
//     {
//         // Add new words to the queue
//         pendingWords.Enqueue(newWords);
        
//         // Process immediately if we're not currently scrolling
//         if (!isScrolling)
//         {
//             ProcessNextWords();
//         }
//     }

//     private void ProcessNextWords()
//     {
//         if (pendingWords.Count == 0) return;

//         string newWords = pendingWords.Dequeue();

//         // Test if adding the word to line 2 would cause overflow
//         overflowTester.text = line2Text.text + " " + newWords;
//         // overflowTester.text = line2Text.text + " " + newWords;
//         print("Overflow tester: " + overflowTester.text);
//         overflowTester.ForceMeshUpdate();

//         print($"Text: '{overflowTester.text}', Length: {overflowTester.text.Length}");
//         print($"Is Overflowing: {overflowTester.isTextOverflowing}");
//         print($"Preferred Width: {overflowTester.preferredWidth}, Rect Width: {overflowTester.rectTransform.rect.width}");

//         if (overflowTester.isTextOverflowing)
//         {
//             print("OVERFLOWING. Scrolling now");
//             // If it would overflow, scroll lines up
//             StartCoroutine(ScrollLines(newWords));
//         }
//         else
//         {
//             // If line 2 is empty, try to add to line 1
//             if (string.IsNullOrEmpty(line2Text.text))
//             {
//                 print("Nothing in line 2, adding to line 1");
//                 // Test line 1 for overflow
//                 overflowTester.text = line1Text.text + " " + newWords;
//                 overflowTester.ForceMeshUpdate();


//                 if (overflowTester.isTextOverflowing)
//                 {
//                     // If line 1 would overflow, move to line 2
//                     print("Line 1 overflows, moving to line 2");
//                     line2Text.text = newWords;
//                     print("Line 2: " + line2Text.text);
//                 }
//                 else
//                 {
//                     // Add to line 1 if no overflow
//                     print("Adding to line 1");
//                     line1Text.text += (line1Text.text.Length > 0 ? " " : "") + newWords;
//                     print("Line 2: " + line2Text.text);
//                 }
//             }
//             else
//             {
//                 // Add to line 2 if no overflow
//                 line2Text.text += (line2Text.text.Length > 0 ? " " : "") + newWords;
//             }
//         }
        
//     }

//     private IEnumerator ScrollLines(string newWords)
//     {
//         isScrolling = true;

//         // Store original positions
//         Vector2 line1StartPos = line1Text.rectTransform.anchoredPosition;
//         Vector2 line2StartPos = line2Text.rectTransform.anchoredPosition;
//         Vector2 line1EndPos = line1StartPos + Vector2.up * line1Text.preferredHeight;
//         Vector2 line2EndPos = line2StartPos + Vector2.up * line2Text.preferredHeight;

//         // Prepare new text
//         string oldLine1 = line1Text.text;
//         string oldLine2 = line2Text.text;
        
//         float elapsed = 0f;
        
//         // Animate the scroll
//         while (elapsed < scrollDuration)
//         {
//             elapsed += Time.deltaTime;
//             float t = elapsed / scrollDuration;
            
//             // Smooth step for more natural motion
//             float smoothT = Mathf.SmoothStep(0f, 1f, t);
            
//             // Move lines up
//             line1Text.rectTransform.anchoredPosition = Vector2.Lerp(line1StartPos, line1EndPos, smoothT);
//             line2Text.rectTransform.anchoredPosition = Vector2.Lerp(line2StartPos, line2EndPos, smoothT);
            
//             // Fade out top line
//             line1Text.alpha = 1 - smoothT;
            
//             yield return null;
//         }

//         // Update text and reset positions
//         line1Text.text = oldLine2;
//         line2Text.text = newWords;
//         line1Text.rectTransform.anchoredPosition = line1StartPos;
//         line2Text.rectTransform.anchoredPosition = line2StartPos;
//         line1Text.alpha = 1f;

//         isScrolling = false;
        
//         // Process any pending words
//         if (pendingWords.Count > 0)
//         {
//             ProcessNextWords();
//         }
//     }
// }

// using UnityEngine;
// using TMPro;
// using System.Collections;

// public class CaptionManager : MonoBehaviour
// {
//     [Header("UI References")]
//     [SerializeField] private TMP_Text visibleLine1;
//     [SerializeField] private TMP_Text visibleLine2;
//     [SerializeField] private TMP_Text overflowTester; // Invisible text component for testing overflow

//     [Header("Configuration")]
//     [SerializeField] private float scrollDuration = 0.25f;

//     private bool isScrolling = false;

//     private void Start()
//     {
//         InitializeUI();
//         print("STARTING");
//     }

//     private void InitializeUI()
//     {
//         // Configure visible text components
//         visibleLine1.color = Color.white;
//         visibleLine2.color = Color.white;
//         visibleLine1.enableWordWrapping = false;
//         visibleLine2.enableWordWrapping = false;

//         // Configure overflow tester (make it invisible but identical in properties)
//         overflowTester.color = new Color(0, 0, 0, 0);
//         overflowTester.enableWordWrapping = false;
//         overflowTester.rectTransform.sizeDelta = visibleLine1.rectTransform.sizeDelta;

//         // Clear initial text
//         visibleLine1.text = "";
//         visibleLine2.text = "";
//         overflowTester.text = "";
//     }

//     public void AddWord(string word)
//     {
//         if (isScrolling) return;
//         print("adding word in captionmanager: " + word);

//         // Test if adding the word to line 2 would cause overflow
//         overflowTester.text = visibleLine2.text + " " + word;
//         overflowTester.ForceMeshUpdate();

//         if (overflowTester.isTextOverflowing)
//         {
//             // If it would overflow, scroll lines up
//             StartCoroutine(ScrollLines(word));
//         }
//         else
//         {
//             // If line 2 is empty, try to add to line 1
//             if (string.IsNullOrEmpty(visibleLine2.text))
//             {
//                 // Test line 1 for overflow
//                 overflowTester.text = visibleLine1.text + " " + word;
//                 overflowTester.ForceMeshUpdate();

//                 if (overflowTester.isTextOverflowing)
//                 {
//                     // If line 1 would overflow, move to line 2
//                     visibleLine2.text = word;
//                 }
//                 else
//                 {
//                     // Add to line 1 if no overflow
//                     visibleLine1.text += (visibleLine1.text.Length > 0 ? " " : "") + word;
//                 }
//             }
//             else
//             {
//                 // Add to line 2 if no overflow
//                 visibleLine2.text += (visibleLine2.text.Length > 0 ? " " : "") + word;
//             }
//         }
//     }

//     private IEnumerator ScrollLines(string newWord)
//     {
//         isScrolling = true;

//         // Store original positions
//         Vector2 line1StartPos = visibleLine1.rectTransform.anchoredPosition;
//         Vector2 line2StartPos = visibleLine2.rectTransform.anchoredPosition;
//         Vector2 line1EndPos = line1StartPos + Vector2.up * visibleLine1.preferredHeight;
//         Vector2 line2EndPos = line2StartPos + Vector2.up * visibleLine2.preferredHeight;

//         float elapsed = 0f;
        
//         // Animate the scroll
//         while (elapsed < scrollDuration)
//         {
//             elapsed += Time.deltaTime;
//             float t = elapsed / scrollDuration;
//             float smoothT = Mathf.SmoothStep(0f, 1f, t);
            
//             // Move lines up
//             visibleLine1.rectTransform.anchoredPosition = Vector2.Lerp(line1StartPos, line1EndPos, smoothT);
//             visibleLine2.rectTransform.anchoredPosition = Vector2.Lerp(line2StartPos, line2EndPos, smoothT);
            
//             // Fade out top line
//             visibleLine1.alpha = 1 - smoothT;
            
//             yield return null;
//         }

//         // Update text and reset positions
//         visibleLine1.text = visibleLine2.text;
//         visibleLine2.text = newWord;
//         visibleLine1.rectTransform.anchoredPosition = line1StartPos;
//         visibleLine2.rectTransform.anchoredPosition = line2StartPos;
//         visibleLine1.alpha = 1f;

//         isScrolling = false;
//     }
// }

using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CaptionManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text line1Text;
    [SerializeField] private TMP_Text line2Text;
    
    [Header("Configuration")]
    [SerializeField] private float maxWidth = 800f;
    [SerializeField] private float scrollDuration = 0.25f;
    
    private Queue<string> pendingWords = new Queue<string>();
    private bool isScrolling = false;

    private void Start()
    {
        // Initialize UI components
        InitializeUI();
        
        // Set initial text states
        line1Text.text = "";
        line2Text.text = "";
    }

    private void InitializeUI()
    {
        // Configure text components
        line1Text.color = Color.white;
        line2Text.color = Color.white;
        line1Text.enableWordWrapping = false;
        line2Text.enableWordWrapping = false;
    }

    public void AddNewWords(string newWords)
    {
        // Add new words to the queue
        pendingWords.Enqueue(newWords);
        
        // Process immediately if we're not currently scrolling
        if (!isScrolling)
        {
            ProcessNextWords();
        }
    }

    private void ProcessNextWords()
    {
        if (pendingWords.Count == 0) return;

        string newWords = pendingWords.Dequeue();
        
        // Check if adding new words would exceed max width
        if (WouldExceedMaxWidth(line2Text.text + " " + newWords))
        {
            StartCoroutine(ScrollLines(newWords));
        }
        else
        {
            // If line2 is empty, add to line1
            if (string.IsNullOrEmpty(line2Text.text))
            {
                if (string.IsNullOrEmpty(line1Text.text))
                {
                    line1Text.text = newWords;
                }
                else
                {
                    line2Text.text = newWords;
                }
            }
            else
            {
                // Append to line2 with a space
                line2Text.text += " " + newWords;
            }
        }
    }

    private bool WouldExceedMaxWidth(string text)
    {
        // Force mesh update to get accurate bounds
        line1Text.text = text;
        line1Text.ForceMeshUpdate();
        float width = line1Text.textBounds.size.x;
        line1Text.text = ""; // Reset
        
        return width > maxWidth;
    }

    private IEnumerator ScrollLines(string newWords)
    {
        isScrolling = true;

        // Store original positions
        Vector2 line1StartPos = line1Text.rectTransform.anchoredPosition;
        Vector2 line2StartPos = line2Text.rectTransform.anchoredPosition;
        Vector2 line1EndPos = line1StartPos + Vector2.up * line1Text.preferredHeight;
        Vector2 line2EndPos = line2StartPos + Vector2.up * line2Text.preferredHeight;

        // Prepare new text
        string oldLine1 = line1Text.text;
        string oldLine2 = line2Text.text;
        
        float elapsed = 0f;
        
        // Animate the scroll
        while (elapsed < scrollDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / scrollDuration;
            
            // Smooth step for more natural motion
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            
            // Move lines up
            line1Text.rectTransform.anchoredPosition = Vector2.Lerp(line1StartPos, line1EndPos, smoothT);
            line2Text.rectTransform.anchoredPosition = Vector2.Lerp(line2StartPos, line2EndPos, smoothT);
            
            // Fade out top line
            line1Text.alpha = 1 - smoothT;
            
            yield return null;
        }

        // Update text and reset positions
        line1Text.text = oldLine2;
        line2Text.text = newWords;
        line1Text.rectTransform.anchoredPosition = line1StartPos;
        line2Text.rectTransform.anchoredPosition = line2StartPos;
        line1Text.alpha = 1f;

        isScrolling = false;
        
        // Process any pending words
        if (pendingWords.Count > 0)
        {
            ProcessNextWords();
        }
    }
}

// // using UnityEngine;
// // using TMPro;
// // using System.Collections;
// // using System.Collections.Generic;

// // public class CaptionManager : MonoBehaviour
// // {
// //     [Header("UI References")]
// //     [SerializeField] private TMP_Text line1Text;
// //     [SerializeField] private TMP_Text line2Text;
    
// //     [Header("Configuration")]
// //     [SerializeField] private float maxWidth = 10f;
// //     [SerializeField] private float scrollDuration = 0.25f;
// //     [SerializeField] private float timeBetweenCaptions = 2f;
// //     [SerializeField] private Color backgroundColor = new Color(0f, 0f, 1f, 0.85f);
    
// //     private readonly string[] testSentences = new string[] {
// //         "Welcome to the VR caption test",
// //         "These captions will automatically scroll",
// //         "when there are too many words",
// //         "You can see how the lines shift up",
// //         "oneonenoenoneoneonenoenoneoneonenoenoneoneonenoenoneoneonenoenoneoneonenoenoneoneonenoenoneoneonenoenoneoneonenoenoneoneonenoenoneoneonenoenone",
// //         "and new text appears below",
// //         "This helps verify the caption system",
// //         "is working as intended in VR",
// //         "The scrolling should be smooth",
// //         "and the text should be clear",
// //         "Let's add some longer sentences to test the width limits of our caption system"
// //     };
    
// //     private Queue<string> pendingWords = new Queue<string>();
// //     private bool isScrolling = false;
// //     private int currentTestIndex = 0;

// //     private void Start()
// //     {
// //         InitializeUI();
// //         StartCoroutine(AutoPlayTest());
// //     }

// //     private void InitializeUI()
// //     {

        
// //         line1Text.text = "";
// //         line2Text.text = "";
// //     }

// //     private IEnumerator AutoPlayTest()
// //     {
// //         while (true)
// //         {
// //             AddNewWords(testSentences[currentTestIndex]);
// //             currentTestIndex = (currentTestIndex + 1) % testSentences.Length;
// //             yield return new WaitForSeconds(timeBetweenCaptions);
// //         }
// //     }

// //     public void AddNewWords(string newWords)
// //     {
// //         pendingWords.Enqueue(newWords);
        
// //         if (!isScrolling)
// //         {
// //             ProcessNextWords();
// //         }
// //     }

// //     private void ProcessNextWords()
// //     {
// //         if (pendingWords.Count == 0) return;

// //         string newWords = pendingWords.Dequeue();
        
// //         if (WouldExceedMaxWidth(line2Text.text + " " + newWords))
// //         {
// //             StartCoroutine(ScrollLines(newWords));
// //         }
// //         else
// //         {
// //             if (string.IsNullOrEmpty(line2Text.text))
// //             {
// //                 if (string.IsNullOrEmpty(line1Text.text))
// //                 {
// //                     line1Text.text = newWords;
// //                 }
// //                 else
// //                 {
// //                     line2Text.text = newWords;
// //                 }
// //             }
// //             else
// //             {
// //                 line2Text.text += " " + newWords;
// //             }
// //         }
// //     }

// //     private bool WouldExceedMaxWidth(string text)
// //     {
// //         line1Text.text = text;
// //         line1Text.ForceMeshUpdate();
// //         float width = line1Text.textBounds.size.x;
// //         line1Text.text = "";
        
// //         return width > maxWidth;
// //     }

// //     private IEnumerator ScrollLines(string newWords)
// //     {
// //         isScrolling = true;

// //         Vector2 line1StartPos = line1Text.rectTransform.anchoredPosition;
// //         Vector2 line2StartPos = line2Text.rectTransform.anchoredPosition;
// //         Vector2 line1EndPos = line1StartPos + Vector2.up * line1Text.preferredHeight;
// //         Vector2 line2EndPos = line2StartPos + Vector2.up * line2Text.preferredHeight;

// //         string oldLine1 = line1Text.text;
// //         string oldLine2 = line2Text.text;
        
// //         float elapsed = 0f;
        
// //         while (elapsed < scrollDuration)
// //         {
// //             elapsed += Time.deltaTime;
// //             float t = elapsed / scrollDuration;
// //             float smoothT = Mathf.SmoothStep(0f, 1f, t);
            
// //             line1Text.rectTransform.anchoredPosition = Vector2.Lerp(line1StartPos, line1EndPos, smoothT);
// //             line2Text.rectTransform.anchoredPosition = Vector2.Lerp(line2StartPos, line2EndPos, smoothT);
// //             line1Text.alpha = 1 - smoothT;
            
// //             yield return null;
// //         }

// //         line1Text.text = oldLine2;
// //         line2Text.text = newWords;
// //         line1Text.rectTransform.anchoredPosition = line1StartPos;
// //         line2Text.rectTransform.anchoredPosition = line2StartPos;
// //         line1Text.alpha = 1f;

// //         isScrolling = false;
        
// //         if (pendingWords.Count > 0)
// //         {
// //             ProcessNextWords();
// //         }
// //     }
// // }