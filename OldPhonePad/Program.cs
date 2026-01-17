using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

class Program
{
    // Mapping of keypad buttons to characters.
    // Index 0 represents Key '1', Index 1 represents Key '2', etc.
    static string[] button_data = new string[12]
    {
        "&'(", "abc", "def", "ghi",
        "jkl", "mno", "pqrs", "tuv",
        "wxyz", "-", " ", ""
    };

    /// <summary>
    /// Iteratively counts consecutive identical characters starting from a specific index.
    /// This replaces the recursive version to prevent StackOverflow on long inputs.
    /// </summary>
    static int CountChar(string text, int startIndex)
    {
        int count = 0;
        char targetChar = text[startIndex];

        // Loop forward starting from the current index
        for (int i = startIndex; i < text.Length; i++)
        {
            if (text[i] == targetChar)
            {
                count++;
            }
            else
            {
                // Stop as soon as we hit a different character
                break;
            }
        }

        return count;
    }

    /// <summary>
    /// Translates a raw numeric string (e.g., "222") into human-readable text (e.g., "C").
    /// Handles spaces ('0') and backspaces ('*').
    /// </summary>
    static List<char> Interpret(string text)
    {
        List<char> characterList = new List<char>();
        int counter = 0;

        while (counter < text.Length)
        {
            char currentKey = text[counter];

            if (char.IsDigit(currentKey))
            {
                // '0' acts as a space bar in this layout
                if (currentKey == '0')
                {
                    characterList.Add(' ');
                    counter++;
                    continue;
                }

                // Keys 7 and 9 have 4 letters; others have 3
                int maxCycleLength = (currentKey == '7' || currentKey == '9') ? 4 : 3;

                // Determine how many times this specific button was pressed in a row
                int consecutivePresses = CountChar(text, counter);
                
                // Calculate which letter to pick: (PressCount - 1) % CycleLength
                int charIndex = (consecutivePresses - 1) % maxCycleLength;

                // Map the ASCII digit (e.g. '1') to our array index (0)
                int buttonDataIndex = currentKey - '1'; 
                
                char letter = button_data[buttonDataIndex][charIndex];
                characterList.Add(letter);

                // Skip ahead past the sequence we just processed
                counter += consecutivePresses;
            }
            // Handle Backspace ('*')
            else if (currentKey == '*' && characterList.Count > 0)
            {
                characterList.RemoveAt(characterList.Count - 1);
                counter++;
            }
            else
            {
                // Skip unknown characters
                counter++;
            }
        }
        return characterList;
    }

    static void Main()
    {
        // Example input with '#' which will be skipped
        Console.WriteLine("Enter your text:");
        string rawInput = Console.ReadLine();

        // Split by space to distinguish between pauses
        string[] inputSequences = rawInput.Split(' ');
        
        // Flatten the results into a single list
        List<char> resultChars = inputSequences
                                    .SelectMany(seq => Interpret(seq))
                                    .ToList();

        string finalMessage = new string(resultChars.ToArray());
        Console.WriteLine(finalMessage);
    }
}