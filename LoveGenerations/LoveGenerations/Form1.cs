using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldsHardestGame;

namespace LoveGenerations
{
    public partial class Form1 : Form
    {
        GameController gc = new GameController();
        GameArea ga;
        int populationSize = 250;
        int nbrOfSteps = 10;
        int nbrOfStepsIncrement = 1;
        int generation = 1;

        public Form1()
        {
            InitializeComponent();

            ga = gc.ActivateDisplay();
            gc.GameOver += Gc_GameOver;
            for (int i = 0; i < populationSize; i++)
            {
                gc.AddPlayer(nbrOfSteps);
            }
           
            this.Controls.Add(ga);
            gc.Start();

            
            //  gc.AddPlayer();
            //  gc.Start(true);
        }

        private void Gc_GameOver(object sender)
        {
            var playerList = from p in gc.GetCurrentPlayers()
                             orderby p.GetFitness() descending
                             select p;
            var topPerformers = playerList.Take(populationSize / 2).ToList();
            generation++;
            this.Text = string.Format(
                "{0}. generáció",
                generation);
            gc.ResetCurrentLevel();
            foreach (var p in topPerformers)
            {
                var b = p.Brain.Clone();
                if (generation % 3 == 0)
                    gc.AddPlayer(b.ExpandBrain(nbrOfStepsIncrement));
                else
                    gc.AddPlayer(b);

                if (generation % 3 == 0)
                    gc.AddPlayer(b.Mutate().ExpandBrain(nbrOfStepsIncrement));
                else
                    gc.AddPlayer(b.Mutate());
            }
            gc.Start();
        }
    }
}
