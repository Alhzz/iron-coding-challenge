using System;
using System.Collections.Generic;
using System.IO; // Required for File operations
using System.Linq;

class Program
{
    // Keypad mapping: Index 0 maps to Key '1', Index 1 to Key '2', etc.
    // Note: Key 1 has symbols, Keys 7 & 9 have 4 letters, others have 3.
    static readonly string[] button_data = new string[]
    {
        "&'(", "ABC", "DEF", "GHI",
        "JKL", "MNO", "PQRS", "TUV",
        "WXYZ", "-", " ", ""
    };

    /// <summary>
    /// Counts how many times a button was pressed consecutively.
    /// Used to determine which character on the key was selected (e.g., 2 vs 22 vs 222).
    /// </summary>
    static int CountChar(string text, int startIndex)
    {
        int count = 0;
        char targetChar = text[startIndex];

        // Scan forward until a different character is found or string ends
        for (int i = startIndex; i < text.Length; i++)
        {
            if (text[i] == targetChar)
                count++;
            else
                break;
        }

        return count;
    }

    /// <summary>
    /// Decodes a sequence of button presses into characters and updates the message list.
    /// Handles cycling characters (22->B), spaces (0), and backspaces (*).
    /// </summary>
    static List<char> Interpret(string text, List<char> characterList)
    {
        int counter = 0;

        while (counter < text.Length)
        {
            char currentKey = text[counter];

            if (char.IsDigit(currentKey))
            {
                // '0' is the specific key for a space character
                if (currentKey == '0')
                {
                    characterList.Add(' ');
                    counter++;
                    continue;
                }

                // Determine cycle length (Keys 7 & 9 wrap every 4 presses, others every 3)
                int maxCycleLength = (currentKey == '7' || currentKey == '9') ? 4 : 3;
                int consecutivePresses = CountChar(text, counter);
                
                // Calculate character index: (Presses - 1) % CycleLength
                int charIndex = (consecutivePresses - 1) % maxCycleLength;
                int buttonDataIndex = currentKey - '1'; 
                
                characterList.Add(button_data[buttonDataIndex][charIndex]);

                // Move past the processed sequence of identical keys
                counter += consecutivePresses;
            }
            // '*' acts as a backspace, removing the last entered character
            else if (currentKey == '*' && characterList.Count > 0)
            {
                characterList.RemoveAt(characterList.Count - 1);
                counter++;
            }
            else
            {
                // Ignore invalid characters (like #)
                counter++;
            }
        }
        return characterList;
    }

    /// <summary>
    /// Main entry point for decoding a raw input string.
    /// Splits input by spaces to simulate "pauses" between characters.
    /// </summary>
    static string OldPhonePad(string rawInput)
    {
        // A space in the input represents a pause, allowing the user to type
        // the same letter twice (e.g. "2 2" -> "AA")
        string[] inputSequences = rawInput.Split(' ');
        List<char> characterList = new List<char>();
        
        foreach (string seq in inputSequences)
        {
            characterList = Interpret(seq, characterList);
        }

        return new string(characterList.ToArray());
    }

    static void Main()
    {
        string filePath = "test-case.txt"; 

        try
        {
            // Process the input file line by line
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                Console.WriteLine(OldPhonePad(line));
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: The file '{filePath}' was not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }
}