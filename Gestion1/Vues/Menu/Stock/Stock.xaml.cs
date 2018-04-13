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
                switch (rowSelected["Categorie"].ToString())
                {
                    case "HDD":
                        ComboBoxCategorie.SelectedIndex = 0;
                        break;
                    case "SSHD":
                        ComboBoxCategorie.SelectedIndex = 0;
                        break;
                    case "SSD":
                        ComboBoxCategorie.SelectedIndex = 0;
                        break;
                    case "GPU":
                        ComboBoxCategorie.SelectedIndex = 1;
                        break;
                    case "CPU":
                        ComboBoxCategorie.SelectedIndex = 2;
                        break;
                    case "MoBo":
                        ComboBoxCategorie.SelectedIndex = 3;
                        break;
                    case "RAM":
                        ComboBoxCategorie.SelectedIndex = 4;
                        break;
                    case "ALIM":
                        ComboBoxCategorie.SelectedIndex = 5;
                        break;
                    case "ACC":
                        ComboBoxCategorie.SelectedIndex = 6;
                        break;
                }
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
                TextBoxQuantite.Text != "" & ComboBoxEtat.Text != "" & ComboBoxCategorie.Text != "") // Si les champs ne sont pas vides, la création est impossible
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
                cmdSqlCommand.Parameters.AddWithValue("@DateAjout", Convert.ToDateTime(DatePickerStock.Text).ToString("yyyy-MM-dd")); // Paramètre de l'Email du client
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
            if (DatePickerStock.Text != "" & TextBoxCodeProduit.Text != "" & TextBoxNomProduit.Text != "" &
                TextBoxQuantite.Text != "" & ComboBoxEtat.Text != "" & ComboBoxCategorie.Text != "") // Si les champs ne sont pas vides, la suppression est impossible
            {
                SqlConnection connection = new SqlConnection(ConString);
                connection.Open(); // Ouvertue de la connexion

                SqlCommand cmdSqlCommand =
                    new SqlCommand(
                        "DELETE FROM dbo.Produits WHERE CodeProduit=@CodeProduit",
                        connection); // Requête de suppression du client
                cmdSqlCommand.Parameters.AddWithValue("@CodeProduit", TextBoxCodeProduit.Text); // Paramètre du CodeProduit
                if (MessageBox.Show("Oui ou Non", "Êtes vous sûr de vouloir supprimer ce produit ?", MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes) // Boite de dialogue demandant la confirmation pour la suppression du produit
                {
                    cmdSqlCommand.ExecuteNonQuery(); // Execution de la requête
                    MessageBox.Show("Le produit " + TextBoxNomProduit.Text +" a été supprimé"); // Affichage du message après execution de la requête
                }
                FillDataGrid(); // Recharge la table Produit
                TextBoxCodeProduit.Clear(); // Vide le champs Code Produit
                TextBoxNomProduit.Clear(); // Vide le champs Nom Produit
                TextBoxPrixHt.Clear(); // Vide le champs Prix HT
                TextBoxQuantite.Clear(); // Vide le champs Quantite
                ComboBoxEtat.Text = ""; // Vide la ComboBox Etat
                DatePickerStock.SelectedDate = DateTime.Today; ; // Remet la date du jour
                TextBoxRecherche.Text = "";
                connection.Close(); // Fermeture de la connexion
            }
        }

        private void ButtonModifier_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerStock.Text != "" & TextBoxCodeProduit.Text != "" & TextBoxNomProduit.Text != "" &
                TextBoxQuantite.Text != "" & ComboBoxEtat.Text != "" & ComboBoxCategorie.Text != "") // Si les champs sont pas vides, la modification est impossible
            {
                SqlConnection connection = new SqlConnection(ConString);
                connection.Open(); // Ouvertue de la connexion
                SqlCommand cmdSqlCommand =
                    new SqlCommand(
                        "UPDATE dbo.Produits SET NomProduit=@NomProduit, Categorie=@Categorie, PrixHt=@PrixHt, Quantite=@Quantite, Etat=@Etat, DateAjout=@DateAjout WHERE CodeProduit=@CodeProduit",
                        connection); // Requête de suppression du client
                cmdSqlCommand.Parameters.AddWithValue("@NomProduit", TextBoxNomProduit.Text); // Paramètre du Nom du produit
                cmdSqlCommand.Parameters.AddWithValue("@Categorie", ComboBoxCategorie.Text); // Paramètre de la Catégorie du produit
                cmdSqlCommand.Parameters.AddWithValue("@PrixHt", TextBoxPrixHt.Text); // Paramètre du Prix HT du produit
                cmdSqlCommand.Parameters.AddWithValue("@Quantite", TextBoxQuantite.Text); // Paramètre du Prix HT du produit
                cmdSqlCommand.Parameters.AddWithValue("@Etat", ComboBoxEtat.Text); // Paramètre de l'Etat du produit
                cmdSqlCommand.Parameters.AddWithValue("@DateAjout", Convert.ToDateTime(DatePickerStock.Text).ToString("yyyy-MM-dd")); // Paramètre de la Date d'ajout du produit
                cmdSqlCommand.Parameters.AddWithValue("@CodeProduit", TextBoxCodeProduit.Text); // Paramètre du Code du produit
                cmdSqlCommand.ExecuteNonQuery(); // Execution de la requête
                int rows = cmdSqlCommand.ExecuteNonQuery(); // Variable qui stocke le nombre de requêtes effectuées
                if (rows > 1)
                {
                    MessageBox.Show(rows + " requêtes ont bien été effectuées"); // Affichage du message après execution de la requête (dans le cas où il y en a plusieurs)
                }
                else
                {
                    MessageBox.Show(rows + " requête a bien été effectuée"); // Affichage du message après execution de la requête (dans le cas où il n'y en a qu'une seule)
                }
                FillDataGrid(); // Recharge la table Produit
                TextBoxCodeProduit.Clear(); // Vide le champs Code Produit
                TextBoxNomProduit.Clear(); // Vide le champs Nom Produit
                TextBoxPrixHt.Clear(); // Vide le champs Prix HT
                TextBoxQuantite.Clear(); // Vide le champs Quantite
                ComboBoxEtat.Text = ""; // Vide la ComboBox Etat
                DatePickerStock.SelectedDate = DateTime.Today; ; // Remet la date du jour
                TextBoxRecherche.Text = "";
                connection.Close(); // Fermeture de la connexion
            }
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
                int i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de produits
                if (i > 1)
                {
                    TextBlockTotal.Text = i.ToString() + " produits trouvés";
                }
                if (i == 1)
                {
                    TextBlockTotal.Text = i.ToString() + " produit trouvé";
                }
                if (i == 0)
                {
                    TextBlockTotal.Text = "Aucun produit n'a pu être trouvé";
                }
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

            #endregion

            switch (ComboBoxCategorie.Text)
            {
                case "Disque dur et SSD":
                {
                    SqlDataAdapter sda =
                        new SqlDataAdapter(
                            "SELECT CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE (Categorie='HDD' OR Categorie='SSHD' OR Categorie='SSD') AND NomProduit LIKE'" +
                            TextBoxRecherche.Text + "%'", ConString);
                    DataTable dt = new DataTable("Produits");
                    sda.Fill(dt);
                    DataGridStock.ItemsSource = dt.DefaultView;
                    int i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de Disque dur et SSD
                    if (i > 1)
                    {
                        TextBlockTotal.Text = i.ToString() + " disques durs ou SSD trouvés";
                    }

                    if (i == 1)
                    {
                        TextBlockTotal.Text = i.ToString() + " disque dur ou SSD trouvé";
                    }

                    if (i == 0)
                    {
                        TextBlockTotal.Text = "Aucun disque dur ou SSD n'a été trouvé";
                    }

                    break;
                }
                case "Carte graphique":
                {
                    SqlDataAdapter sda =
                        new SqlDataAdapter(
                            "SELECT CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE (Categorie='GPU') AND NomProduit LIKE '%" +
                            TextBoxRecherche.Text + "%'", ConString);
                    DataTable dt = new DataTable("Produits");
                    sda.Fill(dt);
                    DataGridStock.ItemsSource = dt.DefaultView;
                    int i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de carte graphique
                    if (i > 1)
                    {
                        TextBlockTotal.Text = i.ToString() + " cartes graphiques trouvées";
                    }

                    if (i == 1)
                    {
                        TextBlockTotal.Text = i.ToString() + " carte graphique trouvée";
                    }

                    if (i == 0)
                    {
                        TextBlockTotal.Text = "Aucune carte graphique n'a été trouvée";
                    }

                    break;
                }
                case "Processeur":
                {
                    SqlDataAdapter sda =
                        new SqlDataAdapter(
                            "SELECT CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE (Categorie ='CPU') AND NomProduit LIKE '%" +
                            TextBoxRecherche.Text + "%'", ConString);
                    DataTable dt = new DataTable("Produits");
                    sda.Fill(dt);
                    DataGridStock.ItemsSource = dt.DefaultView;
                    var i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de carte graphique
                    if (i > 1)
                    {
                        TextBlockTotal.Text = i.ToString() + " processeurs trouvés";
                    }

                    if (i == 1)
                    {
                        TextBlockTotal.Text = i.ToString() + " processeur trouvé";
                    }

                    if (i == 0)
                    {
                        TextBlockTotal.Text = "Aucun processeur n'a été trouvé";
                    }

                    break;
                }
                case "Carte mère":
                {
                    SqlDataAdapter sda =
                        new SqlDataAdapter(
                            "SELECT CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE (Categorie='MoBo') AND NomProduit LIKE '%" +
                            TextBoxRecherche.Text + "%'", ConString);
                    DataTable dt = new DataTable("Produits");
                    sda.Fill(dt);
                    DataGridStock.ItemsSource = dt.DefaultView;
                    var i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de carte graphique
                    if (i > 1)
                    {
                        TextBlockTotal.Text = i.ToString() + " cartes mères trouvées";
                    }

                    if (i == 1)
                    {
                        TextBlockTotal.Text = i.ToString() + " carte mère trouvé";
                    }

                    if (i == 0)
                    {
                        TextBlockTotal.Text = "Aucune carte mère n'a été trouvée";
                    }

                    break;
                }
                case "Barrette mémoire":
                {
                    SqlDataAdapter sda =
                        new SqlDataAdapter(
                            "SELECT CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE (Categorie='RAM') AND NomProduit LIKE '%" +
                            TextBoxRecherche.Text + "%'", ConString);
                    DataTable dt = new DataTable("Produits");
                    sda.Fill(dt);
                    DataGridStock.ItemsSource = dt.DefaultView;
                    var i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de carte graphique
                    if (i > 1)
                    {
                        TextBlockTotal.Text = i.ToString() + " barrettes mémoires trouvées";
                    }

                    if (i == 1)
                    {
                        TextBlockTotal.Text = i.ToString() + " barrette mémoire trouvée";
                    }

                    if (i == 0)
                    {
                        TextBlockTotal.Text = "Aucune barrette mémoire n'a été trouvée";
                    }

                    break;
                }
                case "Alimentation":
                {
                    SqlDataAdapter sda =
                        new SqlDataAdapter(
                            "SELECT CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE (Categorie='ALIM') AND NomProduit LIKE '%" +
                            TextBoxRecherche.Text + "%'", ConString);
                    DataTable dt = new DataTable("Produits");
                    sda.Fill(dt);
                    DataGridStock.ItemsSource = dt.DefaultView;
                    var i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de carte graphique
                    if (i > 1)
                    {
                        TextBlockTotal.Text = i.ToString() + " alimentations trouvées";
                    }

                    if (i == 1)
                    {
                        TextBlockTotal.Text = i.ToString() + " alimentation trouvée";
                    }

                    if (i == 0)
                    {
                        TextBlockTotal.Text = "Aucune alimentation n'a été trouvée";
                    }

                    break;
                }
                case "Accessoire":
                {
                    SqlDataAdapter sda =
                        new SqlDataAdapter(
                            "SELECT Id, CodeProduit, NomProduit, Categorie, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits WHERE (Categorie='ACC') AND NomProduit LIKE '%" +
                            TextBoxRecherche.Text + "%'", ConString);
                    DataTable dt = new DataTable("Produits");
                    sda.Fill(dt);
                    DataGridStock.ItemsSource = dt.DefaultView;
                    var i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de carte graphique
                    if (i > 1)
                    {
                        TextBlockTotal.Text = i.ToString() + " accessoires trouvés";
                    }
                    if (i == 1)
                    {
                        TextBlockTotal.Text = i.ToString() + " accessoire trouvé";
                    }
                    if (i == 0)
                    {
                        TextBlockTotal.Text = "Aucun accessoire n'a été trouvée";
                    }
                    break;
                }
                default:
                {
                    break;
                }

            }
        }

    }
        #endregion
}

