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

        private const string ConString = "Server=den1.mssql6.gear.host;Database=stock4;Uid=stock4;Pwd=gestionstock*";

        #region Bouton Gestion du tableau Client

        private void ButtonAjouter_OnClick(object sender, RoutedEventArgs e)
        {
            if (TextBoxNom.Text != "" & TextBoxPrenom.Text != "" & TextBoxSociete.Text != "" &
                TextBoxTelephone.Text != "" & TextBoxEmail.Text != "")
            {
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = "INSERT INTO dbo.Clients(Nom,Prenom,Societe,Telephone,Email) Values(@Nom, @Prenom, @Societe, @Telephone, @Email)";
                        command.Parameters.AddWithValue("@Nom", TextBoxNom.Text);
                        command.Parameters.AddWithValue("@Prenom", TextBoxPrenom.Text);
                        command.Parameters.AddWithValue("@Societe", TextBoxSociete.Text);
                        command.Parameters.AddWithValue("@Telephone", TextBoxTelephone.Text);
                        command.Parameters.AddWithValue("@Email", TextBoxEmail.Text);
                    }

                    try
                    {
                        connection.Open();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.ToString());
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                        MessageBox.Show("Valeurs ajoutées avec succès");
                        TextBoxNom.Clear();
                        TextBoxPrenom.Clear();
                        TextBoxSociete.Clear();
                        TextBoxTelephone.Clear();
                        TextBoxEmail.Clear();
                    }
                }


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
                CmdString = "SELECT Nom, Prenom, Societe, Telephone, Email FROM dbo.Clients";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Clients");
                sda.Fill(dt);
                DataGridClient.ItemsSource = dt.DefaultView;
            }
        }
        #endregion
    }
}
