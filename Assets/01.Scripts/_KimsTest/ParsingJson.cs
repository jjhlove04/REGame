using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParsingJson : MonoBehaviour
{
    private static ParsingJson instance;

    public static ParsingJson Instnace
    {
        get
        {
            return instance;
        }
    }

    public List<float> maxExp = new List<float>();
    public List<int> changeExp = new List<int>();
    public List<string> effectDetail = new List<string>();

    [Serializable]
    public class Sheet
    {
        public int Level;
        public float NeedEXP;
        public float SumEXP;
        public int InGame;
        public int Change;

    }

    [Serializable]
    public class SheetTwo
    {
        public string variableName;
        public string effectDetails;
    }

    public class SheetNumberts
    {
        public Sheet[] sheet;
    }

    public class SheetTwoNumbers
    {
        public SheetTwo[] sheetTwo;
    }

    void Awake()
    {
        instance = this;

        TextAsset textAsset = Resources.Load<TextAsset>("JsonData/Sheet1");
        TextAsset textAssetT = Resources.Load<TextAsset>("JsonData/Sheet2");

        string sheet = "{\"sheet\":" + textAsset + "}";
        string sheetTwo = "{\"sheetTwo\":" + textAssetT + "}";

        SheetNumberts s = JsonUtility.FromJson<SheetNumberts>(sheet);
        SheetTwoNumbers st = JsonUtility.FromJson<SheetTwoNumbers>(sheetTwo);

        foreach (Sheet see in s.sheet)
        {
            maxExp.Add(see.NeedEXP);
            changeExp.Add(see.Change);
        }

        foreach (SheetTwo see in st.sheetTwo)
        {
            effectDetail.Add(see.effectDetails);
        }

        Debug.Log("ÆÄ½Ì ¿Ï·á");
    }
}
