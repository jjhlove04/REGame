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

    [Serializable]
    public class Sheet
    {
        public int Level;
        public float NeedEXP;
        public float SumEXP;
        public int InGame;
        public int Change;

    }

    public class SheetNumberts
    {
        public Sheet[] sheet;
    }

    void Awake()
    {
        instance = this;

        TextAsset textAsset = Resources.Load<TextAsset>("JsonData/Sheet1");

        string sheet = "{\"sheet\":" + textAsset + "}";

        SheetNumberts s = JsonUtility.FromJson<SheetNumberts>(sheet);

        foreach (Sheet see in s.sheet)
        {
            maxExp.Add(see.NeedEXP);
            changeExp.Add(see.Change);
        }
    }

    private void Update()
    { 

    }
}
