#include <iostream>

#include <SFML/Graphics.hpp>

#include "Level.h"

int main(int argc, char *argv[]) {
	std::cout << "Welcome to Shooting Gallery!" << std::endl;

	//Create a new level and load it from an xml.
	Level *l = new Level();
	std::string filename;
	bool loaded = false;
	
	while (!loaded) {
		std::cout << "Please provide a filename (for a valid XML level): ";
		std::getline(std::cin, filename);
		loaded = l->loadFromFile("../Debug/" + filename);
		
		if (!loaded) {
			std::cout << "Couldn't load requested file." << std::endl;
		}
		std::cout << std::endl;
	}

	//Create the render window and gaming clock.
	sf::RenderWindow window(sf::VideoMode(800, 600), "Shooting Gallery");
	sf::Clock gameClock;
	float gameTime = 0.0f;
	float currentTime = 0.0f;
	float delta = 1.0f / 30.0f;

	//Main gameplay loop
	bool running = true;
	while (running) {
		//Flag for handling a draw.
		bool drawRequested = false;
		//Event polling and handling.
		sf::Event ev;
		while (window.pollEvent(ev)) {
			switch (ev.type) {
			case sf::Event::Closed:
				running = false;
				break;
			}
		} //event polling and handling.
		
		//Update loop for the game using a fixed timestep.
		while ((currentTime = gameClock.getElapsedTime().asSeconds()) > gameTime) {
			gameTime += delta;
			l->update(delta);
			drawRequested = true;
		}

		//Render if requested.
		if (drawRequested) {
			window.clear(sf::Color::White);
			window.draw(*l);
			window.display();
		}
	}
	//Close the window and clean-up
	window.close();
	delete l;
	//Return success
	return 0;
}