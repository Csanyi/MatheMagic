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
                return "�";
            case Operation.DIVISION:
                return "�";
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
        //return this.operand1 + " " + GetOperationSign() + " " + this.operand2 + " = ?";
        return this.operand1 + " " + GetOperationSign() + " " + this.operand2;
    }
}

public class GameHelper
{
    private static Random random = new Random();

    public static int GenerateRandomNumber()
    {
        return random.Next();
    }

    public static int GenerateRandomNumberInclusive(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    public static bool GenerateRandomBool()
    {
        return random.Next(2) % 2 == 0;
    }

    public static Operation GenerateRandomOperation()
    {
        int rnd = random.Next(4);
        switch (rnd)
        {
            case 0:
                return Operation.ADDITION;
            case 1:
                return Operation.SUBTRACTION;
            case 2:
                return Operation.MULTIPLICATION;
            default:
                return Operation.DIVISION;
        }
    }
    public static Operation GenerateRandomOperationAddSub()
    {
        return GenerateRandomBool() ? Operation.ADDITION : Operation.SUBTRACTION;
    }

    // `min` and `max` are not applicable to all numbers in an operation and they only bound the following numbers:
    // ADDITION: result (sum)
    // SUBTRACTION: 1st operand
    // MULTIPLICATION: result (product)
    // DIVISION: 1st operand

    // TODO: In case of MULTIPLICATION/DIVISION the product/1st operand can fall ourside of the [min, max] interval; this could be fixed later
    public static Exercise GenerateRandomExerciseForOperation(Operation operation, int min, int max)
    {
        int sum, diff, prod, quot, oper1, oper2;
        switch (operation)
        {
            case Operation.ADDITION:
                sum = GenerateRandomNumberInclusive(min, max);
                oper1 = GenerateRandomNumberInclusive(1, sum - 1);
                oper2 = sum - oper1;
                return new Exercise(oper1, oper2, sum, operation);
            case Operation.SUBTRACTION:
                oper1 = GenerateRandomNumberInclusive(min, max);
                oper2 = GenerateRandomNumberInclusive(1, oper1 - 1);
                diff = oper1 - oper2;
                return new Exercise(oper1, oper2, diff, operation);
            case Operation.MULTIPLICATION:
                prod = GenerateRandomNumberInclusive(min, max);
                oper1 = GenerateRandomNumberInclusive(1, prod / 2);
                oper2 = prod / oper1;
                prod = oper1 * oper2;
                return new Exercise(oper1, oper2, prod, operation);
            default: // Operation.DIVISION
                oper1 = GenerateRandomNumberInclusive(min, max);
                oper2 = GenerateRandomNumberInclusive(1, oper1 / 2);
                quot = oper1 / oper2;
                oper1 = oper2 * quot;
                return new Exercise(oper1, oper2, quot, operation);
        }
    }

    public static Exercise GenerateRandomExerciseForOperationAndDigits(Operation operation, int digits)
    {
        return GenerateRandomExerciseForOperation(operation, 10 ^ (digits - 1), 10 ^ digits - 1);
    }

    public static Exercise GenerateRandomExerciseForGrade(Grade grade)
    {
        Operation operation =
            (grade == Grade.FIRST) ?
            GenerateRandomOperationAddSub() :
            GenerateRandomOperation();
        switch (grade)
        {
            case Grade.FIRST:
                return GenerateRandomExerciseForOperation(operation, 1, 20);
            case Grade.SECOND:
                return GenerateRandomExerciseForOperation(operation, 10, 99);
            case Grade.THIRD:
                return GenerateRandomExerciseForOperation(operation, 100, 999);
            default: // Grade.FOURTH
                return GenerateRandomExerciseForOperation(operation, 1000, 9999);
        }
    }

    public static Exercise GenerateRandomExerciseForGradeAndResult(Grade grade, int result)
    {
        // TODO: implement other operations
        return GenerateRandomExerciseForOperation(Operation.ADDITION, result, result);
    }

    public static Exercise GenerateRandomExerciseForGradeAndDigits(Grade grade, int digits)
    {
        Operation operation =
            (grade == Grade.FIRST) ?
            GenerateRandomOperationAddSub() :
            GenerateRandomOperation();
        return GenerateRandomExerciseForOperationAndDigits(operation, digits);
        // TODO: Intersect with boundaries in GenerateRandomExerciseForGrade(). Example: FIRST grade, 3 digits -> still [1, 20] interval
    }
}
