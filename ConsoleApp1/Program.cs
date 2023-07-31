using ConsoleApp1;

namespace ConsoleApp1
{
    class Pupil//                                                          מחלקת תלמיד
    {//                                                                         תכונות
        private string name;// שם תלמיד
        private double average;//ממוצע 
        public Pupil(string name, double average)//                         פעולה בונה
        {
            SetName(name);
            SetAverage(average);
        }
        public Pupil(Pupil other)//                                  פעולה בונה מעתיקה
        {
            SetName(other.name);
            SetAverage(other.average);
        }

        public string GetName() { return name; }
        public double GetAverage() { return average; }

        public void SetName(string name) { this.name = name; }
        public void SetAverage(double average) { this.average = average; }
        public override string ToString()
        {
            return "name=" + name + ", average=" + average;
        }
    }
    class Class//                                                           מחלקת כיתה
    {//                                                                         תכונות
        private string teacher;//שם המורה
        private Pupil[] allPupils;//מערך של תלמידים
        private int numOfPupils;//מספר תלמידים במערך

        public Class(string teacher, int maxPupils)//                       פעולה בונה
        {
            this.teacher = teacher;
            allPupils = new Pupil[maxPupils];//הקצאת מערך של האובייקטים
            numOfPupils = 0;
        }
        public string GetTeacher() { return teacher; }
        public bool AddPupil(Pupil thePupil)//מקבל תלמיד שרוצים להוסיף לכתה
        {
            if (numOfPupils < allPupils.Length)
            {
                allPupils[numOfPupils] = thePupil;
                numOfPupils++;
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            string str = " Class with the teacher " + teacher + " has " + numOfPupils + " pupils:\n";

            for (int i = 0; i < numOfPupils; i++)
            {
                str = str + "\t" + (i + 1) + "- " + allPupils[i].ToString() + "\n";
            }
            return str;
        }
    }
    class School//                                                      מחלקת בית הספר
    {//                                                                         תכונות
        private Pupil[] allPupils;//מערך של תלמידים
        private int numOfPupils;//מספר תלמידים במערך
        private Class[] allClasses;//מערך של כיתות
        private int numOfClasses;//מספר הכיתות במערך 

