using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

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

        #region Remplissage DataGrid

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
                    "SELECT NumeroFacture, Description, DateFacture, NomProduit, CodeProduit, Quantite, PrixUnitaire, Nom, Prenom, Societe, Telephone, Email FROM dbo.Factures WHERE NumeroFacture=@NumeroFacture"; // Requête de récupération des éléments de la table Produits
                SqlCommand cmd = new SqlCommand(CmdString, con);
                cmd.Parameters.AddWithValue("@NumeroFacture", TextBoxNumeroFacture.Text); // Paramètre du Nom du client
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Factures");
                sda.Fill(dt); // Remplissage du SQL Data Adapter par la table Produits
                DataGridFacture.ItemsSource = dt.DefaultView; // Choix du type de vue sur l'interface graphique*
            }
        }

        #endregion

        #region Formulaire

        private void FillComboBoxNomProduit()
        {
            ComboBoxContenuNomProduit.Items.Clear();
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString =
                    "SELECT NomProduit, CodeProduit, PrixHt, Quantite  FROM dbo.Produits"; // Requête de récupération des éléments de la table Produits
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt); // Remplissage du SQL Data Adapter par la table Produits
                foreach (DataRow dr in dt.Rows)
                {
                    ComboBoxContenuNomProduit.Items.Add(dr["NomProduit"].ToString());
                    TextBoxContenuCodeProduit.Text = dr["CodeProduit"].ToString();
                    TextBoxContenuPrixUnitaire.Text = dr["PrixHt"].ToString();
                    TextBoxContenuQuantite.Text = dr["Quantite"].ToString();
                }

                //CmdString2 = "SELECT NomProduit FROM dbo.Produits WHERE NomProduit=@NomProduit";
                //SqlCommand cmSqlCommand = new SqlCommand(CmdString2, con);
                //cmSqlCommand.Parameters.AddWithValue("@NomProduit", ComboBoxContenuNomProduit.Text);
                //SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                //DataTable dataTable = new DataTable("Produits");
                //sqlData.Fill(dataTable);
                //foreach (DataRow dataRow in dataTable.Rows)
                //{
                //    TextBoxContenuCodeProduit.Text = dataRow["CodeProduit"].ToString();
                //}
            }
        }

        //private void FillTextBoxCodeProduit()
        //{
        //    SqlConnection connection = new SqlConnection(ConString);
        //    connection.Open(); // Ouvertue de la connexion

        //    SqlCommand cmdSqlCommand =
        //        new SqlCommand(
        //            "SELECT CodeProduit FROM dbo.Produits WHERE NomProduit=@NomProduit", connection); // Requête d'insertion d'un nouveau client
        //    cmdSqlCommand.Parameters.AddWithValue("@NomProduit", ComboBoxContenuNomProduit.Text); // Paramètre du Nom du client
        //    SqlDataReader dr = cmdSqlCommand.ExecuteReader();
        //    if (dr.Read())
        //    {
        //        TextBoxContenuCodeProduit.Text = (dr["CodeProduit"].ToString());
        //    }

        //}

        private void DataGridClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid) sender;
            if (gd.SelectedItem is DataRowView rowSelected)
            {
                TextBoxClientNom.Text = rowSelected["Nom"].ToString();
                TextBoxClientPrenom.Text = rowSelected["Prenom"].ToString();
                TextBoxClientSociété.Text = rowSelected["Societe"].ToString();
                TextBoxClientTelephone.Text = rowSelected["Telephone"].ToString();
                TextBoxClientEmail.Text = rowSelected["Email"].ToString();
            }
        }


        #endregion

        #region Bouton

        private void ButtonAjouterFacture_Click(object sender, RoutedEventArgs e)
            {
                //if (DatePickerFacture.Text != "" & TextBoxNumeroFacture.Text != "" & TextBoxDescription.Text != "" &
                //    TextBoxClientNom.Text != "" & TextBoxClientPrenom.Text != "" & TextBoxClientSociété.Text != "" &
                //    TextBoxClientTelephone.Text != "" & TextBoxClientEmail.Text != "" & TextBoxContenuCodeProduit.Text != "" &
                //    ComboBoxContenuNomProduit.Text != "" & TextBoxContenuQuantite.Text != "" & TextBoxContenuPrixUnitaire.Text != "") // Si les champs ne sont pas vides, la création est impossible
                {
                    SqlConnection connection = new SqlConnection(ConString);
                    connection.Open(); // Ouvertue de la connexion

                    SqlCommand cmdSqlCommand =
                        new SqlCommand(
                            "INSERT INTO dbo.Factures(NumeroFacture, Description, DateFacture, NomProduit, CodeProduit, Quantite, /*PrixUnitaire,*/ Nom, Prenom, Societe, Telephone, Email)" +
                            " Values(@NumeroFacture, @Description, @DateFacture, @NomProduit, @CodeProduit, @Quantite, /*@PrixUnitaire,*/ @Nom, @Prenom, @Societe, @Telephone, @Email)",
                            connection); // Requête d'insertion d'un nouveau client

                    cmdSqlCommand.Parameters.AddWithValue("@NumeroFacture",
                        TextBoxNumeroFacture.Text); // Paramètre du Nom du client
                    cmdSqlCommand.Parameters.AddWithValue("@Description",
                        TextBoxDescription.Text); // Paramètre du Prenom du client
                    cmdSqlCommand.Parameters.AddWithValue("@DateFacture",
                        Convert.ToDateTime(DatePickerFacture.Text)
                            .ToString("yyyy-MM-dd")); // Paramètre du Prenom du client
                    cmdSqlCommand.Parameters.AddWithValue("@NomProduit",
                        ComboBoxContenuNomProduit.Text); // Paramètre de la Societe du client
                    cmdSqlCommand.Parameters.AddWithValue("@CodeProduit",
                        TextBoxContenuCodeProduit.Text); // Paramètre de la Societe du client
                    cmdSqlCommand.Parameters.AddWithValue("@Quantite",
                        TextBoxContenuQuantite.Text); // Paramètre du numéro de Telephone du client
                    //cmdSqlCommand.Parameters.AddWithValue("@PrixUnitaire", TextBoxContenuPrixUnitaire.Text); // Paramètre de l'Email du client
                    cmdSqlCommand.Parameters.AddWithValue("@Nom",
                        TextBoxClientPrenom.Text); // Paramètre de l'Email du client
                    cmdSqlCommand.Parameters.AddWithValue("@Prenom",
                        TextBoxClientPrenom.Text); // Paramètre de l'Email du client
                    cmdSqlCommand.Parameters.AddWithValue("@Societe",
                        TextBoxClientSociété.Text); // Paramètre de l'Email du client
                    cmdSqlCommand.Parameters.AddWithValue("@Telephone",
                        TextBoxClientTelephone.Text); // Paramètre de l'Email du client
                    cmdSqlCommand.Parameters.AddWithValue("@Email",
                        TextBoxClientEmail.Text); // Paramètre de l'Email du client
                    cmdSqlCommand.ExecuteNonQuery(); // Execution de la requête
                    MessageBox.Show("Le produit " + ComboBoxContenuNomProduit.Text + " a été ajouté à la facture " +
                                    TextBoxNumeroFacture); // Affichage du message après execution de la requête
                    FillVenteGrid(); // Recharge la table Clients
                    connection.Close(); // Fermeture de la connexion
                }
            }

            

            #endregion

        #region Barre de recherche

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
            }

        #endregion



        #endregion

        #region Remplissage automatique des champs [ NON FONCTIONNEL !]

        private void ComboBoxContenuNomProduit_OnDropDownOpened(object sender, EventArgs e) // NON FONCTIONNEL
        {
            FillComboBoxNomProduit();
        }

        #endregion

    }
}