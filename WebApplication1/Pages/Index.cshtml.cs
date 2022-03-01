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

        public void OnPost()
        {
            Console.WriteLine("TEST");
        }

        /* Objects */

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

            public void set_space_value(string space, int value)
            {
                switch (space)
                {
                    case "1a":
                        area[0, 0] = value;
                        break;

                    case "2a":
                        area[0, 1] = value;
                        break;

                    case "3a":
                        area[0, 2] = value;
                        break;

                    case "1b":
                        area[1, 0] = value;
                        break;

                    case "2b":
                        area[1, 1] = value;
                        break;

                    case "3b":
                        area[1, 2] = value;
                        break;

                    case "1c":
                        area[2, 0] = value;
                        break;

                    case "2c":
                        area[2, 1] = value;
                        break;

                    case "3c":
                        area[2, 2] = value;
                        break;
                }
            }

            public int get_space_value(int spaceX, int spaceY)
            {
                return area[spaceX, spaceY];
            }
        }

        public class Board
        {
            // The board class holds all of the board spaces in the "main" array. It holds a 3x3 of Nonets. 
            // It calls the functions on those nonets

            private Nonet[,] main = { { null, null, null }, { null, null, null }, { null, null, null } };

            public Board()
            {
                // The constructor for the Board class. It instantiates and adds the nonets into the array, main.

                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        main[x, y] = new Nonet();
                    }
                }
            }

            //    1a, 2a, 3a
            //    1b, 2b, 3b
            //    1c, 2c, 3c

            public string get_nonet_value(int space1, int space2)
            {
                // Returns the value of the space inputed. Finds the correct nonet for the space before 
                // calling the nonet function to get the value.

                Nonet currentNonet = null;

                currentNonet = main[space1 / 3, space2 / 3];
                string value = (currentNonet.get_space_value(space1 % 3, space2 % 3)).ToString();

                return value;
            }
        }

        public Board board = new Board();

    }
}

/*
public class Nonet
{
    private int[,] area = {
        {0, 0, 0}, //    1a, 2a, 3a
        {0, 0, 0}, //    1b, 2b, 3b
        {0, 0, 0}  //    1c, 2c, 3c
    };

    public void set_space_value(string space, int value)
    {
        switch (space)
        {
            case "1a":
                area[0, 0] = value;
                break;

            case "2a":
                area[0, 1] = value;
                break;

            case "3a":
                area[0, 2] = value;
                break;

            case "1b":
                area[1, 0] = value;
                break;

            case "2b":
                area[1, 1] = value;
                break;

            case "3b":
                area[1, 2] = value;
                break;

            case "1c":
                area[2, 0] = value;
                break;

            case "2c":
                area[2, 1] = value;
                break;

            case "3c":
                area[2, 2] = value;
                break;
        }
    }

    public int get_space_value(string space)
    {
        int retVal = 0;

        switch (space)
        {
            case "1a":
                retVal = area[0, 0];
                break;

            case "2a":
                retVal = area[0, 1];
                break;

            case "3a":
                retVal = area[0, 2];
                break;

            case "1b":
                retVal = area[1, 0];
                break;

            case "2b":
                retVal = area[1, 1];
                break;

            case "3b":
                retVal = area[1, 2];
                break;

            case "1c":
                retVal = area[2, 0];
                break;

            case "2c":
                retVal = area[2, 1];
                break;

            case "3c":
                retVal = area[2, 2];
                break;
        }

        return retVal;
    }
}

public class Board
{
    private Nonet[,] main = { { null, null, null }, { null, null, null }, { null, null, null } };

    public Board()
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                main[0, 0] = new Nonet();
            }
        }

        Console.WriteLine(main);
    }

    //    1a, 2a, 3a
    //    1b, 2b, 3b
    //    1c, 2c, 3c

    public string get_nonet_value()
    {
        return "a";
    }
}
*/