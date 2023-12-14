using System;
using System.Collections.Generic;
using System.Linq;

public class Tile
{
    private int height, width;
    private List<List<int>> tiles;

    private List<(int, int)> path;
    private int currentPositionOnPath;

    private bool rulesAreExercises;
    private Exercise currentExercise;

    private int divisionRule;

    public Tile(int height, int width, bool rulesAreExercises, Grade grade)
    {
        this.height = height;
        this.width = width;

        this.tiles = new List<List<int>>(height) { new List<int>(width) { int.MinValue } };

        (int, int)
            start = (0, 0),
            finish = (height - 1, width - 1);
        this.path = GraphPaths.CalculateOnePath(this.height, this.width, start, finish, height + width, height * width / 2);
        this.currentPositionOnPath = 0;

        this.rulesAreExercises = (grade == Grade.FIRST) ? true : rulesAreExercises; // No division rule for first graders

        if (this.rulesAreExercises)
        {
            // TODO
        }
        else
        {
            this.divisionRule = GameHelper.GenerateRandomNumberInclusive(2, 9);
            for (int i = 0; i < path.Count; i++)
            {
                tiles[path[i].Item1][path[i].Item2] = divisionRule * GameHelper.GenerateRandomNumberInclusive(1, 9);
            }
        }

        // ...

    }

    public (int, int) GetCurrentTile()
    {
        return this.path[currentPositionOnPath];
    }

    public int GetNumberOnTile((int, int) coordinates)
    {
        return this.tiles[coordinates.Item1][coordinates.Item2];
    }

    public String GetCurrentRule()
    {
        if (this.rulesAreExercises)
        {
            return this.currentExercise.ExerciseStringWithoutResult();
        }
        else
        {
            return "Divisible by " + this.divisionRule;
        }
    }

    public bool StepOnTile((int, int) coordinates)
    {
        if (this.path[this.currentPositionOnPath] == coordinates)
        {
            ++this.currentPositionOnPath;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsFinished()
    {
        return this.currentPositionOnPath == this.path.Count;
    }
}

public class GraphPaths
{
    public static List<(int, int)> CalculateOnePath(int height, int width, (int, int) start, (int, int) finish, int minLength, int maxLength)
    {
        List<List<(int, int)>> paths = new List<List<(int, int)>>();
        List<(int, int)> currentPath = new List<(int, int)>();

        GenerateOnePath(start, finish, height, width, currentPath, paths, minLength, maxLength);

        return paths[0];
    }

    static void GenerateOnePath((int, int) current, (int, int) finish, int height, int width, List<(int, int)> currentPath, List<List<(int, int)>> paths, int minLength, int maxLength)
    {

        if (IsOutsideGraph(current, height, width) || IsAdjacentToThreeOrMoreStepsBack(current, currentPath) || currentPath.Contains(current) || currentPath.Count > maxLength || paths.Count > 0)
        {
            return;
        }

        currentPath.Add(current);

        if (current.Item1 == finish.Item1 && current.Item2 == finish.Item2 && currentPath.Count >= minLength)
        {
            paths.Add(new List<(int, int)>(currentPath));
        }
        else
        {
            List<(int, int)> possibleAdjacent = GeneratePossibleAdjacent(current);
            foreach ((int, int) position in possibleAdjacent)
            {
                GenerateOnePath(position, finish, height, width, currentPath, paths, minLength, maxLength);
            }
        }

        currentPath.RemoveAt(currentPath.Count - 1);
    }

    static bool IsAdjacentToThreeOrMoreStepsBack((int, int) current, List<(int, int)> currentPath)
    {
        if (currentPath.Count < 3)
        {
            return false;
        }

        for (int i = 0; i < currentPath.Count - 2; i++)
        {
            var position = currentPath[i];

            if (IsAdjacent(current, position))
            {
                return true;
            }
        }

        return false;
    }

    static bool IsOutsideGraph((int, int) position, int height, int width)
    {
        return position.Item1 < 0 || position.Item1 >= height || position.Item2 < 0 || position.Item2 >= width;
    }

    static List<(int, int)> GeneratePossibleAdjacent((int, int) position)
    {
        var possibleAdjacent = new List<(int, int)>()
        {
            (position.Item1 + 1, position.Item2),
            (position.Item1 - 1, position.Item2),
            (position.Item1, position.Item2 + 1),
            (position.Item1, position.Item2 - 1)
        };
        return possibleAdjacent.OrderBy(a => GameHelper.GenerateRandomNumber()).ToList();
    }

    static bool IsAdjacent((int, int) position1, (int, int) position2)
    {
        return (Math.Abs(position1.Item1 - position2.Item1) == 1 && position1.Item2 == position2.Item2) ||
                (Math.Abs(position1.Item2 - position2.Item2) == 1 && position1.Item1 == position2.Item1);
    }
}