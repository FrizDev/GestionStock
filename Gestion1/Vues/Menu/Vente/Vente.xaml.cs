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
                DataGridClient.ItemsSource = dt.DefaultView; // Choix du type de vue sur l'interface graphique*
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

        private void FillVenteGrid()
        {
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString =
                    "SELECT NumeroFacture, Description, DateFacture, NomProduit, CodeProduit, Quantite, PrixUnitaire, Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients WHERE NumeroFacture=@NumeroFacture"; // Requête de récupération des éléments de la table Produits
                SqlCommand cmd = new SqlCommand(CmdString, con);
                cmd.Parameters.AddWithValue("@NumeroFacture", TextBoxNumeroFacture.Text); // Paramètre du Nom du client
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Factures");
                sda.Fill(dt); // Remplissage du SQL Data Adapter par la table Produits
                DataGridFacture.ItemsSource = dt.DefaultView; // Choix du type de vue sur l'interface graphique*
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

        private void DataGridClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void ButtonAjouterFacture_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFacture.Text != "" & TextBoxNumeroFacture.Text != "" & TextBoxDescription.Text != "" &
                TextBoxClientNom.Text != "" & TextBoxClientPrenom.Text != "" & TextBoxClientSociété.Text != "" &
                TextBoxClientTelephone.Text != "" & TextBoxClientEmail.Text != "" & ComboBoxContenuCodeProduit.Text != "" &
                ComboBoxContenuNomProduit.Text != "" & ComboBoxContenuQuantite.Text != "" & TextBoxContenuPrixnitaire.Text != "") // Si les champs ne sont pas vides, la création est impossible
            {
                SqlConnection connection = new SqlConnection(ConString);
                connection.Open(); // Ouvertue de la connexion

                SqlCommand cmdSqlCommand =
                    new SqlCommand(
                        "INSERT INTO dbo.Factures(NumeroFacture, Description, DateFacture, NomProduit, CodeProduit, Quantite, PrixUnitaire, Nom, Prenom, Societe, Telephone, Email)" +
                        " Values(@NumeroFacture, @Description, @DateFacture, @NomProduit, @CodeProduit, @Quantite, @PrixUnitaire, @Nom, @Prenom, @Societe, @Telephone, @Email)",
                        connection); // Requête d'insertion d'un nouveau client

                cmdSqlCommand.Parameters.AddWithValue("@NumeroFacture", TextBoxNumeroFacture.Text); // Paramètre du Nom du client
                cmdSqlCommand.Parameters.AddWithValue("@Description", TextBoxDescription.Text); // Paramètre du Prenom du client
                cmdSqlCommand.Parameters.AddWithValue("@DateFacture", Convert.ToDateTime(DatePickerFacture.Text).ToString("yyyy-MM-dd")); // Paramètre du Prenom du client
                cmdSqlCommand.Parameters.AddWithValue("@NomProduit", ComboBoxContenuNomProduit.Text); // Paramètre de la Societe du client
                cmdSqlCommand.Parameters.AddWithValue("@CodeProduit", ComboBoxContenuCodeProduit.Text); // Paramètre de la Societe du client
                cmdSqlCommand.Parameters.AddWithValue("@Quantite", ComboBoxContenuQuantite.Text); // Paramètre du numéro de Telephone du client
                cmdSqlCommand.Parameters.AddWithValue("@PrixUnitaire", TextBoxContenuPrixnitaire); // Paramètre de l'Email du client
                cmdSqlCommand.Parameters.AddWithValue("@Nom", TextBoxClientPrenom); // Paramètre de l'Email du client
                cmdSqlCommand.Parameters.AddWithValue("@Prenom", TextBoxClientPrenom); // Paramètre de l'Email du client
                cmdSqlCommand.Parameters.AddWithValue("@Societe", TextBoxClientSociété); // Paramètre de l'Email du client
                cmdSqlCommand.Parameters.AddWithValue("@Telephone", TextBoxClientTelephone); // Paramètre de l'Email du client
                cmdSqlCommand.Parameters.AddWithValue("@Email", TextBoxClientEmail); // Paramètre de l'Email du client
                cmdSqlCommand.ExecuteNonQuery(); // Execution de la requête
                MessageBox.Show("Le produit " + ComboBoxContenuNomProduit.Text + " a été ajouté à la facture " + TextBoxNumeroFacture); // Affichage du message après execution de la requête
                FillDataGrid(); // Recharge la table Clients
                connection.Close(); // Fermeture de la connexion
            }
        }
    }
}
