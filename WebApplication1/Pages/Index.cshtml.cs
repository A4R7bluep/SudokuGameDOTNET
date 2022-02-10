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

        public void OnGet()
        {

        }
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