using Signs.Controllers;
using Signs.Enums;
using Signs.Interfaces;
using Signs.Models;
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
        public Form1(ISignRepository signRepository)
        {
            InitializeComponent();
            SignRepository = signRepository;
            Init();
        }

        private void Init()
        {
            var sign = new Sign(48);
            sign.Circles.AddRange(new Directions[] { Directions.Up, Directions.Up, Directions.Left, Directions.Left, Directions.Left, Directions.Down, Directions.Down });
            SignRepository.Add(sign);
           MessageBox.Show(sign.Value.ToString() + ' ' + sign.IsValid.ToString());

            var signController = new SignController();
            
            signBox.Image = signController.SignToImage(sign, 16);
            signBox.Size = signBox.Image.Size;
        }
    }
}
