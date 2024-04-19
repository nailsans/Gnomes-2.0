using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField] GameObject WarningField;

    private void Start()
    {
        WarningField.SetActive(false);
    }

    public void DisplayWarning(string text)
    {
        WarningField.GetComponent<TextMeshProUGUI>().text = text;
        StartCoroutine(FadeWarning());
    }

    public void DisplayWarningOnActivityType(EnemyActivityType ea)
    {
        string text;
        switch (ea) {
            case EnemyActivityType.Wave:
                text = "бнкмю мювюкюяэ";
                break;
            case EnemyActivityType.Rest:
                text = "оепейсп";
                break;
            case EnemyActivityType.Cleaning:
                text = "днахюбюире охднпнб";
                break;
            default:
                text = "JUJJJUJ";
                break;
        }
        WarningField.GetComponent<TextMeshProUGUI>().text = text;
        StartCoroutine(FadeWarning());
    }

    private IEnumerator FadeWarning()
    {
        WarningField.SetActive(true);
        yield return new WaitForSeconds(5);
        WarningField.SetActive(false);
    }
}
