namespace NaxtorGames
{
    public static class Constants
    {
        public static class Conversion
        {
            public const float KMPH_TO_MPS = 1000.0f / 3600.0f;
            public const float MPS_TO_KMPH = 3.6f;
            public const float KMPH_TO_MPH = 0.621371f;
            public const float MPH_TO_KMPH = 1.609344f;

            public const int TORQUE_TO_KW = 9550;

            public const float KW_TO_PS = 1.35962f;
            public const float KW_TO_HP = 1.34102f;

            public const float PS_TO_KW = 0.7355f;
            public const float HP_TO_KW = 0.7457f;

            public const float PS_TO_HP = 0.98632f;
            public const float HP_TO_PS = 1.01387f;

            public const float BAR_TO_PSI = 14.50377377f;
            public const float PSI_TO_BAR = 0.0689475729f;
        }

        public static class Units
        {
            public const string DEGREE = "\u00B0";
            public const string ANGLE = DEGREE;

            public const string KMPH = "km/h";
            public const string MPH = "mph";

            public const string TORQUE_ASCII = "Nm";
            public const string TORQUE_SI = "N\u00B7m";

            public const string CELSIUS = DEGREE + "C";
            public const string FAHRENHEIT = DEGREE + "F";

            public const string LITER = "L";

            public const string KILOWATT = "kW";
            public const string HORSE_POWER_HP = "hp";
            public const string HORSEPOWER_PS = "PS";

            public const string BAR = "bar";
            public const string PSI = "psi";

            public const string CENTIMETER = "cm";
            public const string METER = "m";
            public const string KILOMETER = "km";
        }

        public static class Layers
        {
            public const int DEFAULT = 0;
            public const int TRANSPARENT_FX = 1;
            public const int IGNORE_RAYCAST = 2;
            public const int WATER = 4;
            public const int UI = 5;
        }

        public static class GameObjectNames
        {
            public const string AUDIO_SOURCES_PARENT = "AudioSources";
        }

        public static class ResourcePaths
        {
            public const string PATH_ROOT_FOLDER = "Assets/Resources/";
        }

        public static class Script
        {
            public const string REFERENCES = "References";
            public const string SETTINGS = "Settings";
            public const string DEBUG = "Debug";
        }

        public static class ComponentMenu
        {
            public static class Author
            {
                public const string ROOT = "Naxtor Games/";
                public const string MISC = ROOT + ComponentMenu.MISC;
                public const string AUDIO = ROOT + ComponentMenu.AUDIO;
            }

            /// <summary>
            /// Hides the component in the component menus. A component name is still required.
            /// </summary>
            public const string HIDE = "/";
            public const string MISC = "Miscellaneous/";
            public const string AUDIO = "Audio/";
        }
    }
}
