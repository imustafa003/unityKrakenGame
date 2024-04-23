using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterDialogue : MonoBehaviour
{
    private TextMeshProUGUI _text;

    public string textEntered;
    private char[] textEnteredSplit;
    private char[] textToPrintSplit;

    private bool isPrinting = false;
    public float charPrintToDelay = 0f;

   void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        textEnteredSplit = textEntered.ToCharArray();
        textToPrintSplit = new char[textEnteredSplit.Length];
    }


    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space was pressed");
            if(!isPrinting)
            {
                textToPrintSplit = new char[textEnteredSplit.Length];
                    StartCoroutine(PrintText());
            }
        }
    }

    IEnumerator PrintText ()
    {
        isPrinting = true;

        for (int i = 0; i < textEnteredSplit.Length; i++)
        {
            textToPrintSplit[i] = textEnteredSplit[i];
            string s = new string(textToPrintSplit);
            _text.text = s;

            yield return new WaitForSeconds(charPrintToDelay);
        }

        isPrinting = false;
    }
}
