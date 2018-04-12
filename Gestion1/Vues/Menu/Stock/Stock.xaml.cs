﻿using System;
using System.Collections.Generic;
using System.Data;
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

namespace Gestion1.Vues.Menu.Stock
{
    /// <summary>
    /// Logique d'interaction pour Stock.xaml
    /// </summary>
    public partial class Stock : Page
    {
        public Stock()
        {
            InitializeComponent();
            FillDataGrid();
            DatePickerStock.SelectedDate = DateTime.Today;
        }

        #region Chaine de connexion à la base de données SQL Server

        private const string
            ConString =
                "Server=den1.mssql6.gear.host;Database=stock4;Uid=stock4;Pwd=gestionstock*"; // Chaine de connexion à la base SQL Server

        #endregion

        private void DataGridStock_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            if (gd.SelectedItem is DataRowView rowSelected)
            {
                TextBoxCodeProduit.Text = rowSelected["CodeProduit"].ToString();
                TextBoxNomProduit.Text = rowSelected["NomProduit"].ToString();
                TextBoxQuantite.Text = rowSelected["Quantite"].ToString();
                TextBoxPrixHt.Text = rowSelected["PrixHt"].ToString();
                ComboBoxEtat.Text = rowSelected["Etat"].ToString();
                DatePickerStock.Text = rowSelected["DateAjout"].ToString();
            }
        }

        private void FillDataGrid()
        {
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString =
                    "SELECT CodeProduit, NomProduit,Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits"; // Requête de récupération des éléments de la table Produits
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt); // Remplissage du SQL Data Adapter par la table Produits
                DataGridStock.ItemsSource = dt.DefaultView; // Choix du type de vue sur l'interface graphique
                int i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de produits
                if (i > 1)
                {
                    TextBlockTotal.Text = "" + i.ToString() + " produits trouvés";
                }
                if (i < 1)
                {
                    TextBlockTotal.Text = "" + i.ToString() + " produit trouvé";
                }
                if (i == 0)
                {
                    TextBlockTotal.Text = "Aucun produit n'a pu être trouvé";
                }
            }
        }

        private void ButtonAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerStock.Text != "" & TextBoxCodeProduit.Text != "" & TextBoxNomProduit.Text != "" &
                TextBoxQuantite.Text != "" &
                ComboBoxEtat.Text != "") // Si les champs ne sont pas vides, la création est impossible
            {
                SqlConnection connection = new SqlConnection(ConString);
                connection.Open(); // Ouvertue de la connexion

                SqlCommand cmdSqlCommand =
                    new SqlCommand(
                        "INSERT INTO dbo.Produits(CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout) Values(@CodeProduit, @NomProduit, @Categorie, @Quantite, @PrixHt, @Etat, @DateAjout)",
                        connection); // Requête d'insertion d'un nouveau client

                cmdSqlCommand.Parameters.AddWithValue("@CodeProduit", TextBoxCodeProduit.Text); // Paramètre du Nom du client
                cmdSqlCommand.Parameters.AddWithValue("@NomProduit", TextBoxNomProduit.Text); // Paramètre du Prenom du client
                cmdSqlCommand.Parameters.AddWithValue("@Categorie", ComboBoxCategorie.Text); // Paramètre du Prenom du client
                cmdSqlCommand.Parameters.AddWithValue("@Quantite", TextBoxQuantite.Text); // Paramètre de la Societe du client
                cmdSqlCommand.Parameters.AddWithValue("@PrixHt", TextBoxPrixHt.Text); // Paramètre de la Societe du client
                cmdSqlCommand.Parameters.AddWithValue("@Etat", ComboBoxEtat.Text); // Paramètre du numéro de Telephone du client
                cmdSqlCommand.Parameters.AddWithValue("@DateAjout", DatePickerStock.Text); // Paramètre de l'Email du client
                cmdSqlCommand.ExecuteNonQuery(); // Execution de la requête
                MessageBox.Show("Le produit " + TextBoxNomProduit.Text + " a été ajouté"); // Affichage du message après execution de la requête
                FillDataGrid(); // Recharge la table Clients
                TextBoxCodeProduit.Clear(); // Vide le champs Nom
                TextBoxNomProduit.Clear(); // Vide le champs Prenom
                TextBoxQuantite.Clear(); // Vide le champs Societe
                TextBoxPrixHt.Clear(); // Vide le champs Prix HT
                connection.Close(); // Fermeture de la connexion
            }
        }

        private void ButtonSupprimer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        #region Barre de recherche
        private void TextBoxRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            #region Remise à zéro de la recherche
            if (TextBoxRecherche.Text == "")
            {
                ComboBoxCategorie.Text = "";
                FillDataGrid();
            }
            #endregion

            #region Recherche par CodeProduit
            if (ComboBoxCategorie.Text == "")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE CodeProduit LIKE '%" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }

            else
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE CodeProduit LIKE '%" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }

            #endregion

            #region Recherche par NomProduit
            if (ComboBoxCategorie.Text == "")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE NomProduit LIKE '%" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }

            else
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE NomProduit LIKE '%" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }

            #endregion

            #region Disque dur et SSD
            if (ComboBoxCategorie.Text == "Disque dur et SSD")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE Categorie='HDD' OR Categorie='SSHD' OR Categorie='SSD' AND NomProduit LIKE '" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;

            }

            if (ComboBoxCategorie.Text == "Disque dur et SSD")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE Categorie='HDD' OR Categorie='SSHD' OR Categorie='SSD' AND CodeProduit LIKE '" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }

            #endregion

            #region Carte graphique
            else if (ComboBoxCategorie.Text == "Carte graphique")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE Categorie = 'GPU' AND NomProduit LIKE '" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }

            else if (ComboBoxCategorie.Text == "Carte graphique")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE Categorie = 'GPU' AND CodeProduit LIKE '" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }


            #endregion

            #region Processeur
            else if (ComboBoxCategorie.Text == "Processeur")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE Categorie ='CPU' AND NomProduit LIKE " +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }

            else if (ComboBoxCategorie.Text == "Processeur")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT Id, CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE Categorie='CPU' AND CodeProduit LIKE " +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }
            #endregion

            #region Carte mère
            else if (ComboBoxCategorie.Text == "Carte mère")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT Id, CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE Categorie='MoBo' AND CodeProduit LIKE '%" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }

            else if (ComboBoxCategorie.Text == "Carte mère")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT Id, CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE Categorie='MoBo' AND CodeProduit LIKE '%" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }

            #endregion

            #region Barrette mémoire
            else if (ComboBoxCategorie.Text == "Barrette mémoire")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT Id, CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE Categorie='RAM' AND CodeProduit LIKE '%" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }

            else if (ComboBoxCategorie.Text == "Barrette mémoire")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT Id, CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE Categorie='RAM' AND NomProduit LIKE '%" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }

            #endregion

            #region Alimentation
            else if (ComboBoxCategorie.Text == "Alimentation")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT Id, CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE Categorie='ALIM' AND CodeProduit LIKE '%" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }

            else if (ComboBoxCategorie.Text == "Alimentation")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT Id, CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE Categorie='ALIM' AND NomProduit LIKE '%" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }

            #endregion

            #region Accessoires
            else if (ComboBoxCategorie.Text == "Accessoires")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT Id, CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE Categorie='ACC' AND CodeProduit LIKE '%" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }

            else if (ComboBoxCategorie.Text == "Accessoires")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT Id, CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE Categorie='ACC' AND NomProduit LIKE '%" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt);
                DataGridStock.ItemsSource = dt.DefaultView;
            }

            #endregion
        }
        #endregion
    }
}
