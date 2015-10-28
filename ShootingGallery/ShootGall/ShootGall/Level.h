#pragma once
#include <string>
#include <vector>

#include <SFML\Graphics\Drawable.hpp>

#include "GameEntity.h"

/// <summary>
/// A level is currently just a bag for entities. Some of those entities are static, some are
///  dynamic. Up to you to figure out how to implement that. When you update the level it should
///  update all the entities that need updating, when you draw the level it should draw all the 
///  entities that need drawing.
/// </summary>
/// <author>
/// original: Kevin Forest
/// </author>
class Level :
	public sf::Drawable
{
public:
	//Default constructor means you can put levels in a collection and keep them in
	// various states of initialisation.
	Level();
	//Don't leak memory! You have a big ole bag of pointers down there.
	virtual ~Level();
	//Call this to initialise the level and load it. Currently a "level" is an xml file that is full of 
	// game entities. Each entity is loaded by passing in the XMLElement to GameEntity::loadFromFile, at
	// which point it handles getting the entity type and calling the correct factory method.
	bool loadFromFile(std::string filename);
	//MIDTERM: Implement me! Make the dynamic objects move!
	void update(float delta) {}
	//Inherited from sf::Drawable, should render each of the game entities in the level.
	virtual void draw(sf::RenderTarget &target, sf::RenderStates states) const;
private:
	//Collection for the levels game entities. If you decide on multiple collections for different
	// jobs make sure you do the clean up properly.
	std::vector<GameEntity *> m_entities;
};

