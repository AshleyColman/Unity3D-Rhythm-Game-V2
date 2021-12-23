using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class UtilityMethods
{
    public static StringBuilder AddZerosToScoreString(StringBuilder _stringBuilder)
    {
        switch (_stringBuilder.Length)
        {
            case 0:
                _stringBuilder.Insert(0, "00000000");
                break;
            case 1:
                _stringBuilder.Insert(0, "0000000");
                break;
            case 2:
                _stringBuilder.Insert(0, "000000");
                break;
            case 3:
                _stringBuilder.Insert(0, "00000");
                break;
            case 4:
                _stringBuilder.Insert(0, "0000");
                break;
            case 5:
                _stringBuilder.Insert(0, "000");
                break;
            case 6:
                _stringBuilder.Insert(0, "00");
                break;
            case 7:
                _stringBuilder.Insert(0, "0");
                break;
        }
        return _stringBuilder;
    }
}
