using RentalCarDesktop.ListCarsServiceReference;
using RentalCarWebServices.Models.Database.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentalCarDesktop
{
    public partial class List_Available_Cars : Form
    {
        List<ListCarsServiceReference.Car> cars;
        ListCarsServiceReference.ListCarsServiceSoapClient listCarsServiceSoap = new ListCarsServiceReference.ListCarsServiceSoapClient();
        public List_Available_Cars()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       
        private void List_Available_Cars_Load(object sender, EventArgs e)
        {
            if (cars == null)
            {
                label10.Text = "All Cars";
                DataTable cars = new DataTable();
                listCarsServiceSoap.Open();
                cars = listCarsServiceSoap.readAllInDataTable();
                dataGridView1.DataSource = cars;
            }
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cars = null;
            if (textBox1.Text != "" && textBox2.Text == "" && textBox5.Text == "")
            {
                if (validateCarPlate() && validateRentPeriod() && validateDate())
                {
                    cars = returnAvailableCars();
                }
            }
            else if (textBox1.Text == "" && textBox2.Text != "" && textBox5.Text == "")
            {
                if (validateCarModel())
                {
                    cars = cars = returnAvailableCars();
                }
            }
            else if (textBox1.Text == "" && textBox2.Text == "" && textBox5.Text != "")
            {
                if (validateCity())
                {
                    cars = cars = returnAvailableCars();
                }
            }
            else if (textBox1.Text == "" && textBox2.Text == "" && textBox5.Text == "")
            {
                cars = cars = returnAvailableCars();
            }
            else
            {
                MessageBox.Show(" Each search field is independent, and ONLY the Car Plate is linked to the Start/End Dates;\n" +
                            " You can search ONLY by a single criteria, or leave all criterias blank for returning all cars;\n");
                label6.Text = "";
                label7.Text = "";
                label8.Text = "";
                label9.Text = "";
            }

            if (cars != null)
            {
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = cars;
                dataGridView1.Show();
                label10.Text = "Available Cars based on selected criterias";
                label6.Text = "";
                label7.Text = "";
                label8.Text = "";
                label9.Text = "";
            }
        }

        private bool validateCarPlate()
        {
            return listCarsServiceSoap.validateCarPlate(textBox1.Text, label6.Text);
        }

        private bool validateCarModel()
        {
            return listCarsServiceSoap.validateCarModel(textBox2.Text, label7.Text);
        }

        private bool validateCity()
        {
            return listCarsServiceSoap.validateCity(textBox5.Text, label9.Text);
        }

        private bool validateDate()
        {
            return listCarsServiceSoap.validateDate(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, label8.Text);
        }

        private bool validateRentPeriod()
        {
            Reservation r = null;
            return listCarsServiceSoap.validateRentPeriod(textBox1.Text, label8.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, "INSERT", r);
        }

        private List<ListCarsServiceReference.Car> returnAvailableCars()
        {
            return listCarsServiceSoap.searchCars(textBox1.Text, textBox2.Text, textBox5.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date).ToList();
        }
    }
}
