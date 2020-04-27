using RentalCarDesktop.ListCarServiceReference;
using RentalCarWebServices.Models.Database.DAO;
//using RentalCarWebServices.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentalCarDesktop
{
    public partial class List_Available_Cars : Form
    {
        List<ListCarServiceReference.Car> cars;
        ListCarServiceReference.ListCarsServiceSoapClient listCarsServiceSoap = new ListCarServiceReference.ListCarsServiceSoapClient();
        
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
                try
                {
                    cars = listCarsServiceSoap.readAllInDataTable();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                }               
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
                    cars = returnAvailableCars();
                }
            }
            else if (textBox1.Text == "" && textBox2.Text == "" && textBox5.Text != "")
            {
                if (validateCity())
                {
                    cars = returnAvailableCars();
                }
            }
            else if (textBox1.Text == "" && textBox2.Text == "" && textBox5.Text == "")
            {
                cars = returnAvailableCars();
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
                label10.Text = "Available Cars based on selected criterias -> ONLY the Car Plate is linked to Start / End Dates";
                label6.Text = "";
                label7.Text = "";
                label8.Text = "";
                label9.Text = "";
            }
        }
        
        private bool validateCarPlate()
        {
            bool plate = listCarsServiceSoap.validateCarPlate(textBox1.Text);
            if (!plate)
            {
                if (!Regex.IsMatch(textBox1.Text, "[A-Z]{2} [0-9]{2} [A-Z]{3}"))
                {
                    label6.Text = "Invalid input type, the car plate format should be: ZZ 00 ZZZ";
                }
                else
                {
                    label6.Text = "This car plate does not exist, please enter another plate";
                }
            }
            return plate;
        }

        private bool validateCarModel()
        {
            bool model = listCarsServiceSoap.validateCarModel(textBox2.Text);
            if (!model)
            {
                label7.Text = "This model does not exist, please enter another model";
            }
            return model;
        }

        private bool validateCity()
        {
            bool city = listCarsServiceSoap.validateCity(textBox5.Text);
            if (!city)
            {
                label9.Text = "This location does not exist, please enter another location";
            }
            return city;
        }

        private bool validateDate()
        {
            bool date = listCarsServiceSoap.validateDate(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
            if (!date)
            {
                label8.Text = "End Date should be equal or higher than Start Date";
            }
            return date;
        }

        private bool validateRentPeriod()
        {
            //this reservation is just for testing, for not having a null one when the INSERT condtion is not met because the reservation status is <> 1 (when searching db records)
            Reservation r = new Reservation(0, "", 0, 1, DateTime.Now, DateTime.Now, "", "");
            bool period = listCarsServiceSoap.validateRentPeriod(textBox1.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, "INSERT", r);
            if (!period)
            {
                label8.Text = "The selected car was already rented in this period";
            }
            return period;
        }

        private List<ListCarServiceReference.Car> returnAvailableCars()
        {
            List<ListCarServiceReference.Car> carsFound = new List<ListCarServiceReference.Car>();
            try
            {
                carsFound = listCarsServiceSoap.searchCars(textBox1.Text, textBox2.Text, textBox5.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date).ToList();
                return carsFound;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL error: " + ex.Message);
                return carsFound;
            }
            
        }
    }
}
