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

Note 1: All buggs that can easily be resolved using a proactive approach (prevent the error to occur with i.e. IF statements) has been prioritezed above try-catch approaches.

Note 2: No other libraries has been used but the built-in root namespace Systems-tools/functionalites.

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

**Patch 1:**\
By providing the full path to "items.cs" as argument to Shoppinglist (instead of the original relative path).

Note: This error does not seem to be critical and it appears only during the debugging mode. At the time of writing it is not clear if it is a debugger-artifact. This is however an unexpected behaviour and by adhering to a good developer coding practise, this potential problem has been patched. Also, this error prevents using the debugger to enter the shoppinglist.load() method, and makes it harder to access the values within the foreach loop. After fixing this debugger induced error it was possible to see that windows introduce a "\r" in addition to a newline character. The \r is not splitted away, and it is trails all item names (except the last one in the list), which results in Error 8 below. 

## Errors:
### Error 2 message - New line in items.txt file not accepted as valid data
Unhandled exception. System.IndexOutOfRangeException: Index was outside the bounds of the array.
   at ShoppingList.Load() in C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\ShoppingList.cs:line 90
   at Program.<Main>$(String[] args) in C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\Program.cs:line 2

**How to replicate the error 2:**\
-add preceeding/trailing whitespace or a new blank row(s) to the items.txt file
-start the program: dotnet run

**Patch 2:**\
The trailing blankline in items.txt cause an error when the program expects to read a new line containing an item-name and a price separated by ";". This has been fixed by ignoring IsNullOrWhiteSpace lines. The ShoppingList.Load() method has also been upgraded to be more robust by trimming (using trim) for preceeding and trailing whitespace for item names and price, and by skipping any other rows that does not contain (split) into an item and a price.

### Error 3 message - Invalid character in menu-choice
Unhandled exception. System.FormatException: The input string 'b' was not in a correct format.
   at System.Number.ThrowFormatException[TChar](ReadOnlySpan`1 value)
   at System.Int32.Parse(String s)
   at Program.<Main>$(String[] args) in C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\Program.cs:line 18

**How to replicate error 3:**\
-start the program: dotnet run
-Try makea  menu-choice, key in any invalid character that is not a number (including null)

**Patch 3:**\
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

**Patch 4:**\
Code updated for proactive check of user input for valid integers within the correct list range, handles null/whitespace and characters that are not numeric without program crash

**Patch 4.1**\
Bug in patch 4 that does not allow removal of the last item in the shoppinglist - fixed by allowing last index in the list to be removed.

### Error 5 message - Removal of non-existing item (non-numeric character or null)
Unhandled exception. System.FormatException: The input string '' was not in a correct format.
   at System.Number.ThrowFormatException[TChar](ReadOnlySpan`1 value)
   at System.Int32.Parse(String s)
   at Program.<Main>$(String[] args) in C:\Users\MjukvaruutvecklareAI\Documents\Mjukvaruutvecklare AI\VS studio\Project\Module-2\Kunskapskontroll 2\Program.cs:line 31

**How to replicate error 5:**\
-start the program: dotnet run
-Choose menu-option 2 to remove an item
-Choose a non-numeric character (including null)

**Patch 5:**\
Updated together with patch 4.

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

**Patch: 6**\
Added a method in class ShoppingList that adds a new file empty items.txt file if it does not already exist

### Error 7 - Incomplete summation of Total costs
No error message, and not system crash - but the functionality is compromised due to incomplete summation of the total costs. It is clear that the "Totalt" field with the total sum of costs show to low value. When inspecting the code (public int Total() method in the class ShoppingList), it is clear that index 0 (item 1 in the list) has been excluded from the total sum.

**How to replicate error 7:**\
-start the program: dotnet run

**Patch 7:**\
Bugg caused by confusion between actual list-index (starts at zero) and row number (starts at 1) has been corrected for. The first item in the list is now included in Total price.

### Error 8 - Invisible items in the list
No error message, and no system crash - but this bugg causes invicible row number and item name for all but the last item in the list. Caused by splitting on new Line Character (\n) in the ShoppingList.Load() method. In Windows, a row with text is ended with a Carriage Return (\r) together with a New Line character. Thus, splitting on new line character only will leave the Carriage Return after each item, causing all but the last one to appear as hidden when being printed to the console.

**How to replicate error 8:**\
-start the program: dotnet run

**Patch 8:**\
Changed to split on "\r\n" instead of only on \n


