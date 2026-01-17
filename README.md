# Old Phone Pad Decoder

A C# console application that interprets numeric keypad inputs (old-style multi-tap text entry) and translates them into readable English text.

## 📱 Overview

Before smartphones, text messages were written using a standard 12-button keypad. To type the letter "C", a user would press the **2** button three times. To type "S", they would press **7** four times.

This program reads a series of numeric inputs (simulating button presses) from a file and outputs the decoded messages.

## 🚀 Features

* **Multi-tap Decoding:** Accurately cycles through characters based on button presses (e.g., `2`=A, `22`=B, `222`=C).
* **Cycle Handling:** Correctly handles keys with 3 characters (e.g., key 2) and keys with 4 characters (keys 7 & 9).
* **Pause Detection:** Handles spaces in the input string as "pauses," allowing users to type consecutive characters on the same key (e.g., `2 2` becomes "AA").
* **Backspace Support:** The `*` character acts as a backspace, removing the last typed character.
* **Spacebar:** The `0` key inserts a whitespace.
* **Batch Processing:** Reads multiple test cases from a text file.

## 🔢 Keypad Layout

The application uses the standard mapping found on classic mobile phones:

| Key | Characters |
| :--- | :--- |
| **1** | `&` `'` `(` |
| **2** | `A` `B` `C` |
| **3** | `D` `E` `F` |
| **4** | `G` `H` `I` |
| **5** | `J` `K` `L` |
| **6** | `M` `N` `O` |
| **7** | `P` `Q` `R` `S` |
| **8** | `T` `U` `V` |
| **9** | `W` `X` `Y` `Z` |
| **0** | `[Space]` |
| **\*** | `[del]` |

## 🛠️ Installation & Usage

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.

### Setup
1.  Clone this repository or copy the `Program.cs` file.
2.  Ensure you have a file named `test-case.txt` in the same directory as the executable (or project root).

### Creating the Input File
Create a file named `test-case.txt` and add your numeric strings, one per line.

**Example `test-case.txt` content:**
```text
4433555 555666#
8 88777444666*664#
222 2 22#
7777*7777*77777#
```

**Outputs:**

```text
HELLO
TURING
CAB
P
```