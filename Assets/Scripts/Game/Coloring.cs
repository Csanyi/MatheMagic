using System;
using System.Collections.Generic;
using System.Linq;

public enum ColorCode
{
    RED,
    GREEN,
    BLUE,
    YELLOW,
    ORANGE,
    PURPLE,
    BLACK,
    WHITE,
    GRAY,
    BROWN
}

class ColorField
{
    private bool colored;
    private ColorCode colorCode;

    public ColorField(ColorCode colorCode)
    {
        this.colored = false;
        this.colorCode = colorCode;
    }

    public void SetColored()
    {
        this.colored = true;
    }

    public bool GetColored()
    {
        return this.colored;
    }

    public ColorCode GetColorCode()
    {
        return this.colorCode;
    }
}

public class Coloring
{
    private List<(ColorField, Exercise)> fields = new List<(ColorField, Exercise)>();
    private Dictionary<ColorCode, int> colors = new Dictionary<ColorCode, int>();
    private ColorCode currentSelectedColor;

    public Coloring(List<ColorCode> fieldList, Grade grade)
    {
        // Create a corresponding number to each color code that appears in the level
        List<ColorCode> distinctColorCodes = fieldList.Distinct().ToList();
        List<int> numbersToColors = new List<int>();
        while (numbersToColors.Count < distinctColorCodes.Count)
        {
            int randomNumber = GameHelper.GenerateRandomNumberInclusive(2, Math.Max(distinctColorCodes.Count, 20)); // TODO: range based on grade
            if (!numbersToColors.Contains(randomNumber))
            {
                numbersToColors.Add(randomNumber);
            }
        }
        for (int i = 0; i < distinctColorCodes.Count; i++)
        {
            this.colors[distinctColorCodes[i]] = numbersToColors[i];
        }

        // Create exercises for each field
        foreach (var color in fieldList)
        {
            this.fields.Add((new ColorField(color), GameHelper.GenerateRandomExerciseForGradeAndResult(grade, colors[color])));
        }

        this.currentSelectedColor = distinctColorCodes[0];
    }

    public List<(ColorCode, int)> GetColorsAndNumbers()
    {
        List<(ColorCode, int)> colorsAndNumbers = new List<(ColorCode, int)>();
        foreach (var color in this.colors.Keys)
        {
            colorsAndNumbers.Add((color, colors[color]));
        }
        return colorsAndNumbers;
    }

    public void SelectColor(ColorCode color)
    {
        this.currentSelectedColor = color;
    }

    public bool ColorField(int index)
    {
        if (!this.fields[index].Item1.GetColored() && this.fields[index].Item1.GetColorCode() == currentSelectedColor)
        {
            this.fields[index].Item1.SetColored();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsFieldColored(int index)
    {
        return this.fields[index].Item1.GetColored();
    }

    public ColorCode GetFieldColor(int index)
    {
        return this.fields[index].Item1.GetColorCode();
    }

    public String GetFieldLabel(int index)
    {
        return this.fields[index].Item2.ExerciseStringWithoutResult();
    }

    public bool IsFinished()
    {
        foreach (var item in this.fields)
        {
            if (!item.Item1.GetColored())
            {
                return false;
            }
        }
        return true;
    }
}