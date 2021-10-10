using System;
using System.Collections.Generic;
using System.Linq;

namespace RoverNavigator
{
    public class MoveRover
    {
        private int XUpperRight { get; set; }

        private int YUpperRight { get; set; }

        private readonly InputValidator InputValidator;

        public MoveRover(int xUpperRight, int yUpperRight)
        {
            InputValidator = new InputValidator();

            XUpperRight = xUpperRight;
            YUpperRight = yUpperRight;
        }

        public void MoveRoverOnPlateau()
        {
            List<Rover> roverList = new();

            while (true)
            {
                bool isValidInput = false;

                string otherInput = "";

                while (!isValidInput)
                {
                    otherInput = Console.ReadLine();

                    otherInput = otherInput.ToUpper();

                    bool isEnter = InputValidator.IsEnter(otherInput);

                    if (isEnter)
                        break;

                    isValidInput = InputValidator.IsValidInput(otherInput);

                    if (!isValidInput)
                        Console.WriteLine("Input in not valid");
                }

                if (String.IsNullOrEmpty(otherInput))
                    break;

                string[] otherInputWithoutSpace = otherInput.Split(" ");

                if (roverList.Count == 0)
                {
                    if (!InputValidator.IsValidFirstRoverInfo(otherInput))
                    {
                        Console.WriteLine("Rover information is not valid.");
                        continue;
                    }

                    CreateFirstRover(roverList, otherInputWithoutSpace);

                    if (otherInputWithoutSpace.Length > 3)
                    {
                        try
                        {
                            foreach (string item in otherInputWithoutSpace.ToList().GetRange(3, otherInputWithoutSpace.Length - 3))
                                ProcessTheInstructions(roverList, item);
                        }
                        catch (RoverOutOfBoundException e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < otherInputWithoutSpace.Length; i++)
                    {
                        if (otherInputWithoutSpace[i][0].ToString().Equals("L") ||
                            otherInputWithoutSpace[i][0].ToString().Equals("R") ||
                            otherInputWithoutSpace[i][0].ToString().Equals("M"))
                        {
                            try
                            {
                                ProcessTheInstructions(roverList, otherInputWithoutSpace[i]);
                            }
                            catch (RoverOutOfBoundException e)
                            {
                                Console.WriteLine(e.Message);
                                break;
                            }
                        }

                        if (int.TryParse(otherInputWithoutSpace[i][0].ToString(), out _))
                            CreateAnotherRovers(roverList, otherInputWithoutSpace, ref i);
                    }
                }
            }

            int roverCounter = 1;

            foreach (Rover rover in roverList)
            {
                Console.WriteLine(roverCounter + ". Rover");
                Console.WriteLine(rover.X + " " + rover.Y + " " + rover.Heading);

                roverCounter++;
            }
        }

        private void ProcessTheInstructions(List<Rover> roverList, string moveArray)
        {
            foreach (char directionInput in moveArray)
            {
                bool isMoveDirection = Enum.IsDefined(typeof(MoveDirections), directionInput.ToString());

                if (isMoveDirection)
                {
                    string roverHeading = roverList.Last().Heading;

                    GeographicDirections currentDirection = (GeographicDirections)Enum.Parse(typeof(GeographicDirections), roverHeading);

                    if (directionInput.ToString().Equals(Enum.GetName(typeof(MoveDirections), MoveDirections.L)))
                        RotateLeft(roverList, currentDirection);

                    if (directionInput.ToString().Equals(Enum.GetName(typeof(MoveDirections), MoveDirections.R)))
                        RotateRight(roverList, currentDirection);
                }

                else
                    MoveTheRover(roverList);
            }
        }

        private void CreateFirstRover(List<Rover> roverList, string[] input)
        {
            Rover firstRover = new();

            string FirstRoverXCoordinate = input[0];

            if (!int.TryParse(FirstRoverXCoordinate, out _))
                Console.WriteLine("Sikis");
            else
                firstRover.X = Convert.ToInt32(FirstRoverXCoordinate);

            string firstRoverYCoordinate = input[1];

            if (!int.TryParse(firstRoverYCoordinate, out _))
                Console.WriteLine("Sikis");
            else
                firstRover.Y = Convert.ToInt32(firstRoverYCoordinate);

            string firstRoverHeading = input[2];

            firstRover.Heading = firstRoverHeading;

            roverList.Add(firstRover);
        }

        private void CreateAnotherRovers(List<Rover> roverList, string[] input, ref int i)
        {
            string xCordinate = input[i];
            string yCordinate = input[i + 1];
            string heading = input[i + 2];

            Rover otherRover = new()
            {
                X = Convert.ToInt32(xCordinate),
                Y = Convert.ToInt32(yCordinate),
                Heading = heading
            };

            roverList.Add(otherRover);

            i += 3;
        }

        private void RotateLeft(List<Rover> roverList, GeographicDirections currentDirection)
        {
            int currentDirectionValue = (int)currentDirection;

            currentDirectionValue--;

            if (currentDirectionValue == 0)
                roverList.Last().Heading = Enum.GetName(typeof(GeographicDirections), GeographicDirections.W);

            else if (currentDirectionValue == 5)
                roverList.Last().Heading = Enum.GetName(typeof(GeographicDirections), GeographicDirections.N);
            else
            {
                string newHeading = Enum.GetName(typeof(GeographicDirections), currentDirectionValue);

                roverList.Last().Heading = newHeading;
            }
        }

        private void RotateRight(List<Rover> roverList, GeographicDirections currentDirection)
        {
            int currentDirectionValue = (int)currentDirection;

            currentDirectionValue++;

            if (currentDirectionValue == 5)
                roverList.Last().Heading = Enum.GetName(typeof(GeographicDirections), GeographicDirections.N);
            else
            {
                string newHeading = Enum.GetName(typeof(GeographicDirections), currentDirectionValue);

                roverList.Last().Heading = newHeading;
            }
        }

        private void MoveTheRover(List<Rover> roverList)
        {
            if (roverList.Last().Heading.Equals(GeographicDirections.N.ToString()))
                roverList.Last().Y++;

            if (roverList.Last().Heading.Equals(GeographicDirections.E.ToString()))
                roverList.Last().X++;

            if (roverList.Last().Heading.Equals(GeographicDirections.S.ToString()))
                roverList.Last().Y--;

            if (roverList.Last().Heading.Equals(GeographicDirections.W.ToString()))
                roverList.Last().X--;

            if (roverList.Last().X > XUpperRight || roverList.Last().X < 0 || roverList.Last().Y > YUpperRight || roverList.Last().Y < 0)
                throw new RoverOutOfBoundException("The Rover out of the bound.");
        }
    }
}
