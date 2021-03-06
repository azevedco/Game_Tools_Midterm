#include "TrackEntity.h"
#include <iostream>
//Constructor that initialises things to null. You should only call this in 
// a factory method. Note that draw() makes it very dangerous to use an 
// an improperly loaded entity of this type since the draw shape is NULL.
TrackEntity::TrackEntity(int id) : GameEntity(id), type(eTrackType::NONE), initialized(false), 
	time(0), oscillation(0.0f), speed(0.0f), scale(0.0f, 0.0f), height(0), function(eTrackFunction::BLANK)
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

		if (type == eTrackType::CURTAIN && (int)mainImage->getLocalBounds().height > 0 && (int)mainImage->getLocalBounds().width > 0 &&
										(int)bottomImage->getLocalBounds().height > 0 && (int)bottomImage->getLocalBounds().width > 0) {
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
				maxSprites = ((int)m_drawShape->getLocalBounds().width / (int)totalWidth) + 1;
			}

			for (int i = 0; i < maxSprites; i++) {
				sf::Sprite spr = sf::Sprite(*images[0]);
				sf::Vector2f pos = sf::Vector2f(m_drawShape->getPosition().x + (i * spr.getGlobalBounds().width), m_drawShape->getPosition().y);
				spr.setPosition(pos);
				sprites.push_back(spr);
			}

			if (type == eTrackType::CONVEYOR_TRACK) {
				/* Need more sprites to fill the track */

				/* Bottom side */
				for (int i = 0; i < maxSprites; i++) {
					sf::Sprite spr = sf::Sprite(*images[0]);
					sf::Vector2f pos = sf::Vector2f(m_drawShape->getPosition().x + (i * (spr.getGlobalBounds().width-1)), m_drawShape->getPosition().y + m_drawShape->getGlobalBounds().height);
					spr.setPosition(pos);
					sprites.push_back(spr);
				}

				/* Calculate width against hieght */
				if (totalWidth > 0) {
					maxSprites = ((int)m_drawShape->getLocalBounds().height / (int)totalWidth) + 1;
				}

				/* Right side */
				for (int i = 0; i < maxSprites; i++) {
					sf::Sprite spr = sf::Sprite(*images[0]);
					sf::Vector2f pos = sf::Vector2f(m_drawShape->getGlobalBounds().width, m_drawShape->getPosition().y + (i * spr.getGlobalBounds().width));
					spr.setPosition(pos);
					sprites.push_back(spr);
				}

				/* Left side */
				for (int i = 0; i < maxSprites; i++) {
					sf::Sprite spr = sf::Sprite(*images[0]);
					sf::Vector2f pos = sf::Vector2f(m_drawShape->getPosition().x, m_drawShape->getPosition().y + (i * spr.getGlobalBounds().width));
					spr.setPosition(pos);
					sprites.push_back(spr);
				}
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
		/* Incomplete because it is difficult and this looks fine if you hide most of it */

		// semi-circle
		// x = sqrt(1 - y^2)
		
		for (int i = 0; i < sprites.size(); i++) {
			sf::Sprite* spr = &sprites[i];
			sf::Vector2f pos = spr->getPosition();

			float pheight = m_drawShape->getGlobalBounds().height;
			float pwidth = m_drawShape->getGlobalBounds().width;
			float px = m_drawShape->getPosition().x;
			float py = m_drawShape->getPosition().y;

			float t = speed * delta;

			/* adjust position */
			if (pos.y <= py) {
				//pos.x = (i * spr->getGlobalBounds().width) + (t * delta);
				pos.x += t;
				pos.y = py;
			}

			//if (pos.x >= (width * 0.875f) - px) {
			//	float y = ((pos.y - py) / height);
			//	pos.x = (sqrt(1 - pow(y, 2))) + (width * 0.875f - px);
			//	//int x = (pos.x - px - (width * 0.875f)) / (width - (width * 0.875f));
			//	//pos.y = (sqrt(-pow(x, 2) + 1) + m_drawShape->getPosition().y);
			//	pos.y += 0.5f;
			//	//spr->setRotation(270 * (y-1));
			//}

			if (pos.y >= py + pheight) {
				//pos.x = (i * spr->getGlobalBounds().width) + -(t * delta) + (width * 0.875f);
				pos.x -= t;
				pos.y = py + pheight;
			}

			if (pos.x >= pwidth + px) {
				//float y = ((pos.y - py) / height);
				pos.y += t;
				pos.x = pwidth + px;
			}

			if (pos.x <= px) {
				pos.y -= t;
				pos.x = px;
			}

			/* draw sprite */
			spr->setPosition(pos);
		}
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

			switch (function) {
				case eTrackFunction::X:
					pos.y = m_drawShape->getPosition().y + (x) + offset;
					break;
				case eTrackFunction::NX:
					pos.y = m_drawShape->getPosition().y + (-x) + offset;
					break;
				case eTrackFunction::SINX:
					pos.y = m_drawShape->getPosition().y + (sin(x) * 10) + offset;
					break;
				case eTrackFunction::NSINX:
					pos.y = m_drawShape->getPosition().y + (-sin(x) * 10) + offset;
					break;
				case eTrackFunction::COSX:
					pos.y = m_drawShape->getPosition().y + (cos(x) * 10) + offset;
					break;
				case eTrackFunction::NCOSX:
					pos.y = m_drawShape->getPosition().y + (-cos(x) * 10) + offset;
					break;
				default:
					pos.y = m_drawShape->getPosition().y + offset;
					break;
			}

			sprites[i].setPosition(pos);
		}
	}
}