using System;

public class Lock
{
    private Exercise exercise;
    private int[] lockDigits;

    static Random random = new Random();

    public Lock(int digits)
    {
        this.exercise = GameHelper.GenerateRandomExercise((random.Next(2) == 0) ? Operation.ADDITION : Operation.SUBTRACTION, digits);
        this.lockDigits = new int[digits];
    }

    public Exercise GetExercise()
    {
        return this.exercise;
    }

    public int[] GetLockDigits()
    {
        return this.lockDigits;
    }

    public void RotateDigit(int index, bool up)
    {
        if (up)
        {
            this.lockDigits[index]++;
        }
        else
        {
            this.lockDigits[index]--;
        }
        this.lockDigits[index] %= 10;
    }

    public bool IsDigitCorrect(int index)
    {
        int localValue = (int)Math.Pow(10, this.lockDigits.Length - index);
        return this.lockDigits[index] == ((int)(this.exercise.GetResult() / localValue) % 10);
    }

    private int GetCurrentResult()
    {
        int result = this.lockDigits[0];
        for (int i = 1; i < this.lockDigits.Length; i++)
        {
            result *= 10;
            result += this.lockDigits[i];
        }
        return result;
    }

    public bool IsFinished()
    {
        return this.GetCurrentResult() == this.exercise.GetResult();
    }
}