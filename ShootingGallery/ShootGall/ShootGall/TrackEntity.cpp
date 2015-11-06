#include "TrackEntity.h"
#include <iostream>
//Constructor that initialises things to null. You should only call this in 
// a factory method. Note that draw() makes it very dangerous to use an 
// an improperly loaded entity of this type since the draw shape is NULL.
TrackEntity::TrackEntity(int id) : GameEntity(id), initialized(false), maxSprites(0), time(0), oscillation(0.0f), speed(0.0f)
{
}


TrackEntity::~TrackEntity()
{
	//Clean up.
	if (m_drawShape != NULL) {
		delete m_drawShape;
		m_drawShape = NULL;
	}
}

//Render the draw shape to screen.
void TrackEntity::draw(sf::RenderTarget &target, sf::RenderStates states) const {
	//target.draw(*m_drawShape, states);

	for (int i = 0; i < sprites.size(); i++) {
		target.draw(sprites[i], states);
	}
}


void TrackEntity::update(float delta) {
	time += delta;
	
	/* figure out how many sprites to use */
	if (!initialized) {
		int totalWidth = 0;
		for (int i = 0; i < images.size(); i++) {
			images[i]->setScale(sf::Vector2f(0.5f, 0.5f));
			totalWidth += images[i]->getGlobalBounds().width;
		}

		if (totalWidth > 0) {
			maxSprites = (int)m_drawShape->getLocalBounds().width / (int)totalWidth;
		}

		for (int i = 0; i < maxSprites; i++) {
			sf::Sprite spr = sf::Sprite(*images[0]);
			spr.setPosition(m_drawShape->getPosition());
			sprites.push_back(spr);
		}

		initialized = true;
	}

	/* Wave Track */
	/*for (int i = 0; i < sprites.size(); i++) {
		sf::Sprite* spr = &sprites[i];
		sf::Vector2f pos = spr->getPosition();

		float s = speed;
		if (i % 2 == 1) {
			s = -s;
		}

		pos.y = (sin(time * s) * oscillation) + m_drawShape->getPosition().y;
		pos.x = i * sprites[i].getGlobalBounds().width;

		sprites[i].setPosition(pos);
	}*/

	// semi-circle
	// x = sqrt(1 - y^2)

	/* Conveyor Track */
	//for (int i = 0; i < maxSprites; i++) {
	//	sf::Sprite* spr = &sprites[i];
	//	sf::Vector2f pos = spr->getPosition();

	//	float height = m_drawShape->getGlobalBounds().height;
	//	float width = m_drawShape->getGlobalBounds().width;
	//	float px = m_drawShape->getPosition().x;
	//	float py = m_drawShape->getPosition().y;

	//	float t = speed * delta;

	//	/* adjust position */
	//	if (pos.y <= py) {
	//		//pos.x = (i * spr->getGlobalBounds().width) + (t * delta);
	//		pos.x += t;
	//	}

	//	//if (pos.x >= (width * 0.875f) - px) {
	//	//	float y = ((pos.y - py) / height);
	//	//	pos.x = (sqrt(1 - pow(y, 2))) + (width * 0.875f - px);
	//	//	//int x = (pos.x - px - (width * 0.875f)) / (width - (width * 0.875f));
	//	//	//pos.y = (sqrt(-pow(x, 2) + 1) + m_drawShape->getPosition().y);
	//	//	pos.y += 0.5f;
	//	//	//spr->setRotation(270 * (y-1));
	//	//}

	//	if (pos.x >= (width * 0.875f) - px) {
	//		float y = ((pos.y - py) / height);
	//		pos.x = (sqrt(1 - pow(y, 2))) + (width * 0.875f - px);
	//		pos.y += 0.5f;
	//	}

	//	if (pos.y >= py + height) {
	//		//pos.x = (i * spr->getGlobalBounds().width) + -(t * delta) + (width * 0.875f);
	//		pos.x -= t;
	//	}

	//	/* draw sprite */
	//	spr->setPosition(pos);
	//}
}