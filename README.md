# Run Escape
BCIT COMP 3951 Technical Programming Course Project
### Description
A game where the player traverses an overworld full of NPC’s (non-playable characters), some friendly and some not. When encountering an enemy, you and the enemy are transported to a “Battle scene” where they engage in turn-based combat. If the player is victorious they will gain experience points.
## Interface
This game will contain several interfaces: Overworld, Battle Scene, and Menu Screen.
### Overworld
The overworld is a very dynamic interface in which there are many choices. The player can interact with objects, the player’s enemies, etc. The player may encounter impassable objects such as walls in which their movement will be impeded. They may also encounter enemies which will transport them to the battle scene. The player might even just have a nice chat with an NPC in which they scroll through a dialog box.
![image](https://github.com/SamuelT95/Scapers/assets/64446306/84c33c48-9b50-4f0d-9490-283863badd97)

### Battle Scene
The battle scene will be a more traditional UI but with some visual aspects. The player and the enemy will take turns attacking each other until one of their health bars reach 0. The enemy will randomly select attacks from a pool it has, the player will select from the attack menu. There will be a main menu with options such as attack or run. Selecting an attack will bring up a sub-menu for the player to select the attack.
There is now a new submenu and dialog system which serves as a way to advance attacks. The player will select his attack type, then choose an attack, the dialog then displays what attack is chosen. The player will press a button to advance dialog to see if their attack was successful, this continues for the enemy attacks as well in a similar fashion.
![image](https://github.com/SamuelT95/Scapers/assets/64446306/4f46cbc4-261c-49fb-b58e-77913640689a)

### Menu Screen
The player can bring up a menu screen where they can exit the game if they choose to do so. This is a very basic interface but could hold many potential options when in the game. For our purposes, it is out of scope to include anything beyond exiting the game.
![image](https://github.com/SamuelT95/Scapers/assets/64446306/2441df09-c98f-4c52-a0ec-f5063410adfa)

## Functionality
### Levelling System
The levelling system is meant to provide a basis for comparing strength relative to enemies. Both players' and enemies' stats will scale based on their current level. If you were to encounter an enemy with a significantly higher level than yours, it would be unlikely that you would be able to defeat it. This allows the player to get a sense of accomplishment and keep battles exciting.
Levels are determined by the player’s Experience points. A player will receive exp points for defeating enemies or perhaps finishing a quest. The number of exp points required for gaining a level will increase every time the player increases in level.
### Battle System
In the battle system, the player and the enemy will take turns attacking each other. When one of their health reaches 0, they will lose the battle. If the player dies it is game over which will restart the game. If the player is victorious, the enemy will disappear from the overworld and the player can continue on their journey.
### Overworld
The overworld is a vibrant playground for the player, beckoning players to explore the map and interact with NPCs and enemies. It is the core functionality to instigate events such as a battle, a dialog with an NPC, and more. The map gives a sense of exploration for the player, as the map is designed with meticulous attention to detail, featuring imported assets to make it look realistic. Although the realism is limited as it is a pixel-art design, there are winding caves, bustling villages, impeding castles, and other explorable captivating structures.

## Class Diagram
![image](https://github.com/SamuelT95/Scapers/assets/64446306/0a2aff69-6771-47ef-911f-479257ce9649)
![image](https://github.com/SamuelT95/Scapers/assets/64446306/da615d00-d489-4e2a-ac7e-9713089f8f7e)

## Technology
### Unity
- Unity is a framework for game development which aids in level creation and provides common functionalities. It has a bunch of free graphical assets that will help us represent our game map/characters.
- Unity has tons of tutorials provided on its website as well as user made tutorials on YouTube.
- Unity allows for the use of templates which removes the need to code common functionality. For example, you can use a 2D game template to streamline the creation of 2D games or you can select various forms of 3D such as first person and third person.
### Visual studio
Visual studio is a developer environment that helps us write all our C# scripts for the game. Microsoft has mountains of documentation regarding development in C# with visual studio. Not only does Microsoft provide excellent documentation, but they also provide examples on how to use certain aspects which rapidly aid in the development of programs written in C#.
