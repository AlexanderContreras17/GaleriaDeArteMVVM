using GaleriaArtesMVVM.Models;
using GaleriaArtesMVVM.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GaleriaArtesMVVM.ViewModels
{
    internal class ArteViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Arte> ListaArte { get; set; } = new ObservableCollection<Arte>();
        public Arte Arte { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand CambiarVistaCommand { get; set; }
        public ICommand VerDetallesCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand ModificarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }

        AgregarArteView AgregarArte;
        ModificarArteView ModificarArte;
        DetallesArteView DetallesArte;

        public ArteViewModel()
        {
            Abrir();
            AgregarCommand = new Command(Agregar);
            CambiarVistaCommand = new Command<string>(CambiarVista);
            EliminarCommand = new Command<Arte>(Eliminar);
            VerDetallesCommand = new Command<Arte>(VerDetalles);
            ModificarCommand = new Command<Arte>(Modificar);
            GuardarCommand = new Command(Guardar);
        }

        int posicion;
        private void Modificar(Arte Arte)
        {
            posicion = ListaArte.IndexOf(Arte);
            this.Arte = new Arte
            {
                Titulo = Arte.Titulo,
                Tipo= Arte.Tipo,
                Descripcion = Arte.Descripcion,
                 Artista = Arte.Artista,
                 Fecha = Arte.Fecha
            };

            ModificarArte = new ModificarArteView()
            {
                BindingContext = this
            };

            Application.Current.MainPage.Navigation.PushAsync(ModificarArte);
        }

        private void Guardar()
        {
            ListaArte[posicion] = Arte;
            Cargar();
            Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        private void VerDetalles(Arte Arte)
        {
            DetallesArte = new DetallesArteView()
            {
                BindingContext = Arte
            };
            Application.Current.MainPage.Navigation.PushAsync(DetallesArte);
            PropertyChanged(this, new PropertyChangedEventArgs(null));
        }

        private void Eliminar(Arte Arte)
        {
            ListaArte.Remove(Arte);
            Cargar();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        private void Agregar()
        {
            ListaArte.Add(Arte);
            Cargar();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            CambiarVista("ver");
        }

        private void CambiarVista(string Vista)
        {
            if (Vista == "agregar")
            {
                Arte = new Arte();
                AgregarArte = new AgregarArteView() { BindingContext = this };
                Application.Current.MainPage.Navigation.PushAsync(AgregarArte);
                PropertyChanged(this, new PropertyChangedEventArgs(null));
            }
            if (Vista == "ver")
            {
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        void Cargar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "lista.json";
            File.WriteAllText(file, JsonConvert.SerializeObject(ListaArte));
        }
        void Abrir()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "lista.json";
            if (File.Exists(file))
            {
                ListaArte = JsonConvert.DeserializeObject<ObservableCollection<Arte>>(File.ReadAllText(file));
            }
        }
    }
}
