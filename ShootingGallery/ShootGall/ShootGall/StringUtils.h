#pragma once
#include <sstream>
#include <string>
#include <vector>

#include <SFML/Graphics/Rect.hpp>
#include <SFML/Graphics/Color.hpp>

//Collection of useful methods for playing around with strings. 
namespace StringUtils {
	//Split strings according to a delimeter.
	//http://stackoverflow.com/a/236803
	std::vector<std::string> &split(const std::string &s, char delim, std::vector<std::string> &elems) {
		std::stringstream ss(s);
		std::string item;
		while (std::getline(ss, item, delim)) {
			elems.push_back(item);
		}
		return elems;
	}
	//Split strings according to a delimeter
	//http://stackoverflow.com/a/236803
	std::vector<std::string> split(const std::string &s, char delim) {
		
		std::vector<std::string> elems;
		split(s, delim, elems);
		return elems;
	}

	//Attempts to convert a string to a type. If this fails it
	// returns false and param out should not be trusted.
	template<typename T>
	bool TryFromString(std::string const &in, T &out) {
		std::istringstream ss(in);
		return static_cast<bool>(ss >> out);
	}

	//An overly trusting coversion routine. If the conversion fails
	// it just returns whatever.
	template <typename T>
	T FromString(std::string const &in) {
		T out;
		std::stringstream ss(in);
		ss >> out;
		return out;
	}
	
	//Explicit specialisation of the FromString overly trusting 
	// conversion routine for handling IntRects.
	template<>
	sf::IntRect FromString(std::string const &in) {
		sf::IntRect r;
		std::vector<std::string> nums = split(in, ' ');
		if (nums.size() != 4) {
			return r;
		}
		r.left = FromString<int>(nums[0]);
		r.top = FromString<int>(nums[1]);
		r.width = FromString<int>(nums[2]);
		r.height = FromString<int>(nums[3]);
		return r;
	}

	//Explicit specialisation of the FromString overly trusting 
	// conversion routine for handling Colors.
	template<>
	sf::Color FromString(std::string const &in) {
		sf::Color c;
		std::vector<std::string> nums = split(in, ' ');
		if (nums.size() != 4) {
			return c;
		}
		
		c.r = FromString<int>(nums[0]);
		c.g = FromString<int>(nums[1]);
		c.b = FromString<int>(nums[2]);
		c.a = FromString<int>(nums[3]);

		return c;
	}
}