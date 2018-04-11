using System;
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

        private const string
            ConString =
                "Server=den1.mssql6.gear.host;Database=stock4;Uid=stock4;Pwd=gestionstock*"; // Chaine de connexion à la base SQL Server

        #endregion

        #region Gestion formulaire

        private void DataGridClient_OnSelectionChanged(object sender, SelectionChangedEventArgs e) // Affichage des données de la table Clients dans les Textbox
        {
            DataGrid gd = (DataGrid)sender;
            if (gd.SelectedItem is DataRowView rowSelected)
            {
                TextBoxId.Text = rowSelected["Id"].ToString();
                TextBoxNom.Text = rowSelected["Nom"].ToString();
                TextBoxPrenom.Text = rowSelected["Prenom"].ToString();
                TextBoxSociete.Text = rowSelected["Societe"].ToString();
                TextBoxTelephone.Text = rowSelected["Telephone"].ToString();
                TextBoxEmail.Text = rowSelected["Email"].ToString();
            }
        }

        #endregion

        #region Boutons

        private void ButtonAjouter_OnClick(object sender, RoutedEventArgs e)
        {
            if (TextBoxNom.Text != "" & TextBoxPrenom.Text != "" & TextBoxSociete.Text != "" &
                TextBoxTelephone.Text != "" &
                TextBoxEmail.Text != "") // Si les champs ne sont pas vides, la création est impossible
            {
                SqlConnection connection = new SqlConnection(ConString);
                connection.Open(); // Ouvertue de la connexion

                SqlCommand cmdSqlCommand =
                    new SqlCommand(
                        "INSERT INTO dbo.Clients(Nom,Prenom,Societe,Telephone,Email) Values(@Nom, @Prenom, @Societe, @Telephone, @Email)",
                        connection); // Requête d'insertion d'un nouveau client

                cmdSqlCommand.Parameters.AddWithValue("@Nom", TextBoxNom.Text); // Paramètre du Nom du client
                cmdSqlCommand.Parameters.AddWithValue("@Prenom", TextBoxPrenom.Text); // Paramètre du Prenom du client
                cmdSqlCommand.Parameters.AddWithValue("@Societe",
                    TextBoxSociete.Text); // Paramètre de la Societe du client
                cmdSqlCommand.Parameters.AddWithValue("@Telephone",
                    TextBoxTelephone.Text); // Paramètre du numéro de Telephone du client
                cmdSqlCommand.Parameters.AddWithValue("@Email", TextBoxEmail.Text); // Paramètre de l'Email du client
                cmdSqlCommand.ExecuteNonQuery(); // Execution de la requête
                MessageBox.Show("Le client " + TextBoxNom.Text + " " + TextBoxPrenom.Text + " " + "de la société " + TextBoxSociete.Text + " a été créé"); // Affichage du message après execution de la requête
                FillDataGrid(); // Recharge la table Clients
                TextBoxNom.Clear(); // Vide le champs Nom
                TextBoxPrenom.Clear(); // Vide le champs Prenom
                TextBoxSociete.Clear(); // Vide le champs Societe
                TextBoxTelephone.Clear(); // Vide le champs Telephone
                TextBoxEmail.Clear(); // Vide le champs Email
                connection.Close(); // Fermeture de la connexion
            }
        }

        private void ButtonEffacer_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonModifier_OnClick(object sender, RoutedEventArgs e)
        {
            if (TextBoxId.Text != "" & TextBoxNom.Text != "" & TextBoxPrenom.Text != "" & TextBoxSociete.Text != "" &
                TextBoxTelephone.Text != "" & TextBoxEmail.Text != "") // Si les champs ne sont pas vides, la modification est impossible
            {
                SqlConnection connection = new SqlConnection(ConString);
                connection.Open(); // Ouvertue de la connexion

                SqlCommand cmdSqlCommand =
                    new SqlCommand(
                        "UPDATE dbo.Clients SET Nom=@Nom, Prenom=@Prenom ,Societe=@Societe, Telephone=@Telephone, Email=@Email WHERE Id=@Id"
                        , connection); // Requête d'insertion d'un nouveau client
                cmdSqlCommand.Parameters.AddWithValue("@Id", TextBoxId.Text); // Paramètre de l'ID du client
                cmdSqlCommand.Parameters.AddWithValue("@Nom", TextBoxNom.Text); // Paramètre du Nom du client
                cmdSqlCommand.Parameters.AddWithValue("@Prenom", TextBoxPrenom.Text); // Paramètre du Prenom du client
                cmdSqlCommand.Parameters.AddWithValue("@Societe", TextBoxSociete.Text); // Paramètre de la Societe du client
                cmdSqlCommand.Parameters.AddWithValue("@Telephone", TextBoxTelephone.Text); // Paramètre du numéro de Telephone du client
                cmdSqlCommand.Parameters.AddWithValue("@Email", TextBoxEmail.Text); // Paramètre de l'Email du client
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
                FillDataGrid(); // Recharge la table Clients
                TextBoxNom.Clear(); // Vide le champs Nom
                TextBoxPrenom.Clear(); // Vide le champs Prenom
                TextBoxSociete.Clear(); // Vide le champs Societe
                TextBoxTelephone.Clear(); // Vide le champs Telephone
                TextBoxEmail.Clear(); // Vide le champs Email
                connection.Close(); // Fermeture de la connexion
            }
        }

        private void ButtonRaz_OnClick(object sender, RoutedEventArgs e)
        {
            TextBoxId.Clear(); // Vide le champs ID
            TextBoxNom.Clear(); // Vide le champs Nom
            TextBoxPrenom.Clear(); // Vide le champs Prenom
            TextBoxSociete.Clear(); // Vide le champs Societe
            TextBoxTelephone.Clear(); // Vide le champs Telephone
            TextBoxEmail.Clear(); // Vide le champs Email
        }

        #endregion

        #region Remplissage tableau Client

        private void FillDataGrid()
        {
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString =
                    "SELECT Id, Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients"; // Requête de récupération des éléments de la table Clients
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Clients");
                sda.Fill(dt); // Remplissage du SQL Data Adapter par la table Clients
                DataGridClient.ItemsSource = dt.DefaultView; // Choix du type de vue sur l'interface graphique
            }
        }

        #endregion

        #region Gestion tableau Client

        private void TextBoxRecherche_OnTextChanged(object sender, TextChangedEventArgs e) // Barre de recherche
        {
            if (ComboBoxCategorie.Text == "Nom")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT Id, Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients WHERE Nom LIKE '" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Clients");
                sda.Fill(dt);
                DataGridClient.ItemsSource = dt.DefaultView;
            }

            else if (ComboBoxCategorie.Text == "Prenom")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT Id, Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients WHERE Prenom LIKE '" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Clients");
                sda.Fill(dt);
                DataGridClient.ItemsSource = dt.DefaultView;
            }

            else if (ComboBoxCategorie.Text == "Societe")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT Id, Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients WHERE Societe LIKE '" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Clients");
                sda.Fill(dt);
                DataGridClient.ItemsSource = dt.DefaultView;
            }

            else if (ComboBoxCategorie.Text == "Telephone")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT Id, Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients WHERE Telephone LIKE '" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Clients");
                sda.Fill(dt);
                DataGridClient.ItemsSource = dt.DefaultView;
            }

            else if (ComboBoxCategorie.Text == "Email")
            {
                SqlDataAdapter sda =
                    new SqlDataAdapter(
                        "SELECT Id, Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients WHERE Email LIKE '" +
                        TextBoxRecherche.Text + "%'", ConString);
                DataTable dt = new DataTable("Clients");
                sda.Fill(dt);
                DataGridClient.ItemsSource = dt.DefaultView;
            }
        }

        #endregion

    }
}
