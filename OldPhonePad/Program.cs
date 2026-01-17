using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Security.AccessControl;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.VisualBasic; // Required for the ToList() extension method

class Program
{
    static string[] button_data = new string[12]
    {
        "&'(", "abc", "def", "ghi",
        "jkl", "mno", "pqrs", "tuv",
        "wxyz", "-", " ", ""
    };

    static int CountChar(string text, int index)
    {
        int count;

        if (index == text.Length - 1) return 1;
        if (text[index] != text[index + 1]) return 1;
        if (index == text.Length - 2) return 2;

        count = CountChar(text, index + 1) + 1;
        return count;
    }


    static List<char> Interpret(string text)
    {
        List<char> charectorList = new List<char>();

        int counter = 0;
        int textLenght = text.Length;

        while (counter < textLenght)
        {
            char buttonIndex = text[counter];
            if (char.IsDigit(buttonIndex))
            {
                int buttonIntIndex = buttonIndex - '1';
                int charactorLength = CountChar(text, counter);
                int textIndex = (charactorLength - 1) % 3;

                // Console.WriteLine($"text: {text}");
                // Console.WriteLine($"buttonIntIndex: {buttonIntIndex}");
                // Console.WriteLine($"textIndex: {textIndex}");
                // Console.WriteLine($"");

                char letter = button_data[buttonIntIndex][textIndex];
                charectorList.Add(letter);

                counter += charactorLength;
            }
            else if (buttonIndex == '*' && charectorList.Count > 0)
            {
                charectorList.RemoveAt(charectorList.Count - 1);
                counter++;
            }
            else
            {
                counter++;
            }

        }
        return charectorList;
    }


    static void Main()
    {
        List<char> charectorList = new List<char>();
        string data = "8 88777444666*664#";

        string[] partsArray = data.Split(' ');
        List<char> smallPartList = partsArray.SelectMany(x => Interpret(x)).ToList(); ;
        charectorList.AddRange(smallPartList);
        
        string finalMessage = new string(charectorList.ToArray());
        Console.WriteLine(finalMessage);

    }
}
