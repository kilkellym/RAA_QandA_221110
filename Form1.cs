using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forms = System.Windows.Forms;
using Autodesk.Revit.DB;

namespace RAA_QandA_221110
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.Add("Text");
            comboBox1.Items.Add("Graphics");
            comboBox1.Items.Add("Other");

            comboBox2.Items.Add("Number");
            comboBox2.Items.Add("Area");
            comboBox2.Items.Add("Text");
            comboBox2.Items.Add("Length");

        }

        public ForgeTypeId GetGroupType()
        {
            if (comboBox1.SelectedIndex == 0)
                return GroupTypeId.Text;
            else if (comboBox1.SelectedIndex == 1)
                return GroupTypeId.Graphics;
            else if (comboBox1.SelectedIndex == 2)
                return GroupTypeId.General;

            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Forms.OpenFileDialog ofd = new Forms.OpenFileDialog())
            {
                ofd.InitialDirectory = "C:\\";
                ofd.Multiselect = true;
                ofd.Filter = "RFA files (*.rfa)|*.rfa";

                if (ofd.ShowDialog() == Forms.DialogResult.OK)
                {
                    foreach (string filename in ofd.FileNames)
                        listBox1.Items.Add(filename);

                }
            }
        }

        public List<string> GetFilenamesFromForm()
        {
            List<string> list = new List<string>();
            foreach (string item in listBox1.Items)
                list.Add(item);

            return list;
        }

        internal ForgeTypeId GetSpecType()
        {
            if (comboBox1.SelectedIndex == 0)
                return SpecTypeId.Number;
            else if (comboBox1.SelectedIndex == 1)
                return SpecTypeId.Area;
            else if (comboBox1.SelectedIndex == 2)
                return SpecTypeId.String.Text;
            else if (comboBox1.SelectedIndex == 3)
                return SpecTypeId.Length;

            return null;
        }

        internal string GetParamName()
        {
            return textBox1.Text;
        }
    }
}
