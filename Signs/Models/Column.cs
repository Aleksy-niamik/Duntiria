using Signs.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Signs.Models
{
    public class Column
    {
        private Form1 Form;
        public int Id { get; set; }

        public List<SignRow> Rows { get; set; }

        public Column(int id, Form1 form)
        {
            Id = id;
            Form = form;
            Rows = new List<SignRow>();
        }

        public void MakeRowsFromSigns(IEnumerable<Sign> signs, int symbolsAmount)
        {
            Rows.Clear();
            int i = 0;
            foreach(Sign sign in signs)
            {
                Rows.Add(new SignRow(sign, Id * 1000 + i++, Form, symbolsAmount));
            }
        }

        public void AddToControl(Control control)
        {
            var panel = new Panel();
            panel.Location = new System.Drawing.Point(0, 0);
            panel.Name = "panel" + Id.ToString();
            panel.TabIndex = 11;
            panel.AutoScroll = true;
            panel.Size = new Size(control.Size.Width, control.Size.Height - 60);

            for (int i=0; i<Rows.Count; i++)
            {
                Rows[i].AddToForm(panel, new System.Drawing.Point(0, i*60));
            }
            control.Controls.Add(panel);
        }
    }
}
