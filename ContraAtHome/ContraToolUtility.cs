using System;
using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography;

namespace ContraAtHome
{
    public static class ContraToolUtility
    {
        // Method to log messages
        public static void LogMessage(string message)
        {
            Debug.WriteLine(message);
        }

        //random number generator
        private static readonly Random random = new Random();
        public static int RandomNumberRange(int minNum, int maxNum)
        {
            return random.Next(minNum, maxNum);
        }


        // Method to check if two PictureBox objects intersect
        public static bool IntersectsWith(PictureBox a, PictureBox b)
        {
            return a.Bounds.IntersectsWith(b.Bounds);
        }

        // Method to display object details
        public static void DisplayObjectInfo(string name, int hp, int speed, int dmg, bool isAlive)
        {
            Debug.WriteLine($"Name: {name}, HP: {hp}, Speed: {speed}, Damage: {dmg}, Is Alive: {isAlive}");
        }

        // Method to set default size and color for PictureBox
        public static void SetDefaultAppearance(PictureBox pictureBox, Size size, Color color)
        {
            pictureBox.Size = size;
            pictureBox.BackColor = color;
        }

        #region Debugging Methods

        //
        public static void DebugLocationPlatform(List<Platform> plf)
        {
            foreach (Platform p in plf) {
                Debug.WriteLine($"Name {p.Name} | X : {p.Location.X} Y { p.Location.Y}" );
            } 
        }

        public static void DebugLocationEnemy(List<Enemy> enmey) { 
            foreach (Enemy e in enmey)
                Debug.WriteLine($"Name {e.Name} | X : {e.Location.X} Y {e.Location.Y}");
        }


        // Debug dictionary entries
        public static void DebugDict(Dictionary<Enemy,Platform> dictEnemyPlatform)
        {
            Debug.WriteLine("\n----- Debugging Dict -----");
            foreach (KeyValuePair<Enemy, Platform> P_E_p in dictEnemyPlatform)
            {
                Debug.WriteLine($"Key = {P_E_p.Key}, Value = {P_E_p.Value}");
                Debug.WriteLine($"Enemy : {P_E_p.Key.Name}, Platform : {P_E_p.Value.Name}");
                Debug.WriteLine($"Tags enemy : {P_E_p.Key.GetTags(1)}, Tag plf : {P_E_p.Value.GetTags(1)}\n");
            }
            Debug.WriteLine("----- End Debugging Dict -----");
        }

        // Debug visual color pair
        public static void DebugVisualColorPair(Dictionary<Enemy, Platform> dictEnemyPlatform)
        {
            Debug.WriteLine("\n----- Visual Pair -----");
            Dictionary<string, Color> tagColorMap = new Dictionary<string, Color>();

            foreach (var pair in dictEnemyPlatform)
            {
                Enemy enemy = pair.Key;
                Platform platform = pair.Value;

                string tag = enemy.GetTags(1); // Get the tag at position 2 (index 1)
                if (!string.IsNullOrEmpty(tag) && tag != "DefaultTag2")
                {
                    if (!tagColorMap.ContainsKey(tag))
                    {
                        tagColorMap[tag] = GetRandomColor();
                    }
                    enemy.BackColor = tagColorMap[tag];
                    platform.BackColor = tagColorMap[tag];
                }
            }

            // Debug output to verify the color assignment
            foreach (var entry in tagColorMap)
            {
                Debug.WriteLine($"Tag: {entry.Key}, Color: {entry.Value}");
            }
            Debug.WriteLine("----- End Visual Pair -----");
        }

        public static void DebugEnemyBulletLocation(Form f)
        {
            Debug.WriteLine("\n---------Checking Tags--------");
            foreach (Control x in f.Controls)
            {
                if (x is PictureBox && ((string)x.Tag == "EnemyBullet"))
                {
                    Debug.WriteLine($"Name: {x.Name}");
                    Debug.WriteLine($"Location: {x.Location.X}, {x.Location.Y}");
                }
            }
            Debug.WriteLine("---------Tags Checked--------");
        }

        public static void DebugPlayerBulletLocation(Form f)
        {
            Debug.WriteLine("\n---------Checking Tags--------");
            foreach (Control x in f.Controls)
            {
                if (x is PictureBox && ((string)x.Tag == "PlayerBullet"))
                {
                    Debug.WriteLine($"Name: {x.Name}");
                    Debug.WriteLine($"Location: {x.Location.X}, {x.Location.Y}");
                }
            }
            Debug.WriteLine("---------Tags Checked--------");
        }

        // Debug check tags
        public static void DebugCheckTagsAllObject(Form f)
        {
            Debug.WriteLine("\n---------Checking Tags--------");
            foreach (Control x in f.Controls)
            {
                if (x is PictureBox && ((string)x.Tag == "platform" || (string)x.Tag == "enemy"))
                {
                    Debug.WriteLine($"Name: {x.Name}");
                    Debug.WriteLine($"PictureBox Tag: {x.Tag}");
                }

                if (x is Tags tag)
                {
                    tag.DisplayTags();
                }
            }
            Debug.WriteLine("---------Tags Checked--------");
        }

        // Generate a random color
        public static Color GetRandomColor()
        {
            Random random = new Random();
            int r = random.Next(256);
            int g = random.Next(256);
            int b = random.Next(256);
            return Color.FromArgb(r, g, b);
        }

        #endregion
        //-----------------------------------------------------//
    }
}
