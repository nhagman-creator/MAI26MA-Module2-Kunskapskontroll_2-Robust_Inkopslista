# Title: Courses and Students - Kunskapskontroll B


#### **Creator:**       Arne HAgman
#### **Date:**          23Sep2025
#### **Program:**       MAI26MA
#### **Course:**        Module II - Programmering och objektorienterad utveckling i C-sharp
#### **Assignment:**    Kunskapskontroll 3

#### **Description:** The main tasks are:
1)  **Fixing the program** - to debugg a program "shoppinglist" with (at the least) six known errors;
    1)  Four errors results in program crash. 
    2)  One results an errenous result, without a crash, and 
    3)  one error masks another error.
    Here, a debugg-report (this **README** file) will describe the errors, and how they were fixed with reference to the before and after code.

2)  **Improving the program** - make the program more robust to handle errors before they appear (proactive - with IF statements) or try catching errors after they occur (reactive - with try/catch statements)

For more detailed info on all features that the program need to fullfill, refer to: https://nbi-handelsakademin.learnpoint.se/GroupForms/Group_LearningContent_Item.aspx?Id=7589&ItemId=25589#modal

## Part 1 - Debugging
**Current system:**\
<u>Visual Studio Code</u>\
Version: 1.141.0 (user setup)
Commit: 2a59476c9bfcb90b3ddc372c36762471b7dfad1c
Date: 2026-10-06T10:15:52+02:00
Electron: 43.7.7
ElectronBuildId: 15553055
Chromium: 150.0.7871.250
Node.js: 24.21.0
V8: 15.0.245.31-electron.0
@github/copilot: 1.0.92-4.unstable.r37397886721.gad270aa
@github/copilot-sdk: 1.0.17.37397886721.gad270aa
OS: Windows_NT x64 10.0.26200

<u>Windows</u>\
Microsoft Windows 11 Pro
Version 25H2 (OS Build 26200.9457)

### Error 1 message - Debugger induced(?) invalid relative file-path
Exception has occurred: CLR/System.IO.FileNotFoundException
An unhandled exception of type 'System.IO.FileNotFoundException' occurred in System.Private.CoreLib.dll: 'Could not find file 'C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\bin\Debug\net10.0\items.txt'.'
   at Microsoft.Win32.SafeHandles.SafeFileHandle.CreateFile(String fullPath, FileMode mode, FileAccess access, FileShare share, FileOptions options)
   at Microsoft.Win32.SafeHandles.SafeFileHandle.Open(String fullPath, FileMode mode, FileAccess access, FileShare share, FileOptions options, Int64 preallocationSize, Nullable`1 unixCreateMode)
   at System.IO.Strategies.OSFileStreamStrategy..ctor(String path, FileMode mode, FileAccess access, FileShare share, FileOptions options, Int64 preallocationSize, Nullable`1 unixCreateMode)
   at System.IO.Strategies.FileStreamHelpers.ChooseStrategyCore(String path, FileMode mode, FileAccess access, FileShare share, FileOptions options, Int64 preallocationSize, Nullable`1 unixCreateMode)
   at System.IO.StreamReader.ValidateArgsAndOpenPath(String path, Int32 bufferSize)
   at System.IO.File.ReadAllText(String path, Encoding encoding)
   at ShoppingList.Load() in C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\ShoppingList.cs:line 84
   at Program.<Main>$(String[] args) in C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\Program.cs:line 4

**How to replicate error 1:**\
-start the program in debug mode - F5

**Patch:**\
By providing the full path to "items.cs" as argument to Shoppinglist (instead of the original relative path).

Note: This error does not seem to be critical and it appears only during debugging mode. At the time of writing it is not clear if it is a debugger-artifact. This is however an unexpected behaviour and by adhering to a good developer coding practise, this potential problem has been patched. Also, this error makes prevents using the debugger to enter the shoppinglist.load() method, and makes it hard to access the values within the foreach loop. After fixing this debugger induced error it was possible to see that windows introduce a "\r" in addition to a newline character. The \r is not split away, and it is kept behind all item names (except the last one in the list), which results in Error 8 below. 

## Errors:
### Error 2 message - New line in items.txt file not accepted as valid data
Unhandled exception. System.IndexOutOfRangeException: Index was outside the bounds of the array.
   at ShoppingList.Load() in C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\ShoppingList.cs:line 90
   at Program.<Main>$(String[] args) in C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\Program.cs:line 2

**How to replicate the error 2:**\
-add preceeding/trailing whitespace or a new blank row(s) to the items.txt file
-start the program: dotnet run

**Patch:**\
The trailing blankline in items.txt cause an error when the program expects to read a new line containing an item-name and a price separated by ";". This has been fixed by ignoring IsNullOrWhiteSpace lines. The ShoppingList.Load() method has also been upgraded to be more robust by trimming for preceeding and trailing whitespace for item names and price, and by skipping any other rows that does not contain (split) into an item and a price.

### Error 3 message - Invalid character in menu-choice
Unhandled exception. System.FormatException: The input string 'b' was not in a correct format.
   at System.Number.ThrowFormatException[TChar](ReadOnlySpan`1 value)
   at System.Int32.Parse(String s)
   at Program.<Main>$(String[] args) in C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\Program.cs:line 18

