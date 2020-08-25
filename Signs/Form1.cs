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
        private ISymbolRepository SymbolRepository;
        private ISignRepository SignRepository;
        private ISymbolsGenerator SymbolsGenerator;
        private Column[] Columns;
        private State state;
        public Form1(ISymbolRepository symbolRepository, ISignRepository signRepository)
        {
            InitializeComponent();
            SymbolRepository = symbolRepository;
            SignRepository = signRepository;
            Init();
        }

        private void Init()
        {
            state = new State(this);

            SymbolsGenerator = new SymbolsGenerator();
            SymbolRepository.AddRange(SymbolsGenerator.Generate(11, 48));
            //TODO: variable alef from user
            SignRepository.AddRange(SymbolRepository.ToSigns());

            Columns = new Column[12];
            for (int i = 0; i < Columns.Length; i++)
                Columns[i] = new Column(i, this);

            Resized();
            Render(state.SymbolsAmount);
        }

        private void MatchTabsNames()
        {
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (radioButton1.Checked)
                    tabControl1.TabPages[i].Text = ((SignFamilies)i).ToString();
                else if (radioButton2.Checked)
                    tabControl1.TabPages[i].Text = ((SignNumbers)i).ToString();
                else if (radioButton3.Checked)
                    tabControl1.TabPages[i].Text = i.ToString();
            }
        }

        private void Render(int symbolsAmount)
        {
            MatchTabsNames();

            for (int i = 0; i < Columns.Length; i++)
            {
                tabControl1.TabPages[i].Controls.Clear();
                if (radioButton1.Checked)
                    Columns[i].MakeRowsFromSigns(SignRepository.GetByFamily((SignFamilies)i), symbolsAmount);
                else if (radioButton2.Checked)
                    Columns[i].MakeRowsFromSigns(SignRepository.GetByNumber((SignNumbers)i), symbolsAmount);
                else if (radioButton3.Checked)
                    Columns[i].MakeRowsFromSigns(SignRepository.GetByLength(i), symbolsAmount);

                if (radioButton4.Checked)
                    Columns[i].Rows.Sort((row1, row2) => (row1.Sign.Value - row2.Sign.Value == 0) ?
                        row1.Sign.Length - row2.Sign.Length : row1.Sign.Value - row2.Sign.Value);
                else if (radioButton5.Checked)
                    Columns[i].Rows.Sort((row1, row2) => (row1.Sign.Length - row2.Sign.Length == 0) ?
                        row1.Sign.Value - row2.Sign.Value : row1.Sign.Length - row2.Sign.Length);
                Columns[i].AddToControl(tabControl1.TabPages[i]);
            }
        }

        private void radioButton_Clicked(object sender, EventArgs e)
        {
            if (!state.CheckChangeAndUpdateState(this)) return;
            Render(state.SymbolsAmount);
        }

        public void signBox_Clicked(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                var symbol = SymbolRepository.GetById(int.Parse(pictureBox.Name.Substring(7)));
                var symbolController = new SymbolController();
                pictureBox1.Image = symbolController.SymbolToSquare(symbol, pictureBox1.Size.Width);
            }
        }

        public void WindowResized(object sender, EventArgs e)
        {
            Resized();
        }

        private void Resized()
        {
            if (state.CheckChangeAndUpdateState(this))
                Render(state.SymbolsAmount);
            tabControl1.Size = new Size(480 + state.SymbolsAmount * 60, this.Size.Height - 100);

            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                var panels = tabControl1.TabPages[i].Controls.Find("panel" + i.ToString(), true);
                if(panels.Any())
                {
                    var panel = panels.First();
                    panel.Size = new Size(tabControl1.Width, this.Height - 120);
                }
            }

            groupBox1.Location = new Point(498 + state.SymbolsAmount * 60, groupBox1.Location.Y);
            groupBox2.Location = new Point(498 + state.SymbolsAmount * 60, groupBox2.Location.Y);
            pictureBox1.Location = new Point(498 + state.SymbolsAmount * 60, pictureBox1.Location.Y);
        }

        private class State
        {
            public bool[] Radios { get; set; }

            public int SymbolsAmount { get; set; }

            public State(Form1 form)
            {
                Radios = new bool[5];
                Update(form);
            }

            public bool CheckChangeAndUpdateState(Form1 form)
            {
                if (Radios[0] != form.radioButton1.Checked ||
                    Radios[1] != form.radioButton2.Checked ||
                    Radios[2] != form.radioButton3.Checked ||
                    Radios[3] != form.radioButton4.Checked ||
                    Radios[4] != form.radioButton5.Checked ||
                    SymbolsAmount != (form.Width - 816) / 60)
                {
                    Update(form);
                    return true;
                }
                else return false;
            }

            public void Update(Form1 form)
            {
                Radios[0] = form.radioButton1.Checked;
                Radios[1] = form.radioButton2.Checked;
                Radios[2] = form.radioButton3.Checked;
                Radios[3] = form.radioButton4.Checked;
                Radios[4] = form.radioButton5.Checked;
                SymbolsAmount = (form.Width - 816) / 60;
            }

        }
    }
}
