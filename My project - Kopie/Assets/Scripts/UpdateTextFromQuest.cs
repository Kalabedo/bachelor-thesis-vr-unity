using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateTextFromQuest : MonoBehaviour
{
    public QuestionnaireToolkit.Scripts.QTQuestionnaireManager qtManager;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LogAverage()
    {
        qtManager.GetAverageResult();
    }
}
