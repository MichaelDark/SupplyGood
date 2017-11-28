using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace supplyGood
{
    public partial class FilterForm : Form
    {
        public List<Filter> Filters { get; private set; }
        public FilterForm(List<Filter> filters)
        {
            InitializeComponent();

            Filters = filters;

            for (int i = 0; i < Filters.Count; i++)
            {
                CheckBox cbx = new CheckBox() { Checked = Filters[i].Checked };
                switch (filters[i].Type)
                {
                    case FilterType.Number:
                        {
                            cbx.AutoSize = true;
                            cbx.Location = new System.Drawing.Point(12, 59 + 40 * i);
                            cbx.Size = new System.Drawing.Size(120, 29);
                            cbx.Name = filters[i].Field;
                            cbx.Text = filters[i].Header;

                            TextBox txt1 = new TextBox();
                            txt1.Location = new System.Drawing.Point(281, 57 + 40 * i);
                            txt1.Size = new System.Drawing.Size(165, 33);
                            txt1.Name = "from" + filters[i].Field;
                            txt1.Text = filters[i].From == null ? "" : filters[i].From.ToString();

                            TextBox txt2 = new TextBox();
                            txt2.Location = new System.Drawing.Point(478, 57 + 40 * i);
                            txt2.Size = new System.Drawing.Size(165, 33);
                            txt2.Name = "to" + filters[i].Field;
                            txt2.Text = filters[i].To == null ? "" : filters[i].To.ToString();

                            Label lbl = new Label();
                            lbl.AutoSize = true;
                            lbl.Location = new System.Drawing.Point(452, 60 + 40 * i);
                            lbl.Size = new System.Drawing.Size(20, 25);
                            lbl.Name = "label1";
                            lbl.TabIndex = 10;
                            lbl.Text = "-";
                            Controls.Add(txt1);
                            Controls.Add(txt2);
                            Controls.Add(lbl);
                            break;
                        }
                    case FilterType.Date:
                        {
                            cbx.AutoSize = true;
                            cbx.Location = new System.Drawing.Point(12, 59 + 40 * i);
                            cbx.Size = new System.Drawing.Size(120, 29);
                            cbx.Name = filters[i].Field;
                            cbx.Text = filters[i].Header;

                            DateTimePicker txt1 = new DateTimePicker();
                            txt1.Location = new System.Drawing.Point(281, 57 + 40 * i);
                            txt1.Size = new System.Drawing.Size(165, 33);
                            txt1.Name = "from" + filters[i].Field;
                            txt1.Value = filters[i].FromDate == null ? DateTime.Now.AddMonths(-1).Date : (DateTime)filters[i].FromDate;

                            DateTimePicker txt2 = new DateTimePicker();
                            txt2.Location = new System.Drawing.Point(478, 57 + 40 * i);
                            txt2.Size = new System.Drawing.Size(165, 33);
                            txt2.Name = "to" + filters[i].Field;
                            txt2.Value = filters[i].ToDate == null ? DateTime.Now.Date : (DateTime)filters[i].ToDate;

                            Label lbl = new Label();
                            lbl.AutoSize = true;
                            lbl.Location = new System.Drawing.Point(452, 60 + 40 * i);
                            lbl.Size = new System.Drawing.Size(20, 25);
                            lbl.Name = "label1";
                            lbl.TabIndex = 10;
                            lbl.Text = "-";
                            Controls.Add(txt1);
                            Controls.Add(txt2);
                            Controls.Add(lbl);
                            break;
                        }
                    case FilterType.Text:
                        {
                            cbx.AutoSize = true;
                            cbx.Location = new System.Drawing.Point(12, 59 + 40 * i);
                            cbx.Size = new System.Drawing.Size(120, 29);
                            cbx.Name = filters[i].Field;
                            cbx.Text = filters[i].Header;

                            TextBox txt1 = new TextBox();
                            txt1.Location = new System.Drawing.Point(281, 57 + 40 * i);
                            txt1.Size = new System.Drawing.Size(362, 33);
                            txt1.Name = "text" + filters[i].Field;
                            txt1.Text = filters[i].Value == null ? "" : filters[i].Value;

                            Controls.Add(txt1);
                            break;
                        }
                    case FilterType.Bool:
                        {
                            cbx.AutoSize = true;
                            cbx.Location = new System.Drawing.Point(12, 59 + 40 * i);
                            cbx.Size = new System.Drawing.Size(120, 29);
                            cbx.Name = filters[i].Field;
                            cbx.Text = filters[i].Header;

                            CheckBox txt1 = new CheckBox();
                            txt1.Location = new System.Drawing.Point(281, 57 + 40 * i);
                            txt1.Size = new System.Drawing.Size(362, 33);
                            txt1.Name = "bool" + filters[i].Field;
                            txt1.Text = "Только истинные";
                            txt1.Checked = filters[i].OnlyTrue == null ? false : (bool)filters[i].OnlyTrue;

                            Controls.Add(txt1);
                            break;
                        }
                }
                Controls.Add(cbx);
            }
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            CheckBox s = (CheckBox)(sender as Control);
            switch (Filters.Find(x => x.Field == s.Name).Type)
            {
                case FilterType.Number:
                case FilterType.Date:
                    {
                        Controls.Find("from" + s.Name, true)[0].Enabled = s.Checked;
                        Controls.Find("to" + s.Name, true)[0].Enabled = s.Checked;
                        break;
                    }
                case FilterType.Text:
                case FilterType.Bool:
                    {
                        Controls.Find("text" + s.Name, true)[0].Enabled = s.Checked;
                        break;
                    }
            }
        }

        private void btnFunc_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Filters.Count; i++)
            {
                CheckBox s = (CheckBox)(Controls.Find(Filters[i].Field, true)[0]);
                Filters[i].Checked = s.Checked;
                if (Filters[i].Checked)
                {
                    switch (Filters[i].Type)
                    {
                        case FilterType.Number:
                            {
                                double from;
                                double to;
                                if ((Controls.Find("from" + Filters[i].Field, true)[0] as TextBox).Text.Trim() != "")
                                {
                                    if (Double.TryParse((Controls.Find("from" + Filters[i].Field, true)[0] as TextBox).Text, out from))
                                    {
                                        Filters[i].From = from;
                                    }
                                    else
                                    {
                                        Filters[i].From = null;
                                    }
                                }
                                if ((Controls.Find("to" + Filters[i].Field, true)[0] as TextBox).Text.Trim() != "")
                                {
                                    if (Double.TryParse((Controls.Find("to" + Filters[i].Field, true)[0] as TextBox).Text, out to))
                                    {
                                        Filters[i].To = to;
                                    }
                                    else
                                    {
                                        Filters[i].To = null;
                                    }
                                }
                                break;
                            }
                        case FilterType.Date:
                            {
                                DateTime from = (Controls.Find("from" + Filters[i].Field, true)[0] as DateTimePicker).Value.Date;
                                DateTime to = (Controls.Find("to" + Filters[i].Field, true)[0] as DateTimePicker).Value.Date;
                                Filters[i].FromDate = from;
                                Filters[i].ToDate = to;
                                break;
                            }
                        case FilterType.Text:
                            {
                                Filters[i].Value = (Controls.Find("text" + Filters[i].Field, true)[0] as TextBox).Text;
                                break;
                            }
                        case FilterType.Bool:
                            {
                                Filters[i].OnlyTrue = (Controls.Find("bool" + Filters[i].Field, true)[0] as CheckBox).Checked;
                                break;
                            }
                    }
                }
            }
            Close();
        }
    }
}
