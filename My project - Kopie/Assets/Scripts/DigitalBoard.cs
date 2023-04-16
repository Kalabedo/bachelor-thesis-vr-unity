using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigitalBoard : MonoBehaviour
{
    [SerializeField]
    private Sprite[] digits;

    [SerializeField]
    private Image[] characters;

    private int scoreAmount;

    private int numberOfDigitsInScoreAmount;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            characters[i].sprite = digits[0];
        }   

        scoreAmount = 0;

        ShootHoop.ScoredPoints += AddScoreAndDisplay;
    }

    private void AddScoreAndDisplay(int scoreValue)
    {
        scoreAmount += scoreValue;

        Debug.Log("+ " + scoreValue);

        if (scoreAmount >= 100)
        {
            scoreAmount = scoreAmount - 100;
        }

        int[] scoreAmountByDigitsArray = GetDigitsArrayFromScoreAmount(scoreAmount);

        switch (scoreAmountByDigitsArray.Length)
        {
            case 1:
            characters[0].sprite = digits[0];
            characters[1].sprite = digits[0];
            characters[2].sprite = digits[scoreAmountByDigitsArray[0]];
            break;

            case 2:
            characters[0].sprite = digits[0];
            characters[1].sprite = digits[scoreAmountByDigitsArray[0]];
            characters[2].sprite = digits[scoreAmountByDigitsArray[1]];
            break;

            case 3:
            characters[0].sprite = digits[scoreAmountByDigitsArray[0]];
            characters[1].sprite = digits[scoreAmountByDigitsArray[1]];
            characters[2].sprite = digits[scoreAmountByDigitsArray[2]];
            break;
        }
    }

    private int[] GetDigitsArrayFromScoreAmount(int scoreAmount)
    {
        List<int> listOfInts = new List<int>();
        while (scoreAmount > 0)
        {
            listOfInts.Add(scoreAmount % 10);
            scoreAmount = scoreAmount / 10;
        }
        listOfInts.Reverse();
        return listOfInts.ToArray();
    }

    private void OnDestroy() 
    {
        ShootHoop.ScoredPoints -= AddScoreAndDisplay;
    }
}
