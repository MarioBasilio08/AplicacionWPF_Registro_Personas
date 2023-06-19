using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroPersonas
{
    public class ListPerson
    {
        public ObservableCollection<Person> personas { get; set; }
        public event EventHandler<Person> PersonAdded;
        public event EventHandler<Person> PersonEdit;
        //public List<Person> personas;

        public ListPerson()
        {
            personas = new ObservableCollection<Person>();
        }

        public bool AgregarPersona(Person persona)
        {
            Person persona2 = personas.FirstOrDefault(p => p.id == persona.id);

            if (persona2 == null)
            {
                personas.Add(persona);
                PersonAdded?.Invoke(this, persona);
                return true;
            }
            return false;
        }

        public bool EliminarPersona(int id)
        {
            Person persona = personas.FirstOrDefault(p => p.id == id);
            if (persona != null)
            {
                personas.Remove(persona);
                return true;
            }
            return false;
        }

        public int SearchPerson(int personId)
        {
            int index = -1;
            for (int i = 0; i < personas.Count; i++)
            {
                if (personas[i].id == personId)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public bool EditPerson(Person persona)
        {
            int index = SearchPerson(persona.id);

            if (index != -1)
            {
                personas.RemoveAt(index);
                personas.Insert(index, persona);
                PersonEdit?.Invoke(this, persona);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
