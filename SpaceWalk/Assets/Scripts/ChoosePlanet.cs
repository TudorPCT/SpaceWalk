using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePlanet : MonoBehaviour
{
    public GameObject VisitButton;
    public TMP_Dropdown dropdown;
    public static string _selectedOption;
    void Start()
    {
        Choose();
    }

    void Update()
    {

    }

    public void Choose()
    {
        _selectedOption = dropdown.options[dropdown.value].text;
        int sceneIndex = SceneUtility.GetBuildIndexByScenePath($"Assets/Scenes/{_selectedOption}.unity");
        if (sceneIndex != -1)
        {
            Debug.Log($"Scene with name {_selectedOption} exists.");
            VisitButton.SetActive(true);
        }
        else
        {
            Debug.Log($"Scene with name {_selectedOption} does not exist.");
            VisitButton.SetActive(false);
        }
    }
}
