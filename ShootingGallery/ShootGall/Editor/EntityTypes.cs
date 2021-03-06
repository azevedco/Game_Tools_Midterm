﻿//Here be magic dragons!
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
        CURTAIN = 4,
        WAVE_TRACK = 5,
        CONVERYOR_TRACK = 6,
        FUNCTION_TRACK = 7,
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
            /* None */
            { "" },
#if CS
        new String[]
#endif
            /* Rectangle */
            {"Dimensions", "OutlineColor", "FillColor" },
#if CS
        new String[]
#endif
            /* Circle */
            {"Position", "Radius", "OutlineColor", "FillColor"},
#if CS
        new String[]
#endif
            /* Sprite */
            {"Dimensions", "Image", "Scale"},
#if CS
        new String[]
#endif
            /* Curtain */
            {"Dimensions", "BottomImage", "MainImage"},
#if CS
        new String[]
#endif
            /* Track 1: Wave Track */
            {"Dimensions", "Oscillation", "Speed", "Image", "Scale"},
#if CS
        new String[]
#endif
            /* Track 2: Conveyor Track */
            {"Dimensions", "Speed", "Image", "Scale"},
#if CS
        new String[]
#endif
            /* Track 3: Function Track */
            {"Dimensions", "Oscillation", "Speed", "Function", "Image", "Scale"}
        };
#if CS
    }
#endif
}