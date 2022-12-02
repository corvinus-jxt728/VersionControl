using mikroszim.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace mikroszim
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        Random rng = new Random(1234);
        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"C:\Temp\nép.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");
          //  dataGridView1.DataSource = Population;
            
        }
        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }

            return population;
        }
        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> bp = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    bp.Add(new BirthProbability()
                    {
                        Age = int.Parse(line[0]),
                        NmbOfChildren = byte.Parse(line[1]),
                        BirthGivingProb = double.Parse(line[0])
                    });
                }
            }

            return bp;
        }
        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbability> dp = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    dp.Add(new DeathProbability()
                    {
                        Age = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        DyingProb = double.Parse(line[0])
                    });
                }
            }

            return dp;
        }
        public void Szimu()
            {
            for (int year = 2005; year <= 2024; year++)
            {
                // Végigmegyünk az összes személyen
                for (int i = 0; i < Population.Count; i++)
                {
                    SimStep(year,Population[i]);
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                richTextBox1.Text+=(
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
            }
        }
            private void SimStep(int year, Person person)
            {
                //Ha halott akkor kihagyjuk, ugrunk a ciklus következő lépésére
                if (!person.IsAlive) return;

                // Letároljuk az életkort, hogy ne kelljen mindenhol újraszámolni
                byte age = (byte)(year - person.BirthYear);

                // Halál kezelése
                // Halálozási valószínűség kikeresése
                double pDeath = (from x in DeathProbabilities
                                 where x.Gender == person.Gender && x.Age == age
                                 select x.DyingProb).FirstOrDefault();
                // Meghal a személy?
                if (rng.NextDouble() <= pDeath)
                    person.IsAlive = false;

                //Születés kezelése - csak az élő nők szülnek
                if (person.IsAlive && person.Gender == Gender.Female)
                {
                    //Szülési valószínűség kikeresése
                    double pBirth = (from x in BirthProbabilities
                                     where x.Age == age
                                     select x.BirthGivingProb).FirstOrDefault();
                    //Születik gyermek?
                    if (rng.NextDouble() <= pBirth)
                    {
                        Person újszülött = new Person();
                        újszülött.BirthYear = year;
                        újszülött.NbrOfChildren = 0;
                        újszülött.Gender = (Gender)(rng.Next(1, 3));
                        Population.Add(újszülött);
                    }
                }
            }

        private void button1_Click(object sender, EventArgs e)
        {
            Szimu();
            DisplayResults();
        }
        public void DisplayResults()
        {
    
        }
    }
    
}
