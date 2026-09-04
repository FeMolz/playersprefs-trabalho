using UnityEngine;
using TMPro;

public class PlayerPref_Manager : MonoBehaviour
{

    [Header("Int Counter")]
    [SerializeField] private TMP_Text intText;
    private int intCounter = 0;
    private int addInt = 1;

    [Header("Float Counter")]
    [SerializeField] private TMP_Text floatText;
    private float floatCounter = 0f;
    private float addFloat = 1.5f;

    [Header("String Input")]
    [SerializeField] private TMP_InputField stringInput;
    [SerializeField] private TMP_Text stringDisplay;

    private const string INT_KEY = "IntValue";
    private const string FLOAT_KEY = "FloatValue";
    private const string STRING_KEY = "StringValue";

    private void Start()
    {
        LoadData();
        UpdateUI();
    }

    public void IncrementInt()
    {
        intCounter += addInt;
        UpdateIntUI();
    }

    public void IncrementFloat()
    {
        floatCounter += addFloat;
        UpdateFloatUI();
    }

    public void OnStringChanged()
    {
        if (stringInput != null && stringDisplay != null)
        {
            stringDisplay.text = stringInput.text;
        }
    }

    public void SaveData()
    {
        string inputText = stringInput?.text ?? "";

        PlayerPrefs.SetInt(INT_KEY, intCounter);
        PlayerPrefs.SetFloat(FLOAT_KEY, floatCounter);
        PlayerPrefs.SetString(STRING_KEY, inputText);
        PlayerPrefs.Save();

        Debug.Log("Data saved successfully");
    }

    public void LoadData()
    {
        floatCounter = PlayerPrefs.GetFloat(FLOAT_KEY, 0f);
        intCounter = PlayerPrefs.GetInt(INT_KEY, 0);
        string savedString = PlayerPrefs.GetString(STRING_KEY, "");

        if (stringInput != null)
         stringInput.text = savedString;

        if (stringDisplay != null)
         stringDisplay.text = savedString;
        
        Debug.Log("Data loaded successfully!");
    }

    public void ResetAllData()
    {
        ResetValues();
        ClearPlayerPrefs();
        PlayerPrefs.Save();
        UpdateUI();
        OnStringChanged();
    }

    public void ResetInt()
    {
        intCounter = 0;
        PlayerPrefs.DeleteKey(INT_KEY);
        PlayerPrefs.SetInt(INT_KEY, intCounter);
        PlayerPrefs.Save();
        UpdateIntUI();
    }

    public void ResetFloat()
    {
        floatCounter = 0f;
        PlayerPrefs.DeleteKey(FLOAT_KEY);
        PlayerPrefs.SetFloat(FLOAT_KEY, floatCounter);
        PlayerPrefs.Save();
        UpdateFloatUI();
    }

    public void ResetString()
    {
        if (stringInput != null)
         stringInput.text = "";

        if (stringDisplay != null)
         stringDisplay.text = "";

        PlayerPrefs.DeleteKey(STRING_KEY);
        PlayerPrefs.SetString(STRING_KEY, "");
        PlayerPrefs.Save(); 
    }

    private void UpdateIntUI()
    {
        if (intText != null) 
         intText.text = intCounter.ToString();
    }

    private void UpdateFloatUI()
    {
        if (floatText != null)
         floatText.text = floatCounter.ToString("F2");
    }

    private void UpdateUI()
    {
        UpdateIntUI();
        UpdateFloatUI();
    }

    private void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteKey(INT_KEY);
        PlayerPrefs.DeleteKey(FLOAT_KEY);
        PlayerPrefs.DeleteKey(STRING_KEY);
    }

    private void ResetValues()
    {
        intCounter = 0;
        floatCounter = 0f;

        if (stringInput != null)
            stringInput.text = "";

        if (stringDisplay != null)
            stringDisplay.text = "";
    }

    public void QuitApplication()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
            //Debug.Log("Quit button was pressed");
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) SaveData();
    }

}
