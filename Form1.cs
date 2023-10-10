using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Kazino
{
    public partial class Form1 : Form
    {
        private readonly Timer Timer;
        private readonly Random Random = new Random();
        private DateTime StartTime;
        private const int MinRandomValue = 0;
        private const int MaxRandomValue = 2;
        private const int MaxLabelValue = 1;
        private const int StartingSum = 1000;

        public Form1()
        {
            InitializeComponent();

            Timer = new Timer();
            Timer.Interval = 100;
            Timer.Tick += new EventHandler(timer1_Tick);

            tbSuma.Text = StartingSum.ToString();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RandomStop(label1, 3, timer1);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            RandomStop(label2, 4, timer2);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            RandomStop(label3, 5, timer3);
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if ((DateTime.Now - StartTime).Seconds >= 6)
            {
                if (checkBox1.Checked)
                {
                    AutoTwistCheck();
                }
            }
        }
        
        private void RandomStop(Label label, int durationTime,Timer timer)
        {
            IncrementAndResetIfNeeded(label);

            if ((DateTime.Now - StartTime).TotalSeconds >= durationTime)
            {
                UpdateLabels(label);
                timer.Stop();
                if (label == label3)
                {
                    bStart.Enabled = true;
                    checkBox1.Enabled = true;
                }
            }
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(tbSuma.Text) <= 0)
            {
                tbSuma.Text = "0";
                MessageBox.Show("Недостаточно средств для ставки!");
                return;
            }

            UpdateValue();  
            StartTime = DateTime.Now;
            timer1.Start();
            timer2.Start();
            timer3.Start();
            timer4.Start();

            bStart.Enabled = false;
        }

        private void IncrementAndResetIfNeeded(Label label)
        {
            try
            {
                label.Text = (Convert.ToInt32(label.Text) + 1).ToString();

                if (Convert.ToInt32(label.Text) > MaxLabelValue)
                {
                    label.Text = 0.ToString();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка: Введенная строка не является допустимым числом.");
                checkBox1.Enabled = false;
            }
        }

        private int GetRandomValue()
        {
            return Random.Next(MinRandomValue, MaxRandomValue);
        }

        private void UpdateLabels(Label label)
        {
            label.Text = GetRandomValue().ToString();

            if (Convert.ToInt16(label.Text) == 1)
            {
                tbSuma.Text = (Convert.ToInt32(tbSuma.Text) + Convert.ToInt32(numericUpDown1.Value)).ToString();
            }
        }

        private void bMaxValue_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = Convert.ToDecimal(tbSuma.Text);
        }

        private void bMinValue_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = numericUpDown1.Minimum;
        }

        private void UpdateValue()
        {
            tbSuma.Text = (Convert.ToInt32(tbSuma.Text) - numericUpDown1.Value).ToString();
        }

        private void AutoTwistCheck()
        {
            if (checkBox1.Checked)
            {
                bStart_Click(this, EventArgs.Empty);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(!checkBox1.Checked &&  bStart.Enabled == false)
            {
                checkBox1.Enabled = false;
            }

            AutoTwistCheck();


        }

        private List<Button> buttons = new List<Button>();        
    }
}
