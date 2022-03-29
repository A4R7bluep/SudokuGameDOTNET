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

        static bool boardSet = false;

        /*
         * This is the C# that manages the data entered from the website
         * 
         * It contains the functions:
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
            // The Nonet Objects are embeded into the board. This separates the values by the 3x3 squares

            private int[,] area = {
                {0, 0, 0}, //    1a, 2a, 3a
                {0, 0, 0}, //    1b, 2b, 3b
                {0, 0, 0}  //    1c, 2c, 3c
            };

            private bool[,] blocked =
            {
                {false, false, false},
                {false, false, false},
                {false, false, false}
            };

            public void block_space(int spacex, int spacey)
            {
                blocked[spacex, spacey] = true;
            }

            public bool get_blocked_space(int spacex, int spacey)
            {
                return blocked[spacex, spacey];
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

            /*
             * Get functions
             * 
             * Contains:
             *     get_nonet_space,
             *     get_nonet_value
             */

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

            //Validity functions
            public bool get_validity_nonet(Nonet nonet, int value)
            {
                bool valid = true;
                
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (nonet.get_space_value(x, y) == value) {
                            valid = false;
                        }
                    }
                }

                return valid;
            }

            public bool get_validity_row(int row, int value)
            {
                bool valid = true;

                for (int column = 0; column < 3; column++)
                {
                    Nonet currentNonet = main[Math.Min(column * 3, 2), row / 3];

                    for (int x = 0; x < 3; x++)
                    {
                        if (currentNonet.get_space_value(row / 3, x) == value)
                        {
                            valid = false;
                        }
                        
                    }
                }

                return valid;
            }

            public bool get_validity_column(int column, int value)
            {
                bool valid = true;

                for (int row = 0; column < 3; column++)
                {
                    Nonet currentNonet = main[row / 3, Math.Min(column * 3, 2)];

                    for (int x = 0; x < 3; x++)
                    {
                        if (currentNonet.get_space_value(x, column / 3) == value)
                        {
                            valid = false;
                        }
                        
                    }
                }

                return valid;
            }
        }

        public Board board = new Board();

        public void fill_board()
        {
            if (!boardSet)
            {
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        Nonet currentNonet = board.get_nonet_space(row / 3, col / 3);
                        Random random = new Random();

                        switch (Convert.ToBoolean(random.Next(2)))
                        {
                            case true:
                                /*switch (board.get_validity_nonet(currentNonet, randomValue) && board.get_validity_row(row, randomValue) && 
                                    board.get_validity_column(col, randomValue))
                                {
                                    case true:
                                        currentNonet.set_space_value(row % 3, col % 3, randomValue);
                                        currentNonet.block_space(row % 3, col % 3);
                                        break;

                                    case false:
                                        continue;
                                }
                                break;*/

                                bool done = false;

                                while (!done)
                                {
                                    int randomValue = random.Next(10);

                                    if (board.get_validity_nonet(currentNonet, randomValue) && board.get_validity_row(row, randomValue) &&
                                    board.get_validity_column(col, randomValue))
                                    {
                                        currentNonet.set_space_value(row % 3, col % 3, randomValue);
                                        currentNonet.block_space(row % 3, col % 3);
                                    }

                                    done = true;
                                }

                                break;
                        }
                    }
                }
            boardSet = true;
            }
        }

        public string add_class(int spacex, int spacey)
        {
            Nonet currentNonet = board.get_nonet_space(spacex / 3, spacey / 3);

            string value = "";

            if (currentNonet.get_blocked_space(spacex % 3, spacey % 3))
            {
                value = "blocked";
            }
            
            return value;
        }

        // Get Methods
        public void OnGet()
        {
            fill_board();
        }

        // Post Methods

        public void OnPostInput()
        {
            popupStyle = "display: none; ";

            try
            {
                int value = int.Parse(Request.Form["inputNumber"]);
                bool validity_nonet = board.get_validity_nonet(board.get_nonet_space(inputSpaceX / 3, inputSpaceY / 3), value);
                bool validity_row = board.get_validity_row(inputSpaceX, value);
                bool validity_column = board.get_validity_column(inputSpaceY, value);
                bool debug_override = true;
                
                if (debug_override)
                {
                    board.get_nonet_space(inputSpaceX / 3, inputSpaceY / 3).set_space_value((inputSpaceX % 3), (inputSpaceY % 3), value);
                    Console.WriteLine("Skipping validation because of debugging");
                }

                else if (validity_nonet && validity_row && validity_column && ! debug_override)
                {
                    board.get_nonet_space(inputSpaceX / 3, inputSpaceY / 3).set_space_value((inputSpaceX % 3), (inputSpaceY % 3), value);
                }
            }
            catch (ArgumentNullException)
            {
                Console.Write("ERROR: Submitted empty form; Canceled instead.");
            }
        }

        public void OnPostPopup(int spacex, int spacey)
        {
            if (! board.get_nonet_space(spacex / 3, spacey / 3).get_blocked_space(spacex % 3, spacey % 3))
            {
                popupStyle = "display: block; ";

                inputSpaceX = spacex;
                inputSpaceY = spacey;
            }
        }

        public void OnPostPopupCancel()
        {
            popupStyle = "display: none; ";
        }
    }
}
