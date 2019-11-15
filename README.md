# The-Warrior
"The Warrior" is a game that can be played either single player or multiplayer. It
is in TPS view and aim of the game is to be the first one to reach the goal of five kills
and win the game. Player eliminates players by shooting at them and each playground
consists items such as jetpacks or health upgrades.

## Abstraction
As Bachelor’s Final project of Computer Engineering, we decided to create an
arena shooter with Third Person Shooter Elements in a 3D environment and we
achieved to do so. “Game of Warrior”, a digital game that is playable on windows, has
been developed in a level which allows players to play against either artificially
intelligent bots or other players through a network system. In both single player and
multiplayer, you can choose one of three playable characters(Alien, Swat and Villager)
and start playing. Each character has different weapons and each weapon has different
abilities. In single player, you can only play against the character “Swat”. The game
also has two different language options as Turkish and English.

## Single Player Gameplay
Player chooses a character, decides on player count on the map which is between 2 and
4. Difficulty is selected and game starts.

No matter how many times the player dies, when the player reaches 5 kills, game stops.
A list with all player stats showing kills and deaths pops up and game closes itself.

## Multi Player Gameplay
The player who is going to be the host, chooses “Create a game” option, chooses his
own character from the menu, decides the maximum amount of players(game starts
when the maximum amount of player limit is reached along with host) and starts the
game.

Client players choose “connect to a game” option, choose a character, choose an
available game(a schema shows the available games, map name, player count etc.),
connect and start playing.

In both game modes, the goal is to reach the maximum amount of kills which happens
to be 5.

Whenever a player reaches kill count of 5, game stops and the same procedure as single
player happens.

### A screenshot from gameplay
<img src="https://raw.githubusercontent.com/ksavas/The-Warrior/master/w1.png">

## Implementation of Menu Design and Network Background
When designing the menu, we tried to make it not much different from a regular game
menu. One of the 2 manager scripts run here.
Components of the menu are:
- Single Player
- Multiplayer
- Options
- Exit

<img src="https://raw.githubusercontent.com/ksavas/The-Warrior/master/w2.png">

## Single Player Menu
When a player opens up the single player menu, player faces a screen like this:

<img src="https://raw.githubusercontent.com/ksavas/The-Warrior/master/w3.png">

As seen in the figure; player chooses a character and nick, decides the number of
players and selects difficulty.

GameManager.cs saves background information like name, character, player count,
difficulty etc. then sends it to appropriate scripts as parameters. When “Start Game”
option is selected, it starts to game according to choices made by the player.

## Multiplayer Menu
As we stated before, multiplayer has two different options. Either a player starts the
game as a host or joins an alreay created game as a client. When multiplayer is selected,
player has to choose one of these two options.

Gamemanager.cs runs appropriate scripts and provides flow according to the choices of
the player

# Create a Game
When the user chooses “create a game” option, he/she is faced with a screen similar to
single player menu. Player then chooses a character, decides the number of players to
join, starts the game and wits for other players to connect. When all the clients are
connected, game starts simultaneously.

<img src="https://raw.githubusercontent.com/ksavas/The-Warrior/master/w4.png">

# Connect to a Game
When a player wants to connect to a game, player will see a screen like below:

<img src="https://raw.githubusercontent.com/ksavas/The-Warrior/master/w5.png">

Here, player sees hosted games, chooses a game to join, picks a character and nickname
and starts playing

## Options
Here; player can choose between two language options.

<img src="https://raw.githubusercontent.com/ksavas/The-Warrior/master/w6.png">

To hold language information here, we created a list which is key, value pair. We added
string values as counterparts to enum keys. We held the created list in a class and all
the outputs in the game took their string values from this list. Contents of the list change
according to chosen language.
