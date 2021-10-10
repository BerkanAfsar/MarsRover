using System;

namespace RoverNavigator
{
    class Program
    {
        static void Main()
        {
            bool isValidFirstInput = false;

            string input = "";

            InputValidator inputValidator = new();           

            while (!isValidFirstInput)
            {
                input = Console.ReadLine();

                input = input.ToUpper();

                isValidFirstInput = inputValidator.IsValidUpperRightInput(input);

                if (!isValidFirstInput)
                    Console.WriteLine("Invalid upper right input.");
            }

            string[] inputWithOutSpace = input.Split(" ");

            int xUpperRight = Convert.ToInt32(inputWithOutSpace[0]);

            int yUpperRight = Convert.ToInt32(inputWithOutSpace[1]);

            MoveRover moveRover = new(xUpperRight, yUpperRight);

            moveRover.MoveRoverOnPlateau();
        }
    }
}
