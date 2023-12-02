using System;

// TODO: Consistent enums with other uses in the code
public enum Grade
{
    FIRST,
    SECOND,
    THIRD,
    FOURTH
}

public enum Operation
{
    ADDITION,
    SUBTRACTION,
    MULTIPLICATION,
    DIVISION
}

public class Exercise
{
    private int
        operand1,
        operand2,
        result;
    private Operation operationType;

    public Exercise(int operand1, int operand2, int result, Operation operationType)
    {
        this.operand1 = operand1;
        this.operand2 = operand2;
        this.result = result;
        this.operationType = operationType;
    }

    public int GetOperand1() { return this.operand1; }
    public int GetOperand2() { return this.operand2; }
    public Operation GetOperationType() { return this.operationType; }
    public int GetResult() { return this.result; }

    private String GetOperationSign()
    {
        switch (this.operationType)
        {
            case Operation.ADDITION:
                return "+";
            case Operation.SUBTRACTION:
                return "-";
            case Operation.MULTIPLICATION:
                return "×";
            case Operation.DIVISION:
                return "÷";
            default:
                return "";
        }
    }

    public String ExerciseString()
    {
        return this.operand1 + " " + GetOperationSign() + " " + this.operand2 + " = " + this.result;
    }

    public String ExerciseStringWithoutResult()
    {
        return this.operand1 + " " + GetOperationSign() + " " + this.operand2 + " = ?";
    }
}

public class GameHelper
{
    private static Random random = new Random();

    public static int GenerateRandomNumberInclusive(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    public static bool GenerateRandomBool()
    {
        return random.Next(2) % 2 == 0;
    }

    public static Exercise GenerateRandomExercise(Operation operation, int digits)
    {
        // TODO
        return new Exercise(1, 2, 3, operation);
    }

    public static Exercise GenerateRandomExercise(Grade grade)
    {
        // TODO
        return new Exercise(1, 2, 3, Operation.ADDITION);
    }

    public static Exercise GenerateRandomExercise(Grade grade, int digits)
    {
        // TODO
        return new Exercise(1, 2, 3, Operation.ADDITION);
    }
}
