class ClassRoom
{
    private Pupil[] students;
    public ClassRoom(params Pupil[] students)
    {
        this.students = students;
    }
    public void PrintStudentInfo()
    {
        Console.WriteLine("Pupil info: ");
        for (int i =0; i < students.Length; ++i)
        {
            Console.WriteLine($"Pupil {i + 1}: ");
            students[i].Study();
            students[i].Read();
            students[i].Write();
            students[i].Relax();
            Console.WriteLine();
        }
    }
}
class Pupil
{
    public virtual void Study()
    {
        Console.WriteLine("Pupil: Pupil is studying...");
    }
    public virtual void Read()
    {
        Console.WriteLine("Pupil: Pupil is reading...");
    }
    public virtual void Write()
    {
        Console.WriteLine("Pupil: Pupil is writing...");
    }
    public virtual void Relax()
    {
        Console.WriteLine("Pupil: Pupil is relaxing...");
    }
}
class ExcellentPupil : Pupil
{
    public override void Study()
    {
        Console.WriteLine("ExcellentPupil: Pupil is studying excellent :)");
    }
    public override void Read()
    {
        Console.WriteLine("ExcellentPupil: Pupil is reading excellent :)");
    }
    public override void Write()
    {
        Console.WriteLine("ExcellentPupil: Pupil is writing excellent :)");
    }
    public override void Relax()
    {
        Console.WriteLine("ExcellentPupil: Pupil is relaxing excellent :)");
    }
}
class GoodPupil : Pupil
{
    public override void Study()
    {
        Console.WriteLine("GoodPupil: Pupil is studying good :|");
    }
    public override void Read()
    {
        Console.WriteLine("GoodPupil: Pupil is reading good :|");
    }
    public override void Write()
    {
        Console.WriteLine("GoodPupil: Pupil is writing good :|");
    }
    public override void Relax()
    {
        Console.WriteLine("GoodPupil: Pupil is relaxing good :|");
    }
}
class BadPupil : Pupil
{
    public override void Study()
    {
        Console.WriteLine("BadPupil: Pupil is studying bad :(");
    }
    public override void Read()
    {
        Console.WriteLine("BadPupil: Pupil is reading bad :(");
    }
    public override void Write()
    {
        Console.WriteLine("BadPupil: Pupil is writing bad :(");
    }
    public override void Relax()
    {
        Console.WriteLine("BadPupil: Pupil is relaxing bad :(");
    }
}
class Program
{
    static void Main(string[] args)
    {
        Pupil pupil1 = new Pupil();
        Pupil pupil2 = new ExcellentPupil();
        Pupil pupil3 = new GoodPupil();
        Pupil pupil4 = new BadPupil();
        
        ClassRoom classRoom = new ClassRoom(pupil1,pupil2,pupil3,pupil4);
        classRoom.PrintStudentInfo();
    }
}