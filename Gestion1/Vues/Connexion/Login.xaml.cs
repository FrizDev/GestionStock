using System;
using System.Collections.Generic;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Gestion1.Vues.Tableaudebord;

namespace Gestion1.Vues.Connexion
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        #region Gestion des évenements de la fenêtre
        private void ButtonCloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Ferme l'application
        }
        #endregion

        #region Bouton Connexion
        private void ButtonConnexion_OnClick(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconnection = new SqlConnection("Server=den1.mssql6.gear.host;Database=stock4;Uid=stock4;Pwd=gestionstock*"); // Chaîne de connexion à la base SQL Server
            try
            {
                if (sqlconnection.State == System.Data.ConnectionState.Closed) // Si l'état de la connexion est fermé
                {
                    sqlconnection.Open(); // Ouverture la connexion à la base SQL Server
                }
                string query = "SELECT COUNT (1) FROM Utilisateurs WHERE Identifiant=@identifiant AND Motdepasse=@motdepasse"; // Reqûete pour vérifier les informations de l'utilisateur
                SqlCommand sqlcommand = new SqlCommand(query, sqlconnection);
                sqlcommand.CommandType = System.Data.CommandType.Text;
                sqlcommand.Parameters.AddWithValue("@identifiant", textbox_identifiant.Text);
                sqlcommand.Parameters.AddWithValue("@motdepasse", passwordbox_identifiant.Password);
                int count = Convert.ToInt32(sqlcommand.ExecuteScalar()); // Variable count qui stocke le nombre de résultat suite à la requête
                if (count == 1) // Si le résultat = 1
                {
                    Dashboard dashboard = new Dashboard(); // Affichage du tableau de bord
                    dashboard.Show();
                    Close(); // Fermeture du formulaire de connexion
                }
                else // Sinon
                {
                    DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(2));
                    ErreurMotdepasse.BeginAnimation(TextBlock.OpacityProperty, animation);
                    ErreurMotdepasse.Text = "Identifiant ou mot de passe incorrect"; // Animation avec affichage du message "Identifiant ou mot de passe incorrect"
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error); // Affichage du message en cas d'erreur lors du processus
            }
            finally
            {
                sqlconnection.Close(); // Fermeture de la connexion vers la base SQL Server
            }
        }
        #endregion

    }
}
