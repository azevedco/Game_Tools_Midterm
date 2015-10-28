#include "StaticEntity.h"
#include <SFML/Graphics/RenderTarget.hpp>

//Constructor that initialises things to null. You should only call this in 
// a factory method. Note that draw() makes it very dangerous to use an 
// an improperly loaded entity of this type since the draw shape is NULL.
StaticEntity::StaticEntity(int id) : GameEntity(id), m_drawShape(NULL)
{
}


StaticEntity::~StaticEntity()
{
	//Clean up.
	if (m_drawShape != NULL) {
		delete m_drawShape;
		m_drawShape = NULL;
	}
}

//Render the draw shape to screen.
void StaticEntity::draw(sf::RenderTarget &target, sf::RenderStates states) const{
	target.draw(*m_drawShape, states);
}