**How to replicate error 3:**\
-start the program: dotnet run
-Try makea  menu-choice, key in any invalid character that is not a number (including null)

**Patch:**\
Code updated for proactive check of valid integer (int.Try.Parse) or valid range of numbers (1-5)

### Error 4 message - Removal of non-existing item (numeric character)
Unhandled exception. System.ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')
   at System.Collections.Generic.List`1.RemoveAt(Int32 index)
   at ShoppingList.RemoveAt(Int32 number) in C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\ShoppingList.cs:line 20
   at Program.<Main>$(String[] args) in C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\Program.cs:line 32

**How to replicate error 4:**\
-start the program: dotnet run
-Choose menu-option 2 to remove an item
-Choose a number that does not exist

**Patch:**\

### Error 5 message - Removal of non-existing item (non-numeric character or null)
Unhandled exception. System.FormatException: The input string '' was not in a correct format.
   at System.Number.ThrowFormatException[TChar](ReadOnlySpan`1 value)
   at System.Int32.Parse(String s)
   at Program.<Main>$(String[] args) in C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\Program.cs:line 31

**How to replicate error 5:**\
-start the program: dotnet run
-Choose menu-option 2 to remove an item
-Choose a non-numeric character (including null)

**Patch:**\

### Error 6 - Crash upon non-existing filename "items.txt"
Unhandled exception. System.IO.FileNotFoundException: Could not find file 'C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\items.txt'.
File name: 'C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\items.txt'
   at Microsoft.Win32.SafeHandles.SafeFileHandle.CreateFile(String fullPath, FileMode mode, FileAccess access, FileShare share, FileOptions options)
   at Microsoft.Win32.SafeHandles.SafeFileHandle.Open(String fullPath, FileMode mode, FileAccess access, FileShare share, FileOptions options, Int64 preallocationSize, Nullable`1 unixCreateMode)
   at System.IO.Strategies.OSFileStreamStrategy..ctor(String path, FileMode mode, FileAccess access, FileShare share, FileOptions options, Int64 preallocationSize, Nullable`1 unixCreateMode)
   at System.IO.Strategies.FileStreamHelpers.ChooseStrategyCore(String path, FileMode mode, FileAccess access, FileShare share, FileOptions options, Int64 preallocationSize, Nullable`1 unixCreateMode)
   at System.IO.StreamReader.ValidateArgsAndOpenPath(String path, Int32 bufferSize)
   at System.IO.File.ReadAllText(String path, Encoding encoding)
   at ShoppingList.Load() in C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\ShoppingList.cs:line 84
   at Program.<Main>$(String[] args) in C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\Program.cs:line 4

**How to replicate error 6:**\
-change the name of items.txt file
-start the program: dotnet run

**Patch:**\

### Error 7 - Incomplete summation of Total costs
No error message, and not system crash - but the functionality is compromised due to incomplete summation of the total costs. It is clear that the "Totalt" field with the total sum of costs show to low value. When inspecting the code (public int Total() method in the class ShoppingList), it is clear that index 0 (item 1 in the list) has been excluded from the total sum.

**How to replicate error 7:**\
-start the program: dotnet run

**Patch:**\

### Error 8 - Invisible items in the list
No error message, and no system crash - but this bugg causes invicible row number and item name for all but the last item in the list. Caused by splitting on new Line Character (\n) in the ShoppingList.Load() method. In Windows, a row with text is ended with a Carriage Return (\r) together with a New Line character. Thus, splitting on new line character only will leave the Carriage Return after each item, causing all but the last one to appear as hidden when being printed to the console.

**How to replicate error 8:**\
-start the program: dotnet run

**Patch:**\
Changed to split on "\r\n" instead of only on \n

### Error 9 - Possible to save null or white-space items

**How to replicate error 9:**\
-start the program: dotnet run

**Patch:**\