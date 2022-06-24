using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace BestOil
{
    public partial class BestOil : Form
    {
        private double SummFuel = 0;
        private double SummCafe = 0;
        private double Overall = 0;
        private double QuantityFuel = 0;
        private string Currency = "грн";
        private string FuelAmountUnit = "літри";
        public BestOil()
        {
            InitializeComponent();
            Fill();
        }
        private void Fill()
        {
            comboBox1.Items.AddRange(new Fuel[]
            { 
                new Fuel { Cost = 25, TypeOfFuel = "A-95" },
                new Fuel { Cost = 20, TypeOfFuel = "A-92" },
                new Fuel { Cost = 30, TypeOfFuel = "A-98" },
                new Fuel { Cost = 26, TypeOfFuel = "ДП"   }
            });
            comboBox1.Text = (comboBox1.Items[0] as Fuel).TypeOfFuel;
            textBox3.Text= (comboBox1.Items[0] as Fuel).Cost.ToString();
            textBox3.ReadOnly = true;
            label1.Text = $"{SummFuel}";
            label6.Text = Currency;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            label2.Text = SummCafe.ToString();
            label7.Text = Currency;
            label3.Text = Overall.ToString();
            label8.Text = Currency;
        }
        public void SetCurr(string curr) => Currency = curr;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text=(comboBox1.SelectedItem as Fuel).Cost.ToString();
            if(textBox1.TextLength>0&&textBox2.TextLength>0)
            {
                if (radioButton1.Checked)
                {
                    SummFuel = QuantityFuel * (int)(comboBox1.SelectedItem as Fuel).Cost;
                    textBox2.Text = SummFuel.ToString();
                    label1.Text = SummFuel.ToString();
                    label6.Text = Currency;
                    groupBox2.Text = "До сплати:";
                }
                else if(radioButton2.Checked)
                {
                    QuantityFuel = SummFuel / (int)(comboBox1.SelectedItem as Fuel).Cost;
                    textBox1.Text = QuantityFuel.ToString(); 
                    label6.Text = FuelAmountUnit;
                    groupBox2.Text = "До видачі:";
                    label1.Text = textBox1.Text;
                }
            }
            Overall = SummCafe + SummFuel;
            label3.Text = Overall.ToString();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.ReadOnly = true;
            textBox1.ReadOnly = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.ReadOnly = false;
            textBox1.ReadOnly = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!textBox1.ReadOnly)
            {
                if (textBox1.Text.Length > 0)
                {
                    var q = from i in textBox1.Text where char.IsDigit(i) select i;
                    textBox1.Text = string.Concat(q);
                }
                if (textBox1.Text.Length > 0)
                {
                    textBox2.Text = (Convert.ToDouble(textBox1.Text) * Convert.ToDouble(textBox3.Text)).ToString();
                    SummFuel = Convert.ToDouble(textBox2.Text);
                    QuantityFuel = Convert.ToDouble(textBox1.Text);
                    label1.Text = SummFuel.ToString();
                    label6.Text = Currency;
                    groupBox2.Text = "До сплати:";
                }
                if (textBox1.Text.Length == 0)
                {
                    textBox2.Clear();
                    SummFuel = 0;
                    QuantityFuel = 0;
                    label1.Text = SummFuel.ToString();
                }
                textBox1.SelectionStart = textBox1.TextLength;
                Overall = SummCafe + SummFuel;
                label3.Text = Overall.ToString();
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!textBox2.ReadOnly)
            {
                if (textBox2.Text.Length > 0)
                {
                    var q = from i in textBox2.Text where char.IsDigit(i) select i;
                    textBox2.Text = string.Concat(q);
                }
                if (textBox2.Text.Length > 0)
                {
                    textBox1.Text = (Convert.ToDouble(textBox2.Text) / Convert.ToDouble(textBox3.Text)).ToString();
                    QuantityFuel = Convert.ToDouble(textBox1.Text);
                    SummFuel = Convert.ToDouble(textBox2.Text);
                    label6.Text = FuelAmountUnit;
                    groupBox2.Text = "До видачі:";
                    label1.Text = textBox1.Text;
                }
                if (textBox2.Text.Length == 0)
                {
                    textBox1.Clear();
                    SummFuel = 0;
                    QuantityFuel = 0;
                    label1.Text = QuantityFuel.ToString();
                }
                textBox2.SelectionStart = textBox2.TextLength;
                Overall = SummCafe + SummFuel;
                label3.Text = Overall.ToString();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double sum=0;
            sum+=(double)numericUpDown1.Value*Convert.ToDouble(textBox4.Text);
            sum += (double)numericUpDown2.Value * Convert.ToDouble(textBox5.Text);
            sum += (double)numericUpDown3.Value * Convert.ToDouble(textBox6.Text);
            sum += (double)numericUpDown4.Value * Convert.ToDouble(textBox7.Text);
            SummCafe = sum;
            label2.Text=SummCafe.ToString();
            Overall = SummCafe + SummFuel;
            label3.Text = Overall.ToString();
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            double sum = 0;
            sum += (double)numericUpDown1.Value * Convert.ToDouble(textBox4.Text);
            sum += (double)numericUpDown2.Value * Convert.ToDouble(textBox5.Text);
            sum += (double)numericUpDown3.Value * Convert.ToDouble(textBox6.Text);
            sum += (double)numericUpDown4.Value * Convert.ToDouble(textBox7.Text);
            SummCafe = sum;
            label2.Text = SummCafe.ToString();
            Overall = SummCafe + SummFuel;
            label3.Text = Overall.ToString();
        }
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double sum = 0;
            sum += (double)numericUpDown1.Value * Convert.ToDouble(textBox4.Text);
            sum += (double)numericUpDown2.Value * Convert.ToDouble(textBox5.Text);
            sum += (double)numericUpDown3.Value * Convert.ToDouble(textBox6.Text);
            sum += (double)numericUpDown4.Value * Convert.ToDouble(textBox7.Text);
            SummCafe = sum;
            label2.Text = SummCafe.ToString();
            Overall = SummCafe + SummFuel;
            label3.Text = Overall.ToString();
        }
        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            double sum = 0;
            sum += (double)numericUpDown1.Value * Convert.ToDouble(textBox4.Text);
            sum += (double)numericUpDown2.Value * Convert.ToDouble(textBox5.Text);
            sum += (double)numericUpDown3.Value * Convert.ToDouble(textBox6.Text);
            sum += (double)numericUpDown4.Value * Convert.ToDouble(textBox7.Text);
            SummCafe = sum;
            label2.Text = SummCafe.ToString();
            Overall = SummCafe + SummFuel;
            label3.Text = Overall.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = !numericUpDown1.Enabled;
            numericUpDown1.Value = 0;
            double sum = 0;
            sum += (double)numericUpDown1.Value * Convert.ToDouble(textBox4.Text);
            sum += (double)numericUpDown2.Value * Convert.ToDouble(textBox5.Text);
            sum += (double)numericUpDown3.Value * Convert.ToDouble(textBox6.Text);
            sum += (double)numericUpDown4.Value * Convert.ToDouble(textBox7.Text);
            SummCafe = sum;
            label2.Text = SummCafe.ToString();
            Overall = SummCafe + SummFuel;
            label3.Text=Overall.ToString();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown2.Enabled = !numericUpDown2.Enabled;
            numericUpDown2.Value = 0;
            double sum = 0;
            sum += (double)numericUpDown1.Value * Convert.ToDouble(textBox4.Text);
            sum += (double)numericUpDown2.Value * Convert.ToDouble(textBox5.Text);
            sum += (double)numericUpDown3.Value * Convert.ToDouble(textBox6.Text);
            sum += (double)numericUpDown4.Value * Convert.ToDouble(textBox7.Text);
            SummCafe = sum;
            label2.Text = SummCafe.ToString();
            Overall = SummCafe + SummFuel;
            label3.Text = Overall.ToString();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown3.Enabled = !numericUpDown3.Enabled;
            numericUpDown3.Value = 0;
            double sum = 0;
            sum += (double)numericUpDown1.Value * Convert.ToDouble(textBox4.Text);
            sum += (double)numericUpDown2.Value * Convert.ToDouble(textBox5.Text);
            sum += (double)numericUpDown3.Value * Convert.ToDouble(textBox6.Text);
            sum += (double)numericUpDown4.Value * Convert.ToDouble(textBox7.Text);
            SummCafe = sum;
            label2.Text = SummCafe.ToString();
            Overall = SummCafe + SummFuel;
            label3.Text = Overall.ToString();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown4.Enabled = !numericUpDown4.Enabled;
            numericUpDown4.Value = 0;
            double sum = 0;
            sum += (double)numericUpDown1.Value * Convert.ToDouble(textBox4.Text);
            sum += (double)numericUpDown2.Value * Convert.ToDouble(textBox5.Text);
            sum += (double)numericUpDown3.Value * Convert.ToDouble(textBox6.Text);
            sum += (double)numericUpDown4.Value * Convert.ToDouble(textBox7.Text);
            SummCafe = sum;
            label2.Text = SummCafe.ToString();
            Overall = SummCafe + SummFuel;
            label3.Text = Overall.ToString();
        }
    }
    public class Fuel
    {
        public string TypeOfFuel { get; set; }
        public double Cost { get; set; }
        public override string ToString()
        {
            return TypeOfFuel;
        }
    }
}
