using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static GameHelper;

public class Tile
{
    int height, width;
    List<List<Int32>> tiles;
    List<Tuple<int, int>> path;
    int currentPositionOnPath;

    bool rulesAreExercises;

    int divisionRule;

    public Tile(int height, int width, bool rulesAreExercises, Grade grade)
    {
        this.height = height;
        this.width = width;

        this.tiles = new List<List<Int32>>(height) { new List<Int32>(width) { Int32.MinValue } };

        List<List<Tuple<int, int>>> possiblePaths = GraphPaths.CalculatePaths(this.height, this.width, new Tuple<int, int>(0, 0), new Tuple<int, int>(height - 1, width - 1), height + width, height * width / 2);
        this.path = possiblePaths[GameHelper.GenerateRandomNumber(0, possiblePaths.Count - 1)]; // TODO: optimize length
        this.currentPositionOnPath = 0;

        this.rulesAreExercises = (grade == Grade.FIRST) ? true : rulesAreExercises; // No division rule for first graders

        if (this.rulesAreExercises)
        {
            // TODO
        }
        else
        {
            this.divisionRule = GameHelper.GenerateRandomNumber(2, 9); // TODO: configure numbers
            for (int i = 0; i < path.Count; i++)
            {
                tiles[path[i].Item1][path[i].Item2] = divisionRule * GameHelper.GenerateRandomNumber(1, 9); // TODO: configure numbers
            }
        }

        // ...

    }
}

public class GraphPaths
{
    public static List<List<Tuple<int, int>>> CalculatePaths(int height, int width, Tuple<int, int> start, Tuple<int, int> finish, int minLength, int maxLength)
    {
        List<List<Tuple<int, int>>> paths = new List<List<Tuple<int, int>>>();
        List<Tuple<int, int>> currentPath = new List<Tuple<int, int>>();

        DFS(start, finish, height, width, currentPath, paths);

        return FilterPathsByLength(paths, minLength, maxLength);
    }

    static void DFS(Tuple<int, int> current, Tuple<int, int> finish, int height, int width, List<Tuple<int, int>> currentPath, List<List<Tuple<int, int>>> paths)
    {
        if (IsOutsideGraph(current, height, width) || IsAdjacentToThreeOrMoreStepsBack(current, currentPath))
        {
            return;
        }

        currentPath.Add(current);

        if (current.Item1 == finish.Item1 && current.Item2 == finish.Item2)
        {
            paths.Add(new List<Tuple<int, int>>(currentPath));
        }
        else
        {
            List<Tuple<int, int>> possibleAdjacent = GeneratePossibleAdjacent(current);
            foreach (Tuple<int, int> position in possibleAdjacent)
            {
                DFS(position, finish, height, width, currentPath, paths);
            }
        }

        currentPath.RemoveAt(currentPath.Count - 1);
    }

    static List<List<Tuple<int, int>>> FilterPathsByLength(List<List<Tuple<int, int>>> paths, int minLength, int maxLength)
    {
        List<List<Tuple<int, int>>> filteredPaths = new List<List<Tuple<int, int>>>();

        foreach (var path in paths)
        {
            if (path.Count >= minLength && path.Count <= maxLength)
            {
                filteredPaths.Add(new List<Tuple<int, int>>(path));
            }
        }

        return filteredPaths;
    }

    static bool IsAdjacentToThreeOrMoreStepsBack(Tuple<int, int> current, List<Tuple<int, int>> currentPath)
    {
        if (currentPath.Count < 3)
        {
            return false;
        }

        for (int i = 0; i < currentPath.Count - 3; i++)
        {
            var position = currentPath[i];

            if (IsAdjacent(current, position))
            {
                return true;
            }
        }

        return false;
    }

    static bool IsOutsideGraph(Tuple<int, int> position, int height, int width)
    {
        return position.Item1 < 0 || position.Item1 >= height || position.Item2 < 0 || position.Item2 >= width;
    }

    static List<Tuple<int, int>> GeneratePossibleAdjacent(Tuple<int, int> position)
    {
        return new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(position.Item1 + 1, position.Item2),
            new Tuple<int, int>(position.Item1 - 1, position.Item2),
            new Tuple<int, int>(position.Item1, position.Item2 + 1),
            new Tuple<int, int>(position.Item1, position.Item2 - 1)
        };
    }

    static bool IsAdjacent(Tuple<int, int> position1, Tuple<int, int> position2)
    {
        return (Math.Abs(position1.Item1 - position2.Item1) == 1 && position1.Item2 == position2.Item2) ||
                (Math.Abs(position1.Item2 - position2.Item2) == 1 && position1.Item1 == position2.Item1);
    }
}