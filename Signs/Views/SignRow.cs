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
        private ISignController controller;

        public int Id { get; set; }

        public Sign Sign { get; set; }

        public Point Point { get; set; }

        public int Value { get; set;}

        public SignRow(Sign sign, int id, Point point)
        {
            controller = new SignController();
            Sign = sign;
            Point = point;
            signBoxes = new List<PictureBox>();
            number = new Label();
            description = new Label();
            showMore = new Label();

            this.description.AutoSize = true;
            this.description.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.description.Location = new System.Drawing.Point(Point.X + 60, Point.Y);
            this.description.Name = "number" + Id.ToString();
            this.description.TabIndex = 1;
            this.description.Text = " znak " + ((MaleNumerals)(sign.Value % 12)).ToString() + 
                " (" + ((SignNumbers)(sign.Value % 12)).ToString() + ")\r\n" +
                " rodziny " + ((FemaleNumerals)(sign.Value / 12)).ToString() + 
                " (" + ((SignFamilies)(sign.Value / 12)).ToString() + ")";

            this.number.AutoSize = true;
            this.number.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.number.Location = Point;
            this.number.Name = "number" + Id.ToString();
            this.number.TabIndex = 1;
            this.number.Text = sign.Value.ToString();

            var signBox = new PictureBox();
            signBox.Location = new System.Drawing.Point(Point.X + 260, Point.Y - 4);
            signBox.Name = "signBox" + Id.ToString();
            signBox.TabIndex = 0;
            signBox.TabStop = false;
            signBox.Image = controller.SignToSquare(sign, 50);
            signBox.Size = signBox.Image.Size;

            signBoxes.Add(signBox);
        }

        public void AddToForm(Form form)
        {
            if (!form.Controls.Contains(number))
            {
                form.Controls.Add(number);
            }
            if (!form.Controls.Contains(description))
            {
                form.Controls.Add(description);
            }
            signBoxes.ForEach(signBox =>
            {
                if (!form.Controls.Contains(signBox))
                    form.Controls.Add(signBox);
            });
            

        }
    }
}
