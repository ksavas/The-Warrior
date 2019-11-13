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
