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
        }

        #region Chaine de connexion à la base de données SQL Server

        private const string
            ConString =
                "Server=den1.mssql6.gear.host;Database=stock4;Uid=stock4;Pwd=gestionstock*"; // Chaine de connexion à la base SQL Server

        #endregion

        private void DataGridStock_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FillDataGrid()
        {
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString =
                    "SELECT Id, CodeProduit, NomProduit, Quantite, PrixHt, Etat, DateAjout FROM dbo.Produits"; // Requête de récupération des éléments de la table Produits
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Produits");
                sda.Fill(dt); // Remplissage du SQL Data Adapter par la table Produits
                DataGridStock.ItemsSource = dt.DefaultView; // Choix du type de vue sur l'interface graphique
                int i = Convert.ToInt32(dt.Rows.Count); // Compteur du nombre de produits
                //if (i > 1)
                //{
                //    TextBlockTotal.Text = "" + i.ToString() + " clients trouvés";
                //}
                //else
                //{
                //    TextBlockTotal.Text = "" + i.ToString() + " client trouvé";
                //}


                // TODO Compteur de produit en stock
            }
        }
    }
}
