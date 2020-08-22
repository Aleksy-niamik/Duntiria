using Signs.Controllers;
using Signs.Enums;
using Signs.Interfaces;
using Signs.Models;
using Signs.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Signs
{
    public partial class Form1 : Form
    {
        private ISignRepository SignRepository;
        private Sign Sign;
        private SignController SignController;
        private SignRow SignRow;
        private int idx = 0;
        public Form1(ISignRepository signRepository)
        {
            InitializeComponent();
            SignRepository = signRepository;
            Init();
        }

        private void Init()
        {
            Sign = new Sign(48);
            Sign.Circles.AddRange(new Directions[] { Directions.Up, Directions.Up, Directions.Left, Directions.Left, Directions.Left, Directions.Down, Directions.Down });
            SignRepository.Add(Sign);
            SignController = new SignController();
            SignRow = new SignRow(Sign, 0, new Point(0,0));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Directions[] directions = new Directions[(int) numericUpDown1.Value-1];
            var random = new Random();
            do
            {
                for (int i = 0; i < numericUpDown1.Value - 1; i++)
                {
                    directions[i] = random.Next(0, 6) switch
                    {
                        0 => Directions.Left,
                        1 => Directions.Left,
                        2 => Directions.Right,
                        3 => Directions.Right,
                        4 => Directions.Up,
                        5 => Directions.Down
                    };
                    if(i > 0)
                    {
                        if (directions[i - 1] == Directions.Left && directions[i] == Directions.Right ||
                         directions[i - 1] == Directions.Right && directions[i] == Directions.Left ||
                         directions[i - 1] == Directions.Up && directions[i] == Directions.Down ||
                         directions[i - 1] == Directions.Down && directions[i] == Directions.Up)
                            i--;
                    }
                }
                Sign.Circles = directions.ToList();
            } while (!Sign.IsValid);
           

            if(checkBox1.Checked)
            {
                signBox.Image = SignController.SignToImage(Sign, 50);
            }
            else
            {
                signBox.Image = SignController.SignToSquare(Sign, 100);
            }
            signBox.Size = signBox.Image.Size;
            label1.Text = Sign.IsValid ? Sign.Value.ToString() : Sign.Status.ToString() + "(" + Sign.FloatValue + ")";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignRow = new SignRow(Sign, idx, new Point( 5, 60*idx + 5));
            idx++;
            SignRow.AddToForm(this);
        }
    }
}
