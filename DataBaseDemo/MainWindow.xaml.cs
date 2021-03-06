﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBaseDemo {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e) {
            if (chkGetCitizens.IsChecked == true) {
                List<Citizen> cits = DAL.GetAllCitizens();
                foreach (Citizen sup in cits) {
                    lbSupers.Items.Add(sup);
                }
            } else {
                List<SuperHero> supers = DAL.GetAllSuperHeroes();
                foreach (SuperHero sup in supers) {
                    lbSupers.Items.Add(sup);
                }
                //tbOutput.Text = "Error: " + ex.Message;
            }
        }

        private void btnGetByID_Click(object sender, RoutedEventArgs e) {
            int id = int.Parse(txtSuperID.Text);
            SuperHero superPerson = DAL.GetSuperHero(id);
            MessageBox.Show(superPerson.ToString());
             txtFirstName.Text= superPerson.FirstName;
            txtLastName.Text = superPerson.LastName;
            txtHeight.Text = superPerson.Height.ToString();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e) {
            int id = int.Parse(txtSuperID.Text);
            //SuperHero superPerson = new SuperHero();//DAL.GetSuperHero(id);
            SuperHero superPerson = DAL.GetSuperHero(id);

            superPerson.ID = id;
            superPerson.FirstName = txtFirstName.Text;
            superPerson.LastName = txtLastName.Text;
            superPerson.Height = decimal.Parse(txtHeight.Text);

            DAL.UpdateSuperHero(superPerson);

        }

        private void lbSupers_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ListBox lst = (ListBox)sender;
            object picked = lst.SelectedItem;

            //SuperHero pickSup = (SuperHero)picked;
            //Citizen cit = DAL.GetCitizen(pickSup.AlterEgoID);
            //string whatToShow = string.Format(
            //    "Name: {0} \r\nHeight: {1} \r\nAlter Ego: {2}",
            //    pickSup.ToString(), pickSup.Height,cit.ToString());

            SuperHero pickSup = (SuperHero)picked;
            ////pickSup.AlterEgo = new Citizen();
            //string whatToShow = string.Format(
            //    "Name: {0} \r\nHeight: {1} \r\nAlter Ego: {2} {3}",
            //    pickSup.ToString(), pickSup.Height, 
            //    pickSup.AlterEgo.FirstName, pickSup.AlterEgo.LastName);

            //MessageBox.Show(whatToShow);
            foreach (SuperHero frd in pickSup.Friends){
                lbSupers.Items.Add(frd);
            }


        }

        //private void btnLoad_Click(object sender, RoutedEventArgs e) {
        //    // Get data from database
        //    SqlConnection conn = null;

        //    try {
        //        // Create Connection
        //        conn = new SqlConnection();
        //        //conn.ConnectionString = "Server=localhost;Database=SuperHeroes;User Id=holmjona;Password=password;";
        //        conn.ConnectionString = "Data Source=localhost;Initial Catalog=SuperHeroes;Integrated Security=True";

        //        // Open Connection
        //        conn.Open();
        //        //tbOutput.Text = "I connected";

        //        // Create Command
        //        SqlCommand comm = new SqlCommand();
        //        comm.Connection = conn;
        //        comm.CommandType = System.Data.CommandType.Text;
        //        comm.CommandText = "SELECT * FROM SuperHeroes";

        //        // Execute Command
        //        SqlDataReader dr = comm.ExecuteReader();

        //        // Read Results

        //        // FIll from Database
        //        while (dr.Read()) {
        //            SuperHero sup = new SuperHero();
        //            sup.ID = (int)dr["SuperHeroID"];
        //            sup.FirstName = (string)dr["FirstName"];
        //            sup.LastName = (string)dr["LastName"];
        //            sup.Height = (decimal)dr["HeightInInches"];
        //            lbSupers.Items.Add(sup);
        //        }



        //    } catch (Exception ex) {
        //        tbOutput.Text = "Error: " + ex.Message;
        //    } finally {
        //        // Close Connection
        //        if(conn != null) conn.Close();
        //    }
        //}
    }
}