        public School(int maxPupils, int maxClasses)//                      פעולה בונה
        {
            allPupils = new Pupil[maxPupils];
            numOfPupils = 0;

            allClasses = new Class[maxClasses];
            numOfClasses = 0;
        }
        public bool AddClass(string teacher, int maxPupils)//הוסף כיתה 
        {
            if (numOfClasses == allClasses.Length)
                return false;

            allClasses[numOfClasses] = new Class(teacher, maxPupils);
            numOfClasses++;
            return true;
        }
        public Class GetClassByTeacher(string teacher)//  מקבל מורה ומחזיר הפניה לכיתה 
        {
            for (int i = 0; i < numOfClasses; i++)
            {
                if (allClasses[i].GetTeacher().Equals(teacher))
                    return allClasses[i];
            }
            return null;
        }
        public bool AddPupil(Pupil thePupil, string teacher)//הוסף תלמיד 
        {
            if (numOfPupils == allPupils.Length)
                return false;

            Class theClass = GetClassByTeacher(teacher);
            if (theClass == null)
            {
                return false;
            }
            else
            {
                allPupils[numOfPupils] = new Pupil(thePupil);
                theClass.AddPupil(allPupils[numOfPupils]);
                numOfPupils++;
                return true;
            }
        }
        public Pupil GetPupilByName(string name)//מקבל שם של תלמיד ומחזיר הפניה לתלמיד 
        {
            for (int i = 0; i < numOfPupils; i++)
            {
                if (allPupils[i].GetName().Equals(name))
                    return allPupils[i];
            }
            return null;
        }
        public override string ToString()
        {
            string str = "The school has " + numOfClasses + " classes:\n";
            for (int i = 0; i < numOfClasses; i++)
            {
                str = str + allClasses[i].ToString() + "\n";
            }
            return str;
        }
    }
}
namespace ConsoleApp1
{
    internal class Program
    {
        static void ReadClasses(School theSchool)//                    פעולה הוסף כיתה
        {
            bool fcontinue = true;

            while (fcontinue == true)
            {
                string teacherName;
                int maxPupils;

                Console.Write("Enter theachr's name: ");
                teacherName = Console.ReadLine();

                Console.Write("Enter max students in class: ");
                maxPupils = int.Parse(Console.ReadLine());

                bool res = theSchool.AddClass(teacherName, maxPupils);

                if (res == true)
                {
                    Console.Write("Add another class? (Y/N) ");
                    char answer = Console.ReadLine()[0];

                    if (answer == 'n' || answer == 'N')
                        fcontinue = false;
                    else if (answer != 'y' && answer != 'Y')
                    {
                        Console.WriteLine("Invalid answer ... ");
                    }
                }
                else
                {
                    Console.WriteLine("Can not add anymore classes");
                    fcontinue = false;
                }
            }
        }
        static void ReadPupils(School theSchool)//                    פעולה הוסף תלמיד
        {
            bool fcontinue = true;
            while (fcontinue == true)
            {
                string pupilName, teacherName;
                double pupilAverage;

                Console.Write("Enter pupil's name: ");
                pupilName = Console.ReadLine();

                Console.Write("Enter pupil's average: ");
                pupilAverage = double.Parse(Console.ReadLine());

                Console.Write("Enter teacher's name: ");
                teacherName = Console.ReadLine();

                bool res = theSchool.AddPupil(new Pupil(pupilName, pupilAverage), teacherName);

                if (res == true)
                {
                    Console.Write("Add another pupil? (Y/N) ");
                    char another = Console.ReadLine()[0];

                    if (another == 'n' || another == 'N')
                        fcontinue = false;
                    else if (another != 'y' && another != 'Y')
                    {
                        Console.WriteLine("Invalid answer ... ");
                    }
                }
                else
                {
                    Console.WriteLine("Faild adding the pupil (full capacity or teacher doesn't exits)");
                    fcontinue = false;
                }
            }
        }
        static void ChangePupilsName(School theSchool)//      פעולה המקבל הפניה לתלמיד
        {
            string name, newName;

            Console.Write("Enter name of pupil to change: ");
            name = Console.ReadLine();

            Pupil thePupil = theSchool.GetPupilByName(name);
            if (thePupil == null)
            {
                Console.WriteLine("There is no pupil with that name");
            }
            else
            {
                Console.WriteLine("Enter new name: ");
                newName = Console.ReadLine();
                thePupil.SetName(newName);

                Console.WriteLine("name change succeed");
            }
        }
        static void Main(string[] args)
        {
            const int EXIT = -1;
            int maxPupils, maxClasses;

            Console.Write("How many pupils? ");//                          כמה תלמידים
            maxPupils = int.Parse(Console.ReadLine());
            Console.Write("How many classes? ");//                           כמה כיתות
            maxClasses = int.Parse(Console.ReadLine());

            School theSchool = new School(maxPupils, maxClasses);

            int answer = 0;
            while (answer != EXIT)
            {
                Console.WriteLine();
                Console.WriteLine("Choose one of the following options:");//בחר אחת מהאפשרויות הבאות
                Console.WriteLine("1) Add classes");//הוסף כיתות
                Console.WriteLine("2) Add pupils");//הוסף תלמידים
                Console.WriteLine("3) Show all data");//הצג את כל הנתונים
                Console.WriteLine("4) Change pupil's name");//שנה את שם התלמיד
                Console.WriteLine(EXIT + ")EXIT");
                Console.Write("Enter your choice: ");//הזן את בחירתך
                answer = int.Parse(Console.ReadLine());

                switch (answer)
                {
                    case 1:
                        ReadClasses(theSchool);
                        break;
                    case 2:
                        ReadPupils(theSchool);
                        break;
                    case 3:
                        Console.WriteLine(theSchool.ToString());
                        break;
                    case 4:
                        ChangePupilsName(theSchool);
                        break;
                    case EXIT:
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
            }
        }
    }
}