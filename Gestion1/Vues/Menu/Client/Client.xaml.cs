﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Gestion1.Vues.Menu.Client
{
    /// <summary>
    /// Logique d'interaction pour Client.xaml
    /// </summary>
    public partial class Client : Page
    {
        public Client()
        {
            InitializeComponent();
            FillDataGrid();
        }

        #region Chaine de connexion à la base de données SQL Server

        private const string ConString = "Server=den1.mssql6.gear.host;Database=stock4;Uid=stock4;Pwd=gestionstock*"; // Chaine de connexion à la base SQL Server

        #endregion

        #region Bouton Gestion du tableau Client

        private void ButtonAjouter_OnClick(object sender, RoutedEventArgs e)
        {
            if (TextBoxNom.Text != "" & TextBoxPrenom.Text != "" & TextBoxSociete.Text != "" &
                TextBoxTelephone.Text != "" & TextBoxEmail.Text != "") // Si les champs ne sont pas vides, la création est impossible
            {
                SqlConnection connection = new SqlConnection(ConString);
                connection.Open(); // Ouvertue de la connexion

                SqlCommand cmdSqlCommand = new SqlCommand("INSERT INTO dbo.Clients(Nom,Prenom,Societe,Telephone,Email) Values(@Nom, @Prenom, @Societe, @Telephone, @Email)", connection); // Requête d'insertion d'un nouveau client

                cmdSqlCommand.Parameters.AddWithValue("@Nom", TextBoxNom.Text); // Paramètre du Nom du client
                cmdSqlCommand.Parameters.AddWithValue("@Prenom", TextBoxPrenom.Text); // Paramètre du Prenom du client
                cmdSqlCommand.Parameters.AddWithValue("@Societe", TextBoxSociete.Text); // Paramètre de la Societe du client
                cmdSqlCommand.Parameters.AddWithValue("@Telephone", TextBoxTelephone.Text); // Paramètre du numéro de Telephone du client
                cmdSqlCommand.Parameters.AddWithValue("@Email", TextBoxEmail.Text); // Paramètre de l'Email du client
                cmdSqlCommand.ExecuteNonQuery(); // Execution de la requête
                MessageBox.Show(""); // Affichage du message après execution de la requête
                connection.Close(); // Fermeture de la connexion
            }
        }

        private void ButtonEffacer_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonModifier_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Remplissage tableau Client

        private void FillDataGrid()
        {
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients"; // Requête de récupération des éléments de la table Clients
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Clients");
                sda.Fill(dt); // Remplissage du SQL Data Adapter par la table Clients
                DataGridClient.ItemsSource = dt.DefaultView; // Choix du type de vue sur l'interface graphique
            }
        }
        #endregion
    }
}
