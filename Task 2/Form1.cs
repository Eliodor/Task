using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Task_2
{
   public partial class Form1 : Form
    {
       public static int Day;
       public static int Month;
       public static int Year;
       public static int FirstPoint;
       public static int SecondPoint;
       public static int ThirdPoint;
       public static int EndLine;
       public static string file = "Save.txt";
       public static string FileTxt;
       public static string Rezult;
       public static string DayZero;
       public static string MonthZero;
       public static string Date;
       public static string Search_1;
       public static string Search_2;
       public static bool rezult;
       public static bool today;
  
        public Form1()
        {
            InitializeComponent();

            
        }

        public bool CheckTime()
        {
            if (listBox1.SelectedItem == null) { listBox1.SelectedItem = "0"; }
            if (listBox2.SelectedItem == null) { listBox2.SelectedItem = "00"; }
            if (listBox3.SelectedItem == null) { listBox3.SelectedItem = "0"; }
            if (listBox4.SelectedItem == null) { listBox4.SelectedItem = "00"; }
            int stInt1 = int.Parse(listBox1.SelectedItem.ToString());
            int stInt2 = int.Parse(listBox2.SelectedItem.ToString());
            int enInt1 = int.Parse(listBox3.SelectedItem.ToString());
            int enInt2 = int.Parse(listBox4.SelectedItem.ToString());
            if (stInt1 < enInt1)
            {
                rezult = true;
            }
            else if(stInt1 > enInt1)
            {
                rezult = false;
            }
            else if (stInt1 == enInt1 && stInt2 <= enInt2)
            {
                rezult= true;
            }
            else if (stInt1 == enInt1 && stInt2 > enInt2)
            {
                rezult = false;
            }
            return rezult;

        }


        public void DateValue()
        {   Day = dateTimePicker1.Value.Day;
            Month = dateTimePicker1.Value.Month;
            Year = dateTimePicker1.Value.Year;
            int DayToday = DateTime.Today.Day;
            int MonthToday = DateTime.Today.Month;
            int YearToday = DateTime.Today.Year;
            if (YearToday > Year)
            {
                today = false;
            }
            else if (MonthToday > Month)
            {
                today = false;
            }
            else if (DayToday > Day)
            {
                today = false;
            }
            else { today = true; }
            if (Day < 10)
            {
                DayZero = "0";
            }
            else
            {
                DayZero = "";
            }

            if (Month < 10)
            {
                MonthZero = "0";
            }
            else
            {
                MonthZero = "";
            }
            Date = DayZero + Day.ToString() + " " + MonthZero + Month.ToString() + " " + Year.ToString() + " ";

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateValue();
            File.AppendAllText(file, "");
            FileTxt = File.ReadAllText(file);
            FirstPoint = FileTxt.IndexOf(Date);
            if (today == true)
            {
                if (FirstPoint == -1)
                {
                    if (textBox1.Text != "")
                    {
                        if (textBox1.Text.Length > 50)
                        {
                            int Excess = textBox1.Text.Length - 50;
                            MessageBox.Show("Символов больше 50, удалите " + Excess.ToString() + " символов");

                        }
                        else
                        {
                            if (CheckTime() == true)
                            {
                                DateValue();
                                File.AppendAllText(file, Date + textBox1.Text + (char)176 +
                                listBox1.SelectedItem + ":" + listBox2.SelectedItem + (char)169 +
                                listBox3.SelectedItem + ":" + listBox4.SelectedItem + (char)174 + "\r\n");
                            }
                            else { MessageBox.Show("Начало события не может быть после его конца"); }
                        }
                    }
                }
                else
                {
                    string message = "У вас есть планы на этот день, желаете заменить?";
                    string caption = "Ошибка";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (textBox1.Text.Length > 50)
                        {
                            int Excess = textBox1.Text.Length - 50;
                            MessageBox.Show("Символов больше 50, удалите " + Excess.ToString() + " символов");
                        }
                        else
                        {
                            if (CheckTime() == true)
                            {
                                FileTxt = File.ReadAllText(file);
                                FirstPoint = FileTxt.IndexOf(Date);//Начало строки
                                Search_1 = FileTxt.Substring(FirstPoint);
                                EndLine = Search_1.IndexOf((char)174); //Конец строки
                                if (FirstPoint == 0)
                                { FirstPoint = 1; }
                                FileTxt = FileTxt.Remove(FirstPoint - 1, EndLine + 2);
                                File.WriteAllText(file, FileTxt);
                                File.AppendAllText(file, Date + textBox1.Text + (char)176 +
                                    listBox1.SelectedItem + ":" + listBox2.SelectedItem + (char)169 +
                                    listBox3.SelectedItem + ":" + listBox4.SelectedItem + (char)174 + "\r\n");
                            }
                            else
                            { MessageBox.Show("Начало события не может позже его конца"); }
                        }
                    }

                }

            }
            else { MessageBox.Show("Нельзя записать событие в прошлое"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateValue();
            FileTxt = File.ReadAllText(file);
            FirstPoint = FileTxt.IndexOf(Date);
            if (FirstPoint != -1)
            {
                DateValue();
                FileTxt = File.ReadAllText(file);
                FirstPoint = FileTxt.IndexOf(Date);//Начало строки
                FirstPoint += 11;
                Search_1 = FileTxt.Substring(FirstPoint);
                SecondPoint = Search_1.IndexOf((char)176);//Конец названия, начала времени
                Search_2 = Search_1.Substring(SecondPoint);
                ThirdPoint = Search_2.IndexOf((char)169) + SecondPoint;//Начало времени окончания
                EndLine = Search_1.IndexOf((char)174); //Конец строки
                MessageBox.Show("Событие: " + Search_1.Substring(0, SecondPoint) + ". \r\nНачало в: " +
                    Search_1.Substring(SecondPoint + 1, ThirdPoint - SecondPoint - 1) + "(час:мин)" +
                    ". \r\nКонец в: " + Search_1.Substring(ThirdPoint + 1, EndLine - ThirdPoint - 1) + "(час:мин)");

            }
            else
            { 
                MessageBox.Show("У вас нет планов на этот день");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateValue();
            FileTxt = File.ReadAllText(file);
            FirstPoint = FileTxt.IndexOf(Date);//Начало строки
            if (FirstPoint != -1)
            {
                Search_1 = FileTxt.Substring(FirstPoint);
                EndLine = Search_1.IndexOf((char)174); //Конец строки
                if (FirstPoint == 0)
                { FirstPoint = 1; }
                FileTxt = FileTxt.Remove(FirstPoint - 1, EndLine + 2);
                File.WriteAllText(file, FileTxt);
            }
            else 
            { MessageBox.Show("Нельзя удалить то, чего не существует :)");
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
