#include "GameEntity.h"

#include <string>
#include <map>

#include <SFML/Graphics.hpp>

#include "tinyxml2.h"
#include "StringUtils.h"

#include "../Editor/EntityTypes.cs"

#include "StaticEntity.h"
#include "TrackEntity.h"

//Factory method for creating a static rectangle. It gets drawn to a position and has a 
// size, an outline color, and a fill color. Static entities aren't expected to move.
//The string of properties must contain the necessary values - the required props can
// be found by checking EntityTypes.cs. If a property is missing or the object cannot be
// instantiated for any reason, the object is destroyed and NULL is returned.
//MIDTERM: You need to add a new property to the rectangles which sets the outline
// thickness. In the event an outline thickness is not found, you should output
// a warning message and set the thickness to a default value (1.0f). 
GameEntity *StaticEntity::CreateRectangularEntity(Editor::EntityType type, int ID,
	std::map<std::string, std::string> &props) 
{
	//Create the new entity and set up the shape.
	StaticEntity *ge = new StaticEntity(ID);
	ge->m_drawShape = new sf::RectangleShape();
	ge->m_drawShape->setOutlineThickness(1.0f);
	//Check that all properties are present, convert them from strings to the required data type,
	// and set up the object. Note that ideally the order in which the properties are processed should
	// not matter.
	bool propMissing = false;
	for (std::size_t i = 0; i < Editor::EntityProperties[(int)type].size(); i++) {
		std::string propName = Editor::EntityProperties[(int)type][i];
		std::map<std::string, std::string>::iterator it = props.find(propName);
		//Currently we fail as soon as we miss a single value - if we have optional parameters we will
		// need to make this logic a bit more complex.
		if (it == props.end()) {
			propMissing = true;
			delete ge;
			return NULL;
		}
		else {
			//If the property is found, convert it to the appropriate type and initialise the object with
			// the value.
			if (propName == "Dimensions") {
				sf::IntRect dim = StringUtils::FromString<sf::IntRect>(it->second);
				ge->m_drawShape->setPosition(sf::Vector2f((float)dim.left, (float)dim.top));
				((sf::RectangleShape *)(ge->m_drawShape))->setSize(sf::Vector2f((float)dim.width, (float)dim.height));
			}
			else if (propName == "FillColor") {
				ge->m_drawShape->setFillColor(StringUtils::FromString<sf::Color>(it->second));
			}
			else if (propName == "OutlineColor") {
				ge->m_drawShape->setOutlineColor(StringUtils::FromString<sf::Color>(it->second));
			}
		}
	}
	//Return the initialised game entity.
	return ge;
}

//MIDTERM: To be completed.
GameEntity *StaticEntity::CreateCircularEntity(Editor::EntityType type, int ID,
	std::map<std::string, std::string> &props) {

	StaticEntity *ge = new StaticEntity(ID);
	ge->m_drawShape = new sf::CircleShape();
	//MIDTERM: You got this right?
	ge->m_drawShape->setOutlineThickness(1.0f);
	/* Of course I do. */

	bool propMissing = false;
	for (std::size_t i = 0; i < Editor::EntityProperties[(int)type].size(); i++) {
		std::string propName = Editor::EntityProperties[(int)type][i];
		std::map<std::string, std::string>::iterator it = props.find(propName);
		//Currently we fail as soon as we miss a single value - if we have optional parameters we will
		// need to make this logic a bit more complex.
		if (it == props.end()) {
			propMissing = true;
			delete ge;
			return NULL;
		}
		else {
			//If the property is found, convert it to the appropriate type and initialise the object with
			// the value.

			if (propName == "Position") {
				sf::Vector2f dim = StringUtils::FromString<sf::Vector2f>(it->second);
				ge->m_drawShape->setPosition(sf::Vector2f((float)dim.x, (float)dim.y));
			}
			else if (propName == "Radius") {
				float rad = StringUtils::FromString<float>(it->second);
				((sf::CircleShape *)(ge->m_drawShape))->setRadius(rad);
			}
			else if (propName == "FillColor") {
				ge->m_drawShape->setFillColor(StringUtils::FromString<sf::Color>(it->second));
			}
			else if (propName == "OutlineColor") {
				ge->m_drawShape->setOutlineColor(StringUtils::FromString<sf::Color>(it->second));
			}
		}
	}
	//Return the initialised game entity.
	return ge;
}

//MIDTERM: You will need additional factory functions with the correct signature to handle 
// any new gameentity types you create.

