using System;
using System.Collections.Generic;
using System.Linq;

public class Tile
{
    private int height, width;
    private int[][] tiles;

    private (bool, bool)[][] tilesVisitedTried;

    private List<(int, int)> path;
    private int currentPositionOnPath;

    private bool rulesAreExercises;
    private List<Exercise> exerciseRules;

    private int divisionRule;

    public Tile(int height, int width, bool rulesAreExercises, Grade grade)
    {
        this.height = height;
        this.width = width;

        this.tiles = new int[height][];
        this.tilesVisitedTried = new (bool, bool)[height][];
        for (int i = 0; i < height; i++)
        {
            tiles[i] = new int[width];
            tilesVisitedTried[i] = new (bool, bool)[width];
        }

        (int, int)
            start = (0, 0),
            finish = (height - 1, width - 1);
        this.path = GraphPaths.CalculateOnePath(this.height, this.width, start, finish, height + width, height * width / 2); // TODO: configure length
        this.currentPositionOnPath = 0;
        this.tilesVisitedTried[path[0].Item1][path[0].Item2].Item1 = true;

        this.rulesAreExercises = (grade == Grade.FIRST) ? true : rulesAreExercises; // No division rule for first graders

        if (this.rulesAreExercises)
        {
            this.exerciseRules = new List<Exercise>();
            for (int i = 1; i < path.Count; i++)
            {
                // No rule is needed for the 1st tile of the path, as we already start there
                // So `exerciseRules` is 1 shorter than `path`

                // Generate exercises for path
                Exercise exercise = GameHelper.GenerateRandomExerciseForGradeAndDigits(grade, 2);
                exerciseRules.Add(exercise);
                tiles[path[i].Item1][path[i].Item2] = exercise.GetResult();
                // Generate numbers for tiles next to path
                var possibleAdjacent = GraphPaths.GeneratePossibleAdjacent(path[i - 1]);
                foreach (var tile in possibleAdjacent)
                {
                    if (0 <= tile.Item1 && tile.Item1 < tiles.Length &&
                        0 <= tile.Item2 && tile.Item2 < tiles[0].Length &&
                        tiles[tile.Item1][tile.Item2] == 0)
                    {
                        do
                        {
                            tiles[tile.Item1][tile.Item2] = GameHelper.GenerateRandomNumberInclusive(1, 99);
                        } while (tiles[tile.Item1][tile.Item2] == tiles[path[i].Item1][path[i].Item2]);
                    }
                }
            }
            // Generate numbers further from path
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    if (tiles[i][j] == 0)
                    {
                        tiles[i][j] = GameHelper.GenerateRandomNumberInclusive(1, 99);
                    }
                }
            }
        }
        else
        {
            this.divisionRule = GameHelper.GenerateRandomNumberInclusive(2, 9); // TODO: configure numbers
            for (int i = 0; i < path.Count; i++)
            {
                // Generate numbers for path
                tiles[path[i].Item1][path[i].Item2] = divisionRule * GameHelper.GenerateRandomNumberInclusive(1, 99 / divisionRule);
                // Generate numbers for tiles next to path
                var possibleAdjacent = GraphPaths.GeneratePossibleAdjacent(path[i]);
                foreach (var tile in possibleAdjacent)
                {
                    if (0 <= tile.Item1 && tile.Item1 < tiles.Length &&
                        0 <= tile.Item2 && tile.Item2 < tiles[0].Length &&
                        tiles[tile.Item1][tile.Item2] == 0)
                    {
                        do
                        {
                            tiles[tile.Item1][tile.Item2] = GameHelper.GenerateRandomNumberInclusive(1, 99);
                        } while (tiles[tile.Item1][tile.Item2] % divisionRule == 0);
                    }
                }
            }
            // Generate numbers for further from path
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    if (tiles[i][j] == 0)
                    {
                        tiles[i][j] = GameHelper.GenerateRandomNumberInclusive(1, 99);
                    }
                }
            }
        }
    }

    public (int, int) GetCurrentTile()
    {
        return this.path[currentPositionOnPath];
    }

    public (int, int) GetNextTileOnPath()
    {
        if (!IsFinished())
        {
            return this.path[currentPositionOnPath + 1];
        }
        else
        {
            return (-1, -1);
        }
    }

    public int GetNumberOnTile((int, int) coordinates)
    {
        return this.tiles[coordinates.Item1][coordinates.Item2];
    }

    // Returns whether a specific tile has already been stepped on
    public bool GetTileIsVisited((int, int) coordinates)
    {
        return this.tilesVisitedTried[coordinates.Item1][coordinates.Item2].Item1;
    }

    // Returns whether a specific tile was tried to be visited but it was not on the correct path
    public bool GetTileIsTried((int, int) coordinates)
    {
        return this.tilesVisitedTried[coordinates.Item1][coordinates.Item2].Item2;
    }

    public String GetCurrentRule()
    {
        if (this.rulesAreExercises)
        {
            return this.exerciseRules[currentPositionOnPath].ExerciseStringWithoutResult();
        }
        else
        {
            return "Divisible by " + this.divisionRule;
        }
    }

    public bool StepOnTile((int, int) coordinates)
    {
        if (!IsFinished())
        {
            if (this.path[this.currentPositionOnPath + 1] == coordinates)
            {
                this.tilesVisitedTried[coordinates.Item1][coordinates.Item2].Item1 = true;
                ++this.currentPositionOnPath;
                return true;
            }
            else
            {
                this.tilesVisitedTried[coordinates.Item1][coordinates.Item2].Item2 = true;
                return false;
            }
        }
        return false;
    }

    public bool IsFinished()
    {
        return this.currentPositionOnPath == this.path.Count - 1;
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

    public static List<(int, int)> GeneratePossibleAdjacent((int, int) position)
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