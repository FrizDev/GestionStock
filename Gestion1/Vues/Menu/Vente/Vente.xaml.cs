using System;
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

namespace Gestion1.Vues.Menu.Vente
{
    /// <summary>
    /// Logique d'interaction pour Vente.xaml
    /// </summary>
    public partial class Vente : Page
    {
        public Vente()
        {
            InitializeComponent();
            FillDataGrid();
            DatePickerFacture.DisplayDate = DateTime.Today;
        }

        #region Chaine de connexion à la base de données SQL Server

        private const string
            ConString =
                "Server=den1.mssql6.gear.host;Database=stock4;Uid=stock4;Pwd=gestionstock*"; // Chaine de connexion à la base SQL Server

        #endregion

        private void FillDataGrid()
        {
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString =
                    "SELECT Societe, Prenom, Nom, Telephone, Email FROM dbo.Clients"; // Requête de récupération des éléments de la table Produits
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Clients");
                sda.Fill(dt); // Remplissage du SQL Data Adapter par la table Produits
                DataGridClient.ItemsSource = dt.DefaultView; // Choix du type de vue sur l'interface graphique
                int i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de client
                if (i > 1)
                {
                    TextBlockTotal.Text = i.ToString() + " clients trouvés";
                }
                if (i == 1)
                {
                    TextBlockTotal.Text = i.ToString() + " client trouvé";
                }
                if (i == 0)
                {
                    TextBlockTotal.Text = "Aucun client n'a été trouvé";
                }
            }
        }

        private void TextBoxRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            #region Remise à zéro de la recherche
            if (TextBoxRecherche.Text == "")
            {
                ComboBoxRechercheCategorie.SelectedIndex = -1;
                FillDataGrid();
            }
            #endregion

            #region Recherche avec filtre ComboBox

            switch (ComboBoxRechercheCategorie.Text)
            {
                case "Nom":
                    {
                        SqlDataAdapter sda =
                            new SqlDataAdapter(
                                "SELECT Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients WHERE Nom LIKE '" +
                                TextBoxRecherche.Text + "%'", ConString);
                        DataTable dt = new DataTable("Clients");
                        sda.Fill(dt);
                        DataGridClient.ItemsSource = dt.DefaultView;
                        int i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de client
                        if (i > 1)
                        {
                            TextBlockTotal.Text = i.ToString() + " clients trouvés";
                        }
                        if (i == 1)
                        {
                            TextBlockTotal.Text = i.ToString() + " client trouvé";
                        }
                        if (i == 0)
                        {
                            TextBlockTotal.Text = "Aucun client n'a été trouvé";
                        }
                        break;
                    }
                case "Prenom":
                    {
                        SqlDataAdapter sda =
                            new SqlDataAdapter(
                                "SELECT Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients WHERE Prenom LIKE '" +
                                TextBoxRecherche.Text + "%'", ConString);
                        DataTable dt = new DataTable("Clients");
                        sda.Fill(dt);
                        DataGridClient.ItemsSource = dt.DefaultView;
                        int i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de client
                        if (i > 1)
                        {
                            TextBlockTotal.Text = i.ToString() + " clients trouvés";
                        }
                        if (i == 1)
                        {
                            TextBlockTotal.Text = i.ToString() + " client trouvé";
                        }
                        if (i == 0)
                        {
                            TextBlockTotal.Text = "Aucun client n'a été trouvé";
                        }

                        break;
                    }

                case "Societe":
                    {
                        SqlDataAdapter sda =
                            new SqlDataAdapter(
                                "SELECT Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients WHERE Societe LIKE '" +
                                TextBoxRecherche.Text + "%'", ConString);
                        DataTable dt = new DataTable("Clients");
                        sda.Fill(dt);
                        DataGridClient.ItemsSource = dt.DefaultView;
                        int i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de client
                        if (i > 1)
                        {
                            TextBlockTotal.Text = i.ToString() + " clients trouvés";
                        }
                        if (i == 1)
                        {
                            TextBlockTotal.Text = i.ToString() + " client trouvé";
                        }
                        if (i == 0)
                        {
                            TextBlockTotal.Text = "Aucun client n'a été trouvé";
                        }

                        break;
                    }

                case "Telephone":
                    {
                        SqlDataAdapter sda =
                            new SqlDataAdapter(
                                "SELECT Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients WHERE Telephone LIKE '" +
                                TextBoxRecherche.Text + "%'", ConString);
                        DataTable dt = new DataTable("Clients");
                        sda.Fill(dt);
                        DataGridClient.ItemsSource = dt.DefaultView;
                        int i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de client
                        if (i > 1)
                        {
                            TextBlockTotal.Text = i.ToString() + " clients trouvés";
                        }
                        if (i == 1)
                        {
                            TextBlockTotal.Text = i.ToString() + " client trouvé";
                        }
                        if (i == 0)
                        {
                            TextBlockTotal.Text = "Aucun client n'a été trouvé";
                        }

                        break;
                    }

                case "Email":
                    {
                        SqlDataAdapter sda =
                            new SqlDataAdapter(
                                "SELECT Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients WHERE Email LIKE '" +
                                TextBoxRecherche.Text + "%'", ConString);
                        DataTable dt = new DataTable("Clients");
                        sda.Fill(dt);
                        DataGridClient.ItemsSource = dt.DefaultView;
                        int i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de client
                        if (i > 1)
                        {
                            TextBlockTotal.Text = i.ToString() + " clients trouvés";
                        }
                        if (i == 1)
                        {
                            TextBlockTotal.Text = i.ToString() + " client trouvé";
                        }
                        if (i == 0)
                        {
                            TextBlockTotal.Text = "Aucun client n'a été trouvé";
                        }

                        break;
                    }
            }

            #endregion
        }

        private void DatagridClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            if (gd.SelectedItem is DataRowView rowSelected)
            {
                TextBoxClientNom.Text = rowSelected["Nom"].ToString();
                TextBoxClientPrenom.Text = rowSelected["Prenom"].ToString();
                TextBoxClientSociété.Text = rowSelected["Societe"].ToString();
                TextBoxClientTelephone.Text = rowSelected["Telephone"].ToString();
                TextBoxClientEmail.Text = rowSelected["Email"].ToString();
            }
        }
    }
}
