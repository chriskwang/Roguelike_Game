using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace Roguelike_Game
{
    public static class Game
    {
        // Screen height and width in number of tiles
        private static readonly int screenWidth = 100;
        private static readonly int screenHeight = 70;
        private static RLRootConsole rootConsole;

        // Map console
        private static readonly int mapWidth = 80;
        private static readonly int mapHeight = 40;
        private static RLConsole mapConsole;

        // Message console
        private static readonly int messageWidth = 80;
        private static readonly int messageHeight = 11;
        private static RLConsole messageConsole;

        // Stats console
        private static readonly int statWidth = 20;
        private static readonly int statHeight = 70;
        private static RLConsole statConsole;

        // Inventory console
        private static readonly int invWidth = 80;
        private static readonly int invHeight = 11;
        private static RLConsole invConsole;

        public static void Main()
        {
            // This must be the exact name of the bitmap font file we are using or it will error.
            string fontFileName = "terminal8x8.png";
            // The title will appear at the top of the console window
            string consoleTitle = "RougeSharp V3 Tutorial - Level 1";
            // Tell RLNet to use the bitmap font that we specified and that each tile is 8 x 8 pixels
            rootConsole = new RLRootConsole(fontFileName, screenWidth, screenHeight,
              8, 8, 1f, consoleTitle);

            // Initialize subconsoles that make up screen
            mapConsole = new RLConsole(mapWidth, mapHeight);
            messageConsole = new RLConsole(messageWidth, messageHeight);
            statConsole = new RLConsole(statWidth, statHeight);
            invConsole = new RLConsole(invWidth, invHeight);

            // Set up a handler for RLNET's Update event
            rootConsole.Update += OnRootConsoleUpdate;
            // Set up a handler for RLNET's Render event
            rootConsole.Render += OnRootConsoleRender;
            // Begin RLNET's game loop
            rootConsole.Run();
        }

        // Event handler for RLNET's Update event
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            // Set background color and text for each console
            mapConsole.SetBackColor(0, 0, mapWidth, mapHeight, RLColor.Black);
            mapConsole.Print(1, 1, "Map", RLColor.White);

            messageConsole.SetBackColor(0, 0, messageWidth, messageHeight, RLColor.Gray);
            messageConsole.Print(1, 1, "Messages", RLColor.White);

            statConsole.SetBackColor(0, 0, statWidth, statHeight, RLColor.Brown);
            statConsole.Print(1, 1, "Stats", RLColor.White);

            invConsole.SetBackColor(0, 0, invWidth, invHeight, RLColor.Cyan);
            invConsole.Print(1, 1, "Inventory", RLColor.White);

        }

        // Event handler for RLNET's Render event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            RLConsole.Blit(mapConsole, 0, 0, mapWidth, mapHeight, rootConsole, 0, invHeight);
            RLConsole.Blit(statConsole, 0, 0, statWidth, statHeight, rootConsole, mapWidth, 0);
            RLConsole.Blit(messageConsole, 0, 0, messageWidth, messageHeight, rootConsole, 0, screenHeight - messageHeight);
            RLConsole.Blit(invConsole, 0, 0, invWidth, invHeight, rootConsole, 0, 0);

            // Tell RLNET to draw the console that we set
            rootConsole.Draw();
        }
    }
}
