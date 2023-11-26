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

    public void SetColored(bool value)
    {
        colored = value;
    }

    public bool GetColored()
    {
        return colored;
    }

    public ColorCode GetColorCode()
    {
        return colorCode;
    }
}

public class Coloring
{
    private List<(ColorField, Exercise)> fields = new List<(ColorField, Exercise)>();
    private List<(ColorCode, int)> colors = new List<(ColorCode, int)>();
    private int currentSelectedColorIndex;

    public Coloring(List<ColorCode> colorCodeList, Grade grade)
    {
        List<ColorCode> distinctColorCodes = colorCodeList.Distinct().ToList();
        // TODO: generate numbers
        foreach (var item in distinctColorCodes)
        {
            colors.Add((item, -1));
        }

        foreach (var item in colorCodeList)
        {
            fields.Add((new ColorField(item), GameHelper.GenerateRandomExercise(grade))); // TODO: generate exercise based on result
        }

        this.currentSelectedColorIndex = 0;
    }

    public void SelectColor(int index)
    {
        this.currentSelectedColorIndex = index;
    }

    public bool ColorField(int index)
    {
        if (!this.fields[index].Item1.GetColored() && this.fields[index].Item1.GetColorCode() == colors[currentSelectedColorIndex].Item1)
        {
            this.fields[index].Item1.SetColored(true);
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

    public Exercise GetFieldLabel(int index)
    {
        return this.fields[index].Item2;
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