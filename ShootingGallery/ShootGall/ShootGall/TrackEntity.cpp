#include "TrackEntity.h"
#include <iostream>
//Constructor that initialises things to null. You should only call this in 
// a factory method. Note that draw() makes it very dangerous to use an 
// an improperly loaded entity of this type since the draw shape is NULL.
TrackEntity::TrackEntity(int id) : GameEntity(id), type(eTrackType::NONE), initialized(false), 
	time(0), oscillation(0.0f), speed(0.0f), scale(0.0f, 0.0f), height(0)
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
	
	/* Figure out how many sprites to use. */
	if (!initialized) {
		int totalWidth = 0;
		int maxSprites = 0;

		if (type == eTrackType::CURTAIN) {
			int heightSprites = ((int)m_drawShape->getLocalBounds().height / (int)mainImage->getLocalBounds().height);
			int widthSprites = ((int)m_drawShape->getLocalBounds().width / (int)mainImage->getLocalBounds().width) + 1;
			
			for (int i = 0; i < heightSprites; i++) {
				for (int j = 0; j < widthSprites; j++) {
					sf::Sprite spr = sf::Sprite(*mainImage);
					sf::Vector2f pos = sf::Vector2f(mainImage->getLocalBounds().width * j, mainImage->getLocalBounds().height * i);
					spr.setPosition(m_drawShape->getPosition() + pos);
					sprites.push_back(spr);
				}
			}

			maxSprites = ((int)m_drawShape->getLocalBounds().width / (int)bottomImage->getLocalBounds().width) + 1;

			for (int i = 0; i < maxSprites; i++) {
				sf::Sprite spr = sf::Sprite(*bottomImage);
				sf::Vector2f pos = sf::Vector2f(m_drawShape->getPosition().x + (mainImage->getLocalBounds().width * i),
												m_drawShape->getPosition().y + m_drawShape->getGlobalBounds().height - bottomImage->getLocalBounds().height);
				spr.setPosition(m_drawShape->getPosition() + pos);
				sprites.push_back(spr);
			}

			height = m_drawShape->getGlobalBounds().height;
		}
		else {
			for (int i = 0; i < images.size(); i++) {
				images[i]->setScale(scale);
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
		}

		initialized = true;
	}

	/* Determine track type. */
	if (type == eTrackType::CURTAIN) {
		sf::Keyboard kb;
		int offset = 0;
		if (kb.isKeyPressed(kb.Up) && height > bottomImage->getGlobalBounds().height) {
			offset = -5;
		}
		else if (kb.isKeyPressed(kb.Down) && height < m_drawShape->getGlobalBounds().height) {
			offset = 5;
		}
		height += offset;

		for (int i = 0; i < sprites.size(); i++) {
			sf::Sprite* spr = &sprites[i];
			sf::Vector2f pos = spr->getPosition();
			
			pos.y += offset;

			sprites[i].setPosition(pos);
		}
	}
	else if (type == eTrackType::WAVE_TRACK) {
		for (int i = 0; i < sprites.size(); i++) {
			sf::Sprite* spr = &sprites[i];
			sf::Vector2f pos = spr->getPosition();

			float s = speed;
			if (i % 2 == 1) {
				s = -s;
			}

			pos.y = (sin(time * s) * oscillation) + m_drawShape->getPosition().y;
			pos.x = i * sprites[i].getGlobalBounds().width;

			sprites[i].setPosition(pos);
		}
	}
	else if (type == eTrackType::CONVEYOR_TRACK) {
		/* Conveyor Track */
		/* Incomplete because it is difficult */

		// semi-circle
		// x = sqrt(1 - y^2)
		
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
	else if (type == eTrackType::FUNCTION_TRACK) {
		for (int i = 0; i < sprites.size(); i++) {
			sf::Sprite* spr = &sprites[i];
			sf::Vector2f pos = spr->getPosition();

			float s = speed;
			if (i % 2 == 1) {
				s = -s;
			}

			pos.x = i * sprites[i].getGlobalBounds().width;

			float x = pos.x - m_drawShape->getGlobalBounds().left;
			float y = pos.y - m_drawShape->getGlobalBounds().top;
			float offset = (sin(time * s) * oscillation);
			pos.y = m_drawShape->getPosition().y + (-cos(x)) + offset;

			sprites[i].setPosition(pos);
		}
	}
}