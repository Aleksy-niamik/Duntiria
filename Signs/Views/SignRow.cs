using Signs.Controllers;
using Signs.Enums;
using Signs.Interfaces;
using Signs.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Signs.Views
{
    public class SignRow
    {
        private List<PictureBox> signBoxes;
        private Label number;
        private Label description;
        private Label showMore;
        private ISymbolController controller;
        private Form1 Form;

        public int Id { get; set; }

        public Sign Sign { get; set; }

        public int Value { get; set;}

        public int SymbolsAmount { get; set; }

        public SignRow(Sign sign, int id, Form1 form, int symbolsAmount)
        {
            Id = id;
            Form = form;
            SymbolsAmount = symbolsAmount;
            controller = new SymbolController();
            Sign = sign;
            signBoxes = new List<PictureBox>();
            number = new Label();
            description = new Label();
            showMore = new Label();

            this.description.AutoSize = true;
            this.description.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.description.Location = new System.Drawing.Point(60, 4);
            this.description.Name = "number" + Id.ToString();
            this.description.TabIndex = 1;
            this.description.Text = " znak " + ((MaleNumerals)sign.Number).ToString() + 
                " (" + sign.Number.ToString() + ")\r\n" +
                " rodziny " + ((FemaleNumerals)sign.Family).ToString() + 
                " (" + sign.Family.ToString() + ")";

            this.number.AutoSize = true;
            this.number.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.number.Location = new Point(0,4);
            this.number.Name = "number" + Id.ToString();
            this.number.TabIndex = 1;
            this.number.Text = sign.Value.ToString();

            this.showMore.AutoSize = true;
            this.showMore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.showMore.Location = new Point(260 + SymbolsAmount * 60, 4);
            this.showMore.Name = "showMore" + Id.ToString();
            this.showMore.TabIndex = 1;
            this.showMore.Text = "Ogółem " + sign.Symbols.Count.ToString() + " symboli\r\nNajkrótszy ma długość " + 
                sign.Symbols.First().Length + " kółek";

            for (int i=0; i<Math.Min(sign.Symbols.Count, SymbolsAmount); i++)
            {
                var signBox = new PictureBox();
                signBox.Location = new System.Drawing.Point(260 + i*60, 0);
                signBox.Name = "signBox" + sign.Symbols[i].Id.ToString();
                signBox.TabIndex = 0;
                signBox.TabStop = false;
                signBox.Image = controller.SymbolToSquare(sign.Symbols[i], 50);
                signBox.Size = signBox.Image.Size;
                signBox.Click += new System.EventHandler(Form.signBox_Clicked);
                signBoxes.Add(signBox);
            }            
        }

        public void AddToForm(Control control, Point point)
        {
            List<Control> controls = new List<Control>();
            controls.AddRange(signBoxes);
            controls.Add(description);
            controls.Add(showMore);
            controls.Add(number);

            if (!control.Controls.Contains(number))
            {
                controls.ForEach(control => control.Location = new Point(control.Location.X + point.X, control.Location.Y + point.Y));
                control.Controls.Add(number);
                control.Controls.Add(description);
                control.Controls.Add(showMore);
                signBoxes.ForEach(signBox => control.Controls.Add(signBox));
            }
            

        }
    }
}
