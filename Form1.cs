using model.ModelBD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace model
{
    public partial class Form1 : Form
    {
        ModelBD.Model1 connect = new ModelBD.Model1();
        public Form1()
        {
            InitializeComponent();
            connect.Client.Load();
            dataGridView1.DataSource = connect.Client.Local.ToBindingList();

        }

        private void addbd_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            DialogResult result = form.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                Client clientadd = new Client();
                clientadd.FirstName = form.textBox1.Text;
                clientadd.LastName = form.textBox2.Text;
                clientadd.Phone = form.textBox3.Text;
                clientadd.GenderCode = form.comboBox1.Text;

                connect.Client.Add(clientadd);
                connect.SaveChanges();
                MessageBox.Show("add");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == true)
                {
                    Client Clientdel = connect.Client.Find(id);
                    connect.Client.Remove(Clientdel);
                    connect.SaveChanges();
                    string buff = Clientdel.FirstName;
                    MessageBox.Show("Запись " + buff + " удалена");


                }




            }

            else

            {
                MessageBox.Show(" Не выбрана запись");
            }

        }

        private void edit_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = index;
                bool convert = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);



                Client clientedit = connect.Client.Find(id);

                form2.textBox1.Text = clientedit.FirstName;
                form2.textBox2.Text = clientedit.LastName;
                form2.textBox3.Text = clientedit.Phone;
                form2.comboBox1.Text = clientedit.GenderCode;

                DialogResult resultedit = form2.ShowDialog(this);
                if (resultedit == DialogResult.OK)

                {
                    clientedit.FirstName = form2.textBox1.Text;
                    clientedit.LastName = form2.textBox2.Text;
                    clientedit.Phone = form2.textBox3.Text;
                    clientedit.GenderCode = form2.comboBox1.SelectedIndex.ToString();


                    connect.SaveChanges();
                    dataGridView1.Refresh();
                    MessageBox.Show("Объект обновлён");

                }
            }
        }
    }
}
