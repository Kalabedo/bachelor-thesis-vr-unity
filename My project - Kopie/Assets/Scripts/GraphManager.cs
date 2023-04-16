using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GraphManager : MonoBehaviour
{
    public QuestionnaireToolkit.Scripts.QTQuestionnaireManager qtManager;
    public float value1;
    public float value2;
    public QuestionnaireToolkit.Scripts.QTMultipleChoice multipleChoice;
    public QuestionnaireToolkit.Scripts.QTQuestionPageManager pageManager;
    private GameObject questionItem;

    public TMP_Text value1Text;
    public TMP_Text value2Text;


    private Image bar1;
    private Image bar2;

    float averageWRealism;
    float averageWORealism;

    float bar1Size;
    float bar2Size;
    bool swapped = false;
    // Start is called before the first frame update
    void Start()
    {
        questionItem = pageManager.questionItems[1];

        float randomNum = UnityEngine.Random.Range(0f, 1f);

        // Check if the random number is greater than or equal to 0.5
        if (randomNum >= 0.5f) {
            bar1 = transform.Find("Bar1").GetComponent<Image>();
            bar2 = transform.Find("Bar2").GetComponent<Image>(); 

            value1Text = transform.Find("value1").GetComponent<TMP_Text>();
            value2Text = transform.Find("value2").GetComponent<TMP_Text>();
        }
        else {
            bar1 = transform.Find("Bar2").GetComponent<Image>();
            bar2 = transform.Find("Bar1").GetComponent<Image>(); 

            value1Text = transform.Find("value2").GetComponent<TMP_Text>();
            value2Text = transform.Find("value1").GetComponent<TMP_Text>();
            
            swapped = true;
        }
    }

    // Update is called once per frame
    void Update() {
    // Calculate the size of the bars

    averageWRealism = qtManager.averageResultWReality;
    averageWORealism = qtManager.averageResultWOReality;

    value1 = (float)Math.Round(averageWRealism, 1);
    value2 = (float)Math.Round(averageWORealism, 1);

    if (value1 == value2)
    {
        bar2.gameObject.SetActive(false);
        value2Text.gameObject.SetActive(false);
        multipleChoice.answerRequired = false;
        pageManager.questionItems.Remove(questionItem);
        questionItem.SetActive(false);
    }

    bar1Size = value1;
    bar2Size = value2;

    // Set the size of the bars
    bar1.GetComponent<RectTransform>().sizeDelta = new Vector2(20f,bar1Size * 20f);
    bar2.GetComponent<RectTransform>().sizeDelta = new Vector2(20f,bar2Size *  20f);

    // Set the text of the "Values" Text component
    value1Text.text = value1.ToString();
    value2Text.text = value2.ToString();
    }

}
