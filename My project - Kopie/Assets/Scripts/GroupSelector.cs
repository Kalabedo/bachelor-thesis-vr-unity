using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GroupSelector : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Button confirmButton;

    public string selectedGroup;

    private void Start()
    {
        selectedGroup = dropdown.options[dropdown.value].text;

        dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(dropdown);
        });

        confirmButton.onClick.AddListener(delegate
        {
            LoadScene(selectedGroup);
        });
    }

    void DropdownValueChanged(TMP_Dropdown change)
    {
        selectedGroup = change.options[change.value].text;
    }

    void LoadScene(string selectedGroup)
    {
        if (selectedGroup == "Group A")
        {
            SceneManager.LoadScene("Scenario1_1");
        }
        else if (selectedGroup == "Group B")
        {
            SceneManager.LoadScene("Scenario3_1");
        }
        else if (selectedGroup == "Group C")
        {
            SceneManager.LoadScene("Scenario2_1");
        }
    }
}



