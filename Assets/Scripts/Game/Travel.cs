public class Travel
{
    private Grade grade;
    private int length;
    private int currentPositionOnPath;
    private Exercise currentExercise;

    public Travel(int length, Grade grade)
    {
        this.grade = grade;
        this.length = length;
        this.currentPositionOnPath = 0;
        this.currentExercise = GameHelper.GenerateRandomExercise(this.grade);
    }

    public int GetLength()
    {
        return this.length;
    }

    public int GetCurrentPositionOnPath()
    {
        return this.currentPositionOnPath;
    }

    public Exercise GetCurrentExercise()
    {
        return this.currentExercise;
    }

    public bool IsFinished()
    {
        return this.currentPositionOnPath == this.length - 1;
    }

    public bool InputResult(int result)
    {
        if (result == this.currentExercise.GetResult())
        {
            this.currentPositionOnPath++;
            this.currentExercise = this.IsFinished() ? null : GameHelper.GenerateRandomExercise(this.grade);
            return true;
        }
        else
        {
            return false;
        }
    }
}