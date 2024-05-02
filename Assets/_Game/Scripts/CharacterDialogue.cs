using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class CharacterDialogue : MonoBehaviour
{
    private TextMeshProUGUI _dialogueTextMesh;

    public string[] textEntered;
    private int textEnteredIndex = 0;
    
    private char[] textEnteredSplit;
    private char[] textToPrintSplit;

    private bool isPrinting = false;
    public float delay = 0f;

    [Header("Dialogue Box")]

    [SerializeField] private Sprite dialogueBoxSprite;
    [SerializeField] private Color dialogueBoxColorChange;


    [Header("Character Portrait")]


    [SerializeField] private Sprite portraitBoxSprite;
    [SerializeField] private Color portraitBoxColorChange;

    [Header("Character Box")]

    //[Tooltip("Changes the design for the character boxes")]


    [SerializeField] private Sprite characterBoxSprite;
    [SerializeField] private Color characterBoxColorChange;

  




    private SpriteRenderer dialogueBoxSpriteRenderer;

    private SpriteRenderer characterBoxSpriteRenderer;

    private SpriteRenderer portraitBoxSpriteRenderer;

    [Header("Text Attributes")]


    [SerializeField] private Color textColorChoice;
    [SerializeField] private float sizeFont; //data type was wrong, should be a number
    [SerializeField] private TMP_FontAsset fontType;


  void OnValidate()
    {
        GameObject textMeshGameObject = this.transform.GetChild(0).gameObject;

        GameObject dialogueBoxGameObject = this.transform.GetChild(1).gameObject;


        GameObject characterBoxGameObject = this.transform.GetChild(2).gameObject;

        GameObject portraitBoxGameObject = this.transform.GetChild(3).gameObject;


        _dialogueTextMesh = textMeshGameObject.GetComponent<TextMeshProUGUI>();
        dialogueBoxSpriteRenderer = dialogueBoxGameObject.GetComponent<SpriteRenderer>();
        characterBoxSpriteRenderer = characterBoxGameObject.GetComponent<SpriteRenderer>();
        portraitBoxSpriteRenderer = portraitBoxGameObject.GetComponent<SpriteRenderer>();



        EditorApplication.update += DesignerUpdatesField;


}


    void DesignerUpdatesField()
    {
        if (_dialogueTextMesh != null)
        {

            _dialogueTextMesh.font = fontType;

            _dialogueTextMesh.fontSize = sizeFont;

            _dialogueTextMesh.color = textColorChoice;
        }

        if (dialogueBoxSpriteRenderer != null)
        {
            //same stuff as the if statement above too! Just different attributes on a different type of 
            //component, a SpriteRenderer as opposed to a TextMesh 
            dialogueBoxSpriteRenderer.sprite = dialogueBoxSprite;
            dialogueBoxSpriteRenderer.color = dialogueBoxColorChange;

            characterBoxSpriteRenderer.sprite = characterBoxSprite;
            characterBoxSpriteRenderer.color = characterBoxColorChange;



            portraitBoxSpriteRenderer.sprite = portraitBoxSprite;
            portraitBoxSpriteRenderer.color = portraitBoxColorChange;


        }

        EditorApplication.update -= DesignerUpdatesField;

    }






    void Start()
    {
        _dialogueTextMesh = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textEnteredSplit = textEntered[0].ToCharArray();
        textToPrintSplit = new char[textEnteredSplit.Length];
    }


    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space) && textEnteredIndex < textEntered.Length)
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
            _dialogueTextMesh.text = s;

            yield return new WaitForSeconds(delay);
        }

        isPrinting = false;
        textEnteredIndex += 1;
        if (textEnteredIndex < textEntered.Length)
        {

            textEnteredSplit = textEntered[textEnteredIndex].ToCharArray();

        }
    }
}
