#pragma once
#include "GameEntity.h"
#include <vector>
#include <SFML/Graphics.hpp>
/// <summary>
/// TrackEntities are placed in the world and have moving parts. They can be waves, conveyor belts,
/// or tracks defined by a function. You don't construct these directly from a constructor, but should 
/// instead use a factory method, which are implemented over in GameEntity.cpp instead of TrackEntity.cpp.
/// </summary>
/// <author>
/// original: Rion Meehan
/// </author>


/* All the different types of tracks. */
enum eTrackType {
	NONE,
	CURTAIN,
	WAVE_TRACK,
	CONVEYOR_TRACK,
	FUNCTION_TRACK
};

/* All potential functions for the function track, because formula parsing
 * is far too difficult to justify. */
enum eTrackFunction {
	BLANK,
	X,
	NX,
	SINX,
	NSINX,
	COSX,
	NCOSX
};


class TrackEntity :
	public GameEntity 
{
public:
	virtual ~TrackEntity();

	/* Creates a Curtain. Body is in GameEntity.h. Returns NULL if props are incorrect or incomplete.*/
	static GameEntity *CreateCurtainEntity(Editor::EntityType type, int ID,
		std::map<std::string, std::string> &props);

	/* Creates a Wave Track. Body is in GameEntity.h. Returns NULL if props are incorrect or incomplete.*/
	static GameEntity *CreateWaveTrackEntity(Editor::EntityType type, int ID,
		std::map<std::string, std::string> &props);

	/* Creates a Conveyor Track. Body is in GameEntity.h. Returns NULL if props are incorrect or incomplete.*/
	static GameEntity *CreateConveyorTrackEntity(Editor::EntityType type, int ID,
		std::map<std::string, std::string> &props);

	/* Creates a Function Track. Body is in GameEntity.h. Returns NULL if props are incorrect or incomplete.*/
	static GameEntity *CreateFunctionTrackEntity(Editor::EntityType type, int ID,
		std::map<std::string, std::string> &props);

protected:
	//Don't call me from anywhere but the factory methods!
	TrackEntity(int id);

private:
	//The entity that will be rendered to the screen. 
	sf::Shape *m_drawShape;

	/* A list of images to compose the sprites with. */
	std::vector<sf::Sprite*> images;
	sf::Vector2f scale;

	/* Variables specfic to the curtain */
	sf::Sprite* bottomImage;
	sf::Sprite* mainImage;
	float height;

	/* A list of sf::Sprites to render. */
	std::vector<sf::Sprite> sprites;

	/* Track settings and variables */
	eTrackType type;
	bool initialized;
	float time;
	float oscillation;
	float speed;

	/* Variables specific to the function track */
	eTrackFunction function;

	//Draw method inherited from sf::Drawable.
	virtual void draw(sf::RenderTarget &target, sf::RenderStates states) const;

	virtual void update(float delta);
};

