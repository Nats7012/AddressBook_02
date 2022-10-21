using _03_AddressBookWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace _03_AddressBookWPF
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<ContactPerson> contacts;

        public MainWindow()
        {
            InitializeComponent();
            contacts = new ObservableCollection<ContactPerson>();
            lv_Contacts.ItemsSource = contacts;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            var contact = contacts.FirstOrDefault(x => x.Email == tb_Email.Text); // Kollar ifall man skriver samma epost mer än en gång, så säger den till.
            if (contact == null)
            {
                contacts.Add(new ContactPerson
                {
                    FirstName = tb_FirstName.Text,
                    LastName = tb_LastName.Text,
                    Email = tb_Email.Text,
                    StreetName = tb_StreetName.Text,
                    PostalCode = tb_PostalCode.Text,
                    City = tb_City.Text
                });
            }
            else // skriver samma epost så kommer denna ruta upp.
            {
                MessageBox.Show("Someone already has this email address. Please try aggain!");
            }

            ClearFields();
        }

        private void ClearFields()
        {
            tb_FirstName.Text = "";
            tb_LastName.Text = "";
            tb_Email.Text = "";
            tb_StreetName.Text = "";
            tb_PostalCode.Text = "";
            tb_City.Text = "";
        }

        private void lv_Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try // utan try/catch så varje gång jag tog bort en kontakt så kraschade projekten.
            {
                var contact = (ContactPerson)lv_Contacts.SelectedItems[0]!;
                tb_FirstName.Text = contact.FirstName;
                tb_LastName.Text = contact.LastName;
                tb_Email.Text = contact.Email;
                tb_StreetName.Text = contact.StreetName;
                tb_PostalCode.Text = contact.PostalCode;
                tb_City.Text = contact.City;
            }
            catch { }

        }
        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                var contact = (ContactPerson)button!.DataContext;

                contacts.Remove(contact);
            }
            catch { }
            
            ClearFields(); // tömma rutorna när man har tagit bort en kontakt.
        }
        
    }
}
