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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegistroPersonas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ListPerson lista;
        List<Person> listaPerson;
        bool edit = false;
        public MainWindow()
        {
            InitializeComponent();
            lista = (ListPerson)DataContext;
            listaPerson = new List<Person>();

            Person person1 = new Person(1, "Mario Andres", 23, "andres@gmail.com");
            Person person2 = new Person(2, "Guillermo Eduardo", 23, "andres@gmail.com");
            Person person3 = new Person(3, "Samuel Erasto", 23, "andres@gmail.com");
            Person person4 = new Person(4, "Jose Ricardo", 23, "andres@gmail.com");
            Person person5 = new Person(5, "Jose Alejandro", 23, "andres@gmail.com");

            lista.personas.Add(person1);
            lista.personas.Add(person2);
            lista.personas.Add(person3);
            lista.personas.Add(person4);
            lista.personas.Add(person5);

            lista.PersonAdded += PersonAddedChange;
            lista.PersonEdit += PersonEditChange;
            dgPersons.DataContext = lista;
        }

        private void Button_Guardar(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextName.Text) || string.IsNullOrEmpty(TextId.Text) ||
                string.IsNullOrEmpty(TextEmail.Text) || string.IsNullOrEmpty(TextAge.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                Person persona = new Person(int.Parse(TextId.Text), TextName.Text, int.Parse(TextAge.Text), TextEmail.Text);
                if (edit)
                {
                    MessageBoxResult result = MessageBox.Show("¿Seguro que desea editar la información?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                         lista.EditPerson(persona);
                         limpiarText();
                         edit = false;
                         BEliminar.IsEnabled = true;
                    }
                       
                }
                else
                {
                    if (lista.AgregarPersona(persona))
                    {
                        limpiarText();
                    }
                    else
                    {
                        MessageBox.Show($"El Id: {persona.id} ya se encuentra registrado","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                    }
                }

            }
        }

        private void Button_Nuevo(object sender, RoutedEventArgs e)
        {
            limpiarText();
            BEliminar.IsEnabled = true;
            dgPersons.SelectedItem = null;
            TextId.IsEnabled = true;
            edit = false;
        }

        private void Button_Eliminar(object sender, RoutedEventArgs e)
        {
            if (dgPersons.SelectedItem != null)
            {
                Person selectedPerson = (Person)dgPersons.SelectedItem;

                MessageBoxResult result = MessageBox.Show("¿Seguro que desea eliminar la información?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    lista.EliminarPersona(selectedPerson.id);
                }
                else
                {
                    dgPersons.SelectedItem = null;
                }
            }
            else
            {
                MessageBox.Show($"No hay ningun elemento seleccionado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

       
        public void PersonAddedChange(object s, Person e)
        {
           MessageBox.Show($"Se ha agredado una nueva persona {e.id}-{e.name}", "Información", MessageBoxButton.OK, MessageBoxImage.Information);            
        }

        public void PersonEditChange(object s, Person e)
        {
            MessageBox.Show($"Se editado la persona {e.id}-{e.name}", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void limpiarText()
        {
            TextName.Text = String.Empty;
            TextId.Text = String.Empty;
            TextAge.Text = String.Empty;
            TextEmail.Text = String.Empty;
        }

        public void PaintText(Person p)
        {
            TextName.Text = p.name;
            TextEmail.Text = p.email;
            TextAge.Text = p.age.ToString();
            TextId.Text = p.id.ToString();
        }

        private void dgPersons_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgPersons.SelectedItem != null)
            {
                Person selectedPerson = (Person)dgPersons.SelectedItem;
                PaintText(selectedPerson);
                BEliminar.IsEnabled = false;
                TextId.IsEnabled = false;
                edit = true;
            }
        }
    }
}
