using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string Test()
        {
            Console.WriteLine("This is using Model");
            return "s";
        }

        /* Variables */

        public string popupStyle = "";

        public static int inputSpaceX = -1;
        public static int inputSpaceY = -1;



        /*
         * This is the C# that manages the data entered from the website
         * 
         * It contains the functions:
         *     test,
         *     get_styles
         * 
         * It also contains the classes:
         *     Board, 
         *     Nonet
         */

        public string get_styles(int spaceX, int spaceY)
        {
            // This function actually sets the styles for the table cells based on where in the board and nonet it is

            string style = "";

            if (spaceY % 3 == 0) style += "border-left: 3px solid black; ";
            if (spaceY % 3 == 2) style += "border-right: 3px solid black; ";
            if (spaceX % 3 == 0) style += "border-top: 3px solid black; ";
            if (spaceX % 3 == 2) style += "border-bottom: 3px solid black; ";

            int nonetX = spaceX / 3;
            int nonetY = spaceY / 3;

            if ((nonetX + nonetY) % 2 == 0) style += "background-color: gray; ";
            if ((nonetX + nonetY) % 2 == 1) style += "background-color: darkgray; ";

            return style;
        }

        public class Nonet
        {
            // The Nonet class is embeded into the board. This separates the values by the 3x3 squares

            private int[,] area = {
                {0, 0, 0}, //    1a, 2a, 3a
                {0, 0, 0}, //    1b, 2b, 3b
                {0, 0, 0}  //    1c, 2c, 3c
            };

            public Nonet()
            {
                Console.WriteLine("CONSTRUCTING NONET");
            }

            public void set_space_value(int spacex, int spacey, int value)
            {
                area[spacex, spacey] = value;
            }

            public int get_space_value(int spaceX, int spaceY)
            {
                return area[spaceX, spaceY];
            }

            public string returnEntireBoard()
            {
                string output = "";

                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        output += (area[x, y]);
                    }
                    output += "\n";
                }

                return output;
            }
        }

        public class Board
        {
            // The board class holds all of the board spaces in the "main" array. It holds a 3x3 of Nonets. 
            // It calls the functions on those nonets

            private static Nonet[,] main = { { null, null, null }, { null, null, null }, { null, null, null } };

            public Board()
            {
                // The constructor for the Board class. It instantiates and adds the nonets into the array, main.
                Console.WriteLine("CONSTRUCTING BOARD");
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (main[x, y] == null) {
                            main[x, y] = new Nonet();
                        }
                    }
                }
            }

            //    1a, 2a, 3a
            //    1b, 2b, 3b
            //    1c, 2c, 3c

            public Nonet get_nonet_space(int space1, int space2)
            {
                return main[space1, space2];
            }

            public string get_nonet_value(int space1, int space2)
            {
                // Returns the value of the space inputed. Finds the correct nonet for the space before 
                // calling the nonet function to get the value.

                Nonet? currentNonet = null;

                currentNonet = main[space1 / 3, space2 / 3];
                string value = (currentNonet.get_space_value(space1 % 3, space2 % 3)).ToString();

                return value;
            }
        }

        public Board board = new Board();

        /* Post Methods */

        public void OnPostInput()
        {
            popupStyle = "display: none; ";
            Console.WriteLine("Popup Toggled Off");

            try
            {
                Console.WriteLine(Request.Form["inputNumber"]);
                int value = int.Parse(Request.Form["inputNumber"]);
                Console.WriteLine("Input Set");
                Console.WriteLine("Setting inputSpaceX: " + inputSpaceX);
                Console.WriteLine("Setting inputSpaceY: " + inputSpaceY);
                board.get_nonet_space(inputSpaceX / 3, inputSpaceY / 3).set_space_value((inputSpaceX % 3), (inputSpaceY % 3), value);
                Console.WriteLine(board.get_nonet_space(inputSpaceX / 3, inputSpaceY / 3).returnEntireBoard());
            }
            catch (ArgumentNullException)
            {
                Console.Write("ERROR: Submitted empty form; Canceled instead.");
            }
        }

        public void OnPostPopup(int spacex, int spacey)
        {
            popupStyle = "display: block; ";
            Console.WriteLine("Popup Toggled On");
            Console.WriteLine(spacex);
            Console.WriteLine(spacey);
            Console.WriteLine(inputSpaceX);
            Console.WriteLine(inputSpaceY);
            Console.WriteLine(board.get_nonet_space(inputSpaceX / 3, inputSpaceY / 3).returnEntireBoard());

            inputSpaceX = spacex;
            inputSpaceY = spacey;
        }

        public void OnPostPopupCancel()
        {
            popupStyle = "display: none; ";
            Console.WriteLine("Popup Toggled Off");
        }
    }
}
