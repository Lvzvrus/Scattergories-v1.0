using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace scattegories
{
    public partial class Form1 : Form
    {
        ColorDialog dlg = new ColorDialog();
        string words1, questions1,questions2;
        int t = 60, q, b1 = 0, b2 = 0;
        Int32 t1;
        Int16 hidden = 0, alert = 0;
        Random rnd = new Random();
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd1 = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dtbl = new DataTable();
        string[] words = new string[30] { "آ", "ب", "پ", "ث", "ت", "ج", "چ", "ح", "خ", "س", "ش", "د", "ز", "ر", "ژ", "ص", "ض", "ط", "ظ", "ع", "غ", "ک", "گ", "ف", "ق", "ن", "و", "ه", "ی", "ل" };
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t1 = Convert.ToInt32(textBox3.Text);
            t1 = t1 - 1;
            textBox3.Text = Convert.ToString(t1);
            if (b2 == 1)
            {
                if (t1 <= alert && t1 % 2 == 0)
                    textBox3.BackColor = Color.Red;
                if (t1 <= alert && t1 % 2 != 0)
                    textBox3.BackColor = Color.White;
            }
            if (b1 == 1)
                if (t1 < hidden)
                    textBox3.Visible = false;
            if (b1 == 0)
                textBox3.Visible = true;
            if (t1 == 0)
            {
                words1 = textBox2.Text;
                textBox2.Text = "";
                questions1 = textBox1.Text;
                questions2 = textBox5.Text;
                textBox1.Text = "";
                textBox5.Text = "";
                timer1.Stop();
                button3.Enabled = true;
                button2.Enabled = false;
                button1.Enabled = false;
                textBox3.BackColor = Color.White;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button6.Enabled = false;
            button5.Enabled = false;
            button3.Enabled = false;
            button2.Enabled = false;
            conn.ConnectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True";
            try
            {
                conn.Open();
            }
            catch
            {
                conn.Close();
                conn.Open();
            }
            number_row();
            fillgride();
            numericUpDown3.Enabled = false;
            numericUpDown2.Enabled = false;
            
        }
        void fillgride(string s = "select * from table1")
        {
            cmd1.CommandText = s;
            cmd1.Connection = conn;
            da.SelectCommand = cmd1;
            DataTable dtbl = new DataTable();
            da.Fill(dtbl);
            ds.Clear();
            da.Fill(ds, "T1");
            dataGridView1.DataBindings.Clear();
            dataGridView1.DataSource = dtbl;
            dataGridView1.DataBindings.Add("datasource", ds, "t1");
            textBox4.DataBindings.Clear();
            textBox4.DataBindings.Add("text", ds, "t1.question");
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text="",text1="";
            string newLine = Environment.NewLine;
            int random1,number=1;
            button2.Enabled = true;
            button1.Enabled = false;
            textBox3.BackColor = Color.White;
            textBox2.Text = words[rnd.Next(30)];
            textBox3.Text = Convert.ToString(t);
            timer1.Enabled = false;
            timer1.Start();
            textBox3.Visible = true;
            List<int> listNumbers = new List<int>();
            for (int i = 0; i < q; i++)
            {
                
                do
                {
                    random1= rnd.Next(dataGridView1.Rows.Count);
                } while (listNumbers.Contains(random1));
                int w;
                w = this.Width;
                if (w < 970)
                {
                    if (i < 6)
                    {
                        if (i == 0)
                            text = number + "." + dataGridView1.Rows[random1].Cells[1].Value.ToString();
                        else
                            text = text + newLine + newLine + number + "." + dataGridView1.Rows[random1].Cells[1].Value.ToString();
                    }

                    if (i >5)
                    {
                        if (i == 6)
                            text1 = text1 + number + "." + dataGridView1.Rows[random1].Cells[1].Value.ToString();
                        else
                            text1 = text1 + newLine + newLine + number + "." + dataGridView1.Rows[random1].Cells[1].Value.ToString();
                    }
                }
                else
                {
                    if (i < 9)
                    {
                        if (i == 0)
                            text = number + "." + dataGridView1.Rows[random1].Cells[1].Value.ToString();
                        else
                            text = text + newLine + newLine + number + "." + dataGridView1.Rows[random1].Cells[1].Value.ToString();
                    }

                    if (i >8)
                    {
                        if (i == 9)
                            text1 = text1 + number + "." + dataGridView1.Rows[random1].Cells[1].Value.ToString();
                        else
                            text1 = text1 + newLine + newLine + number + "." + dataGridView1.Rows[random1].Cells[1].Value.ToString();
                    }
                }
                
                listNumbers.Add(random1);
                number++;
            }
            textBox1.Text = text;
            textBox5.Text = text1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            textBox2.Text = words1;
            textBox1.Text = questions1;
            textBox5.Text = questions2;
            button2.Enabled = false;
            button3.Enabled = false;
            textBox3.BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button1.Enabled = true;
            button3.Enabled = false;
            textBox3.Visible = true;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox5.Text = "";
            timer1.Stop();
            textBox3.Text = Convert.ToString(t);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            t = Convert.ToInt16(((NumericUpDown)sender).Value);
            textBox3.Text = Convert.ToString(((NumericUpDown)sender).Value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (b2 == 1)
                alert = Convert.ToInt16(((NumericUpDown)sender).Value);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (b1 == 1)
                hidden = Convert.ToInt16(((NumericUpDown)sender).Value);
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            q = Convert.ToInt16(((NumericUpDown)sender).Value);
            int w;
            w = this.Width;
            if (dataGridView1.Rows.Count > 12 && w<970)
                 numericUpDown4.Maximum = 12;
            else if(dataGridView1.Rows.Count > 18 && w>970)
                 numericUpDown4.Maximum = 18;
            else
                 numericUpDown4.Maximum = dataGridView1.Rows.Count;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Boolean b = checkBox1.Checked;
            if (b == true)
            {
                numericUpDown2.Enabled = true;
                b2 = 1;
            }
            if (b == false)
            {
                numericUpDown2.Enabled = false;
                b2 = 0;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Boolean b = checkBox2.Checked;
            if (b == true)
            {
                numericUpDown3.Enabled = true;
                b1 = 1;
            }
            if (b == false)
            {
                numericUpDown3.Enabled = false;
                b1 = 0;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox4.Text = dataGridView1.CurrentRow.Cells["Question"].Value.ToString();
            button6.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.ReadOnly = false;
            button5.Enabled = true;
            button4.Enabled = false;
            textBox4.Text = "";
            button5.Focus();
            textBox4.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int num;
            string a;
            try
            {
                conn.Open();
            }
            catch
            {
                conn.Close();
                conn.Open();
            }
            button5.Enabled = false;
            button4.Enabled = true;
            textBox4.ReadOnly = true;
            SqlCommand c1 = new SqlCommand();
            num = dataGridView1.Rows.Count;
            a = textBox4.Text;
            if (textBox4.Text == "")
            {
                MessageBox.Show("Empty","Error");
            }
            else
            {
                c1.CommandText = "insert into table1 values(@a,@b)";
                    c1.Parameters.AddWithValue("@b", textBox4.Text);
                    c1.Parameters.AddWithValue("@a", num + 1);
                    c1.Connection = conn;
                    textBox4.Text = "";
                    c1.ExecuteNonQuery();
                    button4.Focus();
                    number_row();
                    fillgride();

                    
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Int32 id;
            conn.Open();
            id =Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            cmd1 = new SqlCommand("delete table1 where ID=@id", conn);
            cmd1.Parameters.AddWithValue("@id", id);
            cmd1.ExecuteNonQuery();
            button6.Enabled = false;
            number_row();
            fillgride();
        }
        void number_row(string s = "select * from questions")
        {
            fillgride();
            conn.Open();
            int max, a = 0;
            string sum;
            max = dataGridView1.Rows.Count;
            SqlCommand c1 = new SqlCommand();
            for (int i = 0; i < max; i++)
            {
                sum = dataGridView1.Rows[i].Cells[1].Value.ToString();
                a = i + 1;
                c1.CommandText = "UPDATE table1 SET id=@d where question=@b";
                c1.Parameters.AddWithValue("@d", a);
                c1.Parameters.AddWithValue("@b", sum);
                c1.Connection = conn;
                c1.ExecuteNonQuery();
                c1.Parameters.Clear();

            }
            
            
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            int w, h;
            w = this.Width;
            h = this.Height;
            tabControl1.Width = w-10;
            tabControl1.Height = h-30;
            groupBox1.Width = Convert.ToInt32((w / 1.301351351351351));
            groupBox1.Height = Convert.ToInt32((h / 1.36745406824147));
            button3.Left = Convert.ToInt32((w / 2) - (116));
            button3.Top = Convert.ToInt32(h - 120);
            button2.Left = Convert.ToInt32(button3.Left - ((161/2)+90));
            button2.Top = Convert.ToInt32(h - 120);
            button1.Left = Convert.ToInt32(button3.Left + ((161 / 2) + 160));
            button1.Top = Convert.ToInt32(h - 120);
            textBox1.Left = Convert.ToInt32(groupBox1.Width / 2);
            textBox1.Width = Convert.ToInt32((groupBox1.Width / 2) - 10);
            textBox5.Width = Convert.ToInt32((groupBox1.Width / 2) - 10);
            textBox1.Height = Convert.ToInt32((groupBox1.Height - 30));
            textBox5.Height = Convert.ToInt32((groupBox1.Height - 30));
            label1.Left = Convert.ToInt32(w-100);
            label2.Left = Convert.ToInt32(w - 100);
            label3.Left = Convert.ToInt32(w - 100);
            label4.Left = Convert.ToInt32(w - 100);
            label7.Left = Convert.ToInt32(w - 100);
            label6.Left = Convert.ToInt32(w - 100);
            numericUpDown1.Left=w-250;
            numericUpDown2.Left = w - 250;
            numericUpDown3.Left = w - 250;
            numericUpDown4.Left = w - 250;
            button7.Left = w - 250;
            button9.Left = w - 380;
            button8.Left = w - 250;
            button11.Left = w - 250;
            button10.Left = w - 380;
            checkBox1.Left = w - 280;
            checkBox2.Left = w - 280;
            dataGridView1.Width = w - 40;
            dataGridView1.Height = h - 130;
            
        }

        private void Form1_MaximizedBoundsChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            dlg.ShowDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tabPage1.BackColor = dlg.Color;
                tabPage2.BackColor = dlg.Color;
                tabPage3.BackColor = dlg.Color;
                MessageBox.Show("Color Changed");
            }

        }

        private void Form1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            try
            {
                conn.Close();
            }
            catch
            {

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
             try
            {
                conn.Close();
            }
            catch
            {

            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ColorDialog dlg1 = new ColorDialog();
                dlg1.ShowDialog();
                button1.BackColor = dlg1.Color;
                button2.BackColor = dlg1.Color;
                button3.BackColor = dlg1.Color;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ColorDialog dlg1 = new ColorDialog();
            dlg1.ShowDialog();
            button1.BackColor = dlg1.Color;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ColorDialog dlg1 = new ColorDialog();
            dlg1.ShowDialog();
            button3.BackColor = dlg1.Color;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ColorDialog dlg1 = new ColorDialog();
            dlg1.ShowDialog();
            button2.BackColor = dlg1.Color;
        }
        }
    
        
    }

