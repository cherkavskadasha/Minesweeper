### Functionality

#### UI

* Main Menu
  * Register
  * Login
  * Logout
  * Exit
  * Check History
  * Choose difficulty and field size
  * Start Game
* Game
  * Display the number of adjacent mines to a cell
  * Display flag on cell
  * Display score
  * Notification of victory or defeat
* Game End Screen
  * Display results
  * Display "Play again" and "Back to menu" buttons

#### Game Logic

* Create a field of the required size with the required number of mines
* Check if there is a mine on the cell
* Place the flag on the cell
* Change the number of score depending on the cell and difficulty
* End the game with a loss if the player clicks on the mine
* End the game with a win if the player has opened all the cells except the mines
* Use bonuses (open a cell, open a mine, make the next click safe)
* Start the game again if the user wants
* Return to the main menu if the user wants (after finishing the game or by pressing Esc)

#### Saving data

* The data is stored in the SQLServer database
* Create a new record in the Users table when registering
* Check for a record in the Users table when logging in
* Save game data after game ends in Results table
* Get data from the Results table when a player wants to see the history of his games.

### Startup process

Clone the repository, make sure you have the .NET SDK and SQLServer installed on your locale machine, then run the project from the command line or IDE.

### Programming Principles

* DRY(Methods used to avoid duplicating code) - [Click](https://github.com/VadimSush/Minesweeper/blob/master/Minesweeper/Models/ViewModels/MainViewModel.cs#L154-L162)
* Program to Interfaces not Implementations - [One](https://github.com/VadimSush/Minesweeper/blob/master/Minesweeper/Models/ViewModels/MainViewModel.cs#L30), [Two](https://github.com/VadimSush/Minesweeper/blob/master/Minesweeper/Models/GameManager.cs#L33)
* Composition Over Inheritance - [Click](https://github.com/VadimSush/Minesweeper/blob/master/Minesweeper/Models/ViewModels/MainViewModel.cs#L60)
* Single Responsibility Principle - [Click](https://github.com/VadimSush/Minesweeper/blob/master/Minesweeper/Models/Field/Field.cs)
* YAGNI - The project contains only the used code and does not contain unnecessary code, methods, classes, etc.

### Design Patterns

* Command(All interaction between View and ViewModel occurs using Commands, because according to the MVVM pattern, ViewModel should not know about View, and in the case of regular event listener methods, they receive an object from the View that called it, i.e. they receive information about the state of View. Commands also know whether they are enabled or not, which makes it easier to write code.) - [One](https://github.com/VadimSush/Minesweeper/blob/master/Minesweeper/Models/ViewModels/MainViewModel.cs#L74-L88), [Two](https://github.com/VadimSush/Minesweeper/blob/master/Minesweeper/Models/ViewModels/MenuViewModel.cs#L166-L180)
* Strategy(I use this pattern to work with the game's difficulty levels, because each difficulty level is different in the number of mines, the scoring algorithm, the number of available bonuses, etc. This allows me to get rid of large switch-case statements in different parts of the application and easily add new functionality to difficulty levels in the future.) - [Click](https://github.com/VadimSush/Minesweeper/tree/master/Minesweeper/Models/DifficultyStrategy)
* Observer(I use this pattern so that the ViewModel can notify the View that some properties it is observing have changed. I used it using the standard INotifyPropertyChanged interface.) - [A method that is called when one of the selected properties changes](https://github.com/VadimSush/Minesweeper/blob/master/Minesweeper/Models/ViewModels/ObserverModels/GameStatusViewModel.cs#L99-L102)

### Refactoring Techniques

* Extract Method - [Example](https://github.com/VadimSush/Minesweeper/blob/master/Minesweeper/Models/ViewModels/MainViewModel.cs#L164-L179)
* Extract Class - [Example(Extracted from MainViewModel)](https://github.com/VadimSush/Minesweeper/blob/master/Minesweeper/Models/ViewModels/ObserverModels/GameStatusViewModel.cs)
* Consolidate Duplicate Conditional Fragments - [Example](https://github.com/VadimSush/Minesweeper/blob/master/Minesweeper/Models/GameManager.cs#L116)
* Replace Magic Number with Symbolic Constant - [Example](https://github.com/VadimSush/Minesweeper/blob/master/Minesweeper/Models/DifficultyStrategy/IntermediateDifficultyStrategy.cs#L14)
* Rename Method(All methods in the program have been renamed to explain what the method does) - [Example](https://github.com/VadimSush/Minesweeper/blob/master/Minesweeper/Models/ViewModels/Commands/GameCommandService.cs#L16)
