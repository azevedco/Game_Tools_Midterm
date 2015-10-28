#pragma once
#include <map>
#include <string>

#include <SFML/Graphics/Drawable.hpp>
#include <SFML/Graphics/Shape.hpp>

//Forward declarations.
namespace tinyxml2 {
	class XMLElement;
}
namespace Editor {
	enum EntityType;
}

/// <summary>
/// GameEntity is the parent class of all in-game entities. All GameEntities must be drawable -
/// whether they are updatable is up to you. You can either add a virtual abstract "Update" method
/// or you can create a new interface and have your updateable entities implement it.
/// Entities can be specialised in a number of ways - you can create subclasses or you can use factory
/// methods to construct entities of the same C++ Type that look and/or behave differently. For example
/// you can create multiple factory methods for StaticEntity that allows you to create a static circle,
/// rectangle, or sprite.
/// My preference is to keep all of the factory methods in the same place (I have chosen GameEntity.cpp) but
/// there is no reason why they can be located in their C++ types .cpp file or in a separate .cpp file.
/// All game entities also have a position.
/// </summary>
/// <author>
/// original: Kevin Forest
/// </author>
class GameEntity :
	public sf::Drawable
{
public:	
	
	//Returns the ID
	int ID() const {
		return m_id;
	}
	virtual ~GameEntity();

	//Loads the game entity from an xml element. Handles parsing and calling the 
	// correct factory method, which should be uniquely created for each type of
	// entity.
	static GameEntity *loadFromXml(tinyxml2::XMLElement *element);
	

protected:
	//Create a new game entity. Must have a unique ID (note that the ID is not
	// checked for uniqueness). Should only ever be called from a factory method.
	GameEntity(int id);
	//The position of the entity in 2D space. If the entity has a visual representation
	// that gets drawn, it needs to be moved to this location outside of draw() since 
	// draw() is const.
	sf::Vector2f m_position;
private:
	//The entities id. Will be used once a telegram system is built to send/receive messages.
	int m_id;
	//Draw override. Currently declared as a pure virtual, making GameEntity an abstract class.
	virtual void draw(sf::RenderTarget &target, sf::RenderStates states) const = 0;
	
	
	
};

