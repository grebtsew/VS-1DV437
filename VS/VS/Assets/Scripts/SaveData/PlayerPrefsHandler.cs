using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsHandler
{

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();

    }

    /// <summary>
    /// Sets <variable> to <value> based on its type T and additionally
    /// stores it in the registry or plist using key <varName>
    /// </summary>
    /// <auth: Isaac Dart (isaac@mantle.tech) >
    public static void SetPersistentVar<T>(string varName, ref T variable, T value, bool save)
    {

        variable = value;

        Type varType = variable.GetType();
        if (varType == typeof(int))
        {
            int intVal = Convert.ToInt32(variable);
            PlayerPrefs.SetInt("i_" + varName, intVal);
        }
        else if (varType == typeof(bool))
        {
            int intVal = Convert.ToInt32(variable);
            PlayerPrefs.SetInt("b_" + varName, intVal);
        }
        else if (varType == typeof(float))
        {
            float floatVal = (float)(Convert.ToDouble(variable));
            PlayerPrefs.SetFloat("f_" + varName, floatVal);
        }
        else
        {
            string stringVal = Convert.ToString(variable);
            PlayerPrefs.SetString("s_" + varName, stringVal);
        }

        if (save) PlayerPrefs.Save();

    }

    /// <summary>
    /// Returns a value of type T from the registry or plist using <varName>
    /// </summary>
    /// <auth: Isaac Dart (isaac@mantle.tech) >
    public static T GetPersistentVar<T>(string varName, T defaultValue)
    {

        T variable = defaultValue;

        Type varType = variable.GetType();
        if (varType == typeof(int))
        {
            int defaultIntVal = Convert.ToInt32(defaultValue);
            int intVal = PlayerPrefs.GetInt("i_" + varName, defaultIntVal);
            variable = (T)Convert.ChangeType(intVal, varType);
        }
        else if (varType == typeof(bool))
        {
            int defaultIntVal = Convert.ToInt32(defaultValue);
            int intVal = PlayerPrefs.GetInt("b_" + varName, defaultIntVal);
            bool boolVal = intVal != 0;
            variable = (T)Convert.ChangeType(boolVal, varType);
        }
        else if (varType == typeof(float))
        {
            float defaultFloatVal = (float)(Convert.ToDouble(defaultValue));
            float floatVal = PlayerPrefs.GetFloat("f_" + varName, defaultFloatVal);
            variable = (T)Convert.ChangeType(floatVal, varType);
        }
        else
        {
            string defaultStringVal = Convert.ToString(defaultValue);
            string stringVal = PlayerPrefs.GetString("s_" + varName, defaultStringVal);
            variable = (T)Convert.ChangeType(stringVal, varType);
        }
        return variable;
    }




}
