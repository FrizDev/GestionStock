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
            FillListBox();
            DatePickerFacture.DisplayDate = DateTime.Today;
        }

        #region Chaine de connexion à la base de données SQL Server

        private const string
            ConString =
                "Server=den1.mssql6.gear.host;Database=stock4;Uid=stock4;Pwd=gestionstock*"; // Chaine de connexion à la base SQL Server

        #endregion

        private void FillListBox()
        {
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString =
                    "SELECT Id, Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients"; // Requête de récupération des éléments de la table Produits
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                sda.Fill(dataSet, "Client"); // Remplissage du SQL Data Adapter par la table Client
                DataTable dataTable = dataSet.Tables[0];
                DataRow tempRow = null;

                foreach (DataRow tempRow_Variable in dataSet.Tables[0].Rows)
                {
                    tempRow = tempRow_Variable;
                    string Nom = tempRow["Nom"].ToString();
                    string Prenom = tempRow["Prenom"].ToString();
                    string Societe = tempRow["Societe"].ToString();
                    ListBoxClient.Items.Add("[" + Societe + "]" + " " + Nom.ToUpper() + " " + Prenom);

                }
            }
        }

        private void TextBoxRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListBoxClient.Items.Clear();
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT Id, Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients WHERE Societe LIKE '" +
                        TextBoxRecherche.Text + "%'";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                sda.Fill(dataSet, "Client"); // Remplissage du SQL Data Adapter par la table Client
                DataTable dataTable = dataSet.Tables[0];
                DataRow tempRow = null;

                foreach (DataRow tempRow_Variable in dataSet.Tables[0].Rows)
                {
                    tempRow = tempRow_Variable;
                    string Nom = tempRow["Nom"].ToString();
                    string Prenom = tempRow["Prenom"].ToString();
                    string Societe = tempRow["Societe"].ToString();
                    ListBoxClient.Items.Add("[" + Societe + "]" + " " + Nom.ToUpper() + " " + Prenom);

                }
            }
        }

        private void ListBoxClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (lb.SelectedItem is DataRow rowSelected)
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
