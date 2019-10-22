
The code insinde the following scripts is commented. Here we presend their main behaviours and purposes:

CarController.cs - handles the player car behaviour, all the way from rotations, movement, behaviour on accident, gear shifting, RPM simulation, suspension simulations, etc.

GameplayController.cs - handles the gameplay elements, score/distance, checkpoints, spawns traffic cars, instantiates the player car

SoundController.cs - handles all sounds, it is made as a static instance and can be accessed from other scripts too

trafficCar.cs - handles the behaviour of the traffic cars, movement, signal before lane changing, change lanes, accidents etc.

CarSelection.cs - handles the car selection in the main menu, and the car customisation

MainMenu.cs - handles the initial setup of the game, when running it for the first time on a device. If you want to reset all the player pref data,
just uncomment the code line PlayerPref.DeleteAll(), run the game, than comment it again, and all player pref values will be reset

LevelSelection.cs - handles the level selection, and the main menu button events

TotalCoins.cs - handles all purchase transactions for the game: upgrading a car, buying new wheels, paint, new cars, etc.



If you have any questions, e-mail us at etheriumstudio@gmail.com

