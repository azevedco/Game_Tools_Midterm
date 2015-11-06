//Here be magic dragons!
#if CS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endif

#if CPP
#pragma once
#endif

namespace Editor
{
#if CS
    public
#endif
    enum EntityType
    {
        NONE = 0,
        RECT = 1,
        CIRCLE = 2,
        SPRITE = 3,
        WAVE_TRACK = 4,
        CONVERYOR_TRACK = 5,
        FUNCTION_TRACK = 6,
        COUNT
    };
#if CS
    public static class EntityStrings
    {
        public static String[][] EntityProperties = new String[][]
#endif
#if CPP
    using namespace std;
    class GameEntity;

    std::vector<std::vector<std::string>> EntityProperties=
#endif
        {
#if CS
        new String[]
#endif
            { "" },
#if CS
        new String[]
#endif
            {"Dimensions", "OutlineColor", "FillColor" },
#if CS
        new String[]
#endif
            {"Position", "Radius", "OutlineColor", "FillColor"},
#if CS
        new String[]
#endif
            /* Sprite */
            {"Dimensions", "Image"},
#if CS
        new String[]
#endif
            /* Track 1: Wave Track */
            {"Dimensions", "Oscillation", "Speed", "Image"},
#if CS
        new String[]
#endif
            /* Track 2: Conveyor Track */
            {"Dimensions", "Speed", "Sprites"},
#if CS
        new String[]
#endif
            /* Track 3: Function Track */
            {"Dimensions", "Oscillation", "Function", "Sprites"}
        };
#if CS
    }
#endif
}