GameEntity *StaticEntity::CreateSpriteEntity(Editor::EntityType type, int ID,
	std::map<std::string, std::string> &props) {

	StaticEntity *ge = new StaticEntity(ID);
	ge->m_drawShape = new sf::RectangleShape();

	bool propMissing = false;
	for (std::size_t i = 0; i < Editor::EntityProperties[(int)type].size(); i++) {
		std::string propName = Editor::EntityProperties[(int)type][i];
		std::map<std::string, std::string>::iterator it = props.find(propName);
		//Currently we fail as soon as we miss a single value - if we have optional parameters we will
		// need to make this logic a bit more complex.
		if (it == props.end()) {
			propMissing = true;
			delete ge;
			return NULL;
		}
		else {
			//If the property is found, convert it to the appropriate type and initialise the object with
			// the value.
			if (propName == "Dimensions") {
				sf::IntRect dim = StringUtils::FromString<sf::IntRect>(it->second);
				ge->m_drawShape->setPosition(sf::Vector2f((float)dim.left, (float)dim.top));
				((sf::RectangleShape *)(ge->m_drawShape))->setSize(sf::Vector2f((float)dim.width, (float)dim.height));
			}
			else if (propName == "Image") {
				sf::Texture* txt = new sf::Texture();
				txt->loadFromFile(it->second);
				ge->m_drawShape->setTexture(txt);
			}
		}
	}
	//Return the initialised game entity.
	return ge;
}


GameEntity *TrackEntity::CreateWaveTrackEntity(Editor::EntityType type, int ID,
	std::map < std::string, std::string > &props) {

	TrackEntity *ge = new TrackEntity(ID);
	ge->m_drawShape = new sf::RectangleShape();
	ge->type = eTrackType::WAVE_TRACK;

	bool propMissing = false;
	for (std::size_t i = 0; i < Editor::EntityProperties[(int)type].size(); i++) {
		std::string propName = Editor::EntityProperties[(int)type][i];
		std::map<std::string, std::string>::iterator it = props.find(propName);
		//Currently we fail as soon as we miss a single value - if we have optional parameters we will
		// need to make this logic a bit more complex.
		if (it == props.end()) {
			propMissing = true;
			delete ge;
			return NULL;
		}
		else {
			//If the property is found, convert it to the appropriate type and initialise the object with
			// the value.
			if (propName == "Dimensions") {
				sf::IntRect dim = StringUtils::FromString<sf::IntRect>(it->second);
				ge->m_drawShape->setPosition(sf::Vector2f((float)dim.left, (float)dim.top));
				((sf::RectangleShape *)(ge->m_drawShape))->setSize(sf::Vector2f((float)dim.width, (float)dim.height));
			}
			else if (propName == "Oscillation") {
				ge->oscillation = StringUtils::FromString<float>(it->second);
			}
			else if (propName == "Speed") {
				ge->speed = StringUtils::FromString<float>(it->second);
			}
			else if (propName == "Image") {
				sf::Sprite* spr = new sf::Sprite();
				sf::Texture* txt = new sf::Texture();
				txt->loadFromFile(it->second);
				spr->setTexture(*txt);
				spr->setPosition(sf::Vector2f(350.0f, 350.0f));
				ge->images.push_back(spr);
			}
			else if (propName == "Scale") {
				sf::Vector2f dim = StringUtils::FromString<sf::Vector2f>(it->second);
				ge->scale = sf::Vector2f((float)dim.x, (float)dim.y);
			}
		}
	}
	//Return the initialised game entity.
	return ge;
}


GameEntity *TrackEntity::CreateConveyorTrackEntity(Editor::EntityType type, int ID,
	std::map < std::string, std::string > &props) {

	TrackEntity *ge = new TrackEntity(ID);
	ge->m_drawShape = new sf::RectangleShape();
	ge->type = eTrackType::CONVEYOR_TRACK;

	bool propMissing = false;
	for (std::size_t i = 0; i < Editor::EntityProperties[(int)type].size(); i++) {
		std::string propName = Editor::EntityProperties[(int)type][i];
		std::map<std::string, std::string>::iterator it = props.find(propName);
		//Currently we fail as soon as we miss a single value - if we have optional parameters we will
		// need to make this logic a bit more complex.
		if (it == props.end()) {
			propMissing = true;
			delete ge;
			return NULL;
		}
		else {
			//If the property is found, convert it to the appropriate type and initialise the object with
			// the value.
			if (propName == "Dimensions") {
				sf::IntRect dim = StringUtils::FromString<sf::IntRect>(it->second);
				ge->m_drawShape->setPosition(sf::Vector2f((float)dim.left, (float)dim.top));
				((sf::RectangleShape *)(ge->m_drawShape))->setSize(sf::Vector2f((float)dim.width, (float)dim.height));
			}
			else if (propName == "Oscillation") {
				ge->oscillation = StringUtils::FromString<float>(it->second);
			}
			else if (propName == "Speed") {
				ge->speed = StringUtils::FromString<float>(it->second);
			}
			else if (propName == "Image") {
				sf::Sprite* spr = new sf::Sprite();
				sf::Texture* txt = new sf::Texture();
				txt->loadFromFile(it->second);
				spr->setTexture(*txt);
				spr->setPosition(sf::Vector2f(350.0f, 350.0f));
				ge->images.push_back(spr);
			}
			else if (propName == "Scale") {
				sf::Vector2f dim = StringUtils::FromString<sf::Vector2f>(it->second);
				ge->scale = sf::Vector2f((float)dim.x, (float)dim.y);
			}
		}
	}
	//Return the initialised game entity.
	return ge;
}