### Error 9 - Possible to save items with invalid (non-numeric or negative) prices

**How to replicate error 9:**\
-start the program: dotnet run
-choose option 1, to add an item
-key in null/whitespace as name
-key in non-numeric characters as price 

**Patch 9:**\
Added 3 external functions that check for valid string, integer, or options (yes or no) to prevent incorrect user input. Added an additional user confirmation to choose to add, or cancel adding the item to th elist.

Note: negative values accepted for now, this will be patched in the second part of Kunskapskontroll 2, del 2 - bygg ut programmet.

### Error 10 - Find items is case-sensitive

**How to replicate error 10:**\
-start the program: dotnet run
-choose menu option 4 to search for an item
-type the name of an existing item in the list, change one or more characters to upper/lower case

**Patch 10:**\
Find item is now case-insensitive (added string.ToLower() on both sides of the comparison)

### Error 11 - Not possible to find similar items with the same name (but different prices)

**How to replicate error 11:**\
-start the program: dotnet run
-choose option 1, add an item and price
-choose option 1, add the same item again
-choose option 4, search for that the item
Result - only one item (the item with lowest row number) will be found

**Patch 11:**\
This patch will at the time of writing not be implemented, just documented. The bug is not critical - and out of scope of this exercise. This error does not cause a program crash, and it is easily avoided by the user by choosing different item names for similar items but with different prices (i.e. for two similar "Mjölk" items, use "SkåneMejeriet standard Mjölk" and "Arla standard Mjölk"). For duplicates of identical items, the user could easily key in i.e. 2xMilk.

However, there is an easy fix to this error, should there be enough time and need. In the foreach loop, search through each item in items list. If a specific item.Name is found, store it and pair it with its item.Price (i.e. in a dictionary), or store all item objects in a list. This dictionary or list of hits will form the basis for the format-choise on how the results will be displayed. A format could i.e. be the name of the item, its price, and how many hits, followed by another item with the same name but with a different price.

### Error 12 - Save to file catches all exceptions, and ignores all errors

**How to replicate error 12:**\
-No successfull attempts to provoke any of the possible exceptions - except by changing the path (which is hardcoded) to a non existing folder. This cant normally be triggered from just running the program without editing the hardcoded path. However, since an absolute path was provided to prevent the described debugger induced error 1 above, one could assume that by running this program in another environemnt (computer), a specific DirectoryNotFoundException might be thrown when trying to save a file with the wrong absolute path.

**Patch 12:**\
According to the instructions, no catch{} can be empty. The current ShoppingList.Save() contains a an empty catch, that will ignore all errors. To adhere to the rules, I have decided to specify a File and System I/O Error (DirectoryNotFoundException) in the ShoppingList.Save() method.

## Part 2 - Scaling/Add more features

### Task 1 - Item protects itself (Item skall skydda sig själv)
**Make a robust Item class**\ 
Instanciation of a new Item should prevent:
(I) empty name - throwing ArgumentException
(II) negative price - throwing ArgumentOutOfRangeException

**Patch task 1:**\
Item class has been updated, the constructor could in theory throw the corresponding exceptions when they occur. In reality the item.constructor will only throw ArgumentOutOfRangeException when the user tries to add an item with a negative price. The handling of null/whitespace name was added in patch 9 above.

Thoughts-lesson learned: using System try-catch is in theory less efficient than throwing exceptions with if-statements. The former would throw an exception after the error occured, and the latter prevents the error to occur. In this example the IF-induced exception prevented an invalid object to be created. A simple if-statement could have done the job marginally better by preventing an invalid user input at an earlier stage.

### Task 2 - The list shall have a max budget (Listan skall ha ett budgettak)
**Add feature, max-budget**\ 
ShoppingList has a new feature, a limited budget. The total price is not allowed to exceed the limit. Addition of an item that exceed the limit is not allowed.

**Patch task 2:**\
There are different options on how to implement this feature in terms of the (I) type of try-catch, (II) a pure if-statement that return i.e. false, and (III) where to implement the code. Implementing a test before adding an item (that exceeds the budget) to the list is more efficient than testing the total price after adding an item. The latter would require removal of that item from the list. The choice of letting list.Add() throw an exception, or an If-statement returning i.e. false, falls down to the fact that a try-catch was already implemented in the list.Add() in program.cs. Thus, the choice to add another catch in the existing program.cs try-catch was implemented.