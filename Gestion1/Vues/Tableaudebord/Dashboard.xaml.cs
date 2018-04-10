using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Gestion1.Vues.Connexion;

namespace Gestion1.Vues.Tableaudebord
{
    /// <summary>
    /// Logique d'interaction pour Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        #region Gestion de la fenêtre
        private void GridWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();  // Gestion deplacement de la fenêtre
            }
        }

        private void ButtonCloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Fermeture de l'application
        }

        private void ButtonMinimizeWindow_OnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; // Minimise la fenêtre
        }

        private void ButtonPopUpLogout_OnClick(object sender, RoutedEventArgs e)
        {
            Login login = new Login(); // Deconnexion de l'utilisateur
            login.Show(); // Retour sur l'interface de connexion
            Close();
        }

        private void ButtonPopUpAbout_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Application développée dans le cadre de l'épreuve E4, par Gabin LONGONI"); // Affichage message à propos
        }
        #endregion


        #region Gestion du menu latéral droit
        private void ButtonOpenMenu_OnClick(object sender, RoutedEventArgs e) // Ouverture du menu latéral gauche
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_OnClick(object sender, RoutedEventArgs e) // Fermeture du menu latéral gauche
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region Contenu menu latéral droit
        private void MenuHome_OnClick(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new Menu.Accueil.Accueil(); // La page Accueil devient le contenu de la frame MainFrame
        }

        private void MenuStock_OnClick(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new Menu.Stock.Stock(); // La page Stock devient le contenu de la frame MainFrame
        }

        private void MenuVente_OnClick(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new Menu.Vente.Vente(); // La page Vente devient le contenu de la frame MainFrame
        }

        private void MenuClient_OnClick(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new Menu.Client.Client(); // La page Client devient le contenu de la frame MainFrame
        }

        private void MenuStatistiques_OnClick(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new Menu.Statistiques.Statistiques(); // La page Statistiques devient le contenu de la frame MainFrame
        }
        #endregion
    }
}