GameEntity *TrackEntity::CreateFunctionTrackEntity(Editor::EntityType type, int ID,
	std::map < std::string, std::string > &props) {

	TrackEntity *ge = new TrackEntity(ID);
	ge->m_drawShape = new sf::RectangleShape();
	ge->type = eTrackType::FUNCTION_TRACK;

	bool propMissing = false;
	for (std::size_t i = 0; i < Editor::EntityProperties[(int)type].size(); i++) {
		std::string propName = Editor::EntityProperties[(int)type][i];
		std::map<std::string, std::string>::iterator it = props.find(propName);
		//Currently we fail as soon as we miss a single value - if we have optional parameters we will
		// need to make this logic a bit more complex.
		if (it == props.end()) {
			propMissing = true;
			delete ge;
			return NULL;
		}
		else {
			//If the property is found, convert it to the appropriate type and initialise the object with
			// the value.
			if (propName == "Dimensions") {
				sf::IntRect dim = StringUtils::FromString<sf::IntRect>(it->second);
				ge->m_drawShape->setPosition(sf::Vector2f((float)dim.left, (float)dim.top));
				((sf::RectangleShape *)(ge->m_drawShape))->setSize(sf::Vector2f((float)dim.width, (float)dim.height));
			}
			else if (propName == "Oscillation") {
				ge->oscillation = StringUtils::FromString<float>(it->second);
			}
			else if (propName == "Speed") {
				ge->speed = StringUtils::FromString<float>(it->second);
			}
			else if (propName == "Image") {
				sf::Sprite* spr = new sf::Sprite();
				sf::Texture* txt = new sf::Texture();
				txt->loadFromFile(it->second);
				spr->setTexture(*txt);
				spr->setPosition(sf::Vector2f(350.0f, 350.0f));
				ge->images.push_back(spr);
			}
			else if (propName == "Scale") {
				sf::Vector2f dim = StringUtils::FromString<sf::Vector2f>(it->second);
				ge->scale = sf::Vector2f((float)dim.x, (float)dim.y);
			}
		}
	}
	//Return the initialised game entity.
	return ge;
}

//Collection of factory methods mapped to entity types in EntityTypes.cs. Put the address of the methods
// you want at the appropriate spot in the array. If you want to rewrite this to use the C++11 standard
// feel free. Please bring any troubles with debugging to Will.
std::vector<GameEntity *(*)(Editor::EntityType type, int ID, std::map<std::string, std::string> &props)> 
factories = {
	NULL,
	&StaticEntity::CreateRectangularEntity,
	&StaticEntity::CreateCircularEntity,
	&StaticEntity::CreateSpriteEntity,
	&TrackEntity::CreateWaveTrackEntity,
	&TrackEntity::CreateConveyorTrackEntity,
	&TrackEntity::CreateFunctionTrackEntity
};


//Don't call this unless you have a very good reason, it's declared protected for a reason.
GameEntity::GameEntity(int id) : m_id(id)
{
}


GameEntity::~GameEntity()
{
	
}

//Loads the object from an xml element. Note that this method does not care what type of
// game entity it is loading - it uses the factories collection to ensure the correct
// factory method is called.
GameEntity *GameEntity::loadFromXml(tinyxml2::XMLElement *element) {

	std::string att = element->Attribute("Type");
	Editor::EntityType t = (Editor::EntityType)StringUtils::FromString<int>(att);
	att = element->Attribute("ID");
	int id = StringUtils::FromString<int>(att);
	std::map<std::string, std::string> properties;
	tinyxml2::XMLElement *propEl = element->FirstChildElement();
	while (propEl != NULL) {
		properties[propEl->Name()] = propEl->GetText();
		propEl = propEl->NextSiblingElement();
	}
	return factories[(int)t](t, id, properties);
}